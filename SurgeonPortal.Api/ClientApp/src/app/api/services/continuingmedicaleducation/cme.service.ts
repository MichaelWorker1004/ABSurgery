import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICmeAdjustmentReadOnlyModel } from '../../models/continuingmedicaleducation/cme-adjustment-read-only.model';
import { ICmeCreditReadOnlyModel } from '../../models/continuingmedicaleducation/cme-credit-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class CmeService {

    constructor(private apiService: ApiService) {}

 
        public retrieveCmeAdjustmentReadOnly_GetByUserId(apiVersion = '1.0'): Observable<ICmeAdjustmentReadOnlyModel[]> {
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
            * [get_usercme_waivers_byuserid]
            */
            
            
            return this.apiService.get<ICmeAdjustmentReadOnlyModel[]>(`api/cme/adjustments?api-version=${apiVersion}`);
        }
 
        public retrieveCmeCreditReadOnly_GetById(cmeId: number,
        apiVersion = '1.0'): Observable<ICmeCreditReadOnlyModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * cmeId:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_usercme_byid]
            */
            
            
            return this.apiService.get<ICmeCreditReadOnlyModel>(`api/cme/by-id?api-version=${apiVersion}&cmeId=${cmeId}`);
        }
 
        public retrieveCmeCreditReadOnly_GetByUserId(apiVersion = '1.0'): Observable<ICmeCreditReadOnlyModel[]> {
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
            * [get_usercme_byuserid]
            */
            
            
            return this.apiService.get<ICmeCreditReadOnlyModel[]>(`api/cme/itemized-cme?api-version=${apiVersion}`);
        }


}
