/* eslint-disable no-octal */
/* eslint-disable prettier/prettier */
import { CommonModule } from '@angular/common';
import {
    AfterViewInit,
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
} from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import {
    UploadDocument,
  UploadDocumentCertificate,
} from '../../../state';
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
import { IUserCertificateModel } from '../../../api/models/medicaltraining/user-certificate.model';
import { UserCertificateFileModel } from '../../../api/models/medicaltraining/user-certificateFilemodel';
import { Disabled } from '../action-card/action-card.component.stories';
import { GlobalDialogService } from 'src/app/shared/services/global-dialog.service';
import { Router } from '@angular/router';

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
export class DocumentsUploadComponent implements OnInit, OnChanges, AfterViewInit {
  /**
   * Whether or not to allow upload
   * @type {boolean}
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
  uploadedFile!: File | null;
  certificateTypeId: number | undefined | null;

  uploadForm = new FormGroup({
    typeId: new FormControl(''),
    file: new FormControl(''),
  });

  active: number | undefined;

  constructor(private _store: Store, private globalDialogService: GlobalDialogService, private router: Router) {
    this._store.dispatch(new GetDocumentTypes());

  }
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['documentsData']) {
      this.localDocumentsData = changes['documentsData'].currentValue;
      if (this.router.url.indexOf('/test/sort') > -1) {
        this.active = 0;
      }
    }
  }

  ngOnInit() {
    this.initialize(); 
  }

  initialize() {
    this.localDocumentsData = this.documentsData || [];
    this.setFilterOptions();
    this.setRequired();
  }

  ngAfterViewInit() {
    if (this.router.url.indexOf('/medical-training') === -1) {
      this.uploadForm.get('file')?.enable();
      this.certificateTypeId = 1;
    }
    else {
      this.certificateTypeId = undefined;
      this.uploadForm.get('file')?.disable();      
    }
  }

  setRequired() {
    if (this.dropdownOptions?.length) {
      this.uploadForm.get('typeId')?.setValidators([Validators.required]);
    } else {
      this.uploadForm.get('typeId')?.clearValidators();
    }
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
      file: this.uploadedFile?.stream,
      issueDate: new Date().toISOString(),
    }
    alert('this.uploadedFile?.name = ' + this.uploadedFile?.name);

    alert('model = ' + JSON.stringify(model));
    
    const formData = new FormData();

    formData.append("typeId", this.uploadForm.get('typeId')?.value as string);
    formData.append("createdByUserId", this.userId?.toString() as string);
    formData.append("file", this.uploadedFile as File);
    formData.append("issueDate", new Date().toISOString());

    alert('about to upload document');

    alert('passing form data = ' + JSON.stringify(formData));

    //if (model.file) {
    //  this._store.dispatch(new UploadDocumentCertificate(formData));
    //}

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

    //check file size
    if (this.uploadedFile!.size > 20000000) {
      this.globalDialogService.showSuccessError(
        'Error',
        'File size is too large. Maximum size is 20MB',
        false
      );

      this.uploadedFile = null;
      this.uploadForm.get('file')?.setValue('');
      this.fileUploadedName = undefined;
      return;
    }


    if (this.uploadedFile) {
      this._store.dispatch(new UploadDocumentCertificate(
        {
          certificateTypeId: this.uploadForm.get('typeId')?.value as unknown as number,
          file: $event.target.files[0] as File,
        }));
    }
    
    this.uploadAction.emit({
      gridData: this.documentsData,
      file: this.uploadedFile,
    });

    this.resetData();

  }

  handleDropDownOnChange($event: any) {
    if (this.router.url.indexOf('/medical-training') === 0) {
      this.certificateTypeId = this.uploadForm.get('typeId')?.value as unknown as number;

      if (this.certificateTypeId > 0) {
        this.uploadForm.get('file')?.enable();
      }
      else {
        this.uploadForm.get('file')?.disable();
      }
    }
  }

  resetData() {
    this.fileUploadedName = undefined;
    this.uploadForm.get('typeId')?.setValue('');
    this.uploadForm.get('file')?.setValue('');
    this.uploadedFile = null;

    this.initialize();
  }
}
