import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IOtherCertificationsModel } from '../../models/medicaltraining/other-certifications.model';
import { IOtherCertificationsReadOnlyModel } from '../../models/medicaltraining/other-certifications-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class OtherCertificationsService {

    constructor(private apiService: ApiService) {}

 
        public deleteOtherCertifications(id: number,
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
            * id:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [del_user_certificates_other_byid]
            */
            
            
            return this.apiService.delete<IOtherCertificationsModel>(`api/medical-training/other-certifications?api-version=${apiVersion}&id=${id}`);
        }
 
        public retrieveOtherCertifications_GetById(id: number,
        apiVersion = '1.0'): Observable<IOtherCertificationsModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * id:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_user_certificates_other_byid]
            */
            
            
            return this.apiService.get<IOtherCertificationsModel>(`api/medical-training/other-certifications/by-id?api-version=${apiVersion}&id=${id}`);
        }
 
        public createOtherCertifications(model: IOtherCertificationsModel, 
            apiVersion = '1.0'): Observable<IOtherCertificationsModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * certificateTypeId:Number
            * issueDate:String
            * certificateNumber:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [ins_user_certificates_other]
            */
            
            
            return this.apiService.post<IOtherCertificationsModel>(`api/medical-training/other-certifications?api-version=${apiVersion}`, 
                model);
        }
 
        public updateOtherCertifications(id: number,
        model: IOtherCertificationsModel,
        apiVersion = '1.0') : Observable<IOtherCertificationsModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * id:Number
            * certificateTypeId:Number
            * issueDate:String
            * certificateNumber:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [update_user_certificates_other_byid]
            */
            
            
            
            return this.apiService.put<IOtherCertificationsModel>(`api/medical-training/other-certifications?api-version=${apiVersion}&id=${id}`,
            model);
        }
 
        public retrieveOtherCertificationsReadOnly_GetByUserId(apiVersion = '1.0'): Observable<IOtherCertificationsReadOnlyModel[]> {
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
            * [get_user_certificates_other_byuserid]
            */
            
            
            return this.apiService.get<IOtherCertificationsReadOnlyModel[]>(`api/medical-training/other-certifications/by-userid?api-version=${apiVersion}`);
        }


}
