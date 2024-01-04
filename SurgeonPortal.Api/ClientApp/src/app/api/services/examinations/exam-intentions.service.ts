import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IExamIntentionsModel } from '../../models/examinations/exam-intentions.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class ExamIntentionsService {

    constructor(private apiService: ApiService) {}

 
        public retrieveExamIntentions_GetByExamId(examId: number,
        apiVersion = '1.0'): Observable<IExamIntentionsModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * examId:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_exam_intentions]
            */
            
            
            return this.apiService.get<IExamIntentionsModel>(`api/examinations/intentions/by-examid?api-version=${apiVersion}&examId=${examId}`);
        }
 
        public createExamIntentions(model: IExamIntentionsModel, 
            apiVersion = '1.0'): Observable<IExamIntentionsModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * examId:Number
            * intention:Boolean
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [ins_exam_intentions]
            */
            
            
            return this.apiService.post<IExamIntentionsModel>(`api/examinations/intentions?api-version=${apiVersion}`, 
                model);
        }


}
