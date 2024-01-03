import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IApplicationFeeReadOnlyModel } from '../../models/billing/application-fee-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class ApplicationFeeService {

    constructor(private apiService: ApiService) {}

 
        public retrieveApplicationFeeReadOnly_GetByExamId(examId: number,
        apiVersion = '1.0'): Observable<IApplicationFeeReadOnlyModel> {
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
            * examId:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_application_fee_by_examId]
            */
            
            
            return this.apiService.get<IApplicationFeeReadOnlyModel>(`api/billing/application-fee/by-examid?api-version=${apiVersion}&examId=${examId}`);
        }


}
