import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from 'ytg-n';

import { IAppUserReadOnlyModel } from '../../models/users/app-user-read-only.model';

/**
#############################################################################
WARNING GENERATED CODE DO NOT MODIFY -

All client side API services are generated by the server side API
developer when the API is created. Please do not make changes to this file

############################################################################
**/

@Injectable()
export class UsersApiService {
  private readonly baseEndpoint = 'users';

  constructor(private apiService: ApiService) {}

  public retrieveAppUserReadOnly_GetByCredentials(
    emailAddress: string,
    password: string,
    apiVersion = '1.0'
  ): Observable<IAppUserReadOnlyModel> {
    /**
     * Claims
     */

    /**
     * Business Rules
     * No business rules exist for this model
     */

    /**
     * Required Parameters
     * emailAddress:String
     * password:String
     * apiVersion:String
     */

    /**
     * Calls Sp(s)
     * [get_userlogin]
     */

    return this.apiService.post<IAppUserReadOnlyModel>(
      `${this.baseEndpoint}/authenticate`,
      {
        emailAddress,
        password,
        apiVersion,
      }
    );
  }
}
