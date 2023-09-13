import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IConflictReadOnlyModel } from '../../models/examiners/conflict-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class ConflictService {

    constructor(private apiService: ApiService) {}

 
        public retrieveConflictReadOnly_GetByExamHeaderId(examHeaderId: number,
        apiVersion = '1.0'): Observable<IConflictReadOnlyModel> {
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
            * examHeaderId:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_examiner_conflicts]
            */
            
            
            return this.apiService.get<IConflictReadOnlyModel>(`api/examiners/conflict/by-exam-header-id?api-version=${apiVersion}&examHeaderId=${examHeaderId}`);
        }


}
