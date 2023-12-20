import { CommonModule } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Select, Store } from '@ngxs/store';
import { Observable, skipWhile, take } from 'rxjs';
import { IExamFeeReadOnlyModel } from '../api/models/billing/exam-fee-read-only.model';
import { IAttestationSubmitModel } from '../api/models/continuouscertification/attestation-read-only.model';
import { IStatuses } from '../api/models/users/statuses.model';
import { ActionCardComponent } from '../shared/components/action-card/action-card.component';
import { Action } from '../shared/components/action-card/action.enum';
import { Status } from '../shared/components/action-card/status.enum';
import { AttestationModalComponent } from '../shared/components/attestation-modal/attestation-modal.component';
import { GridComponent } from '../shared/components/grid/grid.component';
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
  ContinuousCertificationSelectors,
  DashboardSelectors,
  ExamProcessSelectors,
  GetContinuousCertificationStatuses,
  GetDashboardCertificationStatus,
  GetExamFees,
  GetRefrenceFormGridData,
  IUserProfile,
  SubmitAttestation,
  UserProfileSelectors,
} from '../state';
import { ApplicationSelectors } from '../state/application/application.selectors';
import { IFeatureFlags } from '../state/application/application.state';
import { IRefrenceFormReadOnlyModel } from '../state/continuous-certification/refrence-form-read-only.model';
import { GetPicklists, PicklistsSelectors } from '../state/picklists';
import { ADD_REFERENCE_LETTER_FIELDS } from './add-reference-letter-fields';
import { OutcomeRegistriesModalComponent } from './outcome-registries-modal/outcome-registries-modal.component';

interface ActionMap {
  [key: string]: () => void;
}

@UntilDestroy()
@Component({
  selector: 'abs-continuous-certification',
  templateUrl: './continuous-certification.component.html',
  styleUrls: ['./continuous-certification.component.scss'],
  imports: [
    CommonModule,
    ActionCardComponent,
    GridComponent,
    FormsModule,
    PayFeeComponent,
    ModalComponent,
    OutcomeRegistriesModalComponent,
    AttestationModalComponent,
    ReferenceFormModalComponent,
    LegendComponent,
  ],
  standalone: true,
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class ContinuousCertificationComponent implements OnInit {
  @Select(ApplicationSelectors.slices.featureFlags) featureFlags$:
    | Observable<IFeatureFlags>
    | undefined;

  @Select(UserProfileSelectors.user) user$:
    | Observable<IUserProfile>
    | undefined;

  @Select(ExamProcessSelectors.slices.examFees) examFees$:
    | Observable<IExamFeeReadOnlyModel[]>
    | undefined;

  @Select(
    ContinuousCertificationSelectors.slices.continuousCertificationStatuses
  )
  continuousCertificationStatuses$: Observable<IStatuses[]> | undefined;

  @Select(ContinuousCertificationSelectors.slices.refrenceFormGridData)
  referenceFormGridData$:
    | Observable<IRefrenceFormReadOnlyModel[] | undefined>
    | undefined;

  userData!: any;
  continousCertificationData!: any;
  outcomeRegistriesModal = false;
  attestationModal = false;
  referenceFormsModal = false;
  payFeeModal = false;
  payFeeCols = PAY_FEE_COLS;
  payFeeData!: any;

  referenceLetterPicklists: IReferenceLetterPicklists = {
    stateOptions: [],
    roleOptions: [],
    altRoleOptions: [],
    explainOptions: [],
  };

  referenceFormModalConfig: IReferenceFormModalConfig = {
    source: 'continuousCertification',
    lapsedPath: false,
  };

  refrenceLetterFormFields = ADD_REFERENCE_LETTER_FIELDS;

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
      text: 'Contingent',
      color: '#a0a0a0',
    },
  ];

  featureFlags: IFeatureFlags = {};

  private actionMap: ActionMap = {
    outcomeRegistriesModal: () => {
      this.outcomeRegistriesModal = !this.outcomeRegistriesModal;
    },
    attestationModal: () => {
      this.attestationModal = !this.attestationModal;
    },
    referenceFormsModal: () => {
      this.referenceFormsModal = !this.referenceFormsModal;
    },
    payFeeModal: () => {
      this._store.dispatch(new GetExamFees());
      this.payFeeModal = !this.payFeeModal;
    },
  };

  constructor(
    private _store: Store,
    private globalDialogService: GlobalDialogService
  ) {
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
    this._store.dispatch(new GetContinuousCertificationStatuses());
    this._store.dispatch(new GetRefrenceFormGridData());
    this.featureFlags$?.pipe(take(1)).subscribe((featureFlags) => {
      if (featureFlags) {
        this.featureFlags = featureFlags;
      }
    });
  }

  ngOnInit(): void {
    this.getContinuousCertificationData();
    this.getPayFeeData();

    this._store
      .dispatch(new GetDashboardCertificationStatus())
      .pipe(take(1))
      .subscribe(() => {
        const certStatus = this._store.selectSnapshot(
          DashboardSelectors.slices.certificationStatus
        );
        if (certStatus) {
          this.referenceFormModalConfig = {
            ...this.referenceFormModalConfig,
            lapsedPath: certStatus.lapsedPath,
          };
        }
      });
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

  getContinuousCertificationData() {
    const continousCertificationData = [
      {
        id: 'Pers_Info',
        title: 'Personal Profile',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: Action.component,
          action: '/personal-profile',
        },
        actionDisplay: this.featureFlags.personalProfilePage
          ? 'View / Update my information'
          : 'Coming Soon',
        icon: 'fa-solid fa-address-card',
        status: Status.Completed,
        disabled: !this.featureFlags.personalProfilePage,
        displayStatusText: false,
      },
      {
        id: 'Outcomes',
        title: 'Outcomes Registries / Quality Assessment Programs',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: Action.dialog,
          action: 'outcomeRegistriesModal',
        },
        actionDisplay: 'View / Update my information',
        icon: 'fa-solid fa-stethoscope',
        status: Status.Completed,
        displayStatusText: false,
      },
      {
        id: 'Med_Training',
        title: 'Medical Training',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: Action.component,
          action: '/medical-training',
        },
        actionDisplay: this.featureFlags.medicalTrainingPage
          ? 'View / Update my training'
          : 'Coming Soon',
        icon: 'fa-solid fa-language',
        status: Status.Completed,
        disabled: !this.featureFlags.medicalTrainingPage,
        displayStatusText: false,
      },
      {
        id: 'Prof_Standing',
        title: 'Professional Standing',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: Action.component,
          action: '/professional-standing',
        },
        actionDisplay: this.featureFlags.professionalStandingPage
          ? 'View / Update my activities'
          : 'Coming Soon',
        icon: 'fa-solid fa-certificate',
        status: Status.InProgress,
        disabled: !this.featureFlags.professionalStandingPage,
        displayStatusText: false,
      },
      {
        id: 'CME',
        title: 'CME Repository',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: Action.component,
          action: '/cme-repository',
        },
        actionDisplay: this.featureFlags.cmeRepositoryPage
          ? 'View CMEs'
          : 'Coming Soon',
        icon: 'fa-solid fa-id-card-clip',
        status: Status.InProgress,
        disabled: !this.featureFlags.cmeRepositoryPage,
        displayStatusText: false,
      },
      {
        id: 'payFee',
        title: 'Pay Fee',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: Action.dialog,
          action: 'payFeeModal',
        },
        actionDisplay: 'View / Pay Fee',
        icon: 'fa-solid fa-language',
        status: Status.InProgress,
        displayStatusText: false,
      },
      {
        id: 'Ref_Let',
        title: 'Reference Forms',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: Action.dialog,
          action: 'referenceFormsModal',
        },
        actionDisplay: 'View / Update my activities',
        icon: 'fa-solid fa-rectangle-list',
        status: Status.InProgress,
        displayStatusText: false,
      },
      {
        id: 'CC_Attestation',
        title: 'Attestation',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: Action.dialog,
          action: 'attestationModal',
        },
        disabled: false,
        actionDisplay: 'View / Update my information',
        icon: 'fa-solid fa-user-check',
        status: Status.InProgress,
        displayStatusText: false,
      },
      {
        id: 'applyForExam',
        title: 'Apply for an Exam',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        actionStyle: 'button',
        disabled: true,
        actionDisplay: 'Apply Now',
        icon: 'fa-solid fa-language',
        displayStatusText: false,
      },
    ];

    this.continuousCertificationStatuses$
      ?.pipe(
        skipWhile((stati) => !stati || stati.length < 0),
        untilDestroyed(this)
      )
      .subscribe((stati: IStatuses[]) => {
        const statuses = {} as any;

        stati.forEach((ccs) => {
          statuses[ccs.id] = ccs;
        });

        continousCertificationData.forEach((cc: any) => {
          cc['status'] = statuses[cc.id]?.status || Status.InProgress;
        });

        continousCertificationData.find((cc) => {
          if (cc.id === 'applyForExam') {
            cc['disabled'] = !this.areAllItemsCompleted(
              continousCertificationData
            );
            if (cc['disabled']) {
              cc['status'] = Status.Contingent;
            }
          }
        });

        this.continousCertificationData = continousCertificationData;
      });
  }

  areAllItemsCompleted(certificationData: any[]): boolean {
    for (const item of certificationData) {
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

  handleAttestationSave() {
    this.globalDialogService.showLoading();
    const model: IAttestationSubmitModel = {
      SigReceive: new Date(),
      CertnoticeReceive: new Date(),
    };

    this._store
      .dispatch(new SubmitAttestation(model))
      .pipe(untilDestroyed(this))
      .subscribe(() => {
        this.handleCardAction('attestationModal');
      });
  }

  // handleReferenceLetterSave(data: IReferenceLetterModel) {
  //   this.globalDialogService.showLoading();
  //   console.log('save reference letter', data);
  //   const model: IReferenceLetterModel = {
  //     ...data,
  //   };

  //   // this._store
  //   //   .dispatch(new RequestRefrence(model))
  //   //   .pipe(untilDestroyed(this))
  //   //   .subscribe(() => {
  //   //     this.globalDialogService.closeOpenDialog();
  //   //     this.handleCardAction('referenceModal');
  //   //   });
  // }
}
