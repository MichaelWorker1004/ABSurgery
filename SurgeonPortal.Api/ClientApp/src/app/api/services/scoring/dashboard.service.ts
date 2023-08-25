import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IDashboardRosterReadOnlyModel } from '../../models/scoring/dashboard-roster-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class DashboardService {

    constructor(private apiService: ApiService) {}

 
        public retrieveDashboardRosterReadOnly_GetByUserId(examDate: string,
        apiVersion = '1.0'): Observable<IDashboardRosterReadOnlyModel[]> {
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
            * examDate:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_examinerschedule_byuserid]
            */
            
            
            return this.apiService.get<IDashboardRosterReadOnlyModel[]>(`api/scoring/dashboard/dashboard-roster?api-version=${apiVersion}&examDate=${examDate}`);
        }


}
