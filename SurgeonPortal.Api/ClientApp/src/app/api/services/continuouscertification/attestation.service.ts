import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IAttestationReadOnlyModel, IAttestationSubmitModel } from '../../models/continuouscertification/attestation-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class AttestationService {

    constructor(private apiService: ApiService) {}

 
        public retrieveAttestationReadOnly_GetAllByUserId(apiVersion = '1.0'): Observable<IAttestationReadOnlyModel[]> {
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
            * [get_cca_attestation_items_byuserid]
            */
            
            
            return this.apiService.get<IAttestationReadOnlyModel[]>(`api/continuous-certification/attestation/by-user-id?api-version=${apiVersion}`);
        }

        public submitAttestations(model: IAttestationSubmitModel, apiVersion = '1.0'): Observable<any> {
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
            * SigReceive
            * CertnoticeReceive
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [submit_cca_attestation]
            */
            
            
            return this.apiService.post<IAttestationSubmitModel>(`api/continuous-certification/attestation/submit?api-version=${apiVersion}`, model);  
        }


}
