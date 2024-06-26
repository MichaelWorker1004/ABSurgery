import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IUserProfessionalStandingModel } from '../../models/professionalstanding/user-professional-standing.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class UserProfessionalStandingService {

    constructor(private apiService: ApiService) {}

 
        public retrieveUserProfessionalStanding_GetByUserId(apiVersion = '1.0'): Observable<IUserProfessionalStandingModel> {
            /**
            * Claims
            * SurgeonPortalClaims.SurgeonClaim
            */
            
            /**
            * Business Rules
            * Business rules for property: ExplanationOfNonPrivileges
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            */
            
            /**
            * Required Parameters
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_user_professional_standing_byuserid]
            */
            
            
            return this.apiService.get<IUserProfessionalStandingModel>(`api/professional-standing/by-userid?api-version=${apiVersion}`);
        }
 
        public createUserProfessionalStanding(model: IUserProfessionalStandingModel, 
            apiVersion = '1.0'): Observable<IUserProfessionalStandingModel> {
            /**
            * Claims
            * SurgeonPortalClaims.SurgeonClaim
            */
            
            /**
            * Business Rules
            * Business rules for property: ExplanationOfNonPrivileges
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            */
            
            /**
            * Required Parameters
            * explanationOfNonPrivileges:String
            * explanationOfNonClinicalActivities:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [ins_user_professional_standing]
            */
            
            
            return this.apiService.post<IUserProfessionalStandingModel>(`api/professional-standing?api-version=${apiVersion}`, 
                model);
        }
 
        public updateUserProfessionalStanding(model: IUserProfessionalStandingModel,
        apiVersion = '1.0') : Observable<IUserProfessionalStandingModel> {
            /**
            * Claims
            * SurgeonPortalClaims.SurgeonClaim
            */
            
            /**
            * Business Rules
            * Business rules for property: ExplanationOfNonPrivileges
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            */
            
            /**
            * Required Parameters
            * explanationOfNonPrivileges:String
            * explanationOfNonClinicalActivities:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [update_user_professional_standing_byuserid]
            */
            
            
            
            return this.apiService.put<IUserProfessionalStandingModel>(`api/professional-standing?api-version=${apiVersion}`,
            model);
        }


}
