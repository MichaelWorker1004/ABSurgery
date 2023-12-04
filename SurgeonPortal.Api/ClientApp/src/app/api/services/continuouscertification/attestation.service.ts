import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IAttestationReadOnlyModel } from '../../models/continuouscertification/attestation-read-only.model';
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


}
