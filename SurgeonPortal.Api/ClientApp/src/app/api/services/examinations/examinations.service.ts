import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IExamHistoryReadOnlyModel } from '../../models/examinations/exam-history-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class ExaminationsService {
    private readonly baseEndpoint = 'api/examinations';

    constructor(private apiService: ApiService) {}

 
        public retrieveExamHistoryReadOnly_GetByUserId(apiVersion = '1.0'): Observable<IExamHistoryReadOnlyModel[]> {
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
            * [get_userexamhistory]
            */
            
            
            return this.apiService.get<IExamHistoryReadOnlyModel[]>(`${this.baseEndpoint}/history?api-version=${apiVersion}`);
        }


}
