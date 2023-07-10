import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICaseDetailReadOnlyModel } from '../../models/scoring/case-detail-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class CaseContentsService {

    constructor(private apiService: ApiService) {}

 
        public retrieveCaseDetailReadOnly_GetByCaseHeaderId(caseHeaderId: number,
        apiVersion = '1.0'): Observable<ICaseDetailReadOnlyModel[]> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * caseHeaderId:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_case_details_by_id]
            */
            
            
            return this.apiService.get<ICaseDetailReadOnlyModel[]>(`api/case-contents/by-case-header-id?api-version=${apiVersion}&caseHeaderId=${caseHeaderId}`);
        }


}
