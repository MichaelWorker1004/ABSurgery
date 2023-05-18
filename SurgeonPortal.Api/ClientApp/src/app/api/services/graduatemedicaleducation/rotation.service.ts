import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IRotationModel } from '../../models/graduatemedicaleducation/rotation.model';
import { IRotationReadOnlyModel } from '../../models/graduatemedicaleducation/rotation-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class RotationService {
    private readonly baseEndpoint = 'api/graduate-medical-education';

    constructor(private apiService: ApiService) {}

 
        public deleteRotation(id: number,
        apiVersion = '1.0'): Observable<any> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * id:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [delete_gmerotations_byid]
            */
            
            
            return this.apiService.delete<IRotationModel>(`${this.baseEndpoint}?api-version=${apiVersion}&id=${id}`);
        }
 
        public retrieveRotation_GetById(id: number,
        apiVersion = '1.0'): Observable<IRotationModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * id:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_gmerotations_byid]
            */
            
            
            return this.apiService.get<IRotationModel>(`${this.baseEndpoint}/by-id?api-version=${apiVersion}&id=${id}`);
        }
 
        public createRotation(model: IRotationModel, 
            apiVersion = '1.0'): Observable<IRotationModel> {
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
            * startDate:String
            * endDate:String
            * clinicalLevelId:Number
            * clinicalActivityId:Number
            * programName:String
            * nonSurgicalActivity:String
            * alternateInstitutionName:String
            * isInternationalRotation:Boolean
            * other:String
            * createdByUserId:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [ins_gmerotations]
            */
            
            
            return this.apiService.post<IRotationModel>(`${this.baseEndpoint}?api-version=${apiVersion}`, 
                model);
        }
 
        public updateRotation(id: number,
        model: IRotationModel,
        apiVersion = '1.0') : Observable<IRotationModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * id:Number
            * userId:Number
            * startDate:String
            * endDate:String
            * clinicalLevelId:Number
            * clinicalActivityId:Number
            * programName:String
            * nonSurgicalActivity:String
            * alternateInstitutionName:String
            * isInternationalRotation:Boolean
            * other:String
            * isNonEssentialRotation:Boolean
            * noCredit:Boolean
            * lastUpdatedByUserId:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [update_gmerotations]
            */
            
            
            
            return this.apiService.put<IRotationModel>(`${this.baseEndpoint}?api-version=${apiVersion}&id=${id}`,
            model);
        }
 
        public retrieveRotationReadOnly_GetByUserId(apiVersion = '1.0'): Observable<IRotationReadOnlyModel[]> {
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
            * [get_gmerotations_byuserid]
            */
            
            
            return this.apiService.get<IRotationReadOnlyModel[]>(`${this.baseEndpoint}/by-userid?api-version=${apiVersion}`);
        }


}
