import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './components/app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AngularMaterialModule } from './angular-material.module';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { ForgetPasswordComponent } from './components/authentication/forget-password/forget-password.component';
import { LoginComponent } from './components/authentication/login/login.component';
import { RegisterComponent } from './components/authentication/register/register.component';
import { ConfirmEmailComponent } from './components/authentication/confirm-email/confirm-email.component';
import { NavbarComponent } from './components/shared/navbar/navbar.component';
import { FooterComponent } from './components/shared/footer/footer.component';
import { AccountsComponent } from './components/+accounts/accounts.component';
import { MainComponent } from './components/main/main.component';
import { DashboardComponent } from './components/+dashboard/dashboard.component';
import { ResetPasswordComponent } from './components/authentication/forget-password/reset-password/reset-password.component';
import { InfoTemplateComponent } from './components/shared/info-template/info-template.component';
import { AccountDetailsComponent } from './components/+accounts/account-details/account-details.component';
import { CreateAccountDialogComponent } from './components/+accounts/create-account/create-account-dialog.component';
import { GatesComponent } from './components/+gates/gates.component';
import { CreateGateDialogComponent } from './components/+gates/create-gate/create-gate-dialog.component';
import { GateDetailsComponent } from './components/+gates/gate-details/gate-details.component';
import { ManageGateusersDialogComponent } from './components/+gates/manage-gateusers/manage-gateusers-dialog.component';
import { ManageUsersDialogComponent } from './components/+accounts/manage-users/manage-users-dialog.component';
import { SidebarComponent } from './components/shared/sidebar/sidebar.component';
import { DashboardAccountadminComponent } from './components/+dashboard/dashboard-accountadmin/dashboard-accountadmin.component';
import { DashboardUserComponent } from './components/+dashboard/dashboard-user/dashboard-user.component';
import { DashboardAdminComponent } from './components/+dashboard/dashboard-admin/dashboard-admin.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ForgetPasswordComponent,
    RegisterComponent,
    ConfirmEmailComponent,
    MainComponent,
    SidebarComponent,
    NavbarComponent,
    FooterComponent,
    AccountsComponent,
    DashboardComponent,
    ResetPasswordComponent,
    InfoTemplateComponent,
    AccountDetailsComponent,
    CreateAccountDialogComponent,
    ManageUsersDialogComponent,
    GatesComponent,
    CreateGateDialogComponent,
    GateDetailsComponent,
    ManageGateusersDialogComponent,
    DashboardAccountadminComponent,
    DashboardUserComponent,
    DashboardAdminComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    AngularMaterialModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  entryComponents: [
    CreateAccountDialogComponent, 
    ManageUsersDialogComponent, 
    CreateGateDialogComponent,
    ManageGateusersDialogComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
