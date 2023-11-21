import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IAttestationStatusReadOnlyModel } from '../../models/continuouscertification/attestation-status-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class AttestationService {

    constructor(private apiService: ApiService) {}

 
        public retrieveAttestationStatusReadOnly_GetByUserId(apiVersion = '1.0'): Observable<IAttestationStatusReadOnlyModel> {
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
            * [get_cca_attestation_status_byuserid]
            */
            
            
            return this.apiService.get<IAttestationStatusReadOnlyModel>(`api/attestation/status?api-version=${apiVersion}`);
        }


}
