import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IExamFeeReadOnlyModel } from '../../models/billing/exam-fee-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class ExamFeeService {

    constructor(private apiService: ApiService) {}

 
        public retrieveExamFeeReadOnly_GetByUserId(apiVersion = '1.0'): Observable<IExamFeeReadOnlyModel[]> {
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
            * [Get_Exam_Fees_byUserId]
            */
            
            
            return this.apiService.get<IExamFeeReadOnlyModel[]>(`api/billing/exam-fee/by-userid?api-version=${apiVersion}`);
        }


}
