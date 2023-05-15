import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICertificationReadOnlyModel } from '../../models/surgeons/certification-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class CertificationsService {
    private readonly baseEndpoint = 'api/surgeons/certifications';

    constructor(private apiService: ApiService) {}


        public retrieveCertificationReadOnly_GetByAbsId(absId: string,
        apiVersion = '1.0'): Observable<ICertificationReadOnlyModel[]> {
            /**
            * Claims
            */

            /**
            * Business Rules
            * No business rules exist for this model
            */

            /**
            * Required Parameters
            * absId:String
            * apiVersion
            */

            /**
            * Calls Sp(s)
            * [get_user_certifications]
            */


            return this.apiService.get<ICertificationReadOnlyModel[]>(`${this.baseEndpoint}?api-version=${apiVersion}&absId=${absId}`);
        }


}
