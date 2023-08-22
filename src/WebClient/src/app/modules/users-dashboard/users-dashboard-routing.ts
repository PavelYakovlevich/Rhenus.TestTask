import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UsersDashboardComponent } from './components/users-dashboard/users-dashboard.component';
import { UserPageComponent } from './components/user-page/user-page.component';
import { UsersListComponent } from './components/users-list/users-list.component';

const routes: Routes = [
    {
        path: "",
        component: UsersDashboardComponent,
        children: [
            {
                path: './:id',
                component: UserPageComponent
            },
            {
                path: '**',
                component: UsersListComponent
            }
        ]
    }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersDashboardRoutingModule { }
