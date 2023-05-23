import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IAdditionalTrainingModel } from '../../../models/examinations/gq/additional-training.model';
import { IAdditionalTrainingReadOnlyModel } from '../../../models/examinations/gq/additional-training-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class AdditionalTrainingsService {
    private readonly baseEndpoint = 'api/examinations/gq/additional-trainings';

    constructor(private apiService: ApiService) {}

 
        public retrieveAdditionalTraining_GetByTrainingId(trainingId: number,
        apiVersion = '1.0'): Observable<IAdditionalTrainingModel> {
            /**
            * Claims
            */

            /**
            * Business Rules
            * No business rules exist for this model
            */

            /**
            * Required Parameters
            * trainingId:Number
            * apiVersion
            */

            /**
            * Calls Sp(s)
            * [get_additionaltraining_bytrainingid]
            */
            
            
            return this.apiService.get<IAdditionalTrainingModel>(`${this.baseEndpoint}?api-version=${apiVersion}&trainingId=${trainingId}`);
        }

        public createAdditionalTraining(model: IAdditionalTrainingModel,
            apiVersion = '1.0'): Observable<IAdditionalTrainingModel> {
            /**
            * Claims
            */

            /**
            * Business Rules
            * No business rules exist for this model
            */

            /**
            * Required Parameters
            * dateEnded:String
            * dateStarted:String
            * other:String
            * institutionId:Number
            * city:String
            * stateId:String
            * typeOfTraining:String
            * apiVersion
            */

            /**
            * Calls Sp(s)
            * [ins_additionaltraining_bytrainingid]
            */
            
            
            return this.apiService.post<IAdditionalTrainingModel>(`${this.baseEndpoint}?api-version=${apiVersion}`, 
                model);
        }

        public updateAdditionalTraining(trainingId: number,
        model: IAdditionalTrainingModel,
        apiVersion = '1.0') : Observable<IAdditionalTrainingModel> {
            /**
            * Claims
            */

            /**
            * Business Rules
            * No business rules exist for this model
            */

            /**
            * Required Parameters
            * trainingId:Number
            * dateEnded:String
            * dateStarted:String
            * other:String
            * institutionId:Number
            * city:String
            * stateId:String
            * typeOfTraining:String
            * apiVersion
            */

            /**
            * Calls Sp(s)
            * [update_additionaltraining_bytrainingid]
            */
            
            
            
            return this.apiService.put<IAdditionalTrainingModel>(`${this.baseEndpoint}?api-version=${apiVersion}&trainingId=${trainingId}`,
            model);
        }

        public retrieveAdditionalTrainingReadOnly_GetAllByUserId(userId: number,
        apiVersion = '1.0'): Observable<IAdditionalTrainingReadOnlyModel[]> {
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
            * [get_additionaltrainingreadonly_allbyuserid]
            */
            
            
            return this.apiService.get<IAdditionalTrainingReadOnlyModel[]>(`${this.baseEndpoint}/all?api-version=${apiVersion}&userId=${userId}`);
        }


}
