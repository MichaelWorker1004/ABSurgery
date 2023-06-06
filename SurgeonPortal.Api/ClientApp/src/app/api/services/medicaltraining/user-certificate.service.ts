import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IUserCertificateModel } from '../../models/medicaltraining/user-certificate.model';
import { IUserCertificateReadOnlyModel } from '../../models/medicaltraining/user-certificate-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class UserCertificateService {
    private readonly baseEndpoint = 'api/user-certificates';

    constructor(private apiService: ApiService) {}

 
        public deleteUserCertificate(id: number,
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
            * certificateId:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [del_usercertificate]
            */
            
            
            return this.apiService.delete<IUserCertificateModel>(`${this.baseEndpoint}?id=${id}&api-version=${apiVersion}`);
        }
 
        public retrieveUserCertificate_GetById(id: number,
        apiVersion = '1.0'): Observable<IUserCertificateModel> {
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
            * [get_usercertificates_byid]
            */
            
            
            return this.apiService.get<IUserCertificateModel>(`${this.baseEndpoint}/by-id?api-version=${apiVersion}&id=${id}`);
        }
 
        public createUserCertificate(model: FormData, 
            apiVersion = '1.0'): Observable<IUserCertificateModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * certificateId:Number
            * documentId:Number
            * certificateTypeId:Number
            * issueDate:String
            * certificateNumber:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [insert_usercertificates]
            */
            
            
            return this.apiService.post<IUserCertificateModel>(`${this.baseEndpoint}?api-version=${apiVersion}`, model);
        }
 
        public retrieveUserCertificateReadOnly_GetByUserId(apiVersion = '1.0'): Observable<IUserCertificateReadOnlyModel[]> {
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
            * [get_usercertificates_byuserid]
            */
            
            
            return this.apiService.get<IUserCertificateReadOnlyModel[]>(`${this.baseEndpoint}/by-userid?api-version=${apiVersion}`);
        }


}
