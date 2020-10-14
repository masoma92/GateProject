import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthenticationService } from '../authentication/authentication.service';
import { EntityService } from '../common/entity.service';
import { CreateGateCommand, Gate } from './gate';

@Injectable({
  providedIn: 'root'
})
export class GateService extends EntityService<Gate, CreateGateCommand> {

  constructor(http: HttpClient,
    authenticationService: AuthenticationService) {
    super(http, authenticationService);
  }

  getPath() {
    return '/gate';
  }
}
