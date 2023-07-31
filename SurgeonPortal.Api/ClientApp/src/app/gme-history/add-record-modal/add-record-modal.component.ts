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
import { RadioButtonModule } from 'primeng/radiobutton';
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
import { IRotationModel, IRotationReadOnlyModel } from 'src/app/api';
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
import {
  validateMaxDuration,
  validateMinDuration,
  validateStartAndEndDates,
} from 'src/app/shared/validators/validators';
import { IAccreditedProgramInstitutionReadOnlyModel } from 'src/app/api/models/picklists/accredited-program-institution-read-only.model';
import { ButtonModule } from 'primeng/button';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

@UntilDestroy()
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
    RadioButtonModule,
    CalendarModule,
    AutoCompleteModule,
    FormErrorsComponent,
    ButtonModule,
  ],
  templateUrl: './add-record-modal.component.html',
  styleUrls: ['./add-record-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AddRecordModalComponent implements OnInit, OnDestroy {
  @Output() closeDialog: EventEmitter<any> = new EventEmitter();
  @Output() relaunchDialog: EventEmitter<any> = new EventEmitter();
  @Input() isEdit$ = new BehaviorSubject<boolean>(false);
  @Input() slectedGmeRotationId$ = new BehaviorSubject<
    { id?: number; nextStart: string; nextEnd?: string } | undefined
  >(undefined);

  @Select(GraduateMedicalEducationSelectors.graduateMedicalEducationList)
  gmeRotations$: Observable<IRotationReadOnlyModel[]> | undefined;

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

  localRotationsList: IRotationReadOnlyModel[] = [];

  selectedRotationSubscription: Subscription | undefined;
  clinicalLevelsSubscription: Subscription | undefined;
  clinicalActivitiesSubscription: Subscription | undefined;
  accreditedInstitutionsSubscription: Subscription | undefined;

  updateActionSubscription: Subscription | undefined;
  createActionSubscription: Subscription | undefined;

  nonClinicalActivities: IClinicalActivityReadOnlyModel[] = [];
  clinicalActivitiesList: IClinicalActivityReadOnlyModel[] = [];

  startDateOverlap: IRotationReadOnlyModel | undefined;
  endDateOverlap: IRotationReadOnlyModel | undefined;

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
      validators: [
        validateStartAndEndDates('startDate', 'endDate'),
        validateMinDuration('startDate', 'endDate', 2),
        validateMaxDuration('startDate', 'endDate', 364),
      ],
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

    this.onChanges();

    this.gmeRotations$?.pipe(untilDestroyed(this)).subscribe((gmeRotations) => {
      this.localRotationsList = gmeRotations;
    });
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
        this.clinicalActivitiesList = clinicalActivities;
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
    this.selectedRotation$?.pipe(untilDestroyed(this)).subscribe((rotation) => {
      if (rotation) {
        this.rotationToEdit = rotation;
        for (const [key, value] of Object.entries(rotation)) {
          let newValue = value;
          if (key === 'startDate' || key === 'endDate') {
            newValue = new Date(value).toLocaleDateString();
          }
          this.addEditRecordsForm.get(key)?.setValue(newValue);
        }
        //this.onChanges();
      } else {
        this.rotationToEdit = undefined;
        //handle if no data is returned or if there was an error
        this.addEditRecordsForm.reset();
        this.addEditRecordsForm.get('isInternationalRotation')?.setValue(false);
        //this.onChanges();
      }
    });

    this.isEdit$.pipe(untilDestroyed(this)).subscribe((isEdit) => {
      this.isEditLocal = isEdit;

      if (!this.isEditLocal) {
        this.addEditRecordsForm.reset();
        this.addEditRecordsForm.get('isInternationalRotation')?.setValue(false);
        //this.onChanges();
      }
    });

    this.slectedGmeRotationId$.pipe(untilDestroyed(this)).subscribe((value) => {
      if (value?.nextStart && value?.nextStart !== '') {
        const startDate = new Date(value.nextStart);
        startDate.setDate(startDate.getDate() + 1);

        this.addEditRecordsForm
          .get('startDate')
          ?.setValue(startDate.toLocaleDateString());
      }
      if (value?.nextEnd && value?.nextEnd !== '') {
        const endDate = new Date(value.nextEnd);
        endDate.setDate(endDate.getDate());

        this.addEditRecordsForm
          .get('endDate')
          ?.setValue(endDate.toLocaleDateString());
      }
    });
  }

  onChanges() {
    let durationInWeeks = 0;
    const otherField = this.addEditRecordFields.find(
      (field) => field.name === 'other'
    );

    const startDateField = this.addEditRecordFields.find(
      (field) => field.name === 'startDate'
    );

    const endDateField = this.addEditRecordFields.find(
      (field) => field.name === 'endDate'
    );

    const calculateWeeks = () => {
      const startDate = this.addEditRecordsForm.get('endDate')?.value
        ? new Date(this.addEditRecordsForm.get('endDate')?.value as string)
        : undefined;

      const endDate = this.addEditRecordsForm.get('startDate')?.value
        ? new Date(this.addEditRecordsForm.get('startDate')?.value as string)
        : undefined;

      if (startDate && endDate) {
        const diffTime = Math.abs(endDate.getTime() - startDate.getTime());
        const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24)) + 1;
        const weeks = diffDays / 7;
        let weeksValue: string | undefined;
        if (weeks >= 1) {
          durationInWeeks = weeks;
          weeksValue = Math.round(weeks).toString();
          this.addEditRecordFields.filter((field) => {
            if (field.name === 'weeks') {
              field.label = 'Week(s)';
            }
          });
        } else {
          durationInWeeks = 0;
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

    const setDurationErrors = () => {
      if (startDateField) {
        if (this.addEditRecordsForm.errors) {
          if (this.addEditRecordsForm.errors['minDurationValid'] === false) {
            startDateField.errorText = 'Rotations must be at least 2 days long';
            startDateField.overlapId = undefined;
          } else if (
            this.addEditRecordsForm.errors['maxDurationValid'] === false
          ) {
            startDateField.errorText =
              'Rotations must be less than 364 days long';
            startDateField.overlapId = undefined;
          } else {
            //startDateField.errorText = undefined;
          }
        } else {
          //startDateField.errorText = undefined;
        }
      }
    };

    const checkForOverlap = () => {
      const startDate = this.addEditRecordsForm.get('startDate')?.value
        ? new Date(this.addEditRecordsForm.get('startDate')?.value as string)
        : undefined;

      const endDate = this.addEditRecordsForm.get('endDate')?.value
        ? new Date(this.addEditRecordsForm.get('endDate')?.value as string)
        : undefined;

      if (startDate) {
        this.startDateOverlap = this.localRotationsList.find((rotation) => {
          // check rotation.id against this.rotationToEdit.id
          if (
            new Date(rotation.startDate) <= startDate &&
            new Date(rotation.endDate) >= startDate &&
            rotation.id !== this.rotationToEdit?.id
          ) {
            return true;
          } else {
            return false;
          }
        });
        if (startDateField) {
          if (this.startDateOverlap) {
            startDateField.errorText =
              'This start date overlaps with an existing rotation';
            startDateField.overlapId = this.startDateOverlap.id;
          } else {
            //startDateField.errorText = undefined;
            startDateField.overlapId = undefined;
          }
        }
      } else {
        if (startDateField) {
          //startDateField.errorText = undefined;
        }
      }

      if (endDate) {
        this.endDateOverlap = this.localRotationsList.find((rotation) => {
          if (
            new Date(rotation.startDate) <= endDate &&
            new Date(rotation.endDate) >= endDate &&
            rotation.id !== this.rotationToEdit?.id
          ) {
            return true;
          } else {
            return false;
          }
        });

        if (endDateField) {
          if (this.endDateOverlap) {
            endDateField.errorText =
              'This end date overlaps with an existing rotation';
            endDateField.overlapId = this.endDateOverlap.id;
          } else {
            endDateField.errorText = undefined;
            endDateField.overlapId = undefined;
          }
        }
      } else {
        if (endDateField) {
          endDateField.errorText = undefined;
        }
      }
    };

    const setClinicalActivityErrors = (
      clinicalLevelId?: number | null,
      clinicalActivityId?: number | null
    ) => {
      const otherFellowshipsText =
        'Please specify which other clinical Fellowships.';
      const durationText =
        'Please explain the reason for this rotation being more than 4 months long.';
      const nonPrimaryText =
        'Please explain the reason for a rotation in a non-primary activity.';
      const generalExplainText = 'Please explain the nature of this rotation.';

      let index = -1;

      const activity = this.clinicalActivitiesList.find(
        (activity) => activity.id === clinicalActivityId
      );
      const helpTextArray: string[] = [];

      // if clinical level = 9, then show other field
      if (clinicalLevelId && clinicalLevelId === 9) {
        if (!helpTextArray.includes(otherFellowshipsText)) {
          helpTextArray.push(
            'Please specify which other clinical Fellowships.'
          );
        }
      } else {
        index = helpTextArray.indexOf(otherFellowshipsText);
        if (index > -1) {
          helpTextArray.splice(index, 1);
        }
      }

      //if clinicalLevelId = 4 or 6 && clinicalActivityId = 5 or 17, then show other field
      if (
        clinicalLevelId &&
        (clinicalLevelId === 4 || clinicalLevelId === 6) &&
        clinicalActivityId &&
        (clinicalActivityId === 5 || clinicalActivityId === 17)
      ) {
        if (!helpTextArray.includes(generalExplainText)) {
          helpTextArray.push(generalExplainText);
        }
      } else {
        index = helpTextArray.indexOf(generalExplainText);
        if (index > -1) {
          helpTextArray.splice(index, 1);
        }
      }

      //if clinicalLevelId = 5 or 7 && duration > 16 weeks, then show other field
      if (
        clinicalLevelId &&
        (clinicalLevelId === 5 || clinicalLevelId === 7) &&
        durationInWeeks >= 16
      ) {
        if (!helpTextArray.includes(durationText)) {
          helpTextArray.push(durationText);
        }
      } else {
        index = helpTextArray.indexOf(durationText);
        if (index > -1) {
          helpTextArray.splice(index, 1);
        }
      }

      //if clinicalLevelId = 5 or 7 and clinical activity is essential === false
      if (
        clinicalLevelId &&
        (clinicalLevelId === 5 || clinicalLevelId === 7) &&
        activity &&
        !activity.isEssential
      ) {
        if (!helpTextArray.includes(nonPrimaryText)) {
          helpTextArray.push(nonPrimaryText);
        }
      } else {
        index = helpTextArray.indexOf(nonPrimaryText);
        if (index > -1) {
          helpTextArray.splice(index, 1);
        }
      }

      // if we found any of the above enable and require the explain field and display the help text
      if (helpTextArray.length > 0) {
        if (otherField) {
          otherField.helpTextArray = helpTextArray;
        }
        this.addEditRecordsForm.get('other')?.enable();
        this.addEditRecordsForm
          .get('other')
          ?.setValidators([Validators.required]);
      } else {
        if (otherField) {
          otherField.helpTextArray = undefined;
        }

        this.addEditRecordsForm.get('other')?.setValue('');
        this.addEditRecordsForm.get('other')?.disable();
        this.addEditRecordsForm.get('other')?.setValidators([]);
      }
    };

    this.addEditRecordsForm
      .get('endDate')
      ?.valueChanges.pipe(untilDestroyed(this))
      .subscribe((val) => {
        this.addEditRecordFields.filter((field) => {
          if (field.name === 'startDate') {
            if (val) {
              field.validators.maxDate = new Date(val);
            } else {
              field.validators.maxDate = null;
            }
          }
        });

        if (startDateField) {
          startDateField.errorText = undefined;
        }

        calculateWeeks();
        checkForOverlap();
        setDurationErrors();

        const clinicalActivityId =
          this.addEditRecordsForm.get('clinicalActivityId')?.value;

        const clinicalLevelId =
          this.addEditRecordsForm.get('clinicalLevelId')?.value;

        if (clinicalActivityId || clinicalLevelId) {
          setClinicalActivityErrors(clinicalLevelId, clinicalActivityId);
        }
      });

    this.addEditRecordsForm
      .get('startDate')
      ?.valueChanges.pipe(untilDestroyed(this))
      .subscribe((val) => {
        this.addEditRecordFields.filter((field) => {
          if (field.name === 'endDate') {
            if (val) {
              field.validators.minDate = new Date(val);
            } else {
              field.validators.minDate = null;
            }
          }
        });

        calculateWeeks();
        checkForOverlap();
        setDurationErrors();

        const clinicalActivityId =
          this.addEditRecordsForm.get('clinicalActivityId')?.value;

        const clinicalLevelId =
          this.addEditRecordsForm.get('clinicalLevelId')?.value;

        if (clinicalActivityId || clinicalLevelId) {
          setClinicalActivityErrors(clinicalLevelId, clinicalActivityId);
        }
      });

    this.addEditRecordsForm
      .get('clinicalLevelId')
      ?.valueChanges.pipe(untilDestroyed(this))
      .subscribe((val) => {
        const clinicalActivityId =
          this.addEditRecordsForm.get('clinicalActivityId')?.value;

        setClinicalActivityErrors(val, clinicalActivityId);
      });

    this.addEditRecordsForm
      .get('clinicalActivityId')
      ?.valueChanges.pipe(untilDestroyed(this))
      .subscribe((val) => {
        const clinicalLevelId =
          this.addEditRecordsForm.get('clinicalLevelId')?.value;

        setClinicalActivityErrors(clinicalLevelId, val);

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

    this.addEditRecordsForm
      .get('programName')
      ?.valueChanges.pipe(untilDestroyed(this))
      .subscribe((val) => {
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
      ?.valueChanges.pipe(untilDestroyed(this))
      .subscribe((val) => {
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
          console.log(res.graduateMedicalEducation?.errors);
          if (!res.graduateMedicalEducation?.errors) {
            this.close();
          }
        });
    }
  }

  changeModalData(id: number) {
    this.relaunchDialog.emit(id);
  }

  close() {
    this.closeDialog.emit();
  }
}
