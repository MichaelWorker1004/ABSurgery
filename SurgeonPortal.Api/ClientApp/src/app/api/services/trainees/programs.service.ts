import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IProgramReadOnlyModel } from '../../models/trainees/program-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class ProgramsService {
    private readonly baseEndpoint = 'api/trainees/programs';

    constructor(private apiService: ApiService) {}

 
        public retrieveProgramReadOnly_GetByUserId(apiVersion = '1.0'): Observable<IProgramReadOnlyModel> {
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
            * [get_user_programs]
            */
            
            
            return this.apiService.get<IProgramReadOnlyModel>(`${this.baseEndpoint}?api-version=${apiVersion}`);
        }


}
