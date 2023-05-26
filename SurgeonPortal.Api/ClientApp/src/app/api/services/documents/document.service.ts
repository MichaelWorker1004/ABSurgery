import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IDocumentModel } from '../../models/documents/document.model';
import { IDocumentReadOnlyModel } from '../../models/documents/document-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class DocumentService {
    private readonly baseEndpoint = 'api/documents';

    constructor(private apiService: ApiService) {}

 
        public deleteDocument(id: number,
        apiVersion = '1.0'): Observable<any> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * Business rules for property: Id
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: UserId
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: DocumentTypeId
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            */
            
            /**
            * Required Parameters
            * id:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [delete_userdocument_byid]
            */
            
            
            return this.apiService.delete<IDocumentModel>(`${this.baseEndpoint}?api-version=${apiVersion}&id=${id}`);
        }
 
        public retrieveDocument_GetById(id: number,
        apiVersion = '1.0'): Observable<IDocumentModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * Business rules for property: Id
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: UserId
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: DocumentTypeId
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            */
            
            /**
            * Required Parameters
            * id:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_document_byid]
            */
            
            
            return this.apiService.get<IDocumentModel>(`${this.baseEndpoint}/by-id?api-version=${apiVersion}&id=${id}`);
        }
 
        public retrieveDocumentReadOnly_GetByUserId(apiVersion = '1.0'): Observable<IDocumentReadOnlyModel[]> {
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
            * [get_documents_byuserid]
            */
            
            
            return this.apiService.get<IDocumentReadOnlyModel[]>(`${this.baseEndpoint}/by-userid?api-version=${apiVersion}`);
        }


}
