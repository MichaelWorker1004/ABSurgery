import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IPdReferenceLetterModel } from '../../models/examinations/pd-reference-letter.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class PdReferenceLetterService {

    constructor(private apiService: ApiService) {}

 
        public retrievePdReferenceLetter_GetByExamId(examId: number,
        apiVersion = '1.0'): Observable<IPdReferenceLetterModel> {
            /**
            * Claims
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
            * [get_pd_refLetters]
            */
            
            
            return this.apiService.get<IPdReferenceLetterModel>(`api/examinations/pd-reference-letter/by-examid?api-version=${apiVersion}&examId=${examId}`);
        }
 
        public createPdReferenceLetter(model: IPdReferenceLetterModel, 
            apiVersion = '1.0'): Observable<IPdReferenceLetterModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * official:String
            * title:String
            * email:String
            * hosp:String
            * examId:Number
            * idCode:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [ins_pd_refLetter]
            */
            
            
            return this.apiService.post<IPdReferenceLetterModel>(`api/examinations/pd-reference-letter?api-version=${apiVersion}`, 
                model);
        }


}
