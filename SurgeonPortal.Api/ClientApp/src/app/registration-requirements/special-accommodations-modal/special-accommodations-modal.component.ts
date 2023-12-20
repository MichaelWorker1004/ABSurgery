import { CommonModule } from '@angular/common';
import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  Input,
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
import { IGridColumns } from 'src/app/shared/components/grid/abs-grid-col.interface';
import { GridComponent } from 'src/app/shared/components/grid/grid.component';
import {
  CreateAccommodation,
  ExamScoringSelectors,
  GetAccommodations,
  ReqistrationRequirmentsSelectors,
  UpdateAccommodation,
  UserProfileSelectors,
} from 'src/app/state';
import {
  GetAccommodationTypes,
  PicklistsSelectors,
} from 'src/app/state/picklists';
import { SPECIAL_ACCOMMODATIONS_COLS } from './special-accommodations-cols';
import { ActivatedRoute } from '@angular/router';

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

  @Input() examHeaderId!: number | null;

  specialAccommodationsCols = SPECIAL_ACCOMMODATIONS_COLS as IGridColumns[];
  specialAccommodationsData: BehaviorSubject<any> = new BehaviorSubject([]);

  fileUploadedName!: string | undefined;
  uploadedFile!: File | undefined;
  documentType!: string;

  selectedAccommodationType: IAccommodationReadOnlyModel | undefined;

  $event: any;

  constructor(private _store: Store, private activatedRoute: ActivatedRoute) {
    this.activatedRoute.params
      .pipe(untilDestroyed(this))
      .subscribe((params) => {
        this.examHeaderId = params['examId'];
        if (this.examHeaderId) {
          this._store.dispatch(new GetAccommodations(this.examHeaderId));
        }
      });
    this._store.dispatch(new GetAccommodationTypes());
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

  handleFileOnChange($event: any) {
    this.fileUploadedName = $event.target.files[0].name;
    this.uploadedFile = $event.target.files;
  }

  onDocumentUpload($event: any) {
    const data = $event.data;
    const existingAccommodation = $event.gridData[0];
    const model = {
      file: data?.file,
      accommodationID: this.selectedAccommodationType?.id || 0,
      examID: this.examHeaderId,
      id: existingAccommodation?.id,
    };

    // const formData = new FormData();

    // Object.keys(model).forEach((key) => {
    //   formData.set(key, model[key as keyof typeof model]);
    // });

    if (!existingAccommodation) {
      console.log(model);
      this._store
        .dispatch(
          new CreateAccommodation(model as unknown as IAccommodationModel)
        )
        .pipe(untilDestroyed(this))
        .subscribe(() => {
          if (data) {
            this.close();
          } else {
            this._store.dispatch(new GetAccommodations(model.examID ?? 0));
          }
        });
    } else {
      this._store
        .dispatch(
          new UpdateAccommodation(
            model.examID ?? 0,
            model as unknown as IAccommodationModel
          )
        )
        .pipe(untilDestroyed(this))
        .subscribe(() => {
          if (data) {
            this.close();
          } else {
            this._store.dispatch(new GetAccommodations(model.examID ?? 0));
          }
        });
    }
  }

  resetData() {
    this.fileUploadedName = undefined;
    this.uploadedFile = undefined;
    this.selectedAccommodationType = undefined;
  }

  close() {
    this.resetData();
    this.closeDialog.emit({ action: 'specialAccommodationsModal' });
  }

  save() {
    this.onDocumentUpload({ gridData: [] });
  }

  gridAction($event: any) {
    console.log('unhandled grid action', $event);
  }
}
