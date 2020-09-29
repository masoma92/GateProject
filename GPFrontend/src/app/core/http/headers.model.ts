import { HttpHeaders } from '@angular/common/http';
import { JsonInHttpHeader } from './json-in-http-header';

export class HeadersBuilder {

    private _headers = new HttpHeaders();
  
    public json(): HeadersBuilder {
      this._headers = this._headers.append('accept', 'application/json');
      this._headers = this._headers.append('Content-Type', 'application/json');
      return this;
    }
  
    public image(contentType, filename): HeadersBuilder {
      this._headers = this._headers.append('accept', 'image/*');
      this._headers = this._headers.append('Content-Type', contentType)
      this._headers = this._headers.append('x-filename', filename)
      return this;
    }
  
    public withAuthorization(token: string): HeadersBuilder {
      this._headers = this._headers.append('Authorization', 'Bearer ' + token);
      return this;
    }
  
    public withFiltering(filter): HeadersBuilder {
      this._headers = this._headers.append('x-filtering', JsonInHttpHeader.encode(filter)); //encodeURIComponent(JSON.stringify(filter)));
      return this;
    }
  
    public withFilteringNoEncoding(filter): HeadersBuilder {
      this._headers = this._headers.append('x-filtering', JSON.stringify(filter)); //encodeURIComponent(JSON.stringify(filter)));
      return this;
    }
  
    public withPagination(currentPage: number, pageSize: number): HeadersBuilder {
      var pagination = {
        currentPage: currentPage,
        pageSize: pageSize
      }
      this._headers = this._headers.append('x-pagination', JSON.stringify(pagination));
      return this;
    }
  
    public withoutPagination(): HeadersBuilder {
      this._headers = this._headers.append('x-pagination', 'none');
      return this;
    }
  
    public withSorting(sortBy: string, isSortAscending: boolean): HeadersBuilder {
      var sorting = {
        sortBy: sortBy,
        isSortAscending: isSortAscending
      }
      this._headers = this._headers.append('x-sorting', JSON.stringify(sorting));
      return this;
    }
  
  
    public build(): HttpHeaders {
      return this._headers;
    }
  }