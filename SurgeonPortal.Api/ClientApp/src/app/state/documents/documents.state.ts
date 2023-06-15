import { Injectable } from '@angular/core';
import { catchError, tap } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { Action, State, StateContext, StateToken, Store } from '@ngxs/store';
import { IFormErrors } from 'src/app/shared/common';
import { IDocumentReadOnlyModel } from 'src/app/api/models/documents/document-read-only.model';
import { DocumentService } from 'src/app/api/services/documents/document.service';
import {
  DeleteCertificate,
  DeleteDocument,
  DownloadDocument,
  GetAllDocuments,
  UploadDocument,
} from './documents.actions';
import { UserCertificateService } from 'src/app/api/services/medicaltraining/user-certificate.service';
import { GetUserCertificates } from '../medical-training/medical-training.actions';
import { GlobalDialogService } from 'src/app/shared/services/global-dialog.service';

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
  constructor(
    private documentService: DocumentService,
    private userCertificateService: UserCertificateService,
    private globalDialogService: GlobalDialogService,
    private _store: Store
  ) {}

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

  @Action(DownloadDocument)
  downloadDocument(
    ctx: StateContext<IDocuments>,
    action: { payload: { documentId: number; documentName: string } }
  ): Observable<Blob> {
    return this.documentService
      .downloadDocument_GetById(action.payload.documentId)
      .pipe(
        tap((blob) => {
          const url = window.URL.createObjectURL(blob);
          const link = document.createElement('a');
          link.href = url;
          link.download = action.payload.documentName;
          link.click();
        }),
        catchError((error) => {
          console.error('------- In Documents Store', error);
          console.error(error);
          return of(error);
        })
      );
  }

  @Action(DeleteCertificate)
  DeleteCertificate(
    ctx: StateContext<IDocuments>,
    action: { payload: number }
  ): Observable<void> {
    const id = action.payload;
    this.globalDialogService.showLoading();
    return this.userCertificateService.deleteUserCertificate(id).pipe(
      tap(() => {
        this._store.dispatch(new GetUserCertificates(true));
        this._store.dispatch(new GetAllDocuments());
        this.globalDialogService.showSuccessError(
          'Success',
          'Document Deleted Successfully',
          true
        );
      }),
      catchError((error) => {
        console.error('------- In Documents Store', error);
        console.error(error);
        return of(error);
      })
    );
  }

  @Action(DeleteDocument)
  deleteDocument(
    ctx: StateContext<IDocuments>,
    action: { payload: number }
  ): Observable<void> {
    const id = action.payload;
    this.globalDialogService.showLoading();
    return this.documentService.deleteDocument(id).pipe(
      tap(() => {
        this._store.dispatch(new GetUserCertificates(true));
        this._store.dispatch(new GetAllDocuments());
        this.globalDialogService.showSuccessError(
          'Success',
          'Document Deleted Successfully',
          true
        );
      }),
      catchError((error) => {
        console.error('------- In Documents Store', error);
        console.error(error);
        return of(error);
      })
    );
  }

  @Action(UploadDocument)
  uploadDocument(
    ctx: StateContext<IDocuments>,
    action: { payload: { model: FormData } }
  ): Observable<void> {
    this.globalDialogService.showLoading();
    return this.userCertificateService
      .createUserCertificate(action.payload.model)
      .pipe(
        tap(() => {
          this._store.dispatch(new GetUserCertificates(true));
          this._store.dispatch(new GetAllDocuments());
          this.globalDialogService.showSuccessError(
            'Success',
            'Document uploaded successfully',
            true
          );
        }),
        catchError((error) => {
          console.error('------- In Documents Store', error);
          console.error(error);
          this.globalDialogService.showSuccessError(
            'Error',
            'Document uploaded failed',
            false
          );
          return of(error);
        })
      );
  }
}
