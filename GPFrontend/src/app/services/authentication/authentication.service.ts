import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { environment } from '../../../environments/environment';
import { HeadersBuilder } from '../../core/http/headers.model';
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
  
  _role: string = "";
  storedToken: string = "";

  constructor(
    private http: HttpClient,
    private router: Router) {

      if(typeof localStorage == "undefined") return;

      this.storedToken = localStorage.getItem(this.LOCAL_STORAGE_TOKEN);
      let loginName = localStorage.getItem(this.LOCAL_STORAGE_LOGIN);

      // if(this.storedToken != null && loginName != null)
      //   this.tryAuthenticateWithToken(loginName, this.storedToken);

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

  // tryAuthenticateWithToken(loginName: string, token: string): boolean {

  //   this.isLoginInProgress.next(true);

  //   this.http.get(environment.authUrl + "/api/v1/status", 
  //   {
  //     headers: new HeadersBuilder()
  //       .json()
  //       .withAuthorization(token)
  //       .build(),
  //       observe: 'response'
  //   }).subscribe(
  //     result => {
  //       if(result.status == 200) {
  //         this.setNewIdentity(loginName, token).subscribe(x => {
  //           if (x != null) {
  //             this.currentIdentity.next(x);
  //             this.isLoginInProgress.next(false);
  //           }
  //         });
  //       }
  //     },
  //     error => {
  //       this.isLoginInProgress.next(false);
  //     }
  //   )
  //   return false;
  // }

  private receivedAuthenticationResult(authenticateResult: EntityResult<AuthenticationResponse>, email: string) {

    localStorage.setItem(this.LOCAL_STORAGE_LOGIN, email);
    localStorage.setItem(this.LOCAL_STORAGE_TOKEN, authenticateResult.value.jwtToken);

    // this.setNewIdentity(email, authenticateResult.value.jwtToken).subscribe(x => {
    //   if (x != null) {
    //       this.currentIdentity.next(x);
    //       this.isLoginInProgress.next(false);
    //   }
    // });

    authenticateResult.finish();
  }

  // private setNewIdentity(email: string, token: string): BehaviorSubject<Identity> {

  //   let result = new BehaviorSubject<Identity>(null);

  //   this.http.get<any>(environment.authUrl + '/api/v1/role/my-role', 
  //     {
  //       headers: new HeadersBuilder()
  //         .json()
  //         .withAuthorization(token)
  //         .build(),
  //       observe: 'response'
  //     }
  //   ).subscribe(
  //     res => {
  //       console.log("role:", res.body);
  //       if(res.body != null) {
  //         this._role = res.body;
  //       }
  //       result.next(new Identity(email, token, this._role));
  //       this.router.navigate(['/main/dashboard']);
  //     },
  //     error => {
  //     }
  //   );
  //   return result;
  // }

  private authenticationFailed(authenticateResult: EntityResult<AuthenticationResponse>) {
    if (this.currentIdentity == null) return;

    this.currentIdentity.next(new Identity(null, null, null));

    authenticateResult.finish();
}

  public logout() {

    localStorage.removeItem(this.LOCAL_STORAGE_LOGIN);
    localStorage.removeItem(this.LOCAL_STORAGE_TOKEN);

    this.router.navigate(['/login']);
    localStorage.removeItem('currentUser');
  }

  public truncateLocalStorage() {
    localStorage.removeItem(this.LOCAL_STORAGE_LOGIN);
    localStorage.removeItem(this.LOCAL_STORAGE_TOKEN);
  }
}