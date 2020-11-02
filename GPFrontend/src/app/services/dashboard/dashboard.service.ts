import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HeadersBuilder } from 'src/app/core/http/headers.model';
import { ListPagination } from 'src/app/core/pagination/list-pagination';
import { environment } from 'src/environments/environment';
import { AuthenticationService } from '../authentication/authentication.service';
import { EntityListResult, EntityResult, ListRecords, Sorting } from '../common/entity.service';
import { ChartResponse, CreateChart, GetEnters, GetEntersResponse } from './dashboard';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  serverName = environment.adminUrl;
  apiVersion = "/v1";

  constructor(private http: HttpClient,
    private authenticationService: AuthenticationService) {
  }

  getSums(sumType: string, result: EntityResult<number>) {

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

  getAccountAdminSums(sumType: string, id: number, result: EntityResult<number>) {

    result.start();
    this.http.get<EntityResult<number>>(`${this.serverName}${this.apiVersion}${this.getPath()}/${sumType}byAccount/${id}`,
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

  createChart(type: string, data: CreateChart, result: EntityResult<ChartResponse>) {
    result.start();
    return this.http.post<EntityResult<ChartResponse>>(`${this.serverName}${this.apiVersion}${this.getPath()}/${type}`, data,
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

  getEnters(request: GetEnters, result: EntityListResult<GetEntersResponse>, listPagination: ListPagination, sorting: Sorting, filtering: string) {

    result.start();
    this.http.post<EntityResult<ListRecords<GetEntersResponse>>>(`${this.serverName}${this.apiVersion}${this.getPath()}/get-enters`, request,
      {
        headers: new HeadersBuilder()
          .json()
          .withAuthorization(this.authenticationService.storedToken)
          .withPagination(listPagination.currentPage, listPagination.pageSize)
          .withSorting(sorting.sortBy, sorting.isSortAscending)
          .withFiltering(filtering)
          .build(),
        observe: 'response'
      }
    ).subscribe(
      res => {
        if (res.body != null) {
          result.result.value = res.body.value.records;
          result.result.errorMessage = res.body.errorMessage;

          const paging = res.headers.get("x-pagination");
          var pagination = ListPagination.parse(paging);
          pagination.allItems = res.body.value.recordCount;
          result.pagination = pagination;
        }

        result.finish();
      },
      err => {
        result.result.value = null;
        result.result.errorMessage = err.error.errorMessage;
        result.finish();
      });
  }

  // user

  getRegDate(result: EntityResult<Date>) {

    result.start();
    this.http.get<EntityResult<Date>>(`${this.serverName}${this.apiVersion}${this.getPath()}/registrationDate`,
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

  getLastGateAccessDate(result: EntityResult<Date>) {
    result.start();
    this.http.get<EntityResult<Date>>(`${this.serverName}${this.apiVersion}${this.getPath()}/lastGateAccessDate`,
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

  createGateUsageChart(data: CreateChart, result: EntityResult<ChartResponse>) {
    result.start();
    return this.http.post<EntityResult<ChartResponse>>(`${this.serverName}${this.apiVersion}${this.getPath()}/createGateUsageChart`, data,
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
