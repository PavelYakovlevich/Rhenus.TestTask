import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UsersListComponent } from './components/users-list/users-list.component';
import { UserPageComponent } from '../user/components/user-page/user-page.component';

const routes: Routes = [
    {
        path: "",
        component: UsersListComponent,
    },
    {
        path: ':id',
        component: UserPageComponent
    },
    {
        path: '**',
        redirectTo: ""
    },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersDashboardRoutingModule { }
