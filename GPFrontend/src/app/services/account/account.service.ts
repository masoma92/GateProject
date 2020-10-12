import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthenticationService } from '../authentication/authentication.service';
import { EntityService } from '../common/entity.service';
import { Account, CreateAccountCommand, UpdateAccountCommand } from './account';

@Injectable({
  providedIn: 'root'
})
export class AccountService extends EntityService<Account, CreateAccountCommand, UpdateAccountCommand> {

  constructor(http: HttpClient,
    authenticationService: AuthenticationService) {
    super(http, authenticationService);
  }

  getPath() {
    return '/account';
  }
}
