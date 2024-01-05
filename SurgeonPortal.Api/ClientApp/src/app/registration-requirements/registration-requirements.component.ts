import { CommonModule } from '@angular/common';
import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  OnInit,
  ViewContainerRef,
} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Select, Store } from '@ngxs/store';
import { Observable, take } from 'rxjs';
import { IApplicationFeeReadOnlyModel } from '../api/models/billing/application-fee-read-only.model';
import { IAttestationReadOnlyModel } from '../api/models/continuouscertification/attestation-read-only.model';
import { IAccommodationModel } from '../api/models/examinations/accommodation.model';
import { IExamTitleReadOnlyModel } from '../api/models/examinations/exam-title-read-only.model';
import { IPdReferenceLetterModel } from '../api/models/examinations/pd-reference-letter.model';
import { IStatuses } from '../api/models/users/statuses.model';
import { ActionCardComponent } from '../shared/components/action-card/action-card.component';
import { Status } from '../shared/components/action-card/status.enum';
import { AttestationModalComponent } from '../shared/components/attestation-modal/attestation-modal.component';
import { LegendComponent } from '../shared/components/legend/legend.component';
import { ModalComponent } from '../shared/components/modal/modal.component';
import { PAY_FEE_COLS } from '../shared/components/pay-fee/pay-fee-cols';
import { PayFeeComponent } from '../shared/components/pay-fee/pay-fee.component';
import {
  IReferenceFormModalConfig,
  IReferenceLetterPicklists,
  ReferenceFormModalComponent,
} from '../shared/components/reference-form-modal/reference-form-modal.component';
import { GlobalDialogService } from '../shared/services/global-dialog.service';
import {
  ExamProcessSelectors,
  ExamScoringSelectors,
  GetApplicationFee,
} from '../state';
import { GetPicklists, PicklistsSelectors } from '../state/picklists';
import {
  ApplyForQeExam,
  GetAccommodations,
  GetPdReferenceLetter,
  GetQeAttestations,
  GetQeExamEligibility,
  GetRegistrationRequirementsTitle,
  GetResgistrationRequirmentsStatuses,
  IExamEligibility,
  ReqistrationRequirmentsSelectors,
  UpdateQeAttestations,
} from '../state/registration-requirements';
import { AcgmeExperienceModalComponent } from './acgme-experience-modal/acgme-experience-modal.component';
import { GraduateMedicalEducationModalComponent } from './graduate-medical-education-modal/graduate-medical-education-modal.component';
import { MedicalLicenseModalComponent } from './medical-license-modal/medical-license-modal.component';
import { ProfessionalActivitiesAndPrivilegesModalComponent } from './professional-activities-and-privileges-modal/professional-activities-and-privileges-modal.component';
import { ProgramDirectorAttestationsComponent } from './program-director-attestations/program-director-attestations.component';
import { PROGRAM_DIRECTOR_COLS } from './program-director-cols';
import { ADD_PROGRAM_DIRECTOR_FIELDS } from './program-director-fields';
import { REGISTRATION_REQUIRMENTS_CARDS } from './reqistration-requirements-cards';
import { SpecialAccommodationsModalComponent } from './special-accommodations-modal/special-accommodations-modal.component';
import { SurgeonProfileModalComponent } from './surgeon-profile-modal/surgeon-profile-modal.component';
import { TrainingModalComponent } from './training-modal/training-modal.component';

interface ActionMap {
  [key: string]: () => void;
}

@UntilDestroy()
@Component({
  selector: 'abs-registration-requirements',
  standalone: true,
  imports: [
    CommonModule,
    ActionCardComponent,
    SurgeonProfileModalComponent,
    MedicalLicenseModalComponent,
    TrainingModalComponent,
    ModalComponent,
    AcgmeExperienceModalComponent,
    GraduateMedicalEducationModalComponent,
    SpecialAccommodationsModalComponent,
    ProfessionalActivitiesAndPrivilegesModalComponent,
    LegendComponent,
    AttestationModalComponent,
    PayFeeComponent,
    ProgramDirectorAttestationsComponent,
    ReferenceFormModalComponent,
  ],
  templateUrl: './registration-requirements.component.html',
  styleUrls: ['./registration-requirements.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class RegistrationRequirementsComponent implements OnInit {
  @Select(ExamProcessSelectors.slices.applicationFee) applicationFee$:
    | Observable<IApplicationFeeReadOnlyModel[]>
    | undefined;

  @Select(
    ReqistrationRequirmentsSelectors.slices.registrationRequirementsStatuses
  )
  registrationRequirementsStatuses$: Observable<any> | undefined;

  @Select(ReqistrationRequirmentsSelectors.slices.pdReferenceLetter)
  pdReferenceLetter$:
    | Observable<IPdReferenceLetterModel[] | undefined>
    | undefined;

  @Select(ExamScoringSelectors.slices.examHeaderId) examHeaderId$:
    | Observable<number>
    | undefined;

  @Select(ReqistrationRequirmentsSelectors.slices.accommodation)
  accommodation$: Observable<IAccommodationModel> | undefined;

  @Select(ReqistrationRequirmentsSelectors.slices.qeAttestations)
  attestations$: Observable<IAttestationReadOnlyModel[]> | undefined;

  @Select(ReqistrationRequirmentsSelectors.slices.examTitle) examTitle$:
    | Observable<IExamTitleReadOnlyModel>
    | undefined;

  referenceLetterPicklists: IReferenceLetterPicklists = {
    stateOptions: [],
    roleOptions: [],
    altRoleOptions: [],
    explainOptions: [],
  };

  referenceFormModalConfig: IReferenceFormModalConfig = {
    source: 'registrationRequirments',
    lapsedPath: true,
  };

  refrenceLetterFormFields = ADD_PROGRAM_DIRECTOR_FIELDS;
  refrenceLetterCols = PROGRAM_DIRECTOR_COLS;

  examHeaderId!: number;
  userData!: any;
  registrationRequirementsData!: Array<any>;
  applyForAnExamActionCardData!: any;
  showSurgeonProfile = false;
  showMedicalLicense = false;
  showACGMEexprience = false;
  showTraining = false;
  showGraduateMedicalEducation = false;
  showSpecialAccommodations = false;
  attestationModal = false;
  showProfessionalActivitiesAndPrivileges = false;
  payFeeModal = false;
  programDirectorAttestationModal = false;

  applicationSubmitted = false;

  payFeeCols = PAY_FEE_COLS;
  payFeeData!: any;

  legendItems = [
    {
      text: 'Completed',
      color: '#1c827c',
    },
    {
      text: 'In Progress',
      color: '#dbad69',
    },
    {
      text: 'Alert',
      color: '#8b0d0a',
    },
    {
      text: 'Contingent',
      color: '#a0a0a0',
    },
  ];

  constructor(
    private _globalDialogService: GlobalDialogService,
    public viewContainerRef: ViewContainerRef,
    private _store: Store,
    private activatedRoute: ActivatedRoute,
    private _router: Router
  ) {
    this.activatedRoute.params.pipe(take(1)).subscribe((params) => {
      this.examHeaderId = params['examId'];
      if (this.examHeaderId) {
        this._store.dispatch(
          new GetRegistrationRequirementsTitle(this.examHeaderId)
        );
        this._store.dispatch(new GetApplicationFee(this.examHeaderId));
        this._store.dispatch(new GetPdReferenceLetter(this.examHeaderId));
        // this._store.dispatch(new GetAccommodations(this.examHeaderId));
        this._store.dispatch(new GetQeAttestations(this.examHeaderId));
        this._store
          .dispatch(new GetQeExamEligibility())
          .pipe(take(1))
          .subscribe(() => {
            const examsEligibility = this._store.selectSnapshot(
              ReqistrationRequirmentsSelectors.slices.qeExamEligibility
            );
            const examEligibility = examsEligibility?.find(
              (x) => x.examId === +this.examHeaderId
            );
            if (examEligibility && examEligibility.examRegistrationAvailable) {
              this.applicationSubmitted = true;
            } else {
              this.applicationSubmitted = false;
            }

            this._store.dispatch(
              new GetResgistrationRequirmentsStatuses(this.examHeaderId)
            );
          });
      }
    });
    this._store
      .dispatch(new GetPicklists('500'))
      .pipe(take(1))
      .subscribe(() => {
        const newReferenceLetterPicklists: IReferenceLetterPicklists = {
          stateOptions: [],
          roleOptions: [],
          altRoleOptions: [],
          explainOptions: [],
        };
        newReferenceLetterPicklists.stateOptions =
          this._store.selectSnapshot(PicklistsSelectors.slices.states) || [];
        newReferenceLetterPicklists.roleOptions =
          this._store.selectSnapshot(
            PicklistsSelectors.slices.referenceLetterRoleTypes
          ) || [];
        newReferenceLetterPicklists.altRoleOptions =
          this._store.selectSnapshot(
            PicklistsSelectors.slices.referenceLetterAltRoleTypes
          ) || [];
        newReferenceLetterPicklists.explainOptions =
          this._store.selectSnapshot(
            PicklistsSelectors.slices.referenceLetterExplainOptions
          ) || [];

        this.referenceLetterPicklists = newReferenceLetterPicklists;
      });

    this._globalDialogService.setViewContainerRef = this.viewContainerRef;
  }

  private actionMap: ActionMap = {
    surgeonProfileModal: () => {
      this.showSurgeonProfile = !this.showSurgeonProfile;
    },
    medicalLicenseModal: () => {
      this.showMedicalLicense = !this.showMedicalLicense;
    },
    ACGMEExperienceModal: () => {
      this.showACGMEexprience = !this.showACGMEexprience;
    },
    trainingModal: () => {
      this.showTraining = !this.showTraining;
    },
    graduateMedicalEducationModal: () => {
      this.showGraduateMedicalEducation = !this.showGraduateMedicalEducation;
    },
    specialAccommodationsModal: () => {
      if (this.examHeaderId) {
        this._store.dispatch(new GetAccommodations(this.examHeaderId));
      }
      this.showSpecialAccommodations = !this.showSpecialAccommodations;
    },
    professionalActivitiesAndPrivilegesModal: () => {
      this.showProfessionalActivitiesAndPrivileges =
        !this.showProfessionalActivitiesAndPrivileges;
    },
    attestationModal: () => {
      this.attestationModal = !this.attestationModal;
    },
    payFeeModal: () => {
      this.payFeeModal = !this.payFeeModal;
    },
    programDirectorAttestationModal: () => {
      this.programDirectorAttestationModal =
        !this.programDirectorAttestationModal;
    },
    applyForExam: () => {
      this._store
        .dispatch(new ApplyForQeExam(this.examHeaderId))
        .pipe(take(1))
        .subscribe((state) => {
          const errors = this._store.selectSnapshot(
            ReqistrationRequirmentsSelectors.slices.errors
          );
          // this._router.navigate([
          //   '/apply-and-register/exam-registration',
          //   this.examHeaderId,
          // ]);
          if (errors) {
            this._globalDialogService.showSuccessError(
              'Error',
              'There was an error submitting your application',
              false
            );
          } else {
            this._globalDialogService
              .showSuccessError(
                'Success',
                'Aplication submitted successfully',
                true
              )
              .then(() => {
                this._router.navigate(['/apply-and-register']);
              });
          }
        });
    },
  };

  ngOnInit(): void {
    this.getRegistrationRequirementsData();
    this.getPayFeeData();
  }

  closeModal(event: any) {
    const actionFunction = this.actionMap[event.action];
    if (actionFunction) {
      actionFunction();
    }
  }

  getPayFeeData() {
    this.applicationFee$?.subscribe((examFees) => {
      const payFeeData = {
        totalAmountOfFee: 0,
        totalAmountPaidDate: new Date(),
        totalAmountPaid: 0,
        remainingBalance: 0,
      };

      examFees.forEach((examFee: IApplicationFeeReadOnlyModel) => {
        payFeeData.totalAmountOfFee += examFee.subTotal;
        payFeeData.totalAmountPaid += examFee.paidTotal;
        payFeeData.remainingBalance += examFee.balanceDue;
      });

      this.payFeeData = payFeeData;
    });
  }

  getRegistrationRequirementsData() {
    this.registrationRequirementsData = REGISTRATION_REQUIRMENTS_CARDS;

    this.registrationRequirementsStatuses$
      ?.pipe(untilDestroyed(this))
      .subscribe((registrationRequirementsStatuses: IStatuses[]) => {
        if (!registrationRequirementsStatuses) {
          return;
        }
        const statuses = {} as any;

        registrationRequirementsStatuses.forEach((status: any) => {
          statuses[status.id] = status;
        });

        this.registrationRequirementsData.forEach((data: any) => {
          if (statuses[data.id]) {
            data.status = statuses[data.id].status;
            data.disabled = statuses[data.id].disabled;
          }
        });

        this.registrationRequirementsData =
          this.registrationRequirementsData.map((x: any) => {
            if (x.id === 'Attestation') {
              x.disabled = !this.areAllItemsCompleted(
                this.registrationRequirementsData,
                ['applyForExam', 'Attestation']
              );
              return {
                ...x,
                status: this.areAllItemsCompleted(
                  this.registrationRequirementsData,
                  ['applyForExam', 'Attestation']
                )
                  ? x.status
                  : Status.Contingent,
                description: this.areAllItemsCompleted(
                  this.registrationRequirementsData,
                  ['applyForExam', 'Attestation']
                )
                  ? x.description
                  : 'You must complete all previous steps before completing this step.',
              };
            }
            if (x.id === 'applyForExam') {
              x.disabled = !this.areAllItemsCompleted(
                this.registrationRequirementsData,
                ['applyForExam']
              );
              if (this.applicationSubmitted) {
                x.disabled = true;
                x.description =
                  'You have already submitted your application for this exam. You can make changes to previous sections while your application is pending approval.';
              }
              return {
                ...x,
                status: this.areAllItemsCompleted(
                  this.registrationRequirementsData,
                  ['applyForExam']
                )
                  ? x.status
                  : Status.Contingent,
                description: this.areAllItemsCompleted(
                  this.registrationRequirementsData,
                  ['applyForExam']
                )
                  ? x.description
                  : 'You must complete all other application requirements before you can apply for this exam.',
              };
            }
            return x;
          });
      });
  }

  areAllItemsCompleted(data: any[], exclude?: string[]): boolean {
    return data.every((item) => {
      if (exclude && exclude.includes(item.id)) {
        return true;
      }
      return item.status === Status.Completed || item.status === undefined;
    });
  }

  handleCardAction(action: string) {
    const actionFunction = this.actionMap[action];
    if (actionFunction) {
      actionFunction();
    }
  }

  handleAttestationSave() {
    this._store
      .dispatch(new UpdateQeAttestations(this.examHeaderId))
      .pipe(untilDestroyed(this))
      .subscribe(() => {
        this.handleCardAction('attestationModal');
      });
  }
}
