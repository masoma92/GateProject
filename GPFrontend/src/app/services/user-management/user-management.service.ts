import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment';
import { HeadersBuilder } from '../../core/http/headers.model';
import { EntityResult } from '../common/entity.service';

export class RegisterUserCommand {
  firstName: string;
  lastName: string;
  email: string;
  birth: Date;
  password: string;
  password2: string;   
}

export class ResetPasswordCommand {
  email: string;
  token: string;
  password: string;
  password2: string;   
}

@Injectable({
  providedIn: 'root'
})
export class UserManagementService {

  apiVersion = "/v1";

  constructor(
    private http: HttpClient) { }
    
  
  public register(command: RegisterUserCommand, result: EntityResult<boolean>) {
    result.start();

    return this.http.post<EntityResult<boolean>>(`${environment.authUrl}${this.apiVersion}/registration/register`, command, 
    {
      headers: new HeadersBuilder()
      .json()
      .build(),
      observe: 'response'
    }).subscribe(
      res => {
        if(res.body != null){
          result.value = res.body.value;
          result.errorMessage = res.body.errorMessage;
        }
        result.finish();
      },
      err => {
        result.value = null;
        result.errorMessage = err.error.errorMessage;
        result.finish();
      }
    );
  }

  public confirmemail(email: string, token: string, result: EntityResult<boolean>) {
    let data = { email: email, token: token }

    result.start();

    return this.http.post<EntityResult<boolean>>(`${environment.authUrl}${this.apiVersion}/registration/confirm-email`, data, 
    {
      headers: new HeadersBuilder()
      .json()
      .build(),
      observe: 'response'
    }).subscribe(
      res => {
        if(res.body != null){
          result.value = res.body.value;
          result.errorMessage = res.body.errorMessage;
        }
        result.finish();
      },
      err => {
        result.value = null;
        result.errorMessage = err.error.errorMessage;
        result.finish();
      }
    );
  }

  public resendconfirmemail(email: string, result: EntityResult<boolean>) {
    let data = { email: email }

    result.start();

    return this.http.post<EntityResult<boolean>>(`${environment.authUrl}${this.apiVersion}/registration/resend-confirm-email`, data, 
    {
      headers: new HeadersBuilder()
      .json()
      .build(),
      observe: 'response'
    }).subscribe(
      res => {
        if(res.body != null){
          result.value = res.body.value;
          result.errorMessage = res.body.errorMessage;
        }
        result.finish();
      },
      err => {
        result.value = null;
        result.errorMessage = err.error.errorMessage;
        result.finish();
      }
    );
  }

  public forgetpassword(email: string, result: EntityResult<boolean>) {
    let data = { email: email }

    result.start();

    return this.http.post<EntityResult<boolean>>(`${environment.authUrl}${this.apiVersion}/resetpassword/forget`, data, 
    {
      headers: new HeadersBuilder()
      .json()
      .build(),
      observe: 'response'
    }).subscribe(
      res => {
        if(res.body != null){
          result.value = res.body.value;
          result.errorMessage = res.body.errorMessage;
        }
        result.finish();
      },
      err => {
        result.value = null;
        result.errorMessage = err.error.errorMessage;
        result.finish();
      }
    );
  }

  public resetpassowrd(command: ResetPasswordCommand, result: EntityResult<boolean>) {
    result.start();

    return this.http.post<EntityResult<boolean>>(`${environment.authUrl}${this.apiVersion}/resetpassword/reset`, command, 
    {
      headers: new HeadersBuilder()
      .json()
      .build(),
      observe: 'response'
    }).subscribe(
      res => {
        if(res.body != null){
          result.value = res.body.value;
          result.errorMessage = res.body.errorMessage;
        }
        result.finish();
      },
      err => {
        result.value = null;
        result.errorMessage = err.error.errorMessage;
        result.finish();
      }
    );
  }
}
