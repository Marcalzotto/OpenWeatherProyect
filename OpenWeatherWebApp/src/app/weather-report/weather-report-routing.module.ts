import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuardService } from '../shared/guards/auth-guard.service';
import { WeatherReportComponent } from './weather-report.component';

const routes: Routes = [
  {path:'weather-report', component: WeatherReportComponent, canActivate:[AuthGuardService]},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class WeatherReportRoutingModule { }
