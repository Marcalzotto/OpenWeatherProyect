import { Component, OnInit } from '@angular/core';
import { CountryDTO } from '../../shared/DTOs/CountryDTO';
import { CountryService } from '../../shared/services/country.service';
import { CityService } from '../../shared/services/city.service';
import { OfficeService } from '../../shared/services/office.service';
import { CityDTO } from '../../shared/DTOs/CityDTO';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BranchOfficeDTO } from '../../shared/DTOs/BranchOfficeDTO';
import { Router, ActivatedRoute} from '@angular/router';
import { HttpParams } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';


@Component({
  selector: 'app-office',
  templateUrl: './office.component.html',
  styleUrls: ['./office.component.css']
})
export class OfficeComponent implements OnInit {

  public countries:CountryDTO[];
  public title:string="";
  public SelectedCountryId:number;
  public cities:CityDTO[];
  public SelectedCity;
  public office:BranchOfficeDTO;
  public updateMode:boolean;
  public readonlySelect:boolean;

  public noCitiesMessaje:boolean;
  
  //mensaje final de operacion y propiedades para mostrar iconos
  public finalMessajeOp:string;
  public icon:string;
  public color:string;

  public officeForm = new FormGroup({
    countryList: new FormControl('',[Validators.required]),
    cityList: new FormControl(['',Validators.required]),
    officeDesc: new FormControl('',[Validators.required,Validators.minLength(5),Validators.maxLength(50)])
  })

  constructor(private _countryService:CountryService, 
              private _cityService:CityService,
              private _officeService:OfficeService,
              private _router:Router,
              private _activdatedRoute:ActivatedRoute) 
  { 
      this.office = new BranchOfficeDTO(0,"",0,new CityDTO(0,"","",0,0,0,null,null));
  }

  ngOnInit(): void {
      this.updateMode = false;
      this.readonlySelect = false;
      this.noCitiesMessaje = false;
      this.SelectedCity = 0; 
      this.title = "New Office";
      this.getCountries();
      this.checkParams();
  }

  getCountries(){
    this._countryService.getAll()
      .subscribe(data => { this.countries = data; },
                 error => { console.log(error); this.countries = [];}
                 );
  }

  private checkParams()
  {
    this._activdatedRoute.paramMap.subscribe(params=> {
          if(params.has("id")){

            let id = params.get("id");

            this._officeService.getById(id).subscribe(office => {
                 if(office !== null){
                    this.title = "Edit Office";
                    this.updateMode = true;
                    this.readonlySelect = true; 
                    this.cities = [];
                    this.cities.push(office.city);
                    this.office = office;
                    this.officeForm.value.officeDesc = this.office.description;
                    this.SelectedCity = this.office.cityId;
                    this.SelectedCountryId = this.office.city.country.id;
                 }
            },
            error => {
                console.log(error);
            });
          }
    },error=>{
          console.log(error);
    });
    
  }

  getCitiesByCountryId(countryId:number){
  
    this._cityService.getCitiesWithOutOffice(countryId)
                     .subscribe(data=>{   
                                  if(data === null || data === undefined)
                                  {
                                    console.log("no cities");
                                    this.noCitiesMessaje = true; 
                                    if(this.cities.length > 0)
                                        this.cities = [];

                                  }else{
                                    this.cities = data;
                                    this.noCitiesMessaje = false; 
                                  }
                                },
                                error=>{console.log(error);})
  }

  createOffice(newOffice:BranchOfficeDTO)
  {
      this._officeService.createOffice(newOffice)
              .subscribe(data => {
                            console.log("new office" + data);
                            this.setMessajeSettings("The office was successfully created", 
                                        "check_circle_outline",
                                        "primary");  
                                        
                            this.getCitiesByCountryId(this.SelectedCountryId);
                            this.SelectedCity = undefined;
                            this.officeForm.value.officeDesc = "";
                            this.disableMessaje();

                        },error=>{
                          this.setMessajeSettings("The office could not be created", 
                          "highlight_off",
                          "warn");
                          console.log(error);
                        })
  }

  patchOffice(id:number, patch:any){
      this._officeService.patchOffice(id, patch)
            .subscribe(response=>{
                console.log("Office patched:" + response);
                this.setMessajeSettings("The office was successfully updated", 
                                        "check_circle_outline",
                                        "primary");
                                        this.disableMessaje();
            },
            error=>{
              this.setMessajeSettings("The office could not be updated", 
                                        "highlight_off",
                                        "warn");

                console.log("Office could not be patched: "+error);
            })
  }

  private setMessajeSettings(messaje:string, icon:string, color:string)
  {
      this.finalMessajeOp = messaje;
      this.icon = icon;
      this.color = color;
  }

  clearMessaje(){
    this.setMessajeSettings("","","");
    this.officeForm.value.officeDesc = "";
    this.cities = []; 
  }

  onSubmit()
  {
    if(!this.updateMode){

      let cityId:number = this.officeForm.value.cityList; 

      let City:CityDTO;

      City = this.cities.find(x => x.id == cityId);
      console.log(City);
    
      let officeDesc = this.officeForm.value.officeDesc; 
      console.log(officeDesc);

      let newOffice = new BranchOfficeDTO(0,officeDesc,cityId,null);
      
      this.createOffice(newOffice);

    }else{
        
      this.officeForm.value.officeDesc = this.office.description;
        
        //patch array para realizar la actualizacion parcial de la informacion de la oficina
        const patch = [{
          "value": this.office.description,
          "path": "/description",
          "op": "replace",
        }];
        
        this.patchOffice(this.office.id, patch);
    }
        
  }

  disableMessaje(){
    setInterval(()=>{
      this.setMessajeSettings("","","");
    },5000)
  }

  enableSubmitButton()
  {
    if(!this.updateMode)
    {
        if(this.officeForm.value.countryList === null || this.officeForm.value.countryList === undefined)
          return false;

        if(this.officeForm.value.cityList === null || this.officeForm.value.cityList === undefined 
                    || this.SelectedCity === null || this.SelectedCity === undefined)
        return false;

        if(this.officeForm.value.officeDesc === null)
          return false;
        else
          if(this.officeForm.value.officeDesc === "" || this.officeForm.value.officeDesc.length < 5 || this.officeForm.value.officeDesc.length > 50)
            return false;
        
    }
      return true;
  }


  selectedChange(countryId)
  {
      this.getCitiesByCountryId(countryId);
      this.setMessajeSettings("","","");
  }
}
