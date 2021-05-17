import { Component, OnInit, ViewChild, ChangeDetectorRef} from '@angular/core';
import { Observable } from 'rxjs';
import { BranchOfficeDTO } from '../shared/DTOs/BranchOfficeDTO';
import { CountryDTO } from '../shared/DTOs/CountryDTO';
import { WeatherConditionDTO } from '../shared/DTOs/WeatherConditionDTO';
import { CountryService } from '../shared/services/country.service';
import { OfficeService} from '../shared/services/office.service';
import { WeatherConditionService } from '../shared/services/weathercondition.service';  
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';
import { FormGroup, FormControl, Validators, ValidatorFn, AbstractControl } from '@angular/forms';
import { AuthService } from '../shared/services/auth.service';

interface Units{
  value:number;
  viewValue:string;
}

@Component({
  selector: 'app-weather-report',
  templateUrl: './weather-report.component.html',
  styleUrls: ['./weather-report.component.css']
})
export class WeatherReportComponent implements OnInit {

  //mat table
  @ViewChild(MatPaginator) paginator: MatPaginator;
  obs:Observable<any>;
  dataSource:MatTableDataSource<WeatherConditionDTO> = new MatTableDataSource<WeatherConditionDTO>();
 
 public displayedColumns: string[] = [
  'Office', 'City','Longitude','Latitude','Temperature', 'Minimum Temprature',
  'Maximum Temprature','Feels Like','Humidity','Clouds','Wind Speed', 'Wind Degrees',
  'Wind Gust','Sea Level','Ground Level','Rain Volume 1h','Rain Volume 3h',
  'Sunrise','Sunset','Timezone','Snow Volume 1h','Snow Volume 3h','RegDate','Description','Icon']; 
 //mat table

  //Property for form title
  public formTitle:string;

  public countries:CountryDTO[];
  public offices:BranchOfficeDTO[];
  public weatherConditions:WeatherConditionDTO[] = [];

  //units list selected default value(kelvin)
  public selected = 1;

  //Propery for get the selected contry id from country-list component
  public SelectedCountryId;

  //Porperty used to display messaje if there is no office for a country
  public noOfficesMessajes:boolean;

  //text for weatherForm's submit button, change depending if historical check is checked or not 
  public buttonText:string;  

  //Porperty used to display a messaje if there is an error with dates.
  public datesErrorMessaje:boolean;

  //Array with city ids passed to rest service.
  public cityIdArr:number[]=[];

  //messaje displayed when no weather results found
  public NoResultsFound:boolean;

  //
  public maxDate = new Date();

  public weatherForm = new FormGroup({
    officesList: new FormControl('', [Validators.required]),
    unitsSelect: new FormControl(this.selected),
    historicalCheck: new FormControl(false),
    dateFrom : new FormControl(new Date(), [this.DateValidator]),
    dateTo: new FormControl(new Date(),[this.DateValidator])
  });

  units: Units[]=[
    {value:1, viewValue: "Kelvin"},
    {value:2, viewValue: "Celcius"},
    {value:3, viewValue: "Fahrenheit"}
  ];

  constructor(
    private _countryService:CountryService,
    private _officeService:OfficeService,
    private _weatherConditionService:WeatherConditionService,
    private _authService:AuthService,
    private changeDetectorRef: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
      this.changeDetectorRef.detectChanges();

      this.formTitle = "Weather Conditions Form";
      this.noOfficesMessajes = false;
      this.buttonText = "Get Current Weather";
      this.datesErrorMessaje = false;
      this.NoResultsFound = false;

      this.getCountries();

      let token = this._authService.getAuthorizationToken();
      console.log(this._authService.getTokenExpirationDate(token));

  }

  getCountryId(countryId:number){
    this.SelectedCountryId = countryId; 
    this.getOffices(countryId);
    
    if(this.dataSource.data.length !== 0)
    {
        this.dataSource.data = [];
        this.noOfficesMessajes = false;   
    }
  }

  getCountries(){
    this._countryService.getAll()
      .subscribe(data => { this.countries = data; },
                 error => { console.log(error); this.countries = [];
                });
  }

  getOffices(countryId:number){
      this._officeService.getByCountryId(countryId, false)
            .subscribe(data => { if(data === null || data === undefined || data.length === 0){ 
                                  console.log(data);
                                    this.offices = null;
                                    this.noOfficesMessajes = true;
                                }
                                 else{ this.offices = data; this.noOfficesMessajes = false;}
                       },
                       error=>{ console.log(error); this.offices = [];});
  }

  getUnits():Units[]{ return this.units; }

  getConditions(cities:number[]){
      this._weatherConditionService.getAll(cities)
                            .subscribe((data) => {
                                      if(data !== null || data !== undefined){
                                        this.weatherConditions = data as WeatherConditionDTO[];
                                        this.dataSource.data = this.weatherConditions;
                                        this.dataSource.paginator = this.paginator;   
                                        this.obs = this.dataSource.connect();  
                                        
                                        if(this.weatherConditions.length === 0)
                                        {
                                          this.NoResultsFound = true; 
                                        }else{
                                          this.NoResultsFound = false; 
                                        }

                                      }
                            },
                            error=> { console.log(error); });
  }

  getHistricalConditions(cities:number[], dateFrom:string, dateTo:string){
    this._weatherConditionService.getHistorical(cities, dateFrom, dateTo)
    .subscribe((data) => {
              if(data !== null || data !== undefined){
                this.weatherConditions = data as WeatherConditionDTO[];
                this.dataSource.data = this.weatherConditions;
                this.dataSource.paginator = this.paginator;   
                this.obs = this.dataSource.connect();  
                
                if(this.weatherConditions.length === 0)
                {
                  this.NoResultsFound = true; 
                }else{
                  this.NoResultsFound = false; 
                }

              }
    },
               error=> { console.log(error); });
  }

  onCheckedChange(e)
  {
    if(e.checked){ 
      this.buttonText = "Get Historical Weather";
    }else{
      this.buttonText = "Get Current Weather";
    }
  }

  enableSubmitButton():boolean
  {
    this.datesErrorMessaje = false;

    if(this.SelectedCountryId === undefined)
      return false;

    if(this.weatherForm.value.officesList === undefined || this.weatherForm.value.officesList.length == 0)
      return false;
    
    if(this.weatherForm.value.historicalCheck){

        if(this.weatherForm.value.dateFrom === null || this.weatherForm.value.dateFrom === undefined || this.weatherForm.value.dateFrom == "") 
          return false;
        else if(this.weatherForm.value.dateTo === null || this.weatherForm.value.dateTo === undefined || this.weatherForm.value.dateTo == "")
          return false;
        else{
            let date1 = new Date(this.weatherForm.value.dateFrom);
            let date2 = new Date(this.weatherForm.value.dateTo);
            let dateNow = new Date();

            if((date1.getTime() > dateNow.getTime()) || (date2.getTime() > dateNow.getTime()))
            {
                return false;
            }
        } 
    }
    return true;
  }

  onSubmit(){
   
    this.datesErrorMessaje = false;
    this.cityIdArr = this.weatherForm.value.officesList;
    
    if(this.weatherForm.value.historicalCheck){

      let date1 = new Date(this.weatherForm.value.dateFrom);
      let date2 = new Date(this.weatherForm.value.dateTo);
      
      if(date1.getTime() > date2.getTime())
      {
          this.datesErrorMessaje = true;
      }else{

        let dateFrom = this.formatDate(this.weatherForm.value.dateFrom);
        let dateTo = this.formatDate(this.weatherForm.value.dateTo);
  
        this.getHistricalConditions(this.cityIdArr, dateFrom, dateTo);
      }

    }else{
      this.getConditions(this.cityIdArr);
    }
      
  }

  formatDate(inputDate:Date):string
  {
      var dd = inputDate.getDate();
      var mm = inputDate.getMonth()+1;
      var yyyy = inputDate.getUTCFullYear();
      
      var formattedDate = yyyy+"-"+mm+"-"+dd;
      return formattedDate;
  }

  DateValidator(control: AbstractControl): { [key: string]: boolean } | null {

    var date = new Date(control.value);
    var dateNow = new Date();
    
    if (date.getTime() > dateNow.getTime()) {
        return { 'DatesValidation': true };
    }
    return null;
  }

  DatesToValidator(control: AbstractControl): { [key: string]: boolean } | null {
    var date = new Date(control.value);
    var dateNow = new Date();

    if (date.getTime() > dateNow.getTime()) {
        return { 'DatesValidation': true };
    }
    return null;
  }


  //pruebas mock
  // getCountries(){
  //   this._countryService.getAll().then(
  //     success =>{
  //         if(success !== null || success !== undefined){
  //           this.countries = success;
  //         }
  //     }
  //   );
  // }

  //
  // getOffices(){
  //   this._officeService.getAll().then(
  //     success =>{
  //         if(success !== null || success !== undefined){
  //           this.offices = success;
  //           this.officeMatrix = this.groupArray(this.offices, 6); 
  //           console.log(this.officeMatrix);
  //         }
  //     }
  //   );
  // }

  // getConditions(){
  //   this._weatherConditionService.getAll().then(
  //     success =>{
  //         if(success !== null || success !== undefined){
  //           this.weatherConditions = success;
  //           this.dataSource.data = this.weatherConditions;
  //           this.dataSource.paginator = this.paginator;   
  //           this.obs = this.dataSource.connect(); 
  //         }
  //     });
  // }
  //pruebas mock

  //   groupArray<T>(data: Array<T>, n: number): Array<T[]> {
//     let group = new Array<T[]>();
// ​
//     for (let i = 0, j = 0; i < data.length; i++) {
//         if (i >= n && i % n === 0)
//             j++;
//         group[j] = group[j] || [];
//         group[j].push(data[i])
//     }
// ​
//     return group;
//   }


}
