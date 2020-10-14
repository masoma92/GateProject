import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthenticationService } from '../authentication/authentication.service';
import { EntityService } from '../common/entity.service';

export class AccountType {
  name: string;
}

@Injectable({
  providedIn: 'root'
})
export class AccountTypeService extends EntityService<AccountType> {

  constructor(http: HttpClient,
    authenticationService: AuthenticationService) {
    super(http, authenticationService);
  }

  getPath() {
    return '/accounttype';
  }
}