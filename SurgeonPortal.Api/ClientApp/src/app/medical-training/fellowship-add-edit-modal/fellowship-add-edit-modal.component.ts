import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { InputSelectComponent } from 'src/app/shared/components/base-input/input-select.component';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { CalendarModule } from 'primeng/calendar';
import { Observable, Subject } from 'rxjs';
import { IFellowshipReadOnlyModel } from 'src/app/api/models/medicaltraining/fellowship-read-only.model';
import { Select, Store } from '@ngxs/store';
import {
  GetFellowshipPrograms,
  GetFellowshipTypes,
  PicklistsSelectors,
} from 'src/app/state/picklists';
import { IFellowshipProgramReadOnlyModel } from 'src/app/api/models/picklists/fellowship-program-read-only.model';
import { GlobalDialogService } from 'src/app/shared/services/global-dialog.service';
import { IFellowshipTypeReadOnlyModel } from 'src/app/api/models/picklists/fellowship-type-read-only.model';
import { RadioButtonModule } from 'primeng/radiobutton';
import { IFormErrors } from 'src/app/shared/common';
import { ClearMedicalTrainingErrors, SetUnsavedChanges } from 'src/app/state';
import { FormErrorsComponent } from 'src/app/shared/components/form-errors/form-errors.component';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

@UntilDestroy()
@Component({
  selector: 'abs-fellowship-add-edit-modal',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    InputSelectComponent,
    AutoCompleteModule,
    InputTextModule,
    DropdownModule,
    CalendarModule,
    RadioButtonModule,
    FormErrorsComponent,
  ],
  templateUrl: './fellowship-add-edit-modal.component.html',
  styleUrls: ['./fellowship-add-edit-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class FellowshipAddEditModalComponent implements OnInit, OnDestroy {
  @Select(PicklistsSelectors.slices.fellowshipPrograms) fellowshipPrograms$:
    | Observable<IFellowshipProgramReadOnlyModel[]>
    | undefined;

  @Select(PicklistsSelectors.slices.fellowshipTypes) fellowshipTypes$:
    | Observable<IFellowshipTypeReadOnlyModel[]>
    | undefined;

  fellowshipTypes: IFellowshipTypeReadOnlyModel[] = [];

  @Input() userId!: number;
  @Input() errors$: Observable<IFormErrors> | undefined;
  @Input() isEdit$: Subject<boolean> = new Subject();
  @Input() fellowship$: Subject<IFellowshipReadOnlyModel> = new Subject();
  @Output() cancelDialog: EventEmitter<any> = new EventEmitter();
  @Output() saveDialog: EventEmitter<any> = new EventEmitter();

  clearErrors = new ClearMedicalTrainingErrors();

  fellowshipPrograms: string[] = [];
  filteredFellowshipPrograms: string[] = [];

  year = new Date().getFullYear();
  maxYear: Date = new Date();
  fellowshipId!: number;
  isEdit = false;

  hasUnsavedChanges = false;

  fellowshipForm = new FormGroup({
    fellowshipType: new FormControl(''),
    programName: new FormControl({ value: '', disabled: true }),
    programOther: new FormControl(''),
    completionYear: new FormControl(''),
  });

  constructor(
    private globalDialogService: GlobalDialogService,
    private _store: Store
  ) {
    this._store.dispatch(new GetFellowshipTypes());
  }
  ngOnDestroy(): void {
    this._store.dispatch(new SetUnsavedChanges(false));
  }

  ngOnInit(): void {
    this.maxYear.setFullYear(this.year);
    this.isEdit$.subscribe((isEdit) => {
      this.isEdit = isEdit;
    });

    this.setPicklistData();
    this.subscribeToRowData();

    this.fellowshipForm.valueChanges.subscribe(() => {
      const isDirty = this.fellowshipForm.dirty;
      if (isDirty && !this.hasUnsavedChanges) {
        this.hasUnsavedChanges = true;
        this._store.dispatch(new SetUnsavedChanges(true));
      }
    });

    this.fellowshipPrograms$?.subscribe(
      (fellowshipPrograms: IFellowshipProgramReadOnlyModel[]) => {
        this.fellowshipPrograms = [];
        fellowshipPrograms.forEach((fellowshipProgram) => {
          this.fellowshipPrograms.push(fellowshipProgram.programName);
        });
        this.fellowshipForm.get('programName')?.enable();
      }
    );
  }

  getFellowshipPrograms(fellowshipType: string) {
    this._store.dispatch(new GetFellowshipPrograms(fellowshipType));
  }

  subscribeToRowData() {
    this.fellowship$.subscribe((formData) => {
      if (Object.keys(formData).length > 0) {
        let fellowshipType = '';
        if (formData.programName) {
          fellowshipType = formData.programName.charAt(
            formData.programName.length - 2
          );

          this._store
            .dispatch(new GetFellowshipPrograms(fellowshipType))
            .pipe(untilDestroyed(this))
            .subscribe(() => {
              const programName = this.fellowshipPrograms.find(
                (i) => i === formData.programName.toString()
              );
              this.fellowshipId = formData.id;

              this.fellowshipForm.patchValue({
                fellowshipType,
                programName: programName,
                programOther: formData.programOther,
                completionYear: formData.completionYear.toString(),
              });
            });
        }
      } else {
        this.fellowshipForm.reset();
      }
    });
  }

  setPicklistData() {
    this.fellowshipTypes$?.subscribe(
      (fellowshipTypes: IFellowshipTypeReadOnlyModel[]) => {
        this.fellowshipTypes = fellowshipTypes;
      }
    );
  }

  onInstitutionSelect() {
    this.fellowshipForm.get('programOther')?.patchValue('');
    this.fellowshipForm.get('programOther')?.disable();
  }

  clearAutoComplete() {
    this.fellowshipForm.get('programName')?.patchValue('');
    this.fellowshipForm.get('programOther')?.enable();
  }

  filterItems($event: any) {
    const value = $event.query;
    this.filteredFellowshipPrograms = this.fellowshipPrograms.filter((i) => {
      return i?.toLowerCase().includes(value.toLowerCase());
    });
  }

  cancel() {
    if (this.hasUnsavedChanges) {
      this.globalDialogService
        .showConfirmation('Unsaved Changes', 'Do you want to navigate away')
        .then((result) => {
          if (result) {
            this.cancelDialog.emit({ show: false });
            this._store.dispatch(new SetUnsavedChanges(false));
          }
        });
    } else {
      this.cancelDialog.emit({ show: false });
      this._store.dispatch(new SetUnsavedChanges(false));
    }
  }

  save() {
    this.saveDialog.emit({
      edit: this.isEdit,
      show: false,
      fellowshipForm: this.fellowshipForm.value,
      fellowshipId: this.fellowshipId,
    });
    this.hasUnsavedChanges = false;
    this._store.dispatch(new SetUnsavedChanges(false));
  }
}
