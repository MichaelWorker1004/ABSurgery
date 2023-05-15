import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

import { IAdvancedTrainingModel } from '../../models/medicaltraining/advanced-training.model';
import { IAdvancedTrainingReadOnlyModel } from '../../models/medicaltraining/advanced-training-read-only.model';

@Injectable({
  providedIn: 'root',
})
export class AdvancedTrainingService {
    private readonly baseEndpoint = 'api/advanced-training';

    constructor(private httpClient: HttpClient) {}

 
        public retrieveAdvancedTraining_GetByTrainingId(id: number,
        apiVersion = '1.0'): Observable<IAdvancedTrainingModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * Business rules for property: UserId
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: TrainingTypeId
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: StartDate
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: EndDate
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            */
            
            /**
            * Required Parameters
            * id:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_advanced_training_by_trainingid]
            */
            
            
            return this.httpClient.get<IAdvancedTrainingModel>(`${this.baseEndpoint}/by-trainingid?api-version=${apiVersion}&id=${id}`);
        }
 
        public createAdvancedTraining(model: IAdvancedTrainingModel, 
            apiVersion = '1.0'): Observable<IAdvancedTrainingModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * Business rules for property: UserId
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: TrainingTypeId
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: StartDate
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: EndDate
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            */
            
            /**
            * Required Parameters
            * userId:Number
            * trainingTypeId:Number
            * programId:Number
            * other:String
            * startDate:Date
            * endDate:Date
            * createdByUserId:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [ins_advanced_training]
            */
            
            
            return this.httpClient.post<IAdvancedTrainingModel>(`${this.baseEndpoint}/?api-version=${apiVersion}`, 
                model);
        }
 
        public updateAdvancedTraining(id: number,
        model: IAdvancedTrainingModel,
        apiVersion = '1.0') : Observable<IAdvancedTrainingModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * Business rules for property: UserId
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: TrainingTypeId
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: StartDate
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: EndDate
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            */
            
            /**
            * Required Parameters
            * id:Number
            * userId:Number
            * trainingTypeId:Number
            * programId:Number
            * other:String
            * startDate:Date
            * endDate:Date
            * lastUpdatedByUserId:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [update_advanced_training]
            */
            
            
            
            return this.httpClient.put<IAdvancedTrainingModel>(`${this.baseEndpoint}?api-version=${apiVersion}&id=${id}`,
            model);
        }
 
        public retrieveAdvancedTrainingReadOnly_GetByUserId(apiVersion = '1.0'): Observable<IAdvancedTrainingReadOnlyModel[]> {
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
            * [get_advanced_training_by_userid]
            */
            
            
            return this.httpClient.get<IAdvancedTrainingReadOnlyModel[]>(`${this.baseEndpoint}/by-userid?api-version=${apiVersion}`);
        }


}
