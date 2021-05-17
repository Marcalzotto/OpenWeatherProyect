import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WeatherReportComponent } from '../app/weather-report/weather-report.component';
import { LoginComponent } from '../app/login/login.component'
import { AuthGuardService } from './shared/guards/auth-guard.service';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
  {path:'', component: LoginComponent},
  {path:'login', component: LoginComponent},
  {path:'register', component: RegisterComponent},
  {path:'weather-report', component: WeatherReportComponent, canActivate:[AuthGuardService]},
  {
    path:'office-list',
    loadChildren: () => import('./office-list/office-list.module').then(mod => mod.OfficeListModule),
    canActivate:[AuthGuardService]
  },
  {path:'**', redirectTo:'/weather-report'}
];

//agregamos configuracion para obtener el token del usuario logueado y enviarlo en todas las peticiones
//y agregamos una lista de dominios validos e invalidos, debido a que la libreria auth0 lo requiere.

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
