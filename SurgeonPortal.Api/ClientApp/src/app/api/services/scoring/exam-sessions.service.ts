import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IActiveExamReadOnlyModel } from '../../models/scoring/active-exam-read-only.model';
import { IExamSessionReadOnlyModel } from '../../models/scoring/exam-session-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class ExamSessionsService {

    constructor(private apiService: ApiService) {}

 
        public retrieveActiveExamReadOnly_GetByExaminerUserId(currentDate: string,
        apiVersion = '1.0'): Observable<IActiveExamReadOnlyModel> {
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
            * currentDate:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_active_exam_byuserid]
            */
            
            
            return this.apiService.get<IActiveExamReadOnlyModel>(`api/exam-sessions/active?api-version=${apiVersion}&currentDate=${currentDate}`);
        }
 
        public retrieveExamSessionReadOnly_GetByUserId(examDate: string,
        apiVersion = '1.0'): Observable<IExamSessionReadOnlyModel[]> {
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
            * examDate:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_examinee_sessions]
            */
            
            
            return this.apiService.get<IExamSessionReadOnlyModel[]>(`api/exam-sessions/by-date?api-version=${apiVersion}&examDate=${examDate}`);
        }

        public skipExamSessionReadOnly_SkipByExamScheduleId(examScheduleId: number,
          apiVersion = '1.0'){
              /**
              * Claims
              */
              
              /**
              * Business Rules
              * No business rules exist for this model
              */
              
              /**
              * Required Parameters
              * examScheduleId:number
              * apiVersion
              */
              
              /**
              * Calls Sp(s)
              * [update_skip_exam]
              */
              
              
              return this.apiService.post<IExamSessionReadOnlyModel[]>(`api/exam-sessions/skip?api-version=${apiVersion}`, {examScheduleId});
          }


}
