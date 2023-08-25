import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IExamineeReadOnlyModel } from '../../../models/scoring/ce/examinee-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class SessionService {

    constructor(private apiService: ApiService) {}

 
        public retrieveExamineeReadOnly_GetById(examScheduleId: number,
        apiVersion = '1.0'): Observable<IExamineeReadOnlyModel> {
            /**
            * Claims
            * SurgeonPortalClaims.ExaminerClaim
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * examScheduleId:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_examinee_session_byid]
            */
            
            
            return this.apiService.get<IExamineeReadOnlyModel>(`api/scoring/ce/session/examinee?api-version=${apiVersion}&examScheduleId=${examScheduleId}`);
        }


}
