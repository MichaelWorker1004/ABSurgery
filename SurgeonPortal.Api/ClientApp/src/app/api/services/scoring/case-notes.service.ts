import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICaseCommentModel } from '../../models/scoring/case-comment.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class CaseNotesService {

    constructor(private apiService: ApiService) {}

 
        public retrieveCaseComment_GetById(id: number,
        apiVersion = '1.0'): Observable<ICaseCommentModel> {
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
            * [get_case_comments_byid]
            */
            
            
            return this.apiService.get<ICaseCommentModel>(`api/case-comments/by-id?api-version=${apiVersion}&id=${id}`);
        }
 
        public createCaseComment(model: ICaseCommentModel, 
            apiVersion = '1.0'): Observable<ICaseCommentModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * caseContentId:Number
            * comments:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [ins_user_case_comments]
            */
            
            
            return this.apiService.post<ICaseCommentModel>(`api/case-comments?api-version=${apiVersion}`, 
                model);
        }
 
        public updateCaseComment(id: number,
        model: ICaseCommentModel,
        apiVersion = '1.0') : Observable<ICaseCommentModel> {
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
            * comments:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [update_case_comments]
            */
            
            
            
            return this.apiService.put<ICaseCommentModel>(`api/case-comments?api-version=${apiVersion}&id=${id}`,
            model);
        }


}
