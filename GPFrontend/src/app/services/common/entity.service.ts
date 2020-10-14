import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HeadersBuilder } from '../../core/http/headers.model';
import { ListPagination } from '../../core/pagination/list-pagination';
import { AuthenticationService } from '../authentication/authentication.service';

export class Sorting {
  sortBy: string;
  isSortAscending: boolean;
}

export class ErrorEntry {

  constructor(public errorCode: string,
    public debugText: string,
    public propertyName: string) { }
}

export class ListRecords<T> {
  records: T[];
  recordCount: number;
}

export interface IEntityReference{
  id: number,
  name: string
}

export class EntityReference implements IEntityReference{
  id: number;
  name: string = '';

  constructor(id: number, name?: string){
      this.id = id;
      this.name = name;
  }
}

export class EntityListResult<T> {
  result = new EntityResult<T[]>();
  pagination: ListPagination;

  public onFinished = () => { };

  public get isInProgress(): boolean {
    return this.result.isInProgress;
  }

  public get isSuccess(): boolean {
    return this.result.isSuccess;
  }

  public get hasError(): boolean {
    return this.result.hasError;
  }

  public get hasValue(): boolean {
    return this.result.hasValue;
  }

  public get value(): T[] {
    return this.result.value;
  }

  public get errors(): string {
    return this.result.errorMessage;
  }

  public start() {
    this.result.start();
  }

  public finish() {
    this.result.isInProgress = false;
    this.onFinished();
  }
}

export class EntityResult<T> {
  public value: T = null;
  public errorMessage: string = '';


  public onFinished = () => { };

  public isInProgress: boolean = false;

  public get isSuccess(): boolean {
    return this.errorMessage == null;
  }

  public get hasError(): boolean {
    return this.errorMessage != null;
  }

  public get hasValue(): boolean {
    return this.value != null;
  }

  public finish() {
    this.isInProgress = false;
    this.onFinished();
  }

  public start() {
    this.value = null;
    this.errorMessage = '';
    this.isInProgress = true;
  }
}

@Injectable()
export class EntityService<T, TCreate = T, TUpdate = T> {

  constructor(protected http: HttpClient,
    protected authenticationService: AuthenticationService) {
  }

  serverName = environment.adminUrl;
  apiVersion = "/v1";

  getPath() {
    return "";
  }

  //Get a list of entities
  getList(result: EntityListResult<T>, listPagination: ListPagination, sorting: Sorting, filtering: string) {

    result.start();
    this.http.get<EntityResult<ListRecords<T>>>(`${this.serverName}${this.apiVersion}${this.getPath()}/get-all`,
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

  //Get all the entities

  getReferences(result: EntityResult<EntityReference[]>, filter = {}) {

    result.start();
    this.http.get<EntityResult<EntityReference[]>>(`${this.serverName}${this.apiVersion}${this.getPath()}`,
      {
        headers: new HeadersBuilder()
          .json()
          .withAuthorization(this.authenticationService.storedToken)
          .withFiltering(filter)
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

  //Get entity by id
  get(id: number, result: EntityResult<T>) {

    result.start();
    this.http.get<EntityResult<T>>(`${this.serverName}${this.apiVersion}${this.getPath()}/${id}`,
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

  // Create entity
  create(data: TCreate, result: EntityResult<number>) {
    result.start();
    return this.http.post<EntityResult<number>>(`${this.serverName}${this.apiVersion}${this.getPath()}/create`, data,
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

  update(data: TUpdate, result: EntityResult<boolean>) {

    result.start();
    this.http.put<EntityResult<boolean>>(`${this.serverName}${this.apiVersion}${this.getPath()}/update`, data,
      {
        headers: new HeadersBuilder()
          .json()
          .withAuthorization(this.authenticationService.storedToken)
          .build(),
        observe: 'response'
      }
    ).subscribe(res => {
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

  //Delete entities 
  delete(id: number, result: EntityResult<boolean>) {

    result.start();
    return this.http.delete<EntityResult<boolean>>(`${this.serverName}${this.apiVersion}${this.getPath()}/delete/${id}`,
      {
        headers: new HeadersBuilder()
          .json()
          .withAuthorization(this.authenticationService.storedToken)
          .build(),
        observe: 'response'
      }
    ).subscribe(res => {
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
      })
  }
}
