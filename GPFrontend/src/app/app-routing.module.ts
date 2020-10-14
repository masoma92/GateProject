import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AccountsComponent } from './components/+accounts/accounts.component';
import { DashboardComponent } from './components/+dashboard/dashboard.component';
import { GatesComponent } from './components/+gates/gates.component';
import { ConfirmEmailComponent } from './components/authentication/confirm-email/confirm-email.component';
import { ForgetPasswordComponent } from './components/authentication/forget-password/forget-password.component';
import { ResetPasswordComponent } from './components/authentication/forget-password/reset-password/reset-password.component';
import { LoginComponent } from './components/authentication/login/login.component';
import { RegisterComponent } from './components/authentication/register/register.component';
import { MainComponent } from './components/main/main.component';
import { InfoTemplateComponent } from './components/shared/info-template/info-template.component';
import { Role } from './core/models/role';
import { AuthGuard } from './services/authentication/auth-guard.service';


const routes: Routes = [
  {path: '', redirectTo: 'login', pathMatch: 'full'},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'register-success', component: InfoTemplateComponent, data: { path: "register-success" }},
  {path: 'confirm-email', component: ConfirmEmailComponent},
  {path: 'forget-password', component: ForgetPasswordComponent},
  {path: 'reset-password', component: ResetPasswordComponent},
  {path: 'forget-password-requested', component: InfoTemplateComponent, data: { path: "forget-password-requested" }},
  {path: 'forget-password-success', component: InfoTemplateComponent, data: { path: "forget-password-success" }},
  {path: 'main', component: MainComponent, canActivate: [AuthGuard], data: {roles: [Role.Admin, Role.User]},
    children: [
      {path: '', component: DashboardComponent, canActivate: [AuthGuard], data: {roles: [Role.Admin, Role.User]}},
      {path: 'accounts', component: AccountsComponent, canActivate: [AuthGuard], data: {roles: [Role.Admin]}},
      {path: 'gates', component: GatesComponent, canActivate: [AuthGuard], data: {roles: [Role.Admin]}}]},
  {path: '**', redirectTo: 'login'}
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
