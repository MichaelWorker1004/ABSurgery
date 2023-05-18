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
  Validators,
} from '@angular/forms';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import { InputSelectComponent } from 'src/app/shared/components/base-input/input-select.component';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { CalendarModule } from 'primeng/calendar';
import { IFormFields } from 'src/app/shared/models/form-fields/form-fields';
import { ADD_EDIT_RECORD_FIELDS } from './add-record-form-fields';
import { FormErrorsComponent } from '../../shared/components/form-errors/form-errors.component';

import {
  GraduateMedicalEducationSelectors,
  GetGraduateMedicalEducationDetails,
  UpdateGraduateMedicalEducation,
  CreateGraduateMedicalEducation,
  ClearGraduateMedicalEducationErrors,
} from '../../state';
import { Select, Store } from '@ngxs/store';
import { IRotationModel } from 'src/app/api';
import {
  GetClinicalLevelList,
  GetClinicalActivityList,
  PicklistsSelectors,
  GetAccreditedProgramInstitutionsList,
} from '../../state/picklists';
import {
  IClinicalLevelReadOnlyModel,
  IClinicalActivityReadOnlyModel,
} from '../../api';
import { validateStartAndEndDates } from 'src/app/shared/validators/validators';
import { IAccreditedProgramInstitutionReadOnlyModel } from 'src/app/api/models/picklists/accredited-program-institution-read-only.model';

@Component({
  selector: 'abs-add-record-modal',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    InputSelectComponent,
    InputTextModule,
    DropdownModule,
    InputTextareaModule,
    CalendarModule,
    AutoCompleteModule,
    FormErrorsComponent,
  ],
  templateUrl: './add-record-modal.component.html',
  styleUrls: ['./add-record-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AddRecordModalComponent implements OnInit, OnDestroy {
  @Output() closeDialog: EventEmitter<any> = new EventEmitter();
  @Input() isEdit$ = new BehaviorSubject<boolean>(false);
  @Input() slectedGmeRotationId$ = new BehaviorSubject<number | undefined>(
    undefined
  );

  @Select(GraduateMedicalEducationSelectors.graduateMedicalEducationDetails)
  selectedRotation$: Observable<IRotationModel> | undefined;

  @Select(GraduateMedicalEducationSelectors.errors) errors$:
    | Observable<any>
    | undefined;

  @Select(PicklistsSelectors.slices.clinicalLevels) clinicalLevels$:
    | Observable<IClinicalLevelReadOnlyModel[]>
    | undefined;
  @Select(PicklistsSelectors.slices.clinicalActivities) clinicalActivities$:
    | Observable<IClinicalActivityReadOnlyModel[]>
    | undefined;
  @Select(PicklistsSelectors.slices.accreditedInstitutions)
  accreditedInstitutions$:
    | Observable<IAccreditedProgramInstitutionReadOnlyModel[]>
    | undefined;

  clearErrors = new ClearGraduateMedicalEducationErrors();

  selectedRotationSubscription: Subscription | undefined;
  clinicalLevelsSubscription: Subscription | undefined;
  clinicalActivitiesSubscription: Subscription | undefined;
  accreditedInstitutionsSubscription: Subscription | undefined;

  updateActionSubscription: Subscription | undefined;
  createActionSubscription: Subscription | undefined;

  nonClinicalActivities: IClinicalActivityReadOnlyModel[] = [];

  addEditRecordFields: IFormFields[] = ADD_EDIT_RECORD_FIELDS;
  isEditLocal = false;

  rotationToEdit: IRotationModel | undefined;

  addEditRecordsForm = new FormGroup(
    {
      endDate: new FormControl('', [Validators.required]),
      startDate: new FormControl('', [Validators.required]),
      weeks: new FormControl(''),
      programName: new FormControl('', [Validators.required]),
      alternateInstitutionName: new FormControl('', [Validators.required]),
      clinicalLevelId: new FormControl(null, [Validators.required]),
      clinicalActivityId: new FormControl(null, [Validators.required]),
      other: new FormControl({ value: '', disabled: true }, [
        Validators.required,
      ]),
      nonSurgicalActivity: new FormControl({ value: '', disabled: true }, [
        Validators.required,
      ]),
      isInternationalRotation: new FormControl(false, [Validators.required]),
    },
    {
      validators: [validateStartAndEndDates('startDate', 'endDate')],
    }
  );

  constructor(private _store: Store) {}
  ngOnDestroy(): void {
    this.selectedRotationSubscription?.unsubscribe();
    this.clinicalLevelsSubscription?.unsubscribe();
    this.clinicalActivitiesSubscription?.unsubscribe();
    this.accreditedInstitutionsSubscription?.unsubscribe();

    this.updateActionSubscription?.unsubscribe();
    this.createActionSubscription?.unsubscribe();
  }

  ngOnInit() {
    this.fetchDropdownData();
    this.fetchFormData();
  }

  fetchDropdownData() {
    this._store.dispatch(new GetClinicalLevelList());
    this._store.dispatch(new GetClinicalActivityList());
    this._store.dispatch(new GetAccreditedProgramInstitutionsList());

    this.accreditedInstitutionsSubscription =
      this.accreditedInstitutions$?.subscribe((accreditedInstitutions) => {
        if (accreditedInstitutions) {
          this.addEditRecordFields.filter((field) => {
            if (field.name === 'programName') {
              field.options = accreditedInstitutions.map(
                (institution) => institution.institutionName
              );
            }
          });
        }
      });

    this.clinicalLevelsSubscription = this.clinicalLevels$?.subscribe(
      (clinicalLevels) => {
        if (clinicalLevels) {
          this.addEditRecordFields.filter((field) => {
            if (field.name === 'clinicalLevelId') {
              field.options = clinicalLevels.map((level) => {
                return {
                  label: level.name,
                  value: level.id,
                };
              });
            }
          });
        }
      }
    );
    this.clinicalActivitiesSubscription = this.clinicalActivities$?.subscribe(
      (clinicalActivities) => {
        if (clinicalActivities) {
          this.nonClinicalActivities = clinicalActivities.filter((activity) => {
            if (activity.name.includes('Non-Clinical')) {
              return true;
            } else {
              return false;
            }
          });
          const essentialActivities = clinicalActivities.filter((activity) => {
            return activity.isEssential;
          });
          const otherActivities = clinicalActivities.filter((activity) => {
            return !activity.isEssential;
          });

          this.addEditRecordFields.filter((field) => {
            if (field.name === 'clinicalActivityId') {
              field.options = [
                {
                  label: 'Essential Activities',
                  items: essentialActivities.map((activity) => {
                    return {
                      label: activity.name,
                      value: activity.id,
                    };
                  }),
                },
                {
                  label: 'Other Activities',
                  items: otherActivities.map((activity) => {
                    return {
                      label: activity.name,
                      value: activity.id,
                    };
                  }),
                },
              ];
            }
          });
        }
      }
    );
  }

  fetchFormData() {
    this.selectedRotation$?.subscribe((rotation) => {
      if (rotation) {
        this.rotationToEdit = rotation;
        for (const [key, value] of Object.entries(rotation)) {
          let newValue = value;
          if (key === 'startDate' || key === 'endDate') {
            newValue = new Date(value).toLocaleDateString();
          }
          this.addEditRecordsForm.get(key)?.setValue(newValue);
        }
        this.onChanges();
      } else {
        //handle if no data is returned or if there was an error
        this.addEditRecordsForm.reset();
        this.addEditRecordsForm.get('isInternationalRotation')?.setValue(false);
        this.onChanges();
      }
    });

    this.isEdit$.subscribe((isEdit) => {
      this.isEditLocal = isEdit;
      if (isEdit) {
        this.slectedGmeRotationId$.subscribe((rotationId) => {
          if (rotationId) {
            this._store.dispatch(
              new GetGraduateMedicalEducationDetails(rotationId)
            );
          }
        });
      } else {
        this.addEditRecordsForm.reset();
        this.addEditRecordsForm.get('isInternationalRotation')?.setValue(false);
        this.onChanges();
      }
    });
  }

  onChanges() {
    const calculateWeeks = () => {
      const startDate = this.addEditRecordsForm.get('endDate')?.value
        ? new Date(this.addEditRecordsForm.get('endDate')?.value as string)
        : undefined;

      const endDate = this.addEditRecordsForm.get('startDate')?.value
        ? new Date(this.addEditRecordsForm.get('startDate')?.value as string)
        : undefined;

      if (startDate && endDate) {
        const diffTime = Math.abs(endDate.getTime() - startDate.getTime());
        const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
        const weeks = diffDays / 7;
        let weeksValue: string | undefined;
        if (weeks >= 1) {
          weeksValue = Math.round(weeks).toString();
          this.addEditRecordFields.filter((field) => {
            if (field.name === 'weeks') {
              field.label = 'Week(s)';
            }
          });
        } else {
          this.addEditRecordFields.filter((field) => {
            if (field.name === 'weeks') {
              field.label = 'Day(s)';
            }
          });
          weeksValue = diffDays.toString();
        }

        this.addEditRecordsForm.get('weeks')?.setValue(weeksValue.toString());
      }
    };

    this.addEditRecordsForm.get('endDate')?.valueChanges.subscribe(() => {
      calculateWeeks();
    });

    this.addEditRecordsForm.get('startDate')?.valueChanges.subscribe(() => {
      calculateWeeks();
    });

    this.addEditRecordsForm
      .get('clinicalLevelId')
      ?.valueChanges.subscribe((val) => {
        if (val && val === 9) {
          this.addEditRecordsForm.get('other')?.enable();
          this.addEditRecordsForm
            .get('other')
            ?.setValidators([Validators.required]);
        } else {
          this.addEditRecordsForm.get('other')?.setValue('');
          this.addEditRecordsForm.get('other')?.disable();
          this.addEditRecordsForm.get('other')?.setValidators([]);
        }
      });

    this.addEditRecordsForm
      .get('clinicalActivityId')
      ?.valueChanges.subscribe((val) => {
        if (val) {
          const activity = this.nonClinicalActivities.find(
            (activity) => activity.id === val
          );
          if (activity) {
            this.addEditRecordsForm.get('nonSurgicalActivity')?.enable();
            this.addEditRecordsForm
              .get('nonSurgicalActivity')
              ?.setValidators([Validators.required]);
          } else {
            this.addEditRecordsForm.get('nonSurgicalActivity')?.setValue('');
            this.addEditRecordsForm.get('nonSurgicalActivity')?.disable();
            this.addEditRecordsForm
              .get('nonSurgicalActivity')
              ?.setValidators([]);
          }
        }
      });

    this.addEditRecordsForm.get('startDate')?.valueChanges.subscribe((val) => {
      this.addEditRecordFields.filter((field) => {
        if (field.name === 'endDate') {
          if (val) {
            field.validators.minDate = new Date(val);
          } else {
            field.validators.minDate = null;
          }
        }
      });
    });
    this.addEditRecordsForm.get('endDate')?.valueChanges.subscribe((val) => {
      this.addEditRecordFields.filter((field) => {
        if (field.name === 'startDate') {
          if (val) {
            field.validators.maxDate = new Date(val);
          } else {
            field.validators.maxDate = null;
          }
        }
      });
    });

    this.addEditRecordsForm
      .get('programName')
      ?.valueChanges.subscribe((val) => {
        if (val) {
          this.addEditRecordsForm.get('alternateInstitutionName')?.setValue('');
          this.addEditRecordsForm.get('alternateInstitutionName')?.disable();
          this.addEditRecordsForm
            .get('alternateInstitutionName')
            ?.setValidators([]);
        } else {
          this.addEditRecordsForm.get('alternateInstitutionName')?.enable();
          this.addEditRecordsForm
            .get('alternateInstitutionName')
            ?.setValidators([Validators.required]);
        }
      });
    this.addEditRecordsForm
      .get('alternateInstitutionName')
      ?.valueChanges.subscribe((val) => {
        if (val) {
          this.addEditRecordsForm.get('programName')?.setValidators([]);
        } else {
          this.addEditRecordsForm
            .get('programName')
            ?.setValidators([Validators.required]);
        }
      });
  }

  filterItems($event: any, formField: IFormFields) {
    const value = $event.query;
    formField.filteredOptions = formField.options?.filter((i) => {
      return i?.toLowerCase().includes(value.toLowerCase());
    });
  }

  onSubmit() {
    const formValues = this.addEditRecordsForm.getRawValue();
    let startDate = '';
    let endDate = '';
    if (formValues.startDate) {
      startDate = new Date(formValues.startDate).toISOString();
    }
    if (formValues.endDate) {
      endDate = new Date(formValues.endDate).toISOString();
    }

    const newRotation = {
      id: this.rotationToEdit?.id || 0,
      startDate: startDate,
      endDate: endDate,
      clinicalLevelId: formValues.clinicalLevelId ?? 0,
      clinicalActivityId: formValues.clinicalActivityId || 0,
      programName: formValues.programName ?? '',
      nonSurgicalActivity: formValues.nonSurgicalActivity ?? '',
      alternateInstitutionName: formValues.alternateInstitutionName ?? '',
      isInternationalRotation: formValues.isInternationalRotation ?? false,
      other: formValues.other ?? '',
    } as unknown as IRotationModel;

    if (this.isEditLocal) {
      this.updateActionSubscription = this._store
        .dispatch(new UpdateGraduateMedicalEducation(newRotation))
        .subscribe((res) => {
          if (!res.graduateMedicalEducation?.errors) {
            this.close();
          }
        });
    } else {
      this.createActionSubscription = this._store
        .dispatch(new CreateGraduateMedicalEducation(newRotation))
        .subscribe((res) => {
          if (!res.graduateMedicalEducation?.errors) {
            this.close();
          }
        });
    }
  }

  close() {
    this.closeDialog.emit();
  }
}
