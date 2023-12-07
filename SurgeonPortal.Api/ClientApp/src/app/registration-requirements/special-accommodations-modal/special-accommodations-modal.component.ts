import { CommonModule } from '@angular/common';
import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  OnInit,
  Output,
} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Select, Store } from '@ngxs/store';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { Observable } from 'rxjs';
import { DocumentsUploadComponent } from 'src/app/shared/components/documents-upload/documents-upload.component';
import { GridComponent } from 'src/app/shared/components/grid/grid.component';
import { UserProfileSelectors } from 'src/app/state';
import { SPECIAL_ACCOMMODATIONS_COLS } from './special-accommodations-cols';
import {
  GetAccommodationTypes,
  PicklistsSelectors,
} from 'src/app/state/picklists';
import { IAccommodationReadOnlyModel } from 'src/app/api/models/picklists/accommodation-read-only.model';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

@UntilDestroy()
@Component({
  selector: 'abs-special-accommodations-modal',
  standalone: true,
  imports: [
    CommonModule,
    GridComponent,
    FormsModule,
    DropdownModule,
    ButtonModule,
    DocumentsUploadComponent,
  ],
  templateUrl: './special-accommodations-modal.component.html',
  styleUrls: ['./special-accommodations-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class SpecialAccommodationsModalComponent implements OnInit {
  @Output() closeDialog: EventEmitter<any> = new EventEmitter();

  @Select(UserProfileSelectors.userId) userId$: Observable<number> | undefined;
  @Select(PicklistsSelectors.slices.accommodationTypes) accommodationTypes$:
    | Observable<IAccommodationReadOnlyModel[]>
    | undefined;

  specialAccommodationsCols = SPECIAL_ACCOMMODATIONS_COLS;
  specialAccommodationsData!: any;

  fileUploadedName!: string | undefined;
  uploadedFile!: File | undefined;
  documentType!: string;

  selectedDocumentType: string | null | undefined;

  constructor(private _store: Store) {
    this._store.dispatch(new GetAccommodationTypes());
  }

  ngOnInit(): void {
    this.getSpecialAccommodationsData();
  }

  getSpecialAccommodationsData() {
    this.specialAccommodationsData = [
      {
        fileName: 'ABC_Special-Accommodation-Request_1-2-22.pdf',
        uploadDate: new Date('09/22/19'),
        type: 'Other medical condition',
      },
    ];
  }

  handleSelectChange(event: any) {
    this.selectedDocumentType = event.target.__displayLabel;
  }

  handleFileOnChange($event: any) {
    this.fileUploadedName = $event.target.files[0].name;
    this.uploadedFile = $event.target.files;
  }

  onDocumentUpload() {
    if (this.uploadedFile) {
      this.specialAccommodationsData.push({
        fileName: this.fileUploadedName,
        uploadDate: new Date(),
        type: this.selectedDocumentType,
      });
      this.resetData();
    }
  }

  resetData() {
    this.fileUploadedName = undefined;
    this.uploadedFile = undefined;
    this.selectedDocumentType = null;
  }

  close() {
    this.closeDialog.emit({ action: 'specialAccommodationsModal' });
  }

  gridAction($event: any) {
    console.log('unhandled grid action', $event);
  }
}
