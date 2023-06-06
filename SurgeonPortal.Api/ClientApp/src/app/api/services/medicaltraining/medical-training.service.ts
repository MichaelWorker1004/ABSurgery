import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IMedicalTrainingModel } from '../../models/medicaltraining/medical-training.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class MedicalTrainingService {
    private readonly baseEndpoint = 'api/medical-training';

    constructor(private apiService: ApiService) {}

 
        public retrieveMedicalTraining_GetByUserId(apiVersion = '1.0'): Observable<IMedicalTrainingModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * Business rules for property: MedicalSchoolCompletionYear
            *   Rule Name: RegEx
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: ResidencyCompletionYear
            *   Rule Name: RegEx
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            */
            
            /**
            * Required Parameters
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_medical_training_byuserid]
            */
            
            
            return this.apiService.get<IMedicalTrainingModel>(`${this.baseEndpoint}/by-userid?api-version=${apiVersion}`);
        }
 
        public createMedicalTraining(model: IMedicalTrainingModel, 
            apiVersion = '1.0'): Observable<IMedicalTrainingModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * Business rules for property: MedicalSchoolCompletionYear
            *   Rule Name: RegEx
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: ResidencyCompletionYear
            *   Rule Name: RegEx
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            */
            
            /**
            * Required Parameters
            * graduateProfileId:Number
            * medicalSchoolName:String
            * medicalSchoolCity:String
            * medicalSchoolStateId:String
            * medicalSchoolCountryId:String
            * degreeId:Number
            * medicalSchoolCompletionYear:String
            * residencyProgramName:String
            * residencyCompletionYear:String
            * residencyProgramOther:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [ins_medical_training]
            */
            
            
            return this.apiService.post<IMedicalTrainingModel>(`${this.baseEndpoint}?api-version=${apiVersion}`, 
                model);
        }
 
        public updateMedicalTraining(model: IMedicalTrainingModel,
        apiVersion = '1.0') : Observable<IMedicalTrainingModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * Business rules for property: MedicalSchoolCompletionYear
            *   Rule Name: RegEx
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: ResidencyCompletionYear
            *   Rule Name: RegEx
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            */
            
            /**
            * Required Parameters
            * id:Number
            * userId:Number
            * graduateProfileId:Number
            * medicalSchoolName:String
            * medicalSchoolCity:String
            * medicalSchoolStateId:String
            * medicalSchoolCountryId:String
            * degreeId:Number
            * medicalSchoolCompletionYear:String
            * residencyProgramName:String
            * residencyCompletionYear:String
            * residencyProgramOther:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [update_medical_training]
            */
            
            
            
            return this.apiService.put<IMedicalTrainingModel>(`${this.baseEndpoint}?api-version=${apiVersion}`,
            model);
        }


}
