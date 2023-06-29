import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IRosterReadOnlyModel } from '../../models/scoring/roster-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class RostersService {
    private readonly baseEndpoint = 'api/exam-header/{examHeaderId}/rosters';

    constructor(private apiService: ApiService) {}

 
        public retrieveRosterReadOnly_GetByExaminationHeaderId(examHeaderId: number,
        apiVersion = '1.0'): Observable<IRosterReadOnlyModel[]> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * examHeaderId:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_examination_scores]
            */
            
            
            return this.apiService.get<IRosterReadOnlyModel[]>(`${this.baseEndpoint}/roster-schedule?api-version=${apiVersion}&examHeaderId=${examHeaderId}`);
        }


}
