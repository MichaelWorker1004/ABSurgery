import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from 'ytg-angular';
import { IExamFeeTransactionModel } from '../../models/billing/exam-fee-transaction.mode';

@Injectable({
  providedIn: 'root',
})
export class ExamFeeTransactionService {

    constructor(private apiService: ApiService) {}

 
        public retrieveExamFeeTransactionToken(model: IExamFeeTransactionModel ,apiVersion = '1.0'): Observable<IExamFeeTransactionModel> {
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
            * [UNKNOWN]
            */
            
            
            return this.apiService.post<IExamFeeTransactionModel>(`api/billing/transaction/token?api-version=${apiVersion}`, model);
        }


}
