import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IMedicalLicenseModel } from '../../models/professionalstanding/medical-license.model';
import { IMedicalLicenseReadOnlyModel } from '../../models/professionalstanding/medical-license-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class MedicalLicenseService {
    private readonly baseEndpoint = 'api/professional-standing/medical-license';

    constructor(private apiService: ApiService) {}

 
        public deleteMedicalLicense(licenseId: number,
        apiVersion = '1.0'): Observable<any> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * licenseId:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [del_userlicense]
            */
            
            
            return this.apiService.delete<IMedicalLicenseModel>(`${this.baseEndpoint}?api-version=${apiVersion}&licenseId=${licenseId}`);
        }
 
        public retrieveMedicalLicense_GetById(licenseId: number,
        apiVersion = '1.0'): Observable<IMedicalLicenseModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * licenseId:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_userlicenses_byid]
            */
            
            
            return this.apiService.get<IMedicalLicenseModel>(`${this.baseEndpoint}/by-id?api-version=${apiVersion}&licenseId=${licenseId}`);
        }
 
        public createMedicalLicense(model: IMedicalLicenseModel, 
            apiVersion = '1.0'): Observable<IMedicalLicenseModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * issuingStateId:String
            * licenseNumber:String
            * licenseTypeId:Number
            * reportingOrganization:String
            * issueDate:String
            * expireDate:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [insert_userlicenses]
            */
            
            
            return this.apiService.post<IMedicalLicenseModel>(`${this.baseEndpoint}?api-version=${apiVersion}`, 
                model);
        }
 
        public updateMedicalLicense(licenseId: number,
        model: IMedicalLicenseModel,
        apiVersion = '1.0') : Observable<IMedicalLicenseModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * licenseId:Number
            * issuingStateId:String
            * licenseNumber:String
            * licenseTypeId:Number
            * reportingOrganization:String
            * issueDate:String
            * expireDate:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [update_userlicenses]
            */
            
            
            
            return this.apiService.put<IMedicalLicenseModel>(`${this.baseEndpoint}?api-version=${apiVersion}&licenseId=${licenseId}`,
            model);
        }
 
        public retrieveMedicalLicenseReadOnly_GetByUserId(apiVersion = '1.0'): Observable<IMedicalLicenseReadOnlyModel[]> {
            /**
            * Claims
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
            * [get_userlicenses_byuserid]
            */
            
            
            return this.apiService.get<IMedicalLicenseReadOnlyModel[]>(`${this.baseEndpoint}/by-userid?api-version=${apiVersion}`);
        }


}
