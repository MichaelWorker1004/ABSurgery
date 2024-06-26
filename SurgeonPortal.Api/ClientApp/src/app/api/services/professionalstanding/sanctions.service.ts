import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ISanctionsModel } from '../../models/professionalstanding/sanctions.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class SanctionsService {

    constructor(private apiService: ApiService) {}

 
        public retrieveSanctions_GetByUserId(apiVersion = '1.0'): Observable<ISanctionsModel> {
            /**
            * Claims
            * SurgeonPortalClaims.SurgeonClaim
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_user_sanctions_byuserid]
            */
            
            
            return this.apiService.get<ISanctionsModel>(`api/professional-standing/sanctions/by-userid?api-version=${apiVersion}`);
        }
 
        public createSanctions(model: ISanctionsModel, 
            apiVersion = '1.0'): Observable<ISanctionsModel> {
            /**
            * Claims
            * SurgeonPortalClaims.SurgeonClaim
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * hadDrugAlchoholTreatment:Boolean
            * hadHospitalPrivilegesDenied:Boolean
            * hadLicenseRestricted:Boolean
            * hadHospitalPrivilegesRestricted:Boolean
            * hadFelonyConviction:Boolean
            * hadCensure:Boolean
            * explanation:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [ins_user_sanctions]
            */
            
            
            return this.apiService.post<ISanctionsModel>(`api/professional-standing/sanctions?api-version=${apiVersion}`, 
                model);
        }
 
        public updateSanctions(model: ISanctionsModel,
        apiVersion = '1.0') : Observable<ISanctionsModel> {
            /**
            * Claims
            * SurgeonPortalClaims.SurgeonClaim
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * hadDrugAlchoholTreatment:Boolean
            * hadHospitalPrivilegesDenied:Boolean
            * hadLicenseRestricted:Boolean
            * hadHospitalPrivilegesRestricted:Boolean
            * hadFelonyConviction:Boolean
            * hadCensure:Boolean
            * explanation:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [update_user_sanctions_byuserid]
            */
            
            
            
            return this.apiService.put<ISanctionsModel>(`api/professional-standing/sanctions?api-version=${apiVersion}`,
            model);
        }


}
