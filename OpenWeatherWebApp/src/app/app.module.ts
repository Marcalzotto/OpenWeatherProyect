import { NgModule} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material/material.module';
import { HttpClientModule} from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from '../app/app-routing.module';
import { WeatherReportModule } from './weather-report/weather-report.module';
import { CountryListModule } from './country-list/country-list.module';

import { AppComponent } from './app.component';
import { MenuBarComponent } from './menu-bar/menu-bar.component';
import { LoginComponent } from './login/login.component';

import { CountryService } from './shared/services/country.service';
import { OfficeService} from './shared/services/office.service';
import { WeatherConditionService} from './shared/services/weathercondition.service';
import { CityService } from './shared/services/city.service';
import { LoginService } from './shared/services/login.service';
import { AuthGuardService } from './shared/guards/auth-guard.service';
import { httpInterceptorProviders } from './shared/http-interceptors/interceptorProviders';
import { AuthChildGuardService } from './shared/guards/auth-child-guard.service';
import { RegisterComponent } from './register/register.component';

@NgModule({
  declarations: [
    AppComponent,
    MenuBarComponent,
    LoginComponent,
    RegisterComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MaterialModule,
    HttpClientModule,
    ReactiveFormsModule,
    WeatherReportModule,
    AppRoutingModule,
    CountryListModule
  ],
  providers: [
    CountryService,
    OfficeService,
    WeatherConditionService,
    CityService,
    LoginService,
    AuthGuardService,
    AuthChildGuardService,
    httpInterceptorProviders
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
