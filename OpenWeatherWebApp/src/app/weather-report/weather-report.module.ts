import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { WeatherReportRoutingModule } from './weather-report-routing.module';
import { MaterialModule } from '../material/material.module';
import { WeatherReportComponent } from './weather-report.component';
import { ReactiveFormsModule } from '@angular/forms';
import { CountryListModule } from '../country-list/country-list.module';
import { NullableValuePipe } from '../shared/Pipes/NullableValue/nullable-value.pipe';


@NgModule({
  declarations: [
    WeatherReportComponent,
    NullableValuePipe
  ],
  imports: [
    CommonModule,
    MaterialModule,
    ReactiveFormsModule,
    CountryListModule,
    WeatherReportRoutingModule,
  ]
  
})
export class WeatherReportModule { }
