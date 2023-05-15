import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IOutcomeRegistryModel } from '../../models/continuouscertification/outcome-registry.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class OutcomeRegistriesService {
    private readonly baseEndpoint = 'api/continuous-certification/outcome-registries';

    constructor(private apiService: ApiService) {}

 
        public retrieveOutcomeRegistry_GetByUserId(userId: number,
        apiVersion = '1.0'): Observable<IOutcomeRegistryModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * userId:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_outcomeregistry_getbyuserid]
            */


            return this.apiService.get<IOutcomeRegistryModel>(`${this.baseEndpoint}/?api-version=${apiVersion}&userId=${userId}`);
        }
 
        public createOutcomeRegistry(model: IOutcomeRegistryModel, 
            apiVersion = '1.0'): Observable<IOutcomeRegistryModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * userId:Number
            * surgeonSpecificRegistry:Boolean
            * registryComments:String
            * registeredWithACHQC:Boolean
            * registeredWithCESQIP:Boolean
            * registeredWithMBSAQIP:Boolean
            * registeredWithABA:Boolean
            * registeredWithASBS:Boolean
            * registeredWithStatewideCollaboratives:Boolean
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
            * userConfirmed:Boolean
            * userConfirmedDateUtc:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [ins_outcomeregistry_getbyuserid]
            */
            
            
            return this.apiService.post<IOutcomeRegistryModel>(`${this.baseEndpoint}?api-version=${apiVersion}`, 
                model);
        }
 
        public updateOutcomeRegistry(userId: number,
        model: IOutcomeRegistryModel,
        apiVersion = '1.0') : Observable<IOutcomeRegistryModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * surgeonSpecificRegistry:Boolean
            * registryComments:String
            * registeredWithACHQC:Boolean
            * registeredWithCESQIP:Boolean
            * registeredWithMBSAQIP:Boolean
            * registeredWithABA:Boolean
            * registeredWithASBS:Boolean
            * registeredWithStatewideCollaboratives:Boolean
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
            * userConfirmed:Boolean
            * userConfirmedDateUtc:String
            * userId:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [update_outcomeregistry_getbyuserid]
            */
            
            
            
            return this.apiService.put<IOutcomeRegistryModel>(`${this.baseEndpoint}?api-version=${apiVersion}&userId=${userId}`,
            model);
        }


}
