import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IDashboardStatusReadOnlyModel } from '../../models/continuouscertification/dashboard-status-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class DashboardStatusService {

    constructor(private apiService: ApiService) {}

 
        public retrieveDashboardStatusReadOnly_GetAllByUserId(apiVersion = '1.0'): Observable<IDashboardStatusReadOnlyModel[]> {
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
            * [get_all_status]
            */
            
            
            return this.apiService.get<IDashboardStatusReadOnlyModel[]>(`api/continuous-certification/dashboard-status/by-user-id?api-version=${apiVersion}`);
        }


}
