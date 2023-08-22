import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
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
    path: "users", 
    loadChildren: () => import('./modules/users-dashboard/users-dashboard.module').then(m => m.UsersDashboardModule),
    canActivate: [userAuthenticatedGuard],
  },
  {
    path: "**", 
    redirectTo: "auth/login"
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
