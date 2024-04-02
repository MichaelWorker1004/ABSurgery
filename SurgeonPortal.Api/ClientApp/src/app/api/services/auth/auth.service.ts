/* eslint-disable prettier/prettier */
// TODO - Convert this to use the setup for YTGIM, currently this is hand codede
import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { map, Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { IAuthCredentials, IAuthState, IError, IRefreshToken } from "../../../state";
import { ApiService } from "ytg-angular";
import { IForgotUsernameReadOnlyModel } from '../../models/auth/forgot-username-read-only.model';
import { IForgotPasswordReadOnlyModel } from '../../models/auth/forgot-password-read-only.model';
import { IResetForgotPasswordReadOnlyModel } from '../../models/auth/reset-forgot-password-read-only-model';


@Injectable({
  providedIn: 'root',
})
export class AuthService {
  token: string | undefined;

  baseUrl = '/api/users';

  constructor(private apiService: ApiService) {}

  login(payload: IAuthCredentials): Observable<IAuthState | IError> {
    return this.apiService
      .post<IAuthState>(`${this.baseUrl}/authenticate`, {
        userName: payload.userName,
        password: payload.password,
      })
      .pipe(
        map((resp) => {
          //sessionStorage.setItem('access_token', <string>resp.access_token);
          return resp;
        }),
        catchError((err: HttpErrorResponse) => {
          return of(err.error as IError);
        })
      );
  }

  refreshToken(payload: IRefreshToken): Observable<IAuthState | IError> {
    return this.apiService
      .post<IAuthState>(`${this.baseUrl}/refresh`, {
        refreshToken: payload.refreshToken,
      })
      .pipe(
        map((resp) => {
          //sessionStorage.setItem('access_token', <string>resp.access_token);
          return resp;
        }),
        catchError((err: HttpErrorResponse) => {
          return of(err.error as IError);
        })
      );
  }

  resetPassword(body: {oldPassword: string, newPassword: string}): Observable<boolean | IError> {
    return this.apiService.post<boolean>(`${this.baseUrl}/reset-password`, {
      oldPassword: body.oldPassword,
      newPassword: body.newPassword,
    }).pipe(
      map((result) => {
        return true;
      }),
      catchError((err: HttpErrorResponse) => {
        return of(err.error as IError);
      })
    );
  }

  public forgotUsername(model: IForgotUsernameReadOnlyModel, apiVersion = '1.0'): Observable<IForgotUsernameReadOnlyModel> {
    /**
    * Business Rules
    * No business rules exist for this model
    */
    
    /**
    * Required Parameters
    * apiVersion
    * email
    */
    
    /**
    * Calls Sp(s)
    * [UNKNOWN]
    */
    
    
    return this.apiService.post<IForgotUsernameReadOnlyModel>(`${this.baseUrl}/forgot-username?api-version=${apiVersion}`, {email: model.email});
  }
  
  public forgotPassword(model: IForgotPasswordReadOnlyModel, apiVersion = '1.0'): Observable<IForgotPasswordReadOnlyModel> {
    /**
    * Business Rules
    * No business rules exist for this model
    */
    
    /**
    * Required Parameters
    * apiVersion
    * username
    */
    
    /**
    * Calls Sp(s)
    * [UNKNOWN]
    */
    
    
    return this.apiService.post<IForgotPasswordReadOnlyModel>(`${this.baseUrl}/create-forgot-password?api-version=${apiVersion}`, {userName: model.userName});
  }

  public resetForgotPassword(model: IResetForgotPasswordReadOnlyModel, apiVersion = '1.0'): Observable<IResetForgotPasswordReadOnlyModel> {
    /**
    * Business Rules
    * No business rules exist for this model
    */
    
    /**
    * Required Parameters
    * guid
    * newPassword
    */
    
    /**
    * Calls Sp(s)
    * [UNKNOWN]
    */
    return this.apiService.post<IResetForgotPasswordReadOnlyModel>(`${this.baseUrl}/reset-forgot-password?api-version=${apiVersion}`, model);
  }

  public getResetForgotPasswordGuid(apiVersion = '1.0'): Observable<string> {
    /**
    * Business Rules
    * No business rules exist for this model
    */

    /**
    * Calls Sp(s)
    * [UNKNOWN]
    */
    return this.apiService.get<string>(`${this.baseUrl}/get-reset-forgot-password-guid?api-version=${apiVersion}`);
  }

}
