import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IQeExamEligibilityReadOnlyModel } from '../../models/examinations/qe-exam-eligibility-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class QeExamEligibilityService {

    constructor(private apiService: ApiService) {}

 
        public retrieveQeExamEligibilityReadOnly_GetByUserId(apiVersion = '1.0'): Observable<IQeExamEligibilityReadOnlyModel[]> {
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
            * [get_qe_eligibility_by_userId]
            */
            
            
            return this.apiService.get<IQeExamEligibilityReadOnlyModel[]>(`api/examinations/qe-exam-eligibility/by-userid?api-version=${apiVersion}`);
        }


}
