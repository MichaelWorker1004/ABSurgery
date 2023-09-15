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
import { IUserProfile, UserProfileSelectors } from '../state';
import { Observable, take } from 'rxjs';
import { Select, Store } from '@ngxs/store';
import { GetStateList } from '../state/picklists';
import { ApplicationSelectors } from '../state/application/application.selectors';
import { IFeatureFlags } from '../state/application/application.state';

interface ActionMap {
  [key: string]: () => void;
}

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
  ],
  standalone: true,
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class ContinuousCertificationComponent implements OnInit {
  @Select(ApplicationSelectors.slices.featureFlags) featureFlags$:
    | Observable<IFeatureFlags>
    | undefined;

  userData!: any;
  continousCertificationData!: any;
  outcomeRegistriesModal = false;
  attestationModal = false;
  referenceFormsModal = false;
  payFeeModal = false;
  payFeeCols = PAY_FEE_COLS;
  payFeeData!: any;

  paymentGridData = [
    {
      paymentDate: new Date('09/18/2015'),
      paymentAmount: '$100',
      balanceRemaining: '$285.00',
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
    this.featureFlags$?.pipe(take(1)).subscribe((featureFlags) => {
      if (featureFlags) {
        this.featureFlags = featureFlags;
      }
    });
  }

  ngOnInit(): void {
    this.getUserData();
    this.getContinuousCertificationData();
    this.getPayFeeData();
  }

  getUserData() {
    this.userData = {
      name: 'John Doe, M.D',
    };
  }

  getPayFeeData() {
    this.payFeeData = {
      totalAmountOfFee: '$285.00',
      totalAmountPaidDate: new Date('11/5/2022'),
      totalAmountPaid: '$0.00',
      remainingBalance: '$285.00',
    };
  }

  getContinuousCertificationData() {
    this.continousCertificationData = [
      {
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
      },
      {
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
      },
      {
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
      },
      {
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
      },
      {
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
      },
      {
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
      },
      {
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
      },
      {
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
      },
      {
        title: 'Apply for an Exam',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          style: 2,
        },
        disabled: true,
        actionDisplay: 'Apply Now',
        icon: 'fa-solid fa-language',
      },
    ];
  }

  handleCardAction(action: string) {
    const actionFunction = this.actionMap[action];
    if (actionFunction) {
      actionFunction();
    }
  }
}
