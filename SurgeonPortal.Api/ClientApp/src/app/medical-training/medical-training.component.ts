/* eslint-disable prettier/prettier */
import { CommonModule } from '@angular/common';
import {
  Component,
  CUSTOM_ELEMENTS_SCHEMA,
  OnDestroy,
  OnInit,
} from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { BehaviorSubject, Observable } from 'rxjs';
import { GridComponent } from '../shared/components/grid/grid.component';
import { ProfileHeaderComponent } from '../shared/components/profile-header/profile-header.component';
import { TrainingAddEditModalComponent } from '../shared/components/training-add-edit-modal/training-add-edit-modal.component';
import { MEDICAL_TRAINING_COLS } from '../shared/gridDefinitions/medical-training-cols';

import { Select, Store } from '@ngxs/store';
import { IAdvancedTrainingReadOnlyModel } from '../api/models/medicaltraining/advanced-training-read-only.model';
import { ModalComponent } from '../shared/components/modal/modal.component';
import { GlobalDialogService } from '../shared/services/global-dialog.service';
import {
  MedicalTrainingSelectors,
  UploadDocument,
  UserProfileSelectors,
} from '../state';

import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { CalendarModule } from 'primeng/calendar';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { IStateReadOnlyModel } from '../api';
import { IAdvancedTrainingModel } from '../api/models/medicaltraining/advanced-training.model';
import { IFellowshipReadOnlyModel } from '../api/models/medicaltraining/fellowship-read-only.model';
import { IFellowshipModel } from '../api/models/medicaltraining/fellowship.model';
import { IMedicalTrainingModel } from '../api/models/medicaltraining/medical-training.model';
import { IOtherCertificationsModel } from '../api/models/medicaltraining/other-certifications.model';
import { IUserCertificateReadOnlyModel } from '../api/models/medicaltraining/user-certificate-read-only.model';
import { IUserCertificateModel } from '../api/models/medicaltraining/user-certificate.model';
import { ICertificateTypeReadOnlyModel } from '../api/models/picklists/certificate-type-read-only.model';
import { IDegreeReadOnlyModel } from '../api/models/picklists/degree-read-only.model';
import { IDocumentTypeReadOnlyModel } from '../api/models/picklists/document-type-read-only.model';
import { IGraduateProfileReadOnlyModel } from '../api/models/picklists/graduate-profile-read-only.model';
import { IResidencyProgramReadOnlyModel } from '../api/models/picklists/residency-program-read-only.model';
import { IFormErrors } from '../shared/common';
import { DocumentsUploadComponent } from '../shared/components/documents-upload/documents-upload.component';
import { FormErrorsComponent } from '../shared/components/form-errors/form-errors.component';
import { IGridColumns } from '../shared/components/grid/abs-grid-col.interface';
import { SetUnsavedChanges } from '../state/application/application.actions';
import {
  ClearMedicalTrainingErrors,
  CreateAdvancedTraining,
  CreateFellowship,
  CreateMedicalTraining,
  CreateOtherCertification,
  DeleteFellowship,
  GetAdvancedTrainingData,
  GetFellowships,
  GetMedicalTraining,
  GetOtherCertifications,
  GetUserCertificates,
  UpdateAdvancedTraining,
  UpdateFellowship,
  UpdateMedicalTraining,
  UpdateOtherCertifications,
} from '../state/medical-training/medical-training.actions';
import {
  GetCertificateTypes,
  GetDegrees,
  GetGraduateProfiles,
  GetResidencyPrograms,
  GetStateList,
  IPickListItem,
  PicklistsSelectors,
} from '../state/picklists';
import { DOCUMENTS_COLS } from './documents-col';
import { FellowshipAddEditModalComponent } from './fellowship-add-edit-modal/fellowship-add-edit-modal.component';
import { FELLOWSHIP_COLS } from './fellowship-cols';
import { MedicalTrainingActions } from './medical-training-models';
import { OtherCertificatesAddEditModalComponent } from './other-certificates-add-edit-modal/other-certificates-add-edit-modal.component';
import { OTHER_CERTIFICATIONS_COLS } from './other-certificates-add-edit-modal/other-certifications-cols';

@UntilDestroy()
@Component({
  selector: 'abs-medical-training',
  templateUrl: './medical-training.component.html',
  styleUrls: ['./medical-training.component.scss'],
  standalone: true,
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ProfileHeaderComponent,
    GridComponent,
    TrainingAddEditModalComponent,
    GridComponent,
    ModalComponent,
    InputTextModule,
    DropdownModule,
    InputTextareaModule,
    CalendarModule,
    FellowshipAddEditModalComponent,
    DocumentsUploadComponent,
    OtherCertificatesAddEditModalComponent,
    FormErrorsComponent,
  ],
})
export class MedicalTrainingComponent implements OnInit, OnDestroy {
  //TODO: [Joe] - add form-errors shared component

  @Select(UserProfileSelectors.userId) userId$: Observable<number> | undefined;

  @Select(PicklistsSelectors.slices.countries) countries$:
    | Observable<IPickListItem[]>
    | undefined;

  @Select(PicklistsSelectors.slices.states) states$:
    | Observable<IStateReadOnlyModel[]>
    | undefined;

  @Select(PicklistsSelectors.slices.graduateProfiles) graduateProfiles$:
    | Observable<IGraduateProfileReadOnlyModel[]>
    | undefined;

  @Select(PicklistsSelectors.slices.degrees) degrees$:
    | Observable<IDegreeReadOnlyModel[]>
    | undefined;

  @Select(PicklistsSelectors.slices.residencyPrograms) residencyPrograms$:
    | Observable<IResidencyProgramReadOnlyModel[]>
    | undefined;

  @Select(PicklistsSelectors.slices.documentTypes) documentTypes$:
    | Observable<IDocumentTypeReadOnlyModel[]>
    | undefined;

  @Select(PicklistsSelectors.slices.certificateTypes) certificateTypes$:
    | Observable<ICertificateTypeReadOnlyModel[]>
    | undefined;

  @Select(MedicalTrainingSelectors.slices.additionalTraining)
  additionalTraining$: Observable<IAdvancedTrainingReadOnlyModel[]> | undefined;

  @Select(MedicalTrainingSelectors.slices.medicalTraining)
  medicalTraining$: Observable<IMedicalTrainingModel> | undefined;

  @Select(MedicalTrainingSelectors.slices.userCertificates) userCertificates$:
    | Observable<IUserCertificateReadOnlyModel[]>
    | undefined;

  @Select(MedicalTrainingSelectors.slices.otherCertifications)
  otherCertifications$: Observable<IOtherCertificationsModel[]> | undefined;

  @Select(MedicalTrainingSelectors.slices.fellowships) fellowships$:
    | Observable<IFellowshipReadOnlyModel[]>
    | undefined;

  @Select(MedicalTrainingSelectors.slices.errors) errors$:
    | Observable<IFormErrors>
    | undefined;

  clearErrors = new ClearMedicalTrainingErrors();

  isAdditionalAdvancedEdit$: BehaviorSubject<boolean> = new BehaviorSubject(
    false
  );

  isFellowshipEdit$: BehaviorSubject<boolean> = new BehaviorSubject(false);

  isOtherCertificatesEdit$: BehaviorSubject<boolean> = new BehaviorSubject(
    false
  );

  documentsData$: BehaviorSubject<any> = new BehaviorSubject([]);
  documentTypeOptions!: any;
  userOtherCertifications$: BehaviorSubject<IOtherCertificationsModel[]> =
    new BehaviorSubject([] as unknown as IOtherCertificationsModel[]);

  tempData$: BehaviorSubject<any> = new BehaviorSubject({});
  countries!: IPickListItem[];
  states!: IStateReadOnlyModel[];
  graduateProfiles!: IGraduateProfileReadOnlyModel[];
  medicalTraining!: IMedicalTrainingModel;
  residencyPrograms!: IResidencyProgramReadOnlyModel[];
  certificateTypes!: ICertificateTypeReadOnlyModel[];
  documentTypes!: IDocumentTypeReadOnlyModel[];
  degrees: IDegreeReadOnlyModel[] = [];
  userId!: number;
  createMode!: boolean;
  trainingCols = MEDICAL_TRAINING_COLS as IGridColumns[];
  fellowshipCols = FELLOWSHIP_COLS;
  documentsCols = DOCUMENTS_COLS as IGridColumns[];
  otherCertificationCols = OTHER_CERTIFICATIONS_COLS as IGridColumns[];
  medicalTrainingReadOnly!: IMedicalTrainingModel;
  medicalTrainingId!: number;
  trainingAddEditTitle: string | undefined;
  isEdit = true;
  showTrainingAddEdit = false;
  showFellowshipAddEdit = false;
  showRPVICertificatesAddEdit = false;
  year = new Date().getFullYear();
  maxYear: Date = new Date();
  canAddRPVI = true;

  medicalTrainingForm = new FormGroup({
    graduateProfileId: new FormControl(''),
    graduateProfileDescription: new FormControl(''),
    medicalSchoolName: new FormControl('', [
      Validators.required,
      Validators.max(30),
    ]),
    medicalSchoolCity: new FormControl('', Validators.required),
    medicalSchoolStateId: new FormControl({ value: null, disabled: true }),
    medicalSchoolCountryId: new FormControl('', Validators.required),
    medicalSchoolCountryName: new FormControl('', [
      Validators.required,
      Validators.max(30),
    ]),
    degreeId: new FormControl('', Validators.required),
    degreeName: new FormControl(''),
    medicalSchoolCompletionYear: new FormControl('', Validators.required),
    residencyProgramName: new FormControl(0),
    residencyCompletionYear: new FormControl('', Validators.required),
    residencyProgramOther: new FormControl('', Validators.max(8000)),
  });

  isSubmitted = false;

  constructor(
    private _store: Store,
    public globalDialogService: GlobalDialogService
  ) {
    this._store.dispatch(new GetResidencyPrograms());
    this._store.dispatch(new GetUserCertificates());
    this._store.dispatch(new GetAdvancedTrainingData());
    this._store.dispatch(new GetDegrees());
    this._store.dispatch(new GetMedicalTraining());
    this._store.dispatch(new GetCertificateTypes());
    this._store.dispatch(new GetGraduateProfiles());
    this._store.dispatch(new GetOtherCertifications());
    this._store.dispatch(new GetFellowships());
  }
  ngOnDestroy(): void {
    this._store.dispatch(new SetUnsavedChanges(false));
  }

  ngOnInit(): void {
    this._store.dispatch(new SetUnsavedChanges(false));

    this.maxYear.setFullYear(this.year + 2);
    this.userId$?.pipe(untilDestroyed(this)).subscribe((id) => {
      this.userId = id;
    });
    this.setPicklists();
    this.getMedicalTraining();
    this.getRPVICertificates();

    this.medicalTrainingForm.valueChanges
      .pipe(untilDestroyed(this))
      .subscribe(() => {
        this._store.dispatch(this.clearErrors);
        const isDirty = this.medicalTrainingForm.dirty;
        this._store.dispatch(
          new SetUnsavedChanges(isDirty && !this.isSubmitted)
        );
      });
    this.setStates();
  }

  onResidencyProgramChange(event: any) {
    
    if (event.value) {
      this.medicalTrainingForm.get('residencyProgramOther')?.disable();
      this.medicalTrainingForm.get('residencyProgramOther')?.patchValue(null);
    } else {
      this.medicalTrainingForm.get('residencyProgramOther')?.enable();
    }
  }

  setPicklists() {
    this.countries$
      ?.pipe(untilDestroyed(this))
      .subscribe((countries: IPickListItem[]) => {
        this.countries = countries;
      });
    this.graduateProfiles$
      ?.pipe(untilDestroyed(this))
      .subscribe((graduateProfiles: IGraduateProfileReadOnlyModel[]) => {
        this.graduateProfiles = graduateProfiles;
      });
    this.degrees$
      ?.pipe(untilDestroyed(this))
      .subscribe((degrees: IDegreeReadOnlyModel[]) => {
        this.degrees = degrees;
      });
    this.residencyPrograms$
      ?.pipe(untilDestroyed(this))
      .subscribe((residencyPrograms: IResidencyProgramReadOnlyModel[]) => {
        this.residencyPrograms = residencyPrograms;
      });
    this.documentTypes$
      ?.pipe(untilDestroyed(this))
      .subscribe((documentTypes: IDocumentTypeReadOnlyModel[]) => {
        this.documentTypes = documentTypes;
      });
    this.certificateTypes$
      ?.pipe(untilDestroyed(this))
      .subscribe((certificateTypes: ICertificateTypeReadOnlyModel[]) => {
        this.certificateTypes = certificateTypes;
      });
  }

  getMedicalTraining() {
    this.medicalTraining$
      ?.pipe(untilDestroyed(this))
      .subscribe((medicalTraining: IMedicalTrainingModel) => {
        if (medicalTraining) {
          this.createMode = false;
          this.medicalTrainingId = medicalTraining.id;
          this.medicalTrainingReadOnly = medicalTraining;
          const residencyProgramId = this.residencyPrograms.filter(
            (program) =>
              program.programName === medicalTraining.residencyProgramName
          )[0]?.programId;
          this.setStates(medicalTraining.medicalSchoolCountryId);
          this.medicalTrainingForm.get('medicalSchoolStateId')?.enable();

          for (const [key, value] of Object.entries(medicalTraining)) {
            this.medicalTrainingForm.get(key)?.patchValue(value);
          }
          this.medicalTrainingForm
            .get('graduateProfileId')
            ?.patchValue(medicalTraining.graduateProfileId.toString());
          this.medicalTrainingForm
            .get('residencyProgramName')
            ?.patchValue(residencyProgramId);

          if (medicalTraining.residencyProgramName?.length > 0) {
            this.medicalTrainingForm.get('residencyProgramOther')?.disable();
            this.medicalTrainingForm
              .get('residencyProgramOther')
              ?.patchValue(null);
          } else {
            this.medicalTrainingForm.get('residencyProgramOther')?.enable();
          }

          this.isEdit = false;
        } else {
          this.createMode = true;
        }
      });
  }

  getRPVICertificates() {
    this.otherCertifications$
      ?.pipe(untilDestroyed(this))
      .subscribe((otherCertifications: IOtherCertificationsModel[]) => {
        if (otherCertifications?.length > 0) {
          this.canAddRPVI = false;
        }
      });
  }

  onCountryChange(event: any) {
    const countryId = event.value;
    this.setStates(countryId);
    const countryName =
      this.countries.filter((country) => country.itemValue === countryId)[0]
        .itemDescription ?? '';

    this.medicalTrainingForm
      .get('medicalSchoolCountryName')
      ?.patchValue(countryName);
  }

  onDegreeChange(event: any) {
    const degreeId = event.value;
    const degreeName = this.degrees.filter(
      (degree) => degree.itemValue === degreeId
    )[0].itemDisplay;
    this.medicalTrainingForm.get('degreeName')?.patchValue(degreeName);
  }

  onGraduateProfileChange(event: any) {
    const graduateProfileId = event.value;

    const graduateProfileDescription =
      this.graduateProfiles.filter(
        (graduateProfile) => graduateProfile.type === graduateProfileId
      )[0].description ?? '';

    this.medicalTrainingForm
      .get('graduateProfileDescription')
      ?.patchValue(graduateProfileDescription);
  }

  setStates(countryId?: string) {
    this._store.dispatch(new GetStateList(countryId ?? '500'));
    this.states$
      ?.pipe(untilDestroyed(this))
      .subscribe((states: IStateReadOnlyModel[]) => {
        this.states = states;
        if (states.length > 0) {
          this.medicalTrainingForm.get('medicalSchoolStateId')?.enable();
        } else {
          this.medicalTrainingForm.get('medicalSchoolStateId')?.disable();
        }
      });
  }

  handleDocumentUpload(event: any) {
    const data = event.data;

    const model = {
      documentId: 1,
      certificateTypeId: data.typeId,
      createdByUserId: this.userId,
      file: data.file,
      issueDate: new Date().toISOString(),
    } as unknown as IUserCertificateModel;

    const formData = new FormData();

    Object.keys(model).forEach((key) => {
      formData.set(key, model[key]);
    });

    if (data.file) {
      this._store.dispatch(new UploadDocument({ model: formData }));
    }
  }

  handleGridAction($event: any, form: string) {
    const data = $event.data;
    const actions: MedicalTrainingActions = {
      edit: {
        additionalTraining: () => {
          this.isAdditionalAdvancedEdit$.next(true);
          this.tempData$.next(data);
          this.showTrainingModal(true);
        },
        fellowship: () => {
          this.isFellowshipEdit$.next(true);
          this.tempData$.next(data);
          this.showFellowshipModal(true);
        },
        otherCertificates: () => {
          this.isOtherCertificatesEdit$.next(true);
          this.tempData$.next(data);
          this.showOtherCertificaions(true);
        },
      },
      delete: {
        fellowship: () => {
          this._store.dispatch(new DeleteFellowship(data.id));
        },
        certificates: () => {
          this._store.dispatch(new GetUserCertificates());
        },
      },
      upload: {
        certificates: () => {
          this._store.dispatch(new GetUserCertificates());
        },
      },
    };

    const action = actions[$event.fieldKey]?.[form];

    if (action) {
      action();
    }
  }

  showTrainingModal(isEdit = false) {
    this.isAdditionalAdvancedEdit$.next(isEdit);
    this.showTrainingAddEdit = !this.showTrainingAddEdit;
  }

  showFellowshipModal(isEdit = false) {
    this.isFellowshipEdit$.next(isEdit);
    this.showFellowshipAddEdit = !this.showFellowshipAddEdit;
  }

  showOtherCertificaions(isEdit = false) {
    this.isOtherCertificatesEdit$.next(isEdit);
    this.showRPVICertificatesAddEdit = !this.showRPVICertificatesAddEdit;
  }

  saveTraining($event: any) {
    const formValues = $event.trainingRecord;
    const programId: number | undefined = parseInt(
      formValues.institutionName?.itemValue ?? ''
    );
    const trainingTypeId: number | undefined = parseInt(
      formValues.trainingType ?? ''
    );

    const model = {
      id: $event.trainingId,
      trainingTypeId: trainingTypeId ?? null,
      programId: programId ?? null,
      other: formValues.other ?? undefined,
      startDate: new Date(formValues.startDate ?? '').toISOString() ?? null,
      endDate: new Date(formValues.endDate ?? '').toISOString() ?? null,
    } as unknown as IAdvancedTrainingModel;

    if ($event.edit === true && $event.trainingId) {
      this._store
        .dispatch(new UpdateAdvancedTraining(model))
        .pipe(untilDestroyed(this))
        .subscribe((res) => {
          if (!res.medical_training.errors) {
            this.showTrainingAddEdit = $event.show;
            this.tempData$.next({});
          }
        });
    }

    if ($event.edit === false) {
      this._store
        .dispatch(new CreateAdvancedTraining(model))
        .pipe(untilDestroyed(this))
        .subscribe((res) => {
          if (!res.medical_training.errors) {
            this.showTrainingAddEdit = $event.show;
            this.tempData$.next({});
          }
        });
    }
  }

  saveFellowship($event: any) {
    const model = {
      id: $event.fellowshipId,
      programName: $event.fellowshipForm.programName ?? '',
      completionYear:
        $event.fellowshipForm.completionYear instanceof Date
          ? $event.fellowshipForm.completionYear.getFullYear().toString()
          : $event.fellowshipForm.completionYear,
      programOther: $event.fellowshipForm.programOther,
    } as unknown as IFellowshipModel;

    if ($event.edit === true && $event.fellowshipId) {
      this._store.dispatch(new UpdateFellowship(model)).subscribe((res) => {
        if (!res.medical_training.errors) {
          this.showFellowshipAddEdit = $event.show;
          this.tempData$.next({});
        }
      });
    }

    if ($event.edit === false) {
      this._store.dispatch(new CreateFellowship(model)).subscribe((res) => {
        if (!res.medical_training.errors) {
          this.showFellowshipAddEdit = $event.show;
          this.tempData$.next({});
        }
      });
    }
  }

  saveRPVICertificate($event: any) {
    const form = $event.otherCertificateForm;

    const model: IOtherCertificationsModel = {
      id: form.id ?? null,
      certificateNumber: form.certificateNumber?.toString(),
      certificateTypeId: 6,
      issueDate: new Date(form.issueDate ?? '').toISOString() ?? null,
    } as IOtherCertificationsModel;

    if ($event.edit === true && $event.otherCertificateId) {
      this._store
        .dispatch(new UpdateOtherCertifications(model))
        .pipe(untilDestroyed(this))
        .subscribe((res) => {
          if (!res.medical_training.errors) {
            this.showRPVICertificatesAddEdit = $event.show;
          }
        });
    }
    if ($event.edit === false) {
      this._store
        .dispatch(new CreateOtherCertification(model))
        .pipe(untilDestroyed(this))
        .subscribe((res) => {
          if (!res.medical_training.errors) {
            this.showRPVICertificatesAddEdit = $event.show;
          }
        });
    }
  }

  cancelAddEditTraining($event: any) {
    this.showTrainingAddEdit = $event.show;
    this.tempData$.next({});
  }

  cancelAddEditFellowship($event: any) {
    this.showFellowshipAddEdit = $event.show;
    this.tempData$.next({});
  }

  cancelOtherCertificatesAddEditModal($event: any) {
    this.showRPVICertificatesAddEdit = $event.show;
    this.tempData$.next({});
  }

  toggleFormEdit(toggle: boolean) {
    this.isEdit = toggle;
    // this.hasUnsavedChanges = toggle;
    this._store.dispatch(new SetUnsavedChanges(toggle));
  }

  save() { 
    const formValues = this.medicalTrainingForm.value;
    const residencyProgramName = this.residencyPrograms.filter(program => program.programId == formValues.residencyProgramName);

    const stateControlDisabled = formValues.medicalSchoolStateId == undefined ? true: false;

    const model = {
      graduateProfileId: parseInt(formValues.graduateProfileId ?? ''),
      medicalSchoolName: formValues.medicalSchoolName,
      medicalSchoolCity: formValues.medicalSchoolCity,
      medicalSchoolStateId: stateControlDisabled ? '' : formValues.medicalSchoolStateId,
      medicalSchoolCountryId: formValues.medicalSchoolCountryId,
      medicalSchoolCountryName: formValues.medicalSchoolCountryName,
      medicalSchoolCompletionYear: formValues.medicalSchoolCompletionYear,
      graduateProfileDescription: formValues.graduateProfileDescription,
      degreeId: formValues.degreeId,
      degreeName: formValues.degreeName,
      residencyProgramName: formValues.residencyProgramName
        ? residencyProgramName[0].programName
        : null,
      residencyCompletionYear: formValues.residencyCompletionYear,
      residencyProgramOther: formValues.residencyProgramOther?.length
        ? formValues.residencyProgramOther
        : null,
    } as unknown as IMedicalTrainingModel;

    this.globalDialogService
      .showConfirmation('Confirmation', 'Are you sure?')
      .then((result) => {
        if (result) {
          this.isSubmitted = true;
          if (this.createMode === true) {
            this._store.dispatch(new CreateMedicalTraining(model));
          } else {
            model['id'] = this.medicalTrainingId;
            this._store.dispatch(new UpdateMedicalTraining(model));
          }
        }
      });
  }
}
