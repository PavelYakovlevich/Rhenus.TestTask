import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UsersDashboardComponent } from './components/users-dashboard/users-dashboard.component';

const routes: Routes = [
    {
        path: "",
        component: UsersDashboardComponent
    },
    {
        path: "users/{id: string}",
        loadChildren: () => import('./modules/auth/registration/registration.module').then(m => m.RegistrationModule)
    }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersDashboardRoutingModule { }
