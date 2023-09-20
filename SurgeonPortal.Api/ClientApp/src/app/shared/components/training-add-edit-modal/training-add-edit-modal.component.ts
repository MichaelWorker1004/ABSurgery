import {
  Component,
  CUSTOM_ELEMENTS_SCHEMA,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Observable, Subject } from 'rxjs';
import {
  GetAccreditedProgramInstitutionsList,
  GetStateList,
  IPickListItem,
  PicklistsSelectors,
} from 'src/app/state/picklists';
import { IStateReadOnlyModel } from 'src/app/api';
import { Select, Store } from '@ngxs/store';
import { InputSelectComponent } from '../../../shared/components/base-input/input-select.component';
import { IAccreditedProgramInstitutionReadOnlyModel } from 'src/app/api/models/picklists/accredited-program-institution-read-only.model';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { CalendarModule } from 'primeng/calendar';
import { ITrainingTypeReadOnlyModel } from 'src/app/api/models/picklists/training-type-read-only.model';
import { IAdvancedTrainingModel } from 'src/app/api/models/medicaltraining/advanced-training.model';
import { validateStartAndEndDates } from '../../validators/validators';
import { GlobalDialogService } from '../../services/global-dialog.service';
import { ClearMedicalTrainingErrors, SetUnsavedChanges } from 'src/app/state';
import { IFormErrors } from '../../common';
import { FormErrorsComponent } from '../form-errors/form-errors.component';

@Component({
  selector: 'abs-training-add-edit-modal',
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
    FormErrorsComponent,
  ],
  templateUrl: './training-add-edit-modal.component.html',
  styleUrls: ['./training-add-edit-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class TrainingAddEditModalComponent implements OnInit {
  @Input() training$: Subject<IAdvancedTrainingModel> = new Subject();
  @Input() isEdit$: Subject<boolean> = new Subject();
  @Input() userId!: number;
  @Input() errors$: Observable<IFormErrors> | undefined;
  @Output() cancelDialog: EventEmitter<any> = new EventEmitter();
  @Output() saveDialog: EventEmitter<any> = new EventEmitter();

  @Select(PicklistsSelectors.slices.states) states$:
    | Observable<IStateReadOnlyModel[]>
    | undefined;

  @Select(PicklistsSelectors.slices.accreditedInstitutions)
  accreditedInstitutions$:
    | Observable<IAccreditedProgramInstitutionReadOnlyModel[]>
    | undefined;

  @Select(PicklistsSelectors.slices.trainingTypes) trainingTypes$:
    | Observable<ITrainingTypeReadOnlyModel[]>
    | undefined;

  clearErrors = new ClearMedicalTrainingErrors();

  hasUnsavedChanges = false;

  additionalTrainingForm = new FormGroup(
    {
      trainingType: new FormControl(0, Validators.required),
      startDate: new FormControl(new Date(), Validators.required),
      endDate: new FormControl(new Date(), Validators.required),
      institutionName: new FormControl({ itemDescription: '', itemValue: '' }),
      state: new FormControl({ value: '', disabled: true }),
      city: new FormControl({ value: '', disabled: true }),
      other: new FormControl(''),
    },
    {
      validators: [validateStartAndEndDates('startDate', 'endDate')],
    }
  );

  stateOptions: IPickListItem[] = [];
  institutionOptions: IPickListItem[] = [];
  accreditedInstitutions: IAccreditedProgramInstitutionReadOnlyModel[] = [];
  trainingTypes: ITrainingTypeReadOnlyModel[] = [];
  filteredInstitutionOptions: IPickListItem[] = [];
  trainingId!: number | undefined;
  isEdit!: boolean;

  constructor(
    private _store: Store,
    private globalDialogService: GlobalDialogService
  ) {
    this._store.dispatch(new GetStateList('500'));
    this._store.dispatch(new GetAccreditedProgramInstitutionsList());
    this.states$?.subscribe((value) => {
      this.stateOptions = value;
    });
  }

  ngOnInit() {
    this.setPicklistOptions();
    this.isEdit$.subscribe((isEdit) => {
      this.isEdit = isEdit;
    });
    this.additionalTrainingForm.valueChanges.subscribe(() => {
      const isDirty = this.additionalTrainingForm.dirty;
      if (isDirty && !this.hasUnsavedChanges) {
        this.hasUnsavedChanges = true;
        this._store.dispatch(new SetUnsavedChanges(true));
      }
    });
    this.subscribeToTraining();
  }

  subscribeToTraining() {
    this.training$.subscribe((formData) => {
      if (Object.keys(formData).length > 0) {
        this.trainingId = formData.id;
        for (const [key, value] of Object.entries(formData)) {
          this.additionalTrainingForm.get(key)?.patchValue(value);
          if (key === 'institutionName') {
            this.institutionOptions.filter((inst) => {
              if (inst.itemDescription === value) {
                this.additionalTrainingForm.get(key)?.patchValue({
                  itemDescription: inst.itemDescription ?? '',
                  itemValue: inst.itemValue ?? '',
                });
              }
            });
          }
          if (key === 'trainingType') {
            this.trainingTypes.filter((training) => {
              if (training.trainingType === value) {
                this.additionalTrainingForm.get(key)?.patchValue(training.id);
              }
            });
          }
          if (key === 'other') {
            if (value.length > 0) {
              this.clearAutoComplete();
            }
          }
        }

        this.additionalTrainingForm
          .get('startDate')
          ?.patchValue(new Date(formData.startDate ?? ''));

        this.additionalTrainingForm
          .get('endDate')
          ?.patchValue(new Date(formData.endDate ?? ''));
      } else {
        this.additionalTrainingForm.reset();
      }
    });
  }

  clearAutoComplete() {
    this.additionalTrainingForm.get('institutionName')?.patchValue(null);
    this.additionalTrainingForm.get('city')?.patchValue('');
    this.additionalTrainingForm.get('state')?.patchValue('');
  }

  get typeOfTraining() {
    return this.additionalTrainingForm.get('typeOfTraining');
  }

  setPicklistOptions() {
    this.accreditedInstitutions$?.subscribe((insitutions) => {
      this.accreditedInstitutions = insitutions;
      insitutions.forEach((insitution) => {
        this.institutionOptions.push({
          itemValue: insitution.programId.toString(),
          itemDescription: insitution.institutionName,
        });
      });
    });

    this.trainingTypes$?.subscribe((training) => {
      this.trainingTypes = training;
    });
  }

  handleDefaultClose(event: any) {
    event.preventDefault();
  }

  onInstitutionSelect(selected: any) {
    this.additionalTrainingForm.get('other')?.patchValue('');
    this.accreditedInstitutions.filter((i) => {
      if (i.programId === +selected.itemValue) {
        this.additionalTrainingForm.patchValue({
          state: i.state,
          city: i.city,
        });
      }
    });
  }

  filterItems($event: any) {
    const value = $event.query;
    this.filteredInstitutionOptions = this.institutionOptions.filter((i) => {
      return i.itemDescription?.toLowerCase().includes(value.toLowerCase());
    });
  }

  cancel() {
    if (this.hasUnsavedChanges) {
      this.globalDialogService
        .showConfirmation('Unsaved Changes', 'Do you want to navigate away')
        .then((result) => {
          if (result) {
            this._store.dispatch(new SetUnsavedChanges(false));
            this.cancelDialog.emit({ show: false });
          }
        });
    } else {
      this._store.dispatch(new SetUnsavedChanges(false));
      this.cancelDialog.emit({ show: false });
    }
  }

  save() {
    this.hasUnsavedChanges = false;
    this._store.dispatch(new SetUnsavedChanges(false));
    this.saveDialog.emit({
      edit: this.isEdit,
      show: false,
      trainingRecord: this.additionalTrainingForm.value,
      trainingId: this.trainingId,
    });
  }

  trackByFn(
    index: number,
    item: IPickListItem
  ): string | number | null | undefined {
    return item.itemValue;
  }
}
