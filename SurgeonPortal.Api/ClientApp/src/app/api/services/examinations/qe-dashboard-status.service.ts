import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IQeDashboardStatusReadOnlyModel } from '../../models/examinations/qe-dashboard-status-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class QeDashboardStatusService {

    constructor(private apiService: ApiService) {}

 
        public retrieveQeDashboardStatusReadOnly_GetByExamId(examheaderId: number,
        apiVersion = '1.0'): Observable<IQeDashboardStatusReadOnlyModel[]> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * examheaderId:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_user_qe_all_status_byuserid]
            */
            
            
            return this.apiService.get<IQeDashboardStatusReadOnlyModel[]>(`api/examinations/qe-dashboard-status/by-examid?api-version=${apiVersion}&examheaderId=${examheaderId}`);
        }


}
