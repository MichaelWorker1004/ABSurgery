import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpHeaders,
  HttpErrorResponse,
  HttpParams,
} from '@angular/common/http';
import { of, throwError } from 'rxjs';
import { catchError, share } from 'rxjs/operators';

import { AppState, StateMode } from '../state';
import { stateAuth } from '../state';

import { ResponseObservable, ApiConfig } from './api.model';
import { StateService, StateChange } from '../state';

@Injectable()
export class ApiService {
  private savedToken: string;
  private token: string;

  @AppState(stateAuth, StateMode.ReadWrite)
  private authorized: boolean;

  private defaultHeaders = new HttpHeaders({
    'Content-Type': 'application/json',
  });

  constructor(
    private http: HttpClient,
    private stateService: StateService,
    private apiConfig: ApiConfig
  ) {
    this.authorized = this.savedToken != null;
    if (this.authorized) {
      this.setToken(this.savedToken, true);
    }
    this.stateService.subscribe<boolean>(stateAuth, (change) =>
      this.authChanged(change)
    );
  }

  private authChanged(change: StateChange<boolean>) {
    if (change.previous && !change.value) {
      this.setToken(null, false, false);
    }
  }

  public setToken(token: string, remember: boolean, setAuth = true) {
    this.token = token;
    this.defaultHeaders = this.defaultHeaders.append(
      'Authorization',
      'Bearer ' + token
    );
    if (remember) {
      this.savedToken = token;
    }
    if (setAuth) {
      this.authorized = true;
    }
  }

  public getToken() {
    return this.token;
  }

  public clearToken(clearSaved = true) {
    this.token = null;
    this.authorized = false;
    this.defaultHeaders = this.defaultHeaders.delete('Authorization');
    if (clearSaved) {
      this.savedToken = null;
    }
  }

  public get<T>(
    url: string,
    params?: { [key: string]: any },
    headers?: HttpHeaders
  ): ResponseObservable<T> {
    url = this.buildUrl(url);
    params = this.filterParams(params);
    return this.http
      .get(url, {
        params: params,
        headers: headers || this.defaultHeaders,
      })
      .pipe(
        share(),
        catchError<any, any>((error) => this.mapErrorResponse(error))
      );
  }

  public post<T>(
    url: string,
    body?: any,
    params?: { [key: string]: any },
    headers?: HttpHeaders
  ): ResponseObservable<T> {
    url = this.buildUrl(url);
    params = this.filterParams(params);
    return this.http
      .post(url, body, {
        params: params,
        headers: headers || this.defaultHeaders,
      })
      .pipe(
        share(),
        catchError<any, any>((error) => this.mapErrorResponse(error))
      );
  }

  public put<T>(
    url: string,
    body?: any,
    headers?: HttpHeaders
  ): ResponseObservable<T> {
    url = this.buildUrl(url);
    return this.http
      .put(url, body, {
        headers: headers || this.defaultHeaders,
      })
      .pipe(
        share(),
        catchError<any, any>((error) => this.mapErrorResponse(error))
      );
  }

  public patch<T>(
    url: string,
    body: any,
    usePatchPayloadFormatter = true,
    headers?: HttpHeaders
  ): ResponseObservable<T> {
    url = this.buildUrl(url);
    return this.http
      .patch(
        url,
        usePatchPayloadFormatter ? this.getPatchPayload(body) : body,
        {
          headers: headers || this.defaultHeaders,
        }
      )
      .pipe(
        share(),
        catchError<any, any>((error) => this.mapErrorResponse(error))
      );
  }

  private getPatchPayload(body: any) {
    const patchPayload = [];

    for (const key of Object.keys(body)) {
      patchPayload.push({
        op: 'replace',
        path: `/${key}`,
        value: body[key],
      });
    }

    return patchPayload;
  }

  public delete<T>(
    url: string,
    params?: { [key: string]: any },
    headers?: HttpHeaders
  ): ResponseObservable<T> {
    url = this.buildUrl(url);
    let filteredParams = this.filterParams(params);
    return this.http
      .delete(url, {
        params: filteredParams,
        headers: headers || this.defaultHeaders,
      })
      .pipe(
        share(),
        catchError<any, any>((error) => this.mapErrorResponse(error))
      );
  }

  public postForm<T>(
    url: string,
    body: { [key: string]: any },
    params?: { [key: string]: any },
    headers?: HttpHeaders
  ): ResponseObservable<T> {
    url = this.buildUrl(url);
    params = this.filterParams(params);
    let bodyParams = new HttpParams();
    for (let key in body) {
      if (body[key] != null) {
        bodyParams.set(key, body[key]);
      }
    }
    return this.http
      .post(url, bodyParams.toString(), {
        params: params,
        headers: headers || this.defaultHeaders,
      })
      .pipe(
        share(),
        catchError<any, any>((error) => this.mapErrorResponse(error))
      );
  }

  private filterParams(params?: { [key: string]: any }) {
    let filteredParams = {};

    // tslint:disable-next-line:forin
    for (let param in params) {
      let val = params[param];
      if (val != null) {
        filteredParams[param] = val;
      }
    }
    return filteredParams;
  }

  private buildUrl(url: string): string {
    return this.apiConfig.url + url;
  }

  // We can handle all other errors in interceptor
  private mapErrorResponse(
    response: HttpErrorResponse
  ): ResponseObservable<any> {
    if (this.apiConfig.throwErrors) {
      return throwError(response);
    }
    // if the body type is a progress event it means no connection was made.
    if (response.status === 401) {
      this.authorized = false;
      return of(<any>{
        // TODO Change this to correspond with our errror response model
        errorMessage: 'Authorization error',
      });
    }
    // propagate the response.
    return of(response.error);
  }
}
