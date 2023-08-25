import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IQualifyingExamReadOnlyModel } from '../../models/examinations/qualifying-exam-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class QualifyingExamService {

    constructor(private apiService: ApiService) {}

 
        public retrieveQualifyingExamReadOnly_Get(apiVersion = '1.0'): Observable<IQualifyingExamReadOnlyModel> {
            /**
            * Claims
            * SurgeonPortalClaims.TraineeClaim
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
            * [get_current_qualifying_exam]
            */
            
            
            return this.apiService.get<IQualifyingExamReadOnlyModel>(`api/qualifying-exam?api-version=${apiVersion}`);
        }


}
