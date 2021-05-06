import { NgModule} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material/material.module';
import { HttpClientModule} from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from '../app/app-routing.module';

import { AppComponent } from './app.component';
import { MenuBarComponent } from './menu-bar/menu-bar.component';

import { CountryService } from './shared/services/country.service';
import { OfficeService} from './shared/services/office.service';
import { WeatherConditionService} from './shared/services/weathercondition.service';

import { WeatherReportModule } from './weather-report/weather-report.module';
import { OfficeListModule } from './office-list/office-list.module';
import { CountryListModule } from './country-list/country-list.module';
import { CityService } from './shared/services/city.service';

@NgModule({
  declarations: [
    AppComponent,
    MenuBarComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MaterialModule,
    HttpClientModule,
    ReactiveFormsModule,
    WeatherReportModule,
    AppRoutingModule,
    CountryListModule,
  ],
  providers: [
    CountryService,
    OfficeService,
    WeatherConditionService,
    CityService
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
