import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WeatherReportComponent } from '../app/weather-report/weather-report.component';

const routes: Routes = [
  {path:'', component: WeatherReportComponent},
  {
    path:'office-list',
    loadChildren: './office-list/office-list.module#OfficeListModule' 
  },
  {path:'**', redirectTo:'/weather-report'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
