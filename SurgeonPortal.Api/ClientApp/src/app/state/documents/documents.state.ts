import { Injectable } from '@angular/core';
import { catchError, share, tap } from 'rxjs/operators';
import { forkJoin, map, Observable, of } from 'rxjs';
import { Action, State, StateContext, StateToken, Store } from '@ngxs/store';
import { IFormErrors } from 'src/app/shared/common';
import { IDocumentReadOnlyModel } from 'src/app/api/models/documents/document-read-only.model';
import { DocumentService } from 'src/app/api/services/documents/document.service';
import { GetAllDocuments } from './documents.actions';

export interface IDocuments {
  documents: IDocumentReadOnlyModel[] | undefined;
  errors?: IFormErrors;
}

export const DOCUMENTS_STATE_TOKEN = new StateToken<IDocuments>('documents');

@State<IDocuments>({
  name: DOCUMENTS_STATE_TOKEN,
  defaults: {
    documents: undefined,
  },
})
@Injectable()
export class DocumentsState {
  constructor(private documentService: DocumentService) {}

  @Action(GetAllDocuments)
  getAllDocuments({
    patchState,
  }: StateContext<IDocuments>): Observable<IDocumentReadOnlyModel[]> {
    return this.documentService.retrieveDocumentReadOnly_GetByUserId().pipe(
      tap((documents) => {
        patchState({
          documents,
        });
      }),
      catchError((error) => {
        console.error('------- In Documents Store', error);
        console.error(error);
        return of(error);
      })
    );
  }
}
