import { Component, OnInit, ViewChild } from '@angular/core';
import { BranchOfficeDTO } from '../shared/DTOs/BranchOfficeDTO';
import { CountryDTO } from '../shared/DTOs/CountryDTO';
import { CountryService } from '../shared/services/country.service';
import { OfficeService } from '../shared/services/office.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { FormGroup } from '@angular/forms';
import { ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
import {DeleteDialogComponent} from '../office-list/office-delete-dialog/delete-dialog.component';
import { MatDialog } from '@angular/material/dialog';

//interface for dialog
export interface DialogData
{
    office:string;
}

@Component({
  selector: 'app-office-list',
  templateUrl: './office-list.component.html',
  styleUrls: ['./office-list.component.css']
})
export class OfficeListComponent implements OnInit {
  

  public displayedColumns:string[] = ['Description','City','Actions'];
  public dataSource:MatTableDataSource<BranchOfficeDTO> = new MatTableDataSource<BranchOfficeDTO>(); 
  @ViewChild(MatPaginator) paginator: MatPaginator; //se necesita el decorador viewchild 
                                                    //para que el paginador pueda saber cuantos 
                                                    //elementos hay y poder paginar.
  deleteClick:boolean;
  officeDesc:string;
  public title:string;
  public countries:CountryDTO[];
  public SelectedCountryId:number;
  public noResultsFound:boolean;
  public offices:BranchOfficeDTO[];

  OfficeListForm = new FormGroup({

  });

  constructor(private _countryService:CountryService,
              private _officeService:OfficeService,
              private changeDetectorRef: ChangeDetectorRef,
              private _router:Router,
              public dialog:MatDialog) 
              { 
                this.title = "Office List";
                this.offices = [];
              }

  ngOnInit(): void {
    this.changeDetectorRef.detectChanges();
    this.getCountries();
  }

  getCountryId(countryId:number){
    this.SelectedCountryId = countryId; 
    this.dataSource.data = [];
  }


  //los observables utilizan el patron observer, patron de programacion reactiva
  //el observable esta pendiente de lo que sucede con la respuesta http, este emite un valor
  //segun lo que sucede con dicha respuesta, nosotros nos suscribimos al observable para
  //para detectar cuando el este emite un valor y tomar una accion segun ese valor, para ello usamos
  //callback functions, la funcion subscribe recibe 3 callback func caso de exito, error, 
  //completado sin errores
  getCountries(){
    this._countryService.getAll()
      .subscribe(data => { this.countries = data; },
                 error => { console.log(error); this.countries = [];}
                 );
  }

  getOffices(countryId:number){
    this._officeService.getByCountryId(countryId, true)
          .subscribe((data) => { if(data === null || data === undefined){       
                                
                                this.dataSource.data = []; 
                                this.noResultsFound = true;
                              }else{
                                this.offices = data;
                                this.dataSource.data = this.offices;
                                this.dataSource.paginator = this.paginator;     
                                this.noResultsFound = false;   
                              }
                     },
                     (error:any)=>{ console.log(error);
                              this.dataSource.data = []; 
                              this.noResultsFound = true;
                     });
  }

  SearchOffices(){
    this.getOffices(this.SelectedCountryId);
  }  

  enableSubmitButton(){

      if(this.SelectedCountryId === undefined)
          return false;
      else
          return true;
  }

  editOffice(id)
  {
      this._router.navigate(["/office-list","office",id]);
  }

  deleteOffice(id)
  {
    
    this.officeDesc = this.offices.find(x => x.id == id).description;

    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      width: '350px',
      data: {office: this.officeDesc}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.deleteClick = result;
      
      if(this.deleteClick)
      {
           this._officeService.deleteOffice(id)
              .subscribe(deleted =>{
                            console.log(deleted);
                            this.SearchOffices();
                        }
                        ,error=>
                        {
                            console.log(error);
                        })     
      }

    },error=>{
        this.deleteClick = false;
        console.log(error); 
    });    
  }

}
