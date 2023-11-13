import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  Input,
  Output,
  OnInit,
  SimpleChanges,
  OnChanges,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Observable, pairwise } from 'rxjs';

import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { CheckboxModule } from 'primeng/checkbox';

import { RadioButtonModule } from 'primeng/radiobutton';
import { CalendarModule } from 'primeng/calendar';
import {
  validateMaxDuration,
  validateMinDuration,
  validateStartAndEndDates,
} from 'src/app/shared/validators/validators';
import { IRotationReadOnlyModel } from 'src/app/api';
import { Select } from '@ngxs/store';
import { GraduateMedicalEducationSelectors } from 'src/app/state';
import { FormErrorsComponent } from '../../shared/components/form-errors/form-errors.component';

interface OptionList {
  clinicalLevelOptions: any[];

  isClinicalActivityOptions: any[];

  clinicalActivityOptions: any[];
}

@UntilDestroy()
@Component({
  selector: 'abs-gme-form',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ButtonModule,
    InputTextModule,
    DropdownModule,
    InputTextareaModule,
    CheckboxModule,
    RadioButtonModule,
    CalendarModule,
    FormErrorsComponent,
  ],
  templateUrl: './gme-form.component.html',
  styleUrls: ['./gme-form.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class GmeFormComponent implements OnInit, OnChanges {
  @Output() cancelForm: EventEmitter<any> = new EventEmitter();
  @Output() saveForm: EventEmitter<any> = new EventEmitter();
  @Output() relaunchDialog: EventEmitter<any> = new EventEmitter();
  @Input() formData: any;
  @Input() isEdit = false;
  @Input() picklists: any;
  @Input() errors$?: Observable<any> | undefined;
  @Input() clearErrors?: any;

  @Select(GraduateMedicalEducationSelectors.graduateMedicalEducationList)
  gmeRotations$: Observable<IRotationReadOnlyModel[]> | undefined;

  localRotationsList: IRotationReadOnlyModel[] = [];

  optionLists: OptionList = {
    clinicalLevelOptions: [],

    isClinicalActivityOptions: [
      { label: 'Clinical', value: true },
      { label: 'Non-Clinical', value: false },
    ],
    clinicalActivityOptions: [],
  };

  originalFormValues: any;
  localEdit = false;

  gemRotationForm = new FormGroup(
    {
      programName: new FormControl({ value: '', disabled: false }),
      clinicalLevelId: new FormControl<number | null>(
        { value: null, disabled: false },
        [Validators.required]
      ),
      startDate: new FormControl({ value: '', disabled: false }, [
        Validators.required,
      ]),
      endDate: new FormControl({ value: '', disabled: false }, [
        Validators.required,
      ]),
      weeks: new FormControl({ value: '', disabled: false }),
      usingAffiliateOrganization: new FormControl({
        value: false,
        disabled: false,
      }),
      alternateInstitutionName: new FormControl({ value: '', disabled: false }),
      isClinicalActivity: new FormControl({ value: true, disabled: false }, [
        Validators.required,
      ]),
      clinicalActivityId: new FormControl({ value: null, disabled: false }, [
        Validators.required,
      ]),
      other: new FormControl({ value: '', disabled: false }),
      nonPrimaryExplain: new FormControl({ value: '', disabled: false }), //nonPrimary
      fourMonthRotationExplain: new FormControl({ value: '', disabled: false }), //duration
      nonClinicalExplain: new FormControl({ value: '', disabled: false }), //non-surgical
      nonSurgicalActivity: new FormControl({ value: '', disabled: true }),
      isInternationalRotation: new FormControl({
        value: false,
        disabled: false,
      }),
    },
    {
      validators: [
        validateStartAndEndDates('startDate', 'endDate'),
        validateMinDuration('startDate', 'endDate', 2),
        validateMaxDuration('startDate', 'endDate', 364),
      ],
    }
  );

  /* Toggle variables */
  weeksLabel = 'Weeks';
  startDateOptions: any = {
    maxDate: null,
    errorText: undefined,
    overlapId: undefined,
  };
  endDateOptions: any = {
    minDate: null,
    errorText: undefined,
    overlapId: undefined,
  };
  displaySurgicalDescription = false;
  displayExplainFellowship = false;
  displayExplainNonPrimary = false;
  displayExplainDuration = false;
  displayExplainNonSurgical = false;

  startDateOverlap: IRotationReadOnlyModel | undefined;
  endDateOverlap: IRotationReadOnlyModel | undefined;

  clinicalActivities: any[] = [];
  nonClinicalActivities: any[] = [];

  groupedClinicalActivities: any[] = [];

  ngOnInit() {
    this.optionLists = { ...this.optionLists, ...this.picklists };

    this.originalFormValues = this.formData;
    this.setFormValues(this.originalFormValues);
    this.onFormChanges();

    this.gmeRotations$?.pipe(untilDestroyed(this)).subscribe((gmeRotations) => {
      this.localRotationsList = gmeRotations;
    });
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['formData']) {
      this.originalFormValues = changes['formData'].currentValue;
      this.setFormValues(this.originalFormValues);
    }
    if (changes['isEdit']) {
      this.localEdit = changes['isEdit'].currentValue;
    }
    if (changes['picklists']) {
      this.optionLists = {
        ...this.optionLists,
        ...changes['picklists'].currentValue,
      };
      this.clinicalActivities = this.optionLists.clinicalActivityOptions.filter(
        (activity) => {
          if (activity.name.includes('Non-Clinical')) {
            return false;
          } else {
            return true;
          }
        }
      );
      this.nonClinicalActivities =
        this.optionLists.clinicalActivityOptions.filter((activity) => {
          if (activity.name.includes('Non-Clinical')) {
            return true;
          } else {
            return false;
          }
        });
      const essentialActivities = this.clinicalActivities.filter((activity) => {
        return activity.isEssential;
      });
      const otherActivities = this.clinicalActivities.filter((activity) => {
        return !activity.isEssential;
      });

      this.groupedClinicalActivities = [
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
          label: 'Non-Essential Activities',
          items: otherActivities.map((activity) => {
            return {
              label: activity.name,
              value: activity.id,
            };
          }),
        },
      ];
    }
  }

  setFormValues(data: any) {
    this.gemRotationForm.reset();
    if (data) {
      for (const [key, value] of Object.entries(data)) {
        let newValue = value;
        if (key.includes('Date')) {
          newValue = new Date(value as any).toLocaleDateString();
        }
        this.gemRotationForm.get(key)?.setValue(newValue);
      }
    }
  }

  onFormChanges() {
    let durationInWeeks = 0;

    const calculateWeeks = () => {
      const startDate = this.gemRotationForm.get('endDate')?.value
        ? new Date(this.gemRotationForm.get('endDate')?.value as string)
        : undefined;

      const endDate = this.gemRotationForm.get('startDate')?.value
        ? new Date(this.gemRotationForm.get('startDate')?.value as string)
        : undefined;

      if (startDate && endDate) {
        const diffTime = Math.abs(endDate.getTime() - startDate.getTime());
        const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24)) + 1;
        const weeks = diffDays / 7;
        let weeksValue: string | undefined;
        if (weeks >= 1) {
          durationInWeeks = weeks;
          weeksValue = Math.round(weeks).toString();
          this.weeksLabel = 'Weeks';
        } else {
          durationInWeeks = 0;
          this.weeksLabel = 'Day(s)';
          weeksValue = diffDays.toString();
        }

        this.gemRotationForm.get('weeks')?.setValue(weeksValue.toString());
      }
    };

    const setDurationErrors = () => {
      if (this.gemRotationForm.errors) {
        if (this.gemRotationForm.errors['minDurationValid'] === false) {
          this.startDateOptions.errorText =
            'Rotations must be at least 2 days long';
          this.startDateOptions.overlapId = undefined;
        } else if (this.gemRotationForm.errors['maxDurationValid'] === false) {
          this.startDateOptions.errorText =
            'Rotations must be less than 364 days long';
          this.startDateOptions.overlapId = undefined;
        } else {
          //this.startDateOptions.errorText = undefined;
        }
      } else {
        //this.startDateOptions.errorText = undefined;
      }
    };

    const checkForOverlap = () => {
      const startDate = this.gemRotationForm.get('startDate')?.value
        ? new Date(this.gemRotationForm.get('startDate')?.value as string)
        : undefined;

      const endDate = this.gemRotationForm.get('endDate')?.value
        ? new Date(this.gemRotationForm.get('endDate')?.value as string)
        : undefined;

      if (startDate) {
        this.startDateOverlap = this.localRotationsList.find((rotation) => {
          // check rotation.id against this.originalFormValues.id
          if (
            new Date(rotation.startDate) <= startDate &&
            new Date(rotation.endDate) >= startDate &&
            // rotation.id !== this.rotationToEdit?.id
            rotation.id !== this.originalFormValues?.id
          ) {
            return true;
          } else {
            return false;
          }
        });
        if (this.startDateOverlap) {
          this.startDateOptions.errorText =
            'This start date overlaps with an existing rotation';
          this.startDateOptions.overlapId = this.startDateOverlap.id;
        } else {
          this.startDateOptions.errorText = undefined;
          this.startDateOptions.overlapId = undefined;
        }
      } else {
        //this.startDateOptions.errorText = undefined;
      }

      if (endDate) {
        this.endDateOverlap = this.localRotationsList.find((rotation) => {
          if (
            new Date(rotation.startDate) <= endDate &&
            new Date(rotation.endDate) >= endDate &&
            // rotation.id !== this.rotationToEdit?.id
            rotation.id !== this.originalFormValues?.id
          ) {
            return true;
          } else {
            return false;
          }
        });

        if (this.endDateOverlap) {
          this.endDateOptions.errorText =
            'This end date overlaps with an existing rotation';
          this.endDateOptions.overlapId = this.endDateOverlap.id;
        } else {
          this.endDateOptions.errorText = undefined;
          this.endDateOptions.overlapId = undefined;
        }
      } else {
        this.endDateOptions.errorText = undefined;
      }
    };

    const setClinicalActivityErrors = (
      clinicalLevelId?: number | null,
      clinicalActivityId?: number | null
    ) => {
      const activity = this.optionLists.clinicalActivityOptions.find(
        (activity) => activity.id === clinicalActivityId
      );

      // if clinical level = 9, then show other field
      if (clinicalLevelId && clinicalLevelId === 9) {
        this.displayExplainFellowship = true;
        this.gemRotationForm.get('other')?.enable();
        this.gemRotationForm.get('other')?.setValidators([Validators.required]);
      } else {
        this.displayExplainFellowship = false;
        this.gemRotationForm.get('other')?.setValue('');
        this.gemRotationForm.get('other')?.disable();
        this.gemRotationForm.get('other')?.setValidators([]);
      }

      //if clinicalLevelId = 4 or 6 && clinicalActivityId = 5 or 17, then show other field
      if (
        clinicalLevelId &&
        (clinicalLevelId === 4 || clinicalLevelId === 6) &&
        clinicalActivityId &&
        (clinicalActivityId === 5 || clinicalActivityId === 17)
      ) {
        this.displayExplainNonSurgical = true;
        this.gemRotationForm.get('nonClinicalExplain')?.enable();
        this.gemRotationForm
          .get('nonClinicalExplain')
          ?.setValidators([Validators.required]);
      } else {
        this.displayExplainNonSurgical = false;
        this.gemRotationForm.get('nonClinicalExplain')?.setValue('');
        this.gemRotationForm.get('nonClinicalExplain')?.disable();
        this.gemRotationForm.get('nonClinicalExplain')?.setValidators([]);
      }

      //if clinicalLevelId = 5 or 7 && duration > 16 weeks, then show other field
      if (
        clinicalLevelId &&
        (clinicalLevelId === 5 || clinicalLevelId === 7) &&
        durationInWeeks >= 17
      ) {
        this.displayExplainDuration = true;
        this.gemRotationForm.get('fourMonthRotationExplain')?.enable();
        this.gemRotationForm
          .get('fourMonthRotationExplain')
          ?.setValidators([Validators.required]);
      } else {
        this.displayExplainDuration = false;
        this.gemRotationForm.get('fourMonthRotationExplain')?.setValue('');
        this.gemRotationForm.get('fourMonthRotationExplain')?.disable();
        this.gemRotationForm.get('fourMonthRotationExplain')?.setValidators([]);
      }

      //if clinicalLevelId = 5 or 7 and clinical activity is essential === false
      if (
        clinicalLevelId &&
        (clinicalLevelId === 5 || clinicalLevelId === 7) &&
        activity &&
        !activity.isEssential
      ) {
        this.displayExplainNonPrimary = true;
        this.gemRotationForm.get('nonPrimaryExplain')?.enable();
        this.gemRotationForm
          .get('nonPrimaryExplain')
          ?.setValidators([Validators.required]);
      } else {
        this.displayExplainNonPrimary = false;
        this.gemRotationForm.get('nonPrimaryExplain')?.setValue('');
        this.gemRotationForm.get('nonPrimaryExplain')?.disable();
        this.gemRotationForm.get('nonPrimaryExplain')?.setValidators([]);
      }
    };

    this.gemRotationForm
      .get('endDate')
      ?.valueChanges.pipe(untilDestroyed(this))
      .subscribe((val) => {
        if (val) {
          this.startDateOptions.maxDate = new Date(val);
        } else {
          this.startDateOptions.maxDate = null;
        }

        this.startDateOptions.errorText = undefined;

        calculateWeeks();
        checkForOverlap();
        setDurationErrors();

        const clinicalActivityId =
          this.gemRotationForm.get('clinicalActivityId')?.value;

        const clinicalLevelId =
          this.gemRotationForm.get('clinicalLevelId')?.value;

        if (clinicalActivityId || clinicalLevelId) {
          setClinicalActivityErrors(clinicalLevelId, clinicalActivityId);
        }
      });

    this.gemRotationForm
      .get('startDate')
      ?.valueChanges.pipe(untilDestroyed(this))
      .subscribe((val) => {
        if (val) {
          this.endDateOptions.minDate = new Date(val);
        } else {
          this.endDateOptions.minDate = null;
        }

        calculateWeeks();
        checkForOverlap();
        setDurationErrors();

        const clinicalActivityId =
          this.gemRotationForm.get('clinicalActivityId')?.value;

        const clinicalLevelId =
          this.gemRotationForm.get('clinicalLevelId')?.value;

        if (clinicalActivityId || clinicalLevelId) {
          setClinicalActivityErrors(clinicalLevelId, clinicalActivityId);
        }
      });

    this.gemRotationForm
      .get('clinicalLevelId')
      ?.valueChanges.pipe(untilDestroyed(this))
      .subscribe((val) => {
        const clinicalActivityId =
          this.gemRotationForm.get('clinicalActivityId')?.value;

        setClinicalActivityErrors(val, clinicalActivityId);
      });

    this.gemRotationForm
      .get('clinicalActivityId')
      ?.valueChanges.pipe(untilDestroyed(this))
      .subscribe((val) => {
        const clinicalLevelId =
          this.gemRotationForm.get('clinicalLevelId')?.value;

        setClinicalActivityErrors(clinicalLevelId, val);

        if (val) {
          const activity = this.optionLists.clinicalActivityOptions.find(
            (activity) =>
              activity.id === val && activity.name.includes('Non-Surgical')
          );
          if (activity) {
            this.displaySurgicalDescription = true;
            this.gemRotationForm.get('nonSurgicalActivity')?.enable();
            this.gemRotationForm
              .get('nonSurgicalActivity')
              ?.setValidators([Validators.required]);
          } else {
            this.displaySurgicalDescription = false;
            this.gemRotationForm.get('nonSurgicalActivity')?.setValue('');
            this.gemRotationForm.get('nonSurgicalActivity')?.disable();
            this.gemRotationForm.get('nonSurgicalActivity')?.setValidators([]);
          }
        }
      });

    this.gemRotationForm
      .get('usingAffiliateOrganization')
      ?.valueChanges.pipe(untilDestroyed(this))
      .subscribe((val) => {
        if (val) {
          this.gemRotationForm.get('alternateInstitutionName')?.enable();
          this.gemRotationForm
            .get('alternateInstitutionName')
            ?.setValidators([Validators.required]);
        } else {
          this.gemRotationForm.get('alternateInstitutionName')?.setValue('');
          this.gemRotationForm.get('alternateInstitutionName')?.disable();
          this.gemRotationForm
            .get('alternateInstitutionName')
            ?.setValidators([]);
        }
      });

    this.gemRotationForm
      .get('isClinicalActivity')
      ?.valueChanges.pipe(untilDestroyed(this), pairwise())
      .subscribe(([prev, next]) => {
        if (prev !== null && next !== null && prev !== next) {
          this.gemRotationForm.get('clinicalActivityId')?.setValue(null);
        }
      });
  }

  onSubmit() {
    const formData: any = this.gemRotationForm.getRawValue();
    const activity = this.optionLists.clinicalActivityOptions.find(
      (activity) => {
        return activity.id === formData.clinicalActivityId;
      }
    );

    if (activity) {
      formData.isEssential = activity.isEssential;
    }
    this.saveForm.emit({
      show: false,
      data: formData,
      isEdit: this.localEdit,
    });
  }

  changeModalData(id: number) {
    this.relaunchDialog.emit(id);
  }

  close() {
    this.cancelForm.emit({ show: false });
  }
}
