import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IAccommodationModel } from '../../models/examinations/accommodation.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class AccommodationService {

    constructor(private apiService: ApiService) {}

 
        public deleteAccommodation(examId: number,
        apiVersion = '1.0'): Observable<any> {
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
            * id:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [delete_user_accommodations]
            */
            
            
            return this.apiService.delete<IAccommodationModel>(`api/exam/accommodations?api-version=${apiVersion}&id=${id}`);
        }
 
        public retrieveAccommodation_GetByExamId(examId: number,
        apiVersion = '1.0'): Observable<IAccommodationModel> {
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
            * examId:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_user_accommodations_byId]
            */
            
            
            return this.apiService.get<IAccommodationModel>(`api/exam/accommodations/by-exam-id?api-version=${apiVersion}&examId=${examId}`);
        }
 
        public createAccommodation(model: IAccommodationModel, 
            apiVersion = '1.0'): Observable<IAccommodationModel> {
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
            * accommodationID:Number
            * documentId:Number
            * examID:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [insert_user_accommodations]
            */
            
            
            return this.apiService.post<IAccommodationModel>(`api/exam/accommodations?api-version=${apiVersion}`, 
                model);
        }
 
        public updateAccommodation(examId: number,
        model: IAccommodationModel,
        apiVersion = '1.0') : Observable<IAccommodationModel> {
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
            * id:Number
            * accommodationID:Number
            * documentId:Number
            * examID:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [update_user_accommodations]
            */
            
            
            
            return this.apiService.put<IAccommodationModel>(`api/exam/accommodations?api-version=${apiVersion}&examId=${examId}`,
            model);
        }


}
