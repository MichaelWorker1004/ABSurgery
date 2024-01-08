import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IAdmissionCardAvailabilityReadOnlyModel } from '../../models/examinations/admission-card-availability-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class AdmissionCardService {

    constructor(private apiService: ApiService) {}

 
        public retrieveAdmissionCardAvailabilityReadOnly_GetByExamId(examID: number,
        apiVersion = '1.0'): Observable<IAdmissionCardAvailabilityReadOnlyModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * examID:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_adm_card_availability]
            */
            
            
            return this.apiService.get<IAdmissionCardAvailabilityReadOnlyModel>(`api/examinations/admission-card/availability?api-version=${apiVersion}&examID=${examID}`);
        }


}
