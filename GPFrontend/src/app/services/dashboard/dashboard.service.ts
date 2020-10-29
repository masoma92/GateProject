import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HeadersBuilder } from 'src/app/core/http/headers.model';
import { environment } from 'src/environments/environment';
import { AuthenticationService } from '../authentication/authentication.service';
import { EntityResult } from '../common/entity.service';
import { ChartResponse, CreateAccountChart } from './dashboard';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  serverName = environment.adminUrl;
  apiVersion = "/v1";

  constructor(private http: HttpClient,
    private authenticationService: AuthenticationService) {
  }

  getAdminSums(sumType: string, result: EntityResult<number>) {

    result.start();
    this.http.get<EntityResult<number>>(`${this.serverName}${this.apiVersion}${this.getPath()}/${sumType}`,
      {
        headers: new HeadersBuilder()
          .json()
          .withAuthorization(this.authenticationService.storedToken)
          .build(),
        observe: 'response'
      }
    ).subscribe(
      res => {
        if (res.body != null) {
          result.value = res.body.value;
          result.errorMessage = res.body.errorMessage;
        }

        result.finish();
      },
      err => {
        result.value = null;
        result.errorMessage = err.error.errorMessage;
        result.finish();
      });
  }

  createAccountChart(data: CreateAccountChart, result: EntityResult<ChartResponse>) {
    result.start();
    return this.http.post<EntityResult<ChartResponse>>(`${this.serverName}${this.apiVersion}${this.getPath()}/createAccountChart`, data,
      {
        headers: new HeadersBuilder()
          .json()
          .withAuthorization(this.authenticationService.storedToken)
          .build(),
        observe: 'response'
      }
    ).subscribe(
      res => {
        if (res.body != null) {
          result.value = res.body.value;
          result.errorMessage = res.body.errorMessage;
        }
        result.finish();
      },
      err => {
        result.value = null;
        result.errorMessage = err.error.errorMessage;
        result.finish();
      });
  }


  getPath() {
    return '/dashboard';
  }
}
