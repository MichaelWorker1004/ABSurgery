import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICaseFeedbackModel } from '../../models/scoring/case-feedback.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class CaseFeedbackService {

    constructor(private apiService: ApiService) {}

 
        public deleteCaseFeedback(id: number,
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
            * id:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [delete_case_feedback_byid]
            */
            
            
            return this.apiService.delete<ICaseFeedbackModel>(`api/cases/feedback?api-version=${apiVersion}&id=${id}`);
        }
 
        public retrieveCaseFeedback_GetById(id: number,
        apiVersion = '1.0'): Observable<ICaseFeedbackModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * id:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_case_feedback_byid]
            */
            
            
            return this.apiService.get<ICaseFeedbackModel>(`api/cases/feedback/by-id?api-version=${apiVersion}&id=${id}`);
        }
 
        public createCaseFeedback(model: ICaseFeedbackModel, 
            apiVersion = '1.0'): Observable<ICaseFeedbackModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * userId:Number
            * caseContentId:Number
            * feedback:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [ins_case_feedback]
            */
            
            
            return this.apiService.post<ICaseFeedbackModel>(`api/cases/feedback?api-version=${apiVersion}`, 
                model);
        }
 
        public updateCaseFeedback(id: number,
        model: ICaseFeedbackModel,
        apiVersion = '1.0') : Observable<ICaseFeedbackModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * id:Number
            * caseContentId:Number
            * feedback:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [update_case_feedback]
            */
            
            
            
            return this.apiService.put<ICaseFeedbackModel>(`api/cases/feedback?api-version=${apiVersion}&id=${id}`,
            model);
        }


}
