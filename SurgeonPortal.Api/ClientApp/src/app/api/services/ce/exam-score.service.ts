import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IExamScoreModel } from '../../models/ce/exam-score.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class ExamScoreService {

    constructor(private apiService: ApiService) {}

 
        public retrieveExamScore_GetById(examScheduleScoreId: number,
        apiVersion = '1.0'): Observable<IExamScoreModel> {
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
            * examScheduleScoreId:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_exam_schedule_scores]
            */
            
            
            return this.apiService.get<IExamScoreModel>(`api/exam-scores/by-id?api-version=${apiVersion}&examScheduleScoreId=${examScheduleScoreId}`);
        }
 
        public createExamScore(model: IExamScoreModel, 
            apiVersion = '1.0'): Observable<IExamScoreModel> {
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
            * examScheduleId:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [ins_exam_schedule_scores]
            */
            
            
            return this.apiService.post<IExamScoreModel>(`api/exam-scores?api-version=${apiVersion}`, 
                model);
        }
 
        public updateExamScore(examScheduleScoreId: number,
        model: IExamScoreModel,
        apiVersion = '1.0') : Observable<IExamScoreModel> {
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
            * examScheduleScoreId:Number
            * examinerScore:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [update_exam_schedule_scores]
            */
            
            
            
            return this.apiService.put<IExamScoreModel>(`api/exam-scores?api-version=${apiVersion}&examScheduleScoreId=${examScheduleScoreId}`,
            model);
        }

        public resetCaseCommentsData(model?: IExamScoreModel,
            apiVersion = '1.0') : Observable<IExamScoreModel> {
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
                * [dev_reset_case_comments_by_ExaminerId]
                */
                
                
                
                return this.apiService.post<IExamScoreModel>(`api/dev/reset/case-comments?api-version=${apiVersion}`,
                { model: null});
        }

        public resetExamScoring(model?: IExamScoreModel,
            apiVersion = '1.0') : Observable<IExamScoreModel> {
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
                * [dev_reset_scoring_by_ExaminerId]
                */
                
                
                
                return this.apiService.post<IExamScoreModel>(`api/dev/reset/exam-scores?api-version=${apiVersion}`,
                {model: null});
        }


}
