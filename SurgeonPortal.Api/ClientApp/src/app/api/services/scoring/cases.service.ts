import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICaseRosterReadOnlyModel } from '../../models/scoring/case-roster-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class CasesService {
    private readonly baseEndpoint = 'api/exam-headers/cases';

    constructor(private apiService: ApiService) {}

 
        public retrieveCaseRosterReadOnly_GetByScheduleId(scheduleId1: number,
        scheduleId2: number,
        apiVersion = '1.0'): Observable<ICaseRosterReadOnlyModel[]> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * scheduleId1:Number
            * scheduleId2:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_toc_case_list]
            */
            
            
            return this.apiService.get<ICaseRosterReadOnlyModel[]>(`${this.baseEndpoint}/case-roster?api-version=${apiVersion}&scheduleId1=${scheduleId1}&scheduleId2=${scheduleId2}`);
        }


}
