import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersDashboardComponent } from './components/users-dashboard/users-dashboard.component';
import { UsersDashboardRoutingModule } from './users-dashboard-routing';
import { UserPageComponent } from './components/user-page/user-page.component';
import { UsersListComponent } from './components/users-list/users-list.component';

import { MatListModule } from '@angular/material/list';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule } from '@angular/material/dialog';

import { CustomPipesModule } from 'src/app/core/pipes/custom-pipes.module';
import { ConfirmationDialogModule } from '../shared/confirmation-dialog/confirmation-dialog.module';


@NgModule({
  declarations: [
    UsersDashboardComponent,
    UserPageComponent,
    UsersListComponent
  ],
  imports: [
    CommonModule,
    UsersDashboardRoutingModule,
    MatListModule,
    MatProgressSpinnerModule,
    CustomPipesModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    ConfirmationDialogModule
  ],
  exports: [
    UsersDashboardComponent
  ]
})
export class UsersDashboardModule{
}
