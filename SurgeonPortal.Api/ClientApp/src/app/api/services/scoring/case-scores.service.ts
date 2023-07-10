import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICaseScoreModel } from '../../models/scoring/case-score.model';
import { ICaseScoreReadOnlyModel } from '../../models/scoring/case-score-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class CaseScoresService {

    constructor(private apiService: ApiService) {}

 
        public deleteCaseScore(examScoringId: number,
        apiVersion = '1.0'): Observable<any> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * examScoringId:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [delete_examinerscore_byid]
            */
            
            
            return this.apiService.delete<ICaseScoreModel>(`api/cases/scores?api-version=${apiVersion}&examScoringId=${examScoringId}`);
        }
 
        public retrieveCaseScore_GetById(examScoringId: number,
        apiVersion = '1.0'): Observable<ICaseScoreModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * examScoringId:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_examcasescore_byid]
            */
            
            
            return this.apiService.get<ICaseScoreModel>(`api/cases/scores?api-version=${apiVersion}&examScoringId=${examScoringId}`);
        }
 
        public createCaseScore(model: ICaseScoreModel, 
            apiVersion = '1.0'): Observable<ICaseScoreModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * examineeUserId:Number
            * examCaseId:Number
            * score:Number
            * remarks:String
            * criticalFail:Boolean
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [upsert_examinerscore]
            */
            
            
            return this.apiService.post<ICaseScoreModel>(`api/cases/scores?api-version=${apiVersion}`, 
                model);
        }
 
        public updateCaseScore(examScoringId: number,
        model: ICaseScoreModel,
        apiVersion = '1.0') : Observable<ICaseScoreModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * examineeUserId:Number
            * examCaseId:Number
            * score:Number
            * remarks:String
            * criticalFail:Boolean
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [upsert_examinerscore]
            */
            
            
            
            return this.apiService.put<ICaseScoreModel>(`api/cases/scores?api-version=${apiVersion}&examScoringId=${examScoringId}`,
            model);
        }
 
        public retrieveCaseScoreReadOnly_GetByExamScheduleId(examScheduleId: number,
        apiVersion = '1.0'): Observable<ICaseScoreReadOnlyModel[]> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * examScheduleId:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_examinerscores_byexamscheduleId]
            */
            
            
            return this.apiService.get<ICaseScoreReadOnlyModel[]>(`api/cases/scores/all-by-examschedule-id?api-version=${apiVersion}&examScheduleId=${examScheduleId}`);
        }


}
