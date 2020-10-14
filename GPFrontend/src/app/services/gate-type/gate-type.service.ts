import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthenticationService } from '../authentication/authentication.service';
import { EntityService } from '../common/entity.service';

export class GateType {
  name: string;
}

@Injectable({
  providedIn: 'root'
})
export class GateTypeService extends EntityService<GateType> {

  constructor(http: HttpClient,
    authenticationService: AuthenticationService) {
    super(http, authenticationService);
  }

  getPath() {
    return '/gatetype';
  }
}
