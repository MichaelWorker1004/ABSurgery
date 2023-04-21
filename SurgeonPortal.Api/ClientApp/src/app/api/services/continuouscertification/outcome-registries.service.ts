import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

import { IOutcomeRegistryModel } from '../../models/continuouscertification/outcome-registry.model';

/**
#############################################################################
WARNING GENERATED CODE DO NOT MODIFY - 

All client side API services are generated by the server side API 
developer when the API is created. Please do not make changes to this file

############################################################################
**/

@Injectable({
  providedIn: 'root',
})
export class OutcomeRegistriesService {
    private readonly baseEndpoint = 'v1/api/continuous-certification/outcome-registries';

    constructor(private httpClient: HttpClient) {}

 
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
            
            
            return this.httpClient.get<IOutcomeRegistryModel>(`${this.baseEndpoint}/?api-version=${apiVersion}&userId=${userId}`);
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
            
            
            return this.httpClient.post<IOutcomeRegistryModel>(`${this.baseEndpoint}/?api-version=${apiVersion}`, 
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
            
            
            
            return this.httpClient.put<IOutcomeRegistryModel>(`${this.baseEndpoint}?api-version=${apiVersion}&userId=${userId}`,
            model);
        }


}
