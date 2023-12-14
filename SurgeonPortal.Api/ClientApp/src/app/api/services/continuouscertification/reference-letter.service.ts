import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IReferenceLetterModel } from '../../models/continuouscertification/reference-letter.model';
import { IReferenceLetterReadOnlyModel } from '../../models/continuouscertification/reference-letter-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class ReferenceLetterService {

    constructor(private apiService: ApiService) {}

 
        public retrieveReferenceLetter_GetById(id: number,
        apiVersion = '1.0'): Observable<IReferenceLetterModel> {
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
            * [get_refLet_details_byId]
            */
            
            
            return this.apiService.get<IReferenceLetterModel>(`api/continuous-certification/reference-letter/by-id?api-version=${apiVersion}&id=${id}`);
        }
 
        public createReferenceLetter(model: IReferenceLetterModel, 
            apiVersion = '1.0'): Observable<IReferenceLetterModel> {
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
            * official:String
            * title:String
            * roleId:String
            * altRoleId:String
            * explain:String
            * email:String
            * phone:String
            * hosp:String
            * city:String
            * state:String
            * secOrder:Number
            * idCode:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [ins_cc_refLetter]
            */
            
            
            return this.apiService.post<IReferenceLetterModel>(`api/continuous-certification/reference-letter?api-version=${apiVersion}`, 
                model);
        }
 
        public retrieveReferenceLetterReadOnly_GetAllByUserId(apiVersion = '1.0'): Observable<IReferenceLetterReadOnlyModel[]> {
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
            * [get_cc_refLetters]
            */
            
            
            return this.apiService.get<IReferenceLetterReadOnlyModel[]>(`api/continuous-certification/reference-letter/all-by-user-id?api-version=${apiVersion}`);
        }


}
