import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersDashboardRoutingModule } from './users-dashboard-routing';
import { UsersListComponent } from './components/users-list/users-list.component';

import { MatListModule } from '@angular/material/list';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule } from '@angular/material/dialog';

import { CustomPipesModule } from 'src/app/core/pipes/custom-pipes.module';
import { ConfirmationDialogModule } from '../shared/confirmation-dialog/confirmation-dialog.module';


@NgModule({
  declarations: [
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
    ConfirmationDialogModule,
    MatProgressBarModule
  ],
  exports: []
})
export class UsersDashboardModule{
}
