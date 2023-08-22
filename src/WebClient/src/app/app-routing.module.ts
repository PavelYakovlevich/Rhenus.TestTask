import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UsersDashboardComponent } from './modules/users-dashboard/components/users-dashboard/users-dashboard.component';
import { userAuthenticatedGuard } from './core/guards/user-authenticated.guard';

const routes: Routes = [
  {
    path: "auth/login",
    loadChildren: () => import('./modules/auth/login/login.module').then(m => m.LoginModule)
  },
  {
    path: "auth/registration",
    loadChildren: () => import('./modules/auth/registration/registration.module').then(m => m.RegistrationModule)
  },
  {
    path: "**", 
    component: UsersDashboardComponent,
    canActivate: [userAuthenticatedGuard],
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
