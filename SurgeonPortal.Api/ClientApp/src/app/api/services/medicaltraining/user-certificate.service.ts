/* eslint-disable prettier/prettier */
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IUserCertificateModel } from '../../models/medicaltraining/user-certificate.model';
import { IUserCertificateReadOnlyModel } from '../../models/medicaltraining/user-certificate-read-only.model';
import { ApiService } from 'ytg-angular';
import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class UserCertificateService {

  constructor(private apiService: ApiService, private httpClient: HttpClient) {}

 
        public deleteUserCertificate(certificateId: number,
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
            
            
            return this.apiService.delete<IUserCertificateModel>(`api/user-certificates?api-version=${apiVersion}&certificateId=${certificateId}`);
        }
 
        public retrieveUserCertificate_GetById(certificateId: number,
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
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_usercertificates_byid]
            */
            
            
            return this.apiService.get<IUserCertificateModel>(`api/user-certificates/by-id?api-version=${apiVersion}&certificateId=${certificateId}`);
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

          return this.apiService.post<IUserCertificateModel>(`api/user-certificates?api-version=${apiVersion}`,
            model);
        }
 
        public retrieveUserCertificateReadOnly_GetByUserAndType(certificateTypeId: number,
        apiVersion = '1.0'): Observable<IUserCertificateReadOnlyModel[]> {
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
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_user_certs_by_certId]
            */
            
            
            return this.apiService.get<IUserCertificateReadOnlyModel[]>(`api/user-certificates/by-userid-and-typeid?api-version=${apiVersion}&certificateTypeId=${certificateTypeId}`);
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
            
            
            return this.apiService.get<IUserCertificateReadOnlyModel[]>(`api/user-certificates/by-userid?api-version=${apiVersion}`);
        }


}
