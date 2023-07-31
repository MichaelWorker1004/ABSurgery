import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IRotationModel } from '../../models/graduatemedicaleducation/rotation.model';
import { IRotationGapReadOnlyModel } from '../../models/graduatemedicaleducation/rotation-gap-read-only.model';
import { IRotationReadOnlyModel } from '../../models/graduatemedicaleducation/rotation-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class RotationService {

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
            
            
            return this.apiService.delete<IRotationModel>(`api/graduate-medical-education?api-version=${apiVersion}&id=${id}`);
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
            
            
            return this.apiService.get<IRotationModel>(`api/graduate-medical-education/by-id?api-version=${apiVersion}&id=${id}`);
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
            * startDate:String
            * endDate:String
            * clinicalLevelId:Number
            * clinicalActivityId:Number
            * programName:String
            * nonSurgicalActivity:String
            * alternateInstitutionName:String
            * isInternationalRotation:Boolean
            * other:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [ins_gmerotations]
            */
            
            
            return this.apiService.post<IRotationModel>(`api/graduate-medical-education?api-version=${apiVersion}`, 
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
            * startDate:String
            * endDate:String
            * clinicalLevelId:Number
            * clinicalActivityId:Number
            * programName:String
            * nonSurgicalActivity:String
            * alternateInstitutionName:String
            * isInternationalRotation:Boolean
            * other:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [update_gmerotations]
            */
            
            
            
            return this.apiService.put<IRotationModel>(`api/graduate-medical-education?api-version=${apiVersion}&id=${id}`,
            model);
        }
 
        public retrieveRotationGapReadOnly_GetByUserId(apiVersion = '1.0'): Observable<IRotationGapReadOnlyModel[]> {
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
            * [get_gme_conflicts]
            */
            
            
            return this.apiService.get<IRotationGapReadOnlyModel[]>(`api/graduate-medical-education/gaps?api-version=${apiVersion}`);
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
            
            
            return this.apiService.get<IRotationReadOnlyModel[]>(`api/graduate-medical-education/by-userid?api-version=${apiVersion}`);
        }


}
