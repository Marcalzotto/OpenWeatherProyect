import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../material/material.module';
import { ReactiveFormsModule } from '@angular/forms';
import { CountryListModule } from '../country-list/country-list.module';
import { OfficeListRoutingModule } from './office-list-routing.module';

import { OfficeListComponent } from './office-list.component';
import { OfficeComponent } from './office/office.component';
import { DeleteDialogComponent } from './office-delete-dialog/delete-dialog.component';


@NgModule({
  declarations: [
    OfficeListComponent,
    OfficeComponent,
    DeleteDialogComponent
  ],
  entryComponents:[
    DeleteDialogComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    ReactiveFormsModule,
    CountryListModule,
    OfficeListRoutingModule
  ]
})
export class OfficeListModule { }
