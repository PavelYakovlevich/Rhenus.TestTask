import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersDashboardComponent } from './components/users-dashboard/users-dashboard.component';



@NgModule({
  declarations: [
    UsersDashboardComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    UsersDashboardComponent
  ]
})
export class UsersDashboardModule { }
