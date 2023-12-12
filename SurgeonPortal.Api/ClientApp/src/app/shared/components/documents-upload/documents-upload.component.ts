import { CommonModule } from '@angular/common';
import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
} from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Store } from '@ngxs/store';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { AbsFilterType } from 'src/app/shared/components/grid/abs-grid.enum';
import { IGridOptions } from 'src/app/shared/components/grid/grid-options.model';
import { GridComponent } from 'src/app/shared/components/grid/grid.component';
import {
  DeleteCertificate,
  DeleteDocument,
  DownloadDocument,
} from 'src/app/state';
import { GetDocumentTypes } from 'src/app/state/picklists';
import { IGridColumns } from '../grid/abs-grid-col.interface';

@Component({
  selector: 'abs-documents-upload',
  standalone: true,
  imports: [
    CommonModule,
    GridComponent,
    DropdownModule,
    ReactiveFormsModule,
    ButtonModule,
  ],
  templateUrl: './documents-upload.component.html',
  styleUrls: ['./documents-upload.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class DocumentsUploadComponent implements OnInit, OnChanges {
  /**
   * Whether or not to allow upload
   * @type {boolean}
   */
  @Input() allowUpload = true;

  /**
   * grid column definitions
   * @type {IGridColumns[]}
   */
  @Input() gridCols: IGridColumns[] = [];

  /**
   * Data to display in the grid
   * @type {any[]}
   */
  @Input() documentsData: any[] | null = [];

  /**
   * Data to display in the dropdown
   * @type {any[] | undefined}
   */
  @Input() dropdownOptions: any[] | null | undefined = [];

  /**
   * Placeholder to display in the dropdown
   * @type {string}
   */
  @Input() dropdownPlaceholder = 'Select Document Type';

  /**
   * Field to filter on
   * @type {string}
   */
  @Input() filterOn!: string;

  /**
   * Whether or not to display filter
   * @type {boolean}
   */
  @Input() showFilter = true;

  /**
   * label field for dropdown
   * @type {string}
   */
  @Input() dropdownLabel = 'itemDescription';

  /**
   * value field for dropdown
   * @type {string}
   */
  @Input() dropdownValue = 'itemValue';

  /**
   * User Id
   * @type {number}
   */
  @Input() userId!: number | undefined | null;

  /**
   * event emitter for document actions
   * @type {EventEmitter<any>}
   */
  @Output() documentsAction: EventEmitter<any> = new EventEmitter();

  /**
   * event emitter for upload actions
   * @type {EventEmitter<any>}
   */
  @Output() uploadAction: EventEmitter<any> = new EventEmitter();

  localDocumentsData: any[] = [];

  gridOptions: IGridOptions = {
    showFilter: this.showFilter,
    filterOn: '',
    filterType: AbsFilterType.Text,
  };
  fileUploadedName: string | undefined;
  uploadedFile: File | undefined;

  uploadForm = new FormGroup({
    typeId: new FormControl(''),
    file: new FormControl(''),
  });

  constructor(private _store: Store) {
    this._store.dispatch(new GetDocumentTypes());
  }
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['documentsData']) {
      this.localDocumentsData = changes['documentsData'].currentValue;
    }
  }

  ngOnInit() {
    this.localDocumentsData = this.documentsData || [];
    this.setFilterOptions();
  }

  setFilterOptions() {
    this.gridOptions.filterOn = this.filterOn;
    this.gridOptions.showFilter = this.showFilter;
  }

  handleGridAction($event: any) {
    const data = $event.data;
    if ($event.fieldKey === 'download') {
      this._store.dispatch(
        new DownloadDocument({
          documentId: data.documentId || data.id,
          documentName: data.documentName,
        })
      );
    }

    if ($event.fieldKey === 'delete') {
      if (data.certificateId) {
        this._store.dispatch(
          new DeleteCertificate(data.certificateId || data.id)
        );
      } else {
        this._store.dispatch(new DeleteDocument(data.id));
      }

      this.documentsAction.emit({
        fieldKey: 'delete',
      });
    }
  }

  onDocumentUpload() {
    const model = {
      typeId: this.uploadForm.get('typeId')?.value,
      createdByUserId: this.userId,
      file: this.uploadedFile,
      issueDate: new Date().toISOString(),
    };

    this.uploadAction.emit({
      data: model,
      gridData: this.documentsData,
      file: this.uploadedFile,
    });

    this.resetData();
  }

  handleFileOnChange($event: any) {
    this.fileUploadedName = $event.target.files[0].name;
    this.uploadedFile = $event.target.files[0];
  }

  resetData() {
    this.fileUploadedName = undefined;
    this.uploadForm.get('typeId')?.setValue('');
    this.uploadedFile = undefined;
  }
}
