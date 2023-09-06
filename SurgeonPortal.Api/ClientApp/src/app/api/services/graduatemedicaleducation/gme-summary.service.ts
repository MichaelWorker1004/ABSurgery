import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IGmeSummaryReadOnlyModel } from '../../models/graduatemedicaleducation/gme-summary-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class GmeSummaryService {

    constructor(private apiService: ApiService) {}

 
        public retrieveGmeSummaryReadOnly_GetByUserId(apiVersion = '1.0'): Observable<IGmeSummaryReadOnlyModel[]> {
            /**
            * Claims
            * SurgeonPortalClaims.TraineeClaim
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
            * [get_gmesummary_byuserid]
            */
            
            
            return this.apiService.get<IGmeSummaryReadOnlyModel[]>(`api/gme-summary/by-userid?api-version=${apiVersion}`);
        }


}
