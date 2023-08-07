import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IExamOverviewReadOnlyModel } from '../../models/examinations/exam-overview-read-only.model';
import { ApiService } from 'ytg-angular';
import { IExamHistoryReadOnlyModel } from '../../models/examinations/exam-history-read-only.model';

@Injectable({
  providedIn: 'root',
})
export class ExaminationsService {

    constructor(private apiService: ApiService) {}

 
        public retrieveExamOverviewReadOnly_GetAll(apiVersion = '1.0'): Observable<IExamOverviewReadOnlyModel[]> {
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
            * [get_exam_overview]
            */
            
            
            return this.apiService.get<IExamOverviewReadOnlyModel[]>(`api/examinations/overview?api-version=${apiVersion}`);
        }

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
                
                
                return this.apiService.get<IExamHistoryReadOnlyModel[]>(`api/examinations/history?api-version=${apiVersion}`);
            }


}
