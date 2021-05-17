import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthChildGuardService } from '../shared/guards/auth-child-guard.service';
import { AuthGuardService } from '../shared/guards/auth-guard.service';
import { OfficeListComponent } from './office-list.component';
import { OfficeComponent } from './office/office.component';

const routes: Routes = [//configuraciones para la carga perezosa de offices list
  {
    path: '',
    canActivateChild:[AuthChildGuardService],
    children:[
      {
        path: '',
        component: OfficeListComponent,
      },
      {
        path:'office',
        component: OfficeComponent
      },
      {
        path:'office/:id',
        component: OfficeComponent
      }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OfficeListRoutingModule { }
