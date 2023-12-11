import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICertificationStatusReadOnlyModel } from '../../models/user/certification-status-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class CertificationStatusService {

    constructor(private apiService: ApiService) {}

 
        public retrieveCertificationStatusReadOnly_GetByUserId(apiVersion = '1.0'): Observable<ICertificationStatusReadOnlyModel> {
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
            * [get_user_certification_current_status_byuserid]
            */
            
            
            return this.apiService.get<ICertificationStatusReadOnlyModel>(`api/user/certification-status?api-version=${apiVersion}`);
        }


}
