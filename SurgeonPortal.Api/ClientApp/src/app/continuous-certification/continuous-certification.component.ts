import { CommonModule } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActionCardComponent } from '../shared/components/action-card/action-card.component';
import { Status } from '../shared/components/action-card/status.enum';
import { GridComponent } from '../shared/components/grid/grid.component';
import { PAY_FEE_COLS } from '../shared/components/pay-fee/pay-fee-cols';
import { PayFeeComponent } from '../shared/components/pay-fee/pay-fee.component';
import { ModalComponent } from '../shared/components/modal/modal.component';
import { OutcomeRegistriesModalComponent } from './outcome-registries-modal/outcome-registries-modal.component';
import { AttestationModalComponent } from './attestation-modal/attestation-modal.component';
import { ReferenceFormModalComponent } from './reference-form-modal/reference-form-modal.component';
import { Action } from '../shared/components/action-card/action.enum';
import { Observable, take } from 'rxjs';
import { Select, Store } from '@ngxs/store';
import { GetStateList } from '../state/picklists';
import { ApplicationSelectors } from '../state/application/application.selectors';
import { IFeatureFlags } from '../state/application/application.state';
import { LegendComponent } from '../shared/components/legend/legend.component';
import { IExamFeeReadOnlyModel } from '../api/models/billing/exam-fee-read-only.model';
import {
  UserProfileSelectors,
  IUserProfile,
  ExamProcessSelectors,
  GetExamFees,
  ContinuousCertificationSelectors,
  GetContinuousCertificationStatuses,
  GetRefrenceFormGridData,
} from '../state';
import { IContinuousCerticationStatuses } from '../state/continuous-certification/continuous-certification-statuses.model';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

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
  continuousCertificationStatuses$:
    | Observable<IContinuousCerticationStatuses>
    | undefined;

  userData!: any;
  continousCertificationData!: any;
  outcomeRegistriesModal = false;
  attestationModal = false;
  referenceFormsModal = true;
  payFeeModal = false;
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
      this.payFeeModal = !this.payFeeModal;
    },
  };

  constructor(private _store: Store) {
    this._store.dispatch(new GetStateList('500'));
    this._store.dispatch(new GetExamFees());
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
        id: 'personalProfile',
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
        id: 'outcomeRegistries',
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
        id: 'medicalTraining',
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
        id: 'professionalStanding',
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
        id: 'cmeRepository',
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
        id: 'referenceForms',
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
        id: 'attestation',
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
      ?.pipe(untilDestroyed(this))
      .subscribe((continuousCertificationStatuses: any) => {
        continousCertificationData.forEach((cc: any) => {
          cc['status'] = continuousCertificationStatuses[cc.id]?.status;
        });

        continousCertificationData.find((cc) => {
          if (cc.id === 'applyForExam') {
            cc['disabled'] = !this.areAllItemsCompleted(
              continousCertificationData
            );
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
}
