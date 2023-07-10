import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IExamSessionReadOnlyModel } from '../../models/scoring/exam-session-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class ExamSessionsService {

    constructor(private apiService: ApiService) {}

 
        public retrieveExamSessionReadOnly_GetByUserId(examDate: string,
        apiVersion = '1.0'): Observable<IExamSessionReadOnlyModel[]> {
            /**
            * Claims
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
            * [get_examinee_sessions]
            */
            
            
            return this.apiService.get<IExamSessionReadOnlyModel[]>(`api/exam-sessions/by-date?api-version=${apiVersion}&examDate=${examDate}`);
        }


}
