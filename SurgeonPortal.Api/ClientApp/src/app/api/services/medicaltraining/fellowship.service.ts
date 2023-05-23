import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IFellowshipModel } from '../../models/medicaltraining/fellowship.model';
import { IFellowshipReadOnlyModel } from '../../models/medicaltraining/fellowship-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class FellowshipService {
    private readonly baseEndpoint = 'api/fellowships';

    constructor(private apiService: ApiService) {}

 
        public deleteFellowship(id: number,
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
            * [delete_userfellowships]
            */
            
            
            return this.apiService.delete<IFellowshipModel>(`${this.baseEndpoint}?api-version=${apiVersion}&id=${id}`);
        }
 
        public retrieveFellowship_GetById(id: number,
        apiVersion = '1.0'): Observable<IFellowshipModel> {
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
            * [get_userfellowships_byid]
            */
            
            
            return this.apiService.get<IFellowshipModel>(`${this.baseEndpoint}/by-id?api-version=${apiVersion}&id=${id}`);
        }
 
        public createFellowship(model: IFellowshipModel, 
            apiVersion = '1.0'): Observable<IFellowshipModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * programName:String
            * completionYear:String
            * programOther:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [ins_userfellowships]
            */
            
            
            return this.apiService.post<IFellowshipModel>(`${this.baseEndpoint}?api-version=${apiVersion}`, 
                model);
        }
 
        public updateFellowship(id: number,
        model: IFellowshipModel,
        apiVersion = '1.0') : Observable<IFellowshipModel> {
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
            * programName:String
            * completionYear:String
            * programOther:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [update_userfellowships]
            */
            
            
            
            return this.apiService.put<IFellowshipModel>(`${this.baseEndpoint}?api-version=${apiVersion}&id=${id}`,
            model);
        }
 
        public retrieveFellowshipReadOnly_GetByUserId(apiVersion = '1.0'): Observable<IFellowshipReadOnlyModel[]> {
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
            * [get_userfellowships_byuserid]
            */
            
            
            return this.apiService.get<IFellowshipReadOnlyModel[]>(`${this.baseEndpoint}/by-userid?api-version=${apiVersion}`);
        }


}
