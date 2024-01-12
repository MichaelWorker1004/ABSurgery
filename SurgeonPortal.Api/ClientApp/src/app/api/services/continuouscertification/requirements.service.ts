import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IRequirementsReadOnlyModel } from '../../models/continuouscertification/requirements-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class RequirementsService {

    constructor(private apiService: ApiService) {}

 
        public retrieveRequirementsReadOnly_GetByUserId(userId: number,
        apiVersion = '1.0'): Observable<IRequirementsReadOnlyModel> {
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
            * [get_user_meeting_cc_requirements_byuserid]
            */
            
            
            return this.apiService.get<IRequirementsReadOnlyModel>(`api/continuous-certification/requirements/by-userid?api-version=${apiVersion}&userId=${userId}`);
        }


}
