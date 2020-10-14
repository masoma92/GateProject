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
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { NavbarComponent } from './components/shared/navbar/navbar.component';
import { FooterComponent } from './components/shared/footer/footer.component';
import { AccountsComponent } from './components/+accounts/accounts.component';
import { MainComponent } from './components/main/main.component';
import { DashboardComponent } from './components/+dashboard/dashboard.component';
import { ResetPasswordComponent } from './components/authentication/forget-password/reset-password/reset-password.component';
import { InfoTemplateComponent } from './components/shared/info-template/info-template.component';
import { AccountDetailsComponent } from './components/+accounts/account-details/account-details.component';
import { CreateAccountDialogComponent } from './components/+accounts/create-account/create-account-dialog/create-account-dialog.component';

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
    CreateAccountDialogComponent
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
  bootstrap: [AppComponent]
})
export class AppModule { }
