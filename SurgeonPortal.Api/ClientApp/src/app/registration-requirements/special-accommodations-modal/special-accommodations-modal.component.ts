import { CommonModule } from '@angular/common';
import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  OnInit,
  Output,
} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Select, Store } from '@ngxs/store';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { BehaviorSubject, Observable } from 'rxjs';
import { IAccommodationModel } from 'src/app/api/models/examinations/accommodation.model';
import { IAccommodationReadOnlyModel } from 'src/app/api/models/picklists/accommodation-read-only.model';
import { DocumentsUploadComponent } from 'src/app/shared/components/documents-upload/documents-upload.component';
import { GridComponent } from 'src/app/shared/components/grid/grid.component';
import {
  CreateAccommodation,
  ExamScoringSelectors,
  GetAccommodations,
  GetActiveExamId,
  ReqistrationRequirmentsSelectors,
  UpdateAccommodation,
  UserProfileSelectors,
} from 'src/app/state';
import {
  GetAccommodationTypes,
  PicklistsSelectors,
} from 'src/app/state/picklists';
import { SPECIAL_ACCOMMODATIONS_COLS } from './special-accommodations-cols';

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

  @Select(ExamScoringSelectors.slices.examHeaderId) examHeaderId$:
    | Observable<number>
    | undefined;

  @Select(PicklistsSelectors.slices.accommodationTypes) accommodationTypes$:
    | Observable<IAccommodationReadOnlyModel[]>
    | undefined;

  @Select(ReqistrationRequirmentsSelectors.slices.accommodation)
  accommodation$: Observable<IAccommodationModel> | undefined;

  examHeaderId!: number;

  specialAccommodationsCols = SPECIAL_ACCOMMODATIONS_COLS;
  specialAccommodationsData: BehaviorSubject<any> = new BehaviorSubject([]);

  fileUploadedName!: string | undefined;
  uploadedFile!: File | undefined;
  documentType!: string;

  selectedDocumentType: string | null | undefined;
  $event: any;

  allowUpload = false;

  constructor(private _store: Store) {
    this._store.dispatch(new GetActiveExamId());
    this.examHeaderId$?.pipe(untilDestroyed(this)).subscribe((id) => {
      this.examHeaderId = id;
      this.allowUpload = !!id;
    });
    this._store.dispatch(new GetAccommodationTypes());
    this._store.dispatch(new GetAccommodations(this.examHeaderId));
  }

  ngOnInit(): void {
    this.getSpecialAccommodationsData();
  }

  getSpecialAccommodationsData() {
    this.accommodation$?.pipe(untilDestroyed(this)).subscribe((data) => {
      if (data) {
        this.specialAccommodationsData.next([data]);
      }
    });
  }

  handleSelectChange(event: any) {
    this.selectedDocumentType = event.target.__displayLabel;
  }

  handleFileOnChange($event: any) {
    this.fileUploadedName = $event.target.files[0].name;
    this.uploadedFile = $event.target.files;
  }

  onDocumentUpload($event: any) {
    const data = $event.data;
    const existingAccommodation = $event.gridData[0];
    const model = {
      file: data.file,
      accommodationID: data.typeId,
      examID: this.examHeaderId,
      id: existingAccommodation?.id,
    };

    const formData = new FormData();

    Object.keys(model).forEach((key) => {
      formData.set(key, model[key as keyof typeof model]);
    });

    if (data.file) {
      if (!existingAccommodation) {
        this._store
          .dispatch(
            new CreateAccommodation(formData as unknown as IAccommodationModel)
          )
          .pipe(untilDestroyed(this))
          .subscribe(() => {
            this._store.dispatch(new GetAccommodations(model.examID ?? 0));
          });
      } else {
        this._store
          .dispatch(
            new UpdateAccommodation(
              model.examID ?? 0,
              formData as unknown as IAccommodationModel
            )
          )
          .pipe(untilDestroyed(this))
          .subscribe(() => {
            this._store.dispatch(new GetAccommodations(model.examID ?? 0));
          });
      }
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
