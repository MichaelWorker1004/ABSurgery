import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IOutcomeRegistryModel } from '../../models/continuouscertification/outcome-registry.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class OutcomeRegistriesService {

    constructor(private apiService: ApiService) {}

 
        public retrieveOutcomeRegistry_GetByUserId(apiVersion = '1.0'): Observable<IOutcomeRegistryModel> {
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
            * [get_outcome_registries]
            */
            
            
            return this.apiService.get<IOutcomeRegistryModel>(`api/continuous-certification/outcome-registries?api-version=${apiVersion}`);
        }
 
        public createOutcomeRegistry(model: IOutcomeRegistryModel, 
            apiVersion = '1.0'): Observable<IOutcomeRegistryModel> {
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
            * surgeonSpecificRegistry:String
            * registryComments:String
            * registeredWithACHQC:Boolean
            * registeredWithCESQIP:Boolean
            * registeredWithMBSAQIP:Boolean
            * registeredWithABA:Boolean
            * registeredWithASBS:Boolean
            * registeredWithMSQC:Boolean
            * registeredWithABMS:Boolean
            * registeredWithNCDB:Boolean
            * registeredWithRQRS:Boolean
            * registeredWithNSQIP:Boolean
            * registeredWithNTDB:Boolean
            * registeredWithSTS:Boolean
            * registeredWithTQIP:Boolean
            * registeredWithUNOS:Boolean
            * registeredWithNCDR:Boolean
            * registeredWithSVS:Boolean
            * registeredWithELSO:Boolean
            * registeredWithSSR:Boolean
            * userConfirmed:Boolean
            * userConfirmedDateUtc:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [insert_outcome_registries]
            */
            
            
            return this.apiService.post<IOutcomeRegistryModel>(`api/continuous-certification/outcome-registries?api-version=${apiVersion}`, 
                model);
        }
 
        public updateOutcomeRegistry(model: IOutcomeRegistryModel,
        apiVersion = '1.0') : Observable<IOutcomeRegistryModel> {
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
            * surgeonSpecificRegistry:String
            * registryComments:String
            * registeredWithACHQC:Boolean
            * registeredWithCESQIP:Boolean
            * registeredWithMBSAQIP:Boolean
            * registeredWithABA:Boolean
            * registeredWithASBS:Boolean
            * registeredWithMSQC:Boolean
            * registeredWithABMS:Boolean
            * registeredWithNCDB:Boolean
            * registeredWithRQRS:Boolean
            * registeredWithNSQIP:Boolean
            * registeredWithNTDB:Boolean
            * registeredWithSTS:Boolean
            * registeredWithTQIP:Boolean
            * registeredWithUNOS:Boolean
            * registeredWithNCDR:Boolean
            * registeredWithSVS:Boolean
            * registeredWithELSO:Boolean
            * registeredWithSSR:Boolean
            * userConfirmed:Boolean
            * userConfirmedDateUtc:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [update_outcome_registries]
            */
            
            
            
            return this.apiService.put<IOutcomeRegistryModel>(`api/continuous-certification/outcome-registries?api-version=${apiVersion}`,
            model);
        }


}
