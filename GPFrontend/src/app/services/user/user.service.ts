import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthenticationService } from '../authentication/authentication.service';
import { EntityService } from '../common/entity.service';

export class User {
  email: string;
}

@Injectable({
  providedIn: 'root'
})
export class UserService extends EntityService<User> {

  constructor(http: HttpClient,
    authenticationService: AuthenticationService) {
    super(http, authenticationService);
  }

  getPath() {
    return '/user';
  }
}
