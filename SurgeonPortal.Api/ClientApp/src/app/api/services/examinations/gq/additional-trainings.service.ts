import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IAdditionalTrainingModel } from '../../../models/examinations/gq/additional-training.model';
import { IAdditionalTrainingReadOnlyModel } from '../../../models/examinations/gq/additional-training-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class AdditionalTrainingsService {

    constructor(private apiService: ApiService) {}

 
        public retrieveAdditionalTraining_GetByTrainingId(trainingId: number,
        apiVersion = '1.0'): Observable<IAdditionalTrainingModel> {
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
            * trainingId:Number
            * apiVersion
            */

            /**
            * Calls Sp(s)
            * [get_additionaltraining_bytrainingid]
            */
            
            
            return this.apiService.get<IAdditionalTrainingModel>(`api/examinations/gq/additional-trainings?api-version=${apiVersion}&trainingId=${trainingId}`);
        }

        public createAdditionalTraining(model: IAdditionalTrainingModel,
            apiVersion = '1.0'): Observable<IAdditionalTrainingModel> {
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
            
            
            return this.apiService.post<IAdditionalTrainingModel>(`api/examinations/gq/additional-trainings?api-version=${apiVersion}`, 
                model);
        }

        public updateAdditionalTraining(trainingId: number,
        model: IAdditionalTrainingModel,
        apiVersion = '1.0') : Observable<IAdditionalTrainingModel> {
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
            
            
            
            return this.apiService.put<IAdditionalTrainingModel>(`api/examinations/gq/additional-trainings?api-version=${apiVersion}&trainingId=${trainingId}`,
            model);
        }

        public retrieveAdditionalTrainingReadOnly_GetAllByUserId(apiVersion = '1.0'): Observable<IAdditionalTrainingReadOnlyModel[]> {
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
            * [get_additionaltrainingreadonly_allbyuserid]
            */
            
            
            return this.apiService.get<IAdditionalTrainingReadOnlyModel[]>(`api/examinations/gq/additional-trainings/all?api-version=${apiVersion}`);
        }


}
