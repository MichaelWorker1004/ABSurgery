import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

import { ICertificationReadOnlyModel } from '../../models/surgeons/certification-read-only.model';

/**
#############################################################################
WARNING GENERATED CODE DO NOT MODIFY - 

All client side API services are generated by the server side API 
developer when the API is created. Please do not make changes to this file

############################################################################
**/

@Injectable({
  providedIn: 'root',
})
export class CertificationsService {
    private readonly baseEndpoint = 'surgeons/certifications';

    constructor(private httpClient: HttpClient) {}

 
        public retrieveCertificationReadOnly_GetByAbsId(absId: string,
        apiVersion = '1.0'): Observable<ICertificationReadOnlyModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * absId:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_user_certifications]
            */
            
            
            return this.httpClient.get<ICertificationReadOnlyModel>(`${this.baseEndpoint}/?api-version=${apiVersion}&absId=${absId}`);
        }


}
