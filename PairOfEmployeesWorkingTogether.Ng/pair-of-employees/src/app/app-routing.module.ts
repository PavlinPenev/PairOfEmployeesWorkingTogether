import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployeesDashboardComponent } from './employees-dashboard/employees-dashboard.component';

const routes: Routes = [
  { path: '', component: EmployeesDashboardComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
