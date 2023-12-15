import { CommonModule } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit } from '@angular/core';
import { IGridOptions } from '../shared/components/grid/grid-options.model';
import { GridComponent } from '../shared/components/grid/grid.component';
import { DOCUMENTS_COLS } from './documents-col';
import { AbsFilterType } from '../shared/components/grid/abs-grid.enum';
import { DocumentsUploadComponent } from '../shared/components/documents-upload/documents-upload.component';
import { Select, Store } from '@ngxs/store';
import { DocumentSelectors, GetAllDocuments } from '../state/documents';
import { IDocumentReadOnlyModel } from '../api/models/documents/document-read-only.model';
import { BehaviorSubject, Observable } from 'rxjs';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { IGridColumns } from '../shared/components/grid/abs-grid-col.interface';

@UntilDestroy()
@Component({
  selector: 'abs-documents',
  templateUrl: './documents.component.html',
  styleUrls: ['./documents.component.scss'],
  standalone: true,
  imports: [CommonModule, GridComponent, DocumentsUploadComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class DocumentsComponent implements OnInit {
  @Select(DocumentSelectors.slices.documents) documents$:
    | Observable<IDocumentReadOnlyModel[]>
    | undefined;

  documentsData$: BehaviorSubject<IDocumentReadOnlyModel[]> =
    new BehaviorSubject<IDocumentReadOnlyModel[]>([]);

  documentsCols = DOCUMENTS_COLS as IGridColumns[];
  gridOptions: IGridOptions = {
    showFilter: true,
    filterOn: 'documentName',
    filterType: AbsFilterType.Text,
  };
  fileUploadedName: string | undefined;
  uploadedFile: File | undefined;
  documentType!: string;
  canUpload = false;

  constructor(private _store: Store) {
    this._store.dispatch(new GetAllDocuments());
  }

  ngOnInit() {
    this.getDocuments();
  }

  getDocuments() {
    this.documents$?.pipe(untilDestroyed(this)).subscribe((documentsData) => {
      if (documentsData && documentsData.length > 0) {
        this.documentsData$.next(documentsData);
      }
    });
  }

  handleGridAction($event: any) {
    console.log('unhandled action', $event);
  }

  handleFileOnChange($event: any) {
    this.fileUploadedName = $event.target.files[0].name;
    this.uploadedFile = $event.target.files;
  }

  resetData() {
    this.fileUploadedName = undefined;
    this.uploadedFile = undefined;
    this.documentType = '';
  }
}
