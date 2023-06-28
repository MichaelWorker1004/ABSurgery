import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IExamScoreReadOnlyModel } from '../../models/scoring/exam-score-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class ExamScoresService {
    private readonly baseEndpoint = 'api/exam-scores';

    constructor(private apiService: ApiService) {}

 
        public retrieveExamScoreReadOnly_GetById(apiVersion = '1.0'): Observable<IExamScoreReadOnlyModel[]> {
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
            * [get_examination_scores]
            */
            
            
            return this.apiService.get<IExamScoreReadOnlyModel[]>(`${this.baseEndpoint}?api-version=${apiVersion}`);
        }


}
