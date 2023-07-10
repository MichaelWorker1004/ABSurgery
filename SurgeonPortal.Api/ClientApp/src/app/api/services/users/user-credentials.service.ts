import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IUserCredentialModel } from '../../models/users/user-credential.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class UserCredentialsService {

    constructor(private apiService: ApiService) {}

 
        public retrieveUserCredential_GetByUserId(apiVersion = '1.0'): Observable<IUserCredentialModel> {
            /**
            * Claims
            */

            /**
            * Business Rules
            * Business rules for property: EmailAddress
            *   Rule Name: RegEx
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: Password
            *   Rule Name: RegEx
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            */

            /**
            * Required Parameters
            * apiVersion
            */

            /**
            * Calls Sp(s)
            * [get_user_account]
            */
            
            
            return this.apiService.get<IUserCredentialModel>(`api/users/credentials/by-userid?api-version=${apiVersion}`);
        }

        public updateUserCredential(model: IUserCredentialModel,
        apiVersion = '1.0') : Observable<IUserCredentialModel> {
            /**
            * Claims
            */

            /**
            * Business Rules
            * Business rules for property: EmailAddress
            *   Rule Name: RegEx
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: Password
            *   Rule Name: RegEx
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            */

            /**
            * Required Parameters
            * emailAddress:String
            * password:String
            * apiVersion
            */

            /**
            * Calls Sp(s)
            * [upd_user_account]
            */
            
            
            
            return this.apiService.put<IUserCredentialModel>(`api/users/credentials?api-version=${apiVersion}`,
            model);
        }


}
