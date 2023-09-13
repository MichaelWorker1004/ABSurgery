import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IAgendaReadOnlyModel } from '../../models/examiners/agenda-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class AgendaService {

    constructor(private apiService: ApiService) {}

 
        public retrieveAgendaReadOnly_GetByExamHeaderId(examHeaderId: number,
        apiVersion = '1.0'): Observable<IAgendaReadOnlyModel> {
            /**
            * Claims
            * SurgeonPortalClaims.ExaminerClaim
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * examHeaderId:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_examiner_agenda]
            */
            
            
            return this.apiService.get<IAgendaReadOnlyModel>(`api/examiners/agenda/by-exam-header-id?api-version=${apiVersion}&examHeaderId=${examHeaderId}`);
        }


}
