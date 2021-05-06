import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WeatherReportComponent } from './weather-report.component';

const routes: Routes = [
  {path:'weather-report', component: WeatherReportComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class WeatherReportRoutingModule { }
