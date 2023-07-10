import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IDocumentModel } from '../../models/documents/document.model';
import { IDocumentReadOnlyModel } from '../../models/documents/document-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class DocumentService {

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
            
            
            return this.apiService.delete<IDocumentModel>(`api/documents?api-version=${apiVersion}&id=${id}`);
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
            
            
            return this.apiService.get<IDocumentModel>(`api/documents/by-id?api-version=${apiVersion}&id=${id}`);
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
            
            
            return this.apiService.get<IDocumentReadOnlyModel[]>(`api/documents/by-userid?api-version=${apiVersion}`);
        }

        public downloadDocument_GetById(id: number, apiVersion = '1.0'): Observable<File> {

          return this.apiService.get<File>(`api/documents/${id}?api-version=${apiVersion}`, {
            responseType: 'blob' as 'json'
          });
        }
}
