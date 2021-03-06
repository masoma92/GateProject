import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { environment } from '../../../environments/environment';
import { HeadersBuilder } from '../../core/http/headers.model';
import { JwtHelper } from '../../core/jwt/jwt-helper';
import { Identity } from '../../core/models/identity';
import { EntityResult } from '../common/entity.service';

export class AuthenticationResponse{
  success: boolean;
  jwtToken: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  readonly LOCAL_STORAGE_LOGIN: string = "rfid-login";
  readonly LOCAL_STORAGE_TOKEN: string = "rfid-token";

  apiVersion = "/v1";
  path = "/authentication"

  currentIdentity = new BehaviorSubject<Identity>(new Identity('','','')); // ennek a next metódusa szól a változásról mindenkinek aki fel van iratkozva a currentIdentity-re (login és logout)
  isLoginInProgress = new BehaviorSubject<boolean>(false);
  
  storedToken: string = "";
  storedEmail: string = "";
  role: string = "";
  isAccountAdmin: boolean = false;

  constructor(
    private http: HttpClient,
    private router: Router) {

      if(typeof localStorage == "undefined") return;

      this.storedToken = localStorage.getItem(this.LOCAL_STORAGE_TOKEN);
      this.storedEmail = localStorage.getItem(this.LOCAL_STORAGE_LOGIN);

      if(this.storedToken != null && this.storedEmail != null)
        this.tryAuthenticateWithToken(this.storedToken);

  }

  public authenticate(email: string, password: string, authenticateResult: EntityResult<AuthenticationResponse>): EntityResult<AuthenticationResponse> {

    let data = { Email: email, Password: password };

    authenticateResult.start();

    this.http.post<any>(`${environment.authUrl}${this.apiVersion}${this.path}`, data, 
      {
        headers: new HeadersBuilder()
        .json()
        .build(),
        observe: 'response'
      }
    ).subscribe(
        result => {
          if (result.body != null) {
            authenticateResult.value = result.body.value;
            authenticateResult.errorMessage = result.body.errorMessage;
          }
          this.receivedAuthenticationResult(authenticateResult, email);
          
        },
        err => {
          authenticateResult.value = null;
          authenticateResult.errorMessage = err.error.errorMessage;
          this.authenticationFailed(authenticateResult);
        }
        
      );
    return authenticateResult;
  }

  tryAuthenticateWithToken(token: string, authenticateResult: EntityResult<AuthenticationResponse> = null): boolean {

    this.isLoginInProgress.next(true);

    this.http.get<any>(`${environment.adminUrl}${this.apiVersion}` + "/status", 
    {
      headers: new HeadersBuilder()
        .json()
        .withAuthorization(token)
        .build(),
        observe: 'response'
    }).subscribe(
      result => {
        if(result.body != null) {
          this.role = result.body.value.role;
          this.isAccountAdmin = result.body.value.isAccountAdmin;
          this.currentIdentity.next(new Identity(this.storedEmail, token, this.role, this.isAccountAdmin));
          this.isLoginInProgress.next(false);
          this.router.navigate(['main']);
          if (authenticateResult)
            authenticateResult.finish();
        }
      },
      error => {
        this.isLoginInProgress.next(false);
      }
    );

    return false;
  }

  private receivedAuthenticationResult(authenticateResult: EntityResult<AuthenticationResponse>, email: string) {

    localStorage.setItem(this.LOCAL_STORAGE_LOGIN, email);
    localStorage.setItem(this.LOCAL_STORAGE_TOKEN, authenticateResult.value.jwtToken);
    this.storedEmail = email;
    this.storedToken = authenticateResult.value.jwtToken;

    this.tryAuthenticateWithToken(authenticateResult.value.jwtToken, authenticateResult);
  }

  private authenticationFailed(authenticateResult: EntityResult<AuthenticationResponse>) {
    if (this.currentIdentity == null) return;

    this.currentIdentity.next(new Identity(null, null, null));

    authenticateResult.finish();
}

  public logout() {

    localStorage.removeItem(this.LOCAL_STORAGE_LOGIN);
    localStorage.removeItem(this.LOCAL_STORAGE_TOKEN);

    this.router.navigate(['login']);
    localStorage.removeItem('currentUser');
  }

  public truncateLocalStorage() {
    localStorage.removeItem(this.LOCAL_STORAGE_LOGIN);
    localStorage.removeItem(this.LOCAL_STORAGE_TOKEN);
  }

  public isAuthenticated(): boolean {
    const token = localStorage.getItem(this.LOCAL_STORAGE_TOKEN);
    // Check whether the token is expired and return
    // true or false
    return !JwtHelper.isTokenExpired(token);
  }
}