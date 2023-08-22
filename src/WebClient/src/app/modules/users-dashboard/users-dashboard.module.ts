import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersDashboardComponent } from './components/users-dashboard/users-dashboard.component';
import { UsersDashboardRoutingModule } from './users-dashboard-routing';
import { UserPageComponent } from './components/user-page/user-page.component';
import { UsersListComponent } from './components/users-list/users-list.component';

@NgModule({
  declarations: [
    UsersDashboardComponent,
    UserPageComponent,
    UsersListComponent
  ],
  imports: [
    CommonModule,
    UsersDashboardRoutingModule
  ],
  exports: [
    UsersDashboardComponent
  ]
})
export class UsersDashboardModule{
}
