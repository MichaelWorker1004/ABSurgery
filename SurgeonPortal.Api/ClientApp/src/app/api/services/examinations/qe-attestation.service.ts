import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IQeAttestationReadOnlyModel } from '../../models/examinations/qe-attestation-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class QeAttestationService {

    constructor(private apiService: ApiService) {}

 
        public retrieveQeAttestationReadOnly_GetByExamId(examId: number,
        apiVersion = '1.0'): Observable<IQeAttestationReadOnlyModel[]> {
            /**
            * Claims
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
            * [get_qe_attestation_items_by_userId]
            */
            
            
            return this.apiService.get<IQeAttestationReadOnlyModel[]>(`api/examinations/qe-attestation/by-examid?api-version=${apiVersion}&examId=${examId}`);
        }


}
