import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IExamHistoryReadOnlyModel } from '../../models/examinations/exam-history-read-only.model';
import { IExamOverviewReadOnlyModel } from '../../models/examinations/exam-overview-read-only.model';
import { IExamTitleReadOnlyModel } from '../../models/examinations/exam-title-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class ExaminationsService {

    constructor(private apiService: ApiService) {}

 
        public retrieveExamHistoryReadOnly_GetByUserId(apiVersion = '1.0'): Observable<IExamHistoryReadOnlyModel[]> {
            /**
            * Claims
            * SurgeonPortalClaims.SurgeonClaim
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
 
        public retrieveExamOverviewReadOnly_GetAll(apiVersion = '1.0'): Observable<IExamOverviewReadOnlyModel[]> {
            /**
            * Claims
            * SurgeonPortalClaims.SurgeonClaim
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

        public retrieveExamTitleReadOnly_GetByExamId(examId: number,
        apiVersion = '1.0'): Observable<IExamTitleReadOnlyModel> {
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
            * examId:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_exam_title_byExamId]
            */
            
            
            return this.apiService.get<IExamTitleReadOnlyModel>(`api/examinations/title?api-version=${apiVersion}&examId=${examId}`);
        }


}
