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


}
