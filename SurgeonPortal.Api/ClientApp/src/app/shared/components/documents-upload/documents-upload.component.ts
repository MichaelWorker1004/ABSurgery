import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { GridComponent } from 'src/app/shared/components/grid/grid.component';
import { AbsFilterType } from 'src/app/shared/components/grid/abs-grid.enum';
import { IGridOptions } from 'src/app/shared/components/grid/grid-options.model';
import { Subject } from 'rxjs';
import { DropdownModule } from 'primeng/dropdown';
import { Store } from '@ngxs/store';
import {
  DeleteCertificate,
  DeleteDocument,
  DownloadDocument,
  UploadDocument,
} from 'src/app/state';
import { GetDocumentTypes } from 'src/app/state/picklists';
import { IUserCertificateModel } from 'src/app/api/models/medicaltraining/user-certificate.model';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';

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
export class DocumentsUploadComponent implements OnInit {
  // TODO: [Joe] - add form-errors shared component

  @Input() allowUpload = true;
  @Input() gridCols!: any;
  @Input() documentsData$: Subject<any> = new Subject();
  @Input() dropdownOptions!: any;
  @Input() dropdownPlaceholder!: string;
  @Input() filterOn!: string;
  @Input() showFilter!: boolean;
  @Input() dropdownLabel!: string;
  @Input() dropdownValue!: string;
  @Input() userId!: number;
  @Output() documentsAction: EventEmitter<any> = new EventEmitter();

  gridOptions: IGridOptions = {
    showFilter: true,
    filterOn: '',
    filterType: AbsFilterType.Text,
  };
  fileUploadedName: string | undefined;
  uploadedFile: File | undefined;

  uploadForm = new FormGroup({
    certificateTypeId: new FormControl(''),
    file: new FormControl(''),
  });

  constructor(private _store: Store) {
    this._store.dispatch(new GetDocumentTypes());
  }

  ngOnInit() {
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
    const model: IUserCertificateModel = {
      documentId: 1,
      certificateTypeId: this.uploadForm.get('certificateTypeId')?.value,
      createdByUserId: this.userId,
      file: this.uploadedFile,
      issueDate: new Date().toISOString(),
    } as unknown as IUserCertificateModel;

    const formData = new FormData();

    Object.keys(model).forEach((key) => {
      formData.set(key, model[key]);
    });

    if (this.uploadedFile) {
      this._store.dispatch(new UploadDocument({ model: formData }));
    }

    this.documentsAction.emit({
      fieldKey: 'upload',
    });

    this.resetData();
  }

  handleFileOnChange($event: any) {
    this.fileUploadedName = $event.target.files[0].name;
    this.uploadedFile = $event.target.files[0];
  }

  resetData() {
    this.fileUploadedName = undefined;
    this.uploadForm.get('certificateTypeId')?.setValue('');
    this.uploadedFile = undefined;
  }
}
