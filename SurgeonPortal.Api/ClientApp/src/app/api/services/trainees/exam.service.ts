import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IRegistrationStatusReadOnlyModel } from '../../models/trainees/registration-status-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class ExamService {

    constructor(private apiService: ApiService) {}

 
        public retrieveRegistrationStatusReadOnly_GetByExamCode(examCode: string,
        apiVersion = '1.0'): Observable<IRegistrationStatusReadOnlyModel> {
            /**
            * Claims
            * SurgeonPortalClaims.TraineeClaim
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * examCode:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_registration_open]
            */
            
            
            return this.apiService.get<IRegistrationStatusReadOnlyModel>(`api/trainees/exams/registration-status?api-version=${apiVersion}&examCode=${examCode}`);
        }


}
