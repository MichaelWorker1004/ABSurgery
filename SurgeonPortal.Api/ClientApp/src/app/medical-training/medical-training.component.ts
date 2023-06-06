import { CommonModule } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { BehaviorSubject, Observable } from 'rxjs';
import { ProfileHeaderComponent } from '../shared/components/profile-header/profile-header.component';
import { GridComponent } from '../shared/components/grid/grid.component';
import { TrainingAddEditModalComponent } from '../shared/components/training-add-edit-modal/training-add-edit-modal.component';
import { MEDICAL_TRAINING_COLS } from '../shared/gridDefinitions/medical-training-cols';

import { GlobalDialogService } from '../shared/services/global-dialog.service';
import { ModalComponent } from '../shared/components/modal/modal.component';
import {
  MedicalTrainingSelectors,
  UploadDocument,
  UserProfileSelectors,
} from '../state';
import { Select, Store } from '@ngxs/store';
import { AdvancedTrainingService } from '../api/services/medicaltraining/advanced-training.service';
import { IAdvancedTrainingReadOnlyModel } from '../api/models/medicaltraining/advanced-training-read-only.model';

import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { CalendarModule } from 'primeng/calendar';
import {
  CreateMedicalTraining,
  GetMedicalTraining,
  GetUserCertificates,
  UpdateMedicalTraining,
} from '../state/medical-training/medical-training.actions';
import {
  GetCertificateTypes,
  GetDegrees,
  GetFellowshipPrograms,
  GetGraduateProfiles,
  GetResidencyPrograms,
  GetStateList,
  IPickListItem,
  PicklistsSelectors,
} from '../state/picklists';
import { IStateReadOnlyModel } from '../api';
import { IGraduateProfileReadOnlyModel } from '../api/models/picklists/graduate-profile-read-only.model';
import { IDegreeReadOnlyModel } from '../api/models/picklists/degree-read-only.model';
import { FELLOWSHIP_COLS } from './fellowship-cols';
import { FellowshipService } from '../api/services/medicaltraining/fellowship.service';
import { FellowshipAddEditModalComponent } from './fellowship-add-edit-modal/fellowship-add-edit-modal.component';
import { IFellowshipReadOnlyModel } from '../api/models/medicaltraining/fellowship-read-only.model';
import { MedicalTrainingActions } from './medical-training-models';
import { IMedicalTrainingModel } from '../api/models/medicaltraining/medical-training.model';
import { IResidencyProgramReadOnlyModel } from '../api/models/picklists/residency-program-read-only.model';
import { DocumentsUploadComponent } from '../shared/components/documents-upload/documents-upload.component';
import { DOCUMENTS_COLS } from './documents-col';
import { OTHER_CERTIFICATIONS_COLS } from './other-certifications-cols';
import { OtherCertificatesAddEditModalComponent } from './other-certificates-add-edit-modal/other-certificates-add-edit-modal.component';
import { ICertificateTypeReadOnlyModel } from '../api/models/picklists/certificate-type-read-only.model';
import { IDocumentTypeReadOnlyModel } from '../api/models/picklists/document-type-read-only.model';
import { IUserCertificateReadOnlyModel } from '../api/models/medicaltraining/user-certificate-read-only.model';

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
  ],
})
export class MedicalTrainingComponent implements OnInit {
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

  advancedTraining$: BehaviorSubject<IAdvancedTrainingReadOnlyModel[]> =
    new BehaviorSubject<IAdvancedTrainingReadOnlyModel[]>([]);

  fellowships$: BehaviorSubject<IFellowshipReadOnlyModel[]> =
    new BehaviorSubject<IFellowshipReadOnlyModel[]>([]);

  isAdditionalAdvancedEdit$: BehaviorSubject<boolean> = new BehaviorSubject(
    false
  );

  isFellowshipEdit$: BehaviorSubject<boolean> = new BehaviorSubject(false);

  isOtherCertificatesEdit$: BehaviorSubject<boolean> = new BehaviorSubject(
    false
  );

  documentsData$: BehaviorSubject<any> = new BehaviorSubject([]);
  documentTypeOptions!: any;
  otherCertifications$: BehaviorSubject<any> = new BehaviorSubject([]);

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
  trainingCols = MEDICAL_TRAINING_COLS;
  fellowshipCols = FELLOWSHIP_COLS;
  documentsCols = DOCUMENTS_COLS;
  otherCertificationCols = OTHER_CERTIFICATIONS_COLS;
  medicalTrainingReadOnly!: IMedicalTrainingModel;
  medicalTrainingId!: number;
  trainingAddEditTitle: string | undefined;
  isEdit = true;
  showTrainingAddEdit = false;
  showFellowshipAddEdit = false;
  showOtherCertificatesAddEdit = false;
  year = new Date().getFullYear();
  maxYear: Date = new Date();

  medicalTrainingForm = new FormGroup({
    graduateProfileId: new FormControl(''),
    medicalSchoolName: new FormControl(''),
    medicalSchoolCity: new FormControl(''),
    medicalSchoolStateId: new FormControl({ value: '', disabled: true }),
    medicalSchoolCountryId: new FormControl(''),
    degreeId: new FormControl(''),
    medicalSchoolCompletionYear: new FormControl(''),
    residencyProgramName: new FormControl(0),
    residencyCompletionYear: new FormControl(''),
    residencyProgramOther: new FormControl(''),
  });

  constructor(
    private _store: Store,
    private globalDialogService: GlobalDialogService,
    private advancedTrainingService: AdvancedTrainingService,
    private fellowshipService: FellowshipService
  ) {
    this._store.dispatch(new GetFellowshipPrograms());
    this._store.dispatch(new GetResidencyPrograms());
    this._store.dispatch(new GetUserCertificates());
    this._store.dispatch(new GetDegrees());
    this._store.dispatch(new GetMedicalTraining());
    this._store.dispatch(new GetCertificateTypes());
    this._store.dispatch(new GetGraduateProfiles());
  }

  ngOnInit(): void {
    this.maxYear.setFullYear(this.year);
    this.userId$?.subscribe((id) => {
      this.userId = id;
    });
    this.setPicklists();
    this.getDocumentsData();
    this.getOtherCertificationsData();
    this.getAdvancedTrainingGridData();
    this.getFellowshipGridData();
    this.getMedicalTraining();
  }

  setPicklists() {
    this.countries$?.subscribe((countries: IPickListItem[]) => {
      this.countries = countries;
    });
    this.graduateProfiles$?.subscribe(
      (graduateProfiles: IGraduateProfileReadOnlyModel[]) => {
        this.graduateProfiles = graduateProfiles;
      }
    );
    this.degrees$?.subscribe((degrees: IDegreeReadOnlyModel[]) => {
      this.degrees = degrees;
    });
    this.residencyPrograms$?.subscribe(
      (residencyPrograms: IResidencyProgramReadOnlyModel[]) => {
        this.residencyPrograms = residencyPrograms;
      }
    );
    this.documentTypes$?.subscribe(
      (documentTypes: IDocumentTypeReadOnlyModel[]) => {
        this.documentTypes = documentTypes;
      }
    );
    this.certificateTypes$?.subscribe(
      (certificateTypes: ICertificateTypeReadOnlyModel[]) => {
        this.certificateTypes = certificateTypes;
      }
    );
  }

  getAdvancedTrainingGridData() {
    this.advancedTrainingService
      .retrieveAdvancedTrainingReadOnly_GetByUserId()
      .subscribe((res: IAdvancedTrainingReadOnlyModel[]) => {
        this.advancedTraining$.next(res);
      });
  }

  getFellowshipGridData() {
    this.fellowshipService
      .retrieveFellowshipReadOnly_GetByUserId()
      .subscribe((res: IFellowshipReadOnlyModel[]) => {
        this.fellowships$.next(res);
      });
  }

  getMedicalTraining() {
    this.medicalTraining$?.subscribe(
      (medicalTraining: IMedicalTrainingModel) => {
        if (medicalTraining) {
          this.createMode = false;
          this.medicalTrainingId = medicalTraining.id;
          this.medicalTrainingReadOnly = medicalTraining;
          const residencyProgramId = this.residencyPrograms.filter(
            (program) =>
              program.programName === medicalTraining.residencyProgramName
          )[0].programId;
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
        } else {
          this.createMode = true;
        }
      }
    );
  }

  onCountryChange(event: any) {
    const countryId = event.value;
    this.setStates(countryId);
  }

  setStates(countryId: string) {
    this._store.dispatch(new GetStateList(countryId));
    this.states$?.subscribe((states: IStateReadOnlyModel[]) => {
      this.states = states;
      if (states.length > 0) {
        this.medicalTrainingForm.get('medicalSchoolStateId')?.enable();
      } else {
        this.medicalTrainingForm.get('medicalSchoolStateId')?.disable();
      }
    });
  }

  getDocumentsData() {
    this.userCertificates$?.subscribe(
      (userCertificates: IUserCertificateReadOnlyModel[]) => {
        this.documentsData$.next(userCertificates);
      }
    );
  }

  getOtherCertificationsData() {
    this.otherCertifications$.next([
      {
        certificateType: 'RVPI',
        certificateNumber: '13456',
        issueDate: new Date('11/23/2017'),
      },
      {
        certificateType: 'RVPI',
        certificateNumber: '789456',
        issueDate: new Date('05/23/2023'),
      },
    ]);
  }

  handleDocumentUpload(event: any) {
    console.log(event);
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
          this.fellowshipService.deleteFellowship(data.id).subscribe(() => {
            this.getFellowshipGridData();
          });
        },
        certificates: () => {
          this.getDocumentsData();
        },
      },
      upload: {
        certificates: () => {
          this.getDocumentsData();
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
    this.showOtherCertificatesAddEdit = !this.showOtherCertificatesAddEdit;
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
      trainingTypeId: trainingTypeId ?? null,
      programId: programId ?? null,
      other: formValues.other ?? undefined,
      startDate: new Date(formValues.startDate ?? '').toISOString() ?? null,
      endDate: new Date(formValues.endDate ?? '').toISOString() ?? null,
    };

    if ($event.edit === true && $event.trainingId) {
      this.advancedTrainingService
        .updateAdvancedTraining($event.trainingId, model)
        .subscribe(() => {
          this.getAdvancedTrainingGridData();
        });
    }

    if ($event.edit === false) {
      this.advancedTrainingService
        .createAdvancedTraining(model)
        .subscribe(() => {
          this.getAdvancedTrainingGridData();
        });
    }

    this.showTrainingAddEdit = $event.show;
    this.tempData$.next({});
  }

  saveFellowship($event: any) {
    this.showFellowshipAddEdit = $event.show;

    const model = {
      programName: $event.fellowshipForm.programName,
      completionYear:
        new Date($event.fellowshipForm.completionYear ?? '').toISOString() ??
        null,
      programOther: $event.fellowshipForm.programOther,
    };

    if ($event.edit === true && $event.fellowshipId) {
      this.fellowshipService
        .updateFellowship($event.fellowshipId, model)
        .subscribe(() => {
          this.getFellowshipGridData();
        });
    }

    if ($event.edit === false) {
      this.fellowshipService.createFellowship(model).subscribe(() => {
        this.getFellowshipGridData();
      });
    }

    this.showTrainingAddEdit = $event.show;
    this.tempData$.next({});
  }

  saveOtherCertificates($event: any) {
    console.log($event);
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
    this.showOtherCertificatesAddEdit = $event.show;
    this.tempData$.next({});
  }

  toggleFormEdit(toggle: boolean) {
    this.isEdit = toggle;
  }

  save() {
    const formValues = this.medicalTrainingForm.value;

    const medicalSchoolCompletionYear = formValues.medicalSchoolCompletionYear
      ? new Date(formValues.medicalSchoolCompletionYear ?? '')
          .getFullYear()
          .toString()
      : '';

    const residencyCompletionYear = formValues.residencyCompletionYear
      ? new Date(formValues.residencyCompletionYear ?? '')
          .getFullYear()
          .toString()
      : '';

    const residencyProgramName = this.residencyPrograms.filter((program) => {
      if (
        formValues.residencyProgramName &&
        +program.programId === +formValues.residencyProgramName
      ) {
        return program;
      } else return [];
    });

    const model = {
      userId: this.userId,
      graduateProfileId: parseInt(formValues.graduateProfileId ?? ''),
      medicalSchoolName: formValues.medicalSchoolName,
      medicalSchoolCity: formValues.medicalSchoolCity,
      medicalSchoolStateId: formValues.medicalSchoolStateId,
      medicalSchoolCountryId: formValues.medicalSchoolCountryId,
      medicalSchoolCompletionYear,
      degreeId: formValues.degreeId,
      residencyProgramName: residencyProgramName[0].programName,
      residencyCompletionYear,
      residencyProgramOther: formValues.residencyProgramOther,
      createdByUserId: this.userId,
    } as unknown as IMedicalTrainingModel;

    this.globalDialogService
      .showConfirmation('Confirmation', 'Are you sure?')
      .then((result) => {
        if (result) {
          if (this.createMode === true) {
            this._store.dispatch(new CreateMedicalTraining(model));
          } else {
            model['id'] = this.medicalTrainingId;
            this._store.dispatch(new UpdateMedicalTraining(model));
          }
          this.globalDialogService.showSuccessError(
            'Success',
            'Medical Training Saved',
            true
          );
        } else {
          this.globalDialogService.showSuccessError(
            'Error',
            'Medical Training not Saved',
            false
          );
        }
      });
  }
}
