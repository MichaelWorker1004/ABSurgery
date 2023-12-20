import { CommonModule } from '@angular/common';
import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  OnInit,
  ViewContainerRef,
} from '@angular/core';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Select, Store } from '@ngxs/store';
import { Observable, take } from 'rxjs';
import { IExamFeeReadOnlyModel } from '../api/models/billing/exam-fee-read-only.model';
import { IAccommodationModel } from '../api/models/examinations/accommodation.model';
import { IPdReferenceLetterModel } from '../api/models/examinations/pd-reference-letter.model';
import { IStatuses } from '../api/models/users/statuses.model';
import { ActionCardComponent } from '../shared/components/action-card/action-card.component';
import { Action } from '../shared/components/action-card/action.enum';
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
  GetExamFees,
} from '../state';
import { GetPicklists, PicklistsSelectors } from '../state/picklists';
import {
  GetAccommodations,
  GetPdReferenceLetter,
  GetResgistrationRequirmentsStatuses,
  ReqistrationRequirmentsSelectors,
} from '../state/registration-requirements';
import { AcgmeExperienceModalComponent } from './acgme-experience-modal/acgme-experience-modal.component';
import { GraduateMedicalEducationModalComponent } from './graduate-medical-education-modal/graduate-medical-education-modal.component';
import { MedicalLicenseModalComponent } from './medical-license-modal/medical-license-modal.component';
import { ProfessionalActivitiesAndPrivilegesModalComponent } from './professional-activities-and-privileges-modal/professional-activities-and-privileges-modal.component';
import { ProgramDirectorAttestationsComponent } from './program-director-attestations/program-director-attestations.component';
import { ADD_PROGRAM_DIRECTOR_FIELDS } from './program-director-fields';
import { REGISTRATION_REQUIRMENTS_CARDS } from './reqistration-requirements-cards';
import { SpecialAccommodationsModalComponent } from './special-accommodations-modal/special-accommodations-modal.component';
import { SurgeonProfileModalComponent } from './surgeon-profile-modal/surgeon-profile-modal.component';
import { TrainingModalComponent } from './training-modal/training-modal.component';
import { PROGRAM_DIRECTOR_COLS } from './program-director-cols';
import { ActivatedRoute, Router } from '@angular/router';

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
  @Select(ExamProcessSelectors.slices.examFees) examFees$:
    | Observable<IExamFeeReadOnlyModel[]>
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
    private activatedRoute: ActivatedRoute
  ) {
    this.activatedRoute.params
      .pipe(untilDestroyed(this))
      .subscribe((params) => {
        this.examHeaderId = params['examId'];
        if (this.examHeaderId) {
          this._store.dispatch(new GetPdReferenceLetter(this.examHeaderId));
          this._store.dispatch(new GetAccommodations(this.examHeaderId));
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

    this._store.dispatch(new GetResgistrationRequirmentsStatuses());

    this._store.dispatch(new GetExamFees());
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
    this.examFees$?.subscribe((examFees) => {
      const payFeeData = {
        totalAmountOfFee: 0,
        totalAmountPaidDate: new Date(),
        totalAmountPaid: 0,
        remainingBalance: 0,
      };

      examFees.forEach((examFee: any) => {
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
        const statuses = {} as any;

        registrationRequirementsStatuses.forEach((status: any) => {
          statuses[status.id] = status;
        });

        this.registrationRequirementsData.forEach((data: any) => {
          data.status = statuses[data.id].status;
          data.disabled = statuses[data.id].disabled;
        });
      });

    this.applyForAnExamActionCardData = {
      title: 'Apply for an Exam',
      description:
        'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
      action: {
        style: 2,
        type: Action.component,
        action: '/apply-and-resgister/exam-registration',
      },
      disabled: !this.areAllItemsCompleted(this.registrationRequirementsData),
      actionDisplay: 'Apply Now',
      icon: 'fa-solid fa-language',
    };
  }

  areAllItemsCompleted(data: any[]): boolean {
    for (const item of data) {
      if (item.status !== undefined && item.status !== Status.Completed) {
        return false;
      }
    }
    return true; // All items have a status of Completed or no status at all
  }

  handleCardAction(action: string) {
    const actionFunction = this.actionMap[action];
    if (actionFunction) {
      actionFunction();
    }
  }
}
