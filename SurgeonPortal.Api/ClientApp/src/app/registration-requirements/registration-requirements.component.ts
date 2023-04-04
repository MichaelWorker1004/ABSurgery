import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActionCardComponent } from '../shared/components/action-card/action-card.component';
import { Status } from '../shared/components/action-card/status.enum';
import { SurgeonProfileModalComponent } from './surgeon-profile-modal/surgeon-profile-modal.component';

interface ActionMap {
  [key: string]: () => void;
}

@Component({
  selector: 'abs-registration-requirements',
  standalone: true,
  imports: [CommonModule, ActionCardComponent, SurgeonProfileModalComponent],
  templateUrl: './registration-requirements.component.html',
  styleUrls: ['./registration-requirements.component.scss'],
})
export class RegistrationRequirementsComponent implements OnInit {
  userData!: any;
  registrationRequirementsData!: Array<any>;
  applyForAnExamActionCardData!: any;
  showSurgeonProfile = false;

  private actionMap: ActionMap = {
    surgeonProfileModal: () => {
      this.showSurgeonProfile = !this.showSurgeonProfile;
      console.log('surgeonProfileModal:', this.showSurgeonProfile);
    },
  };

  ngOnInit(): void {
    this.getUserData();
    this.getRegistrationRequirementsData();
  }

  getUserData() {
    this.userData = {
      name: 'John Doe M.D',
    };
  }

  closeModal(event: any) {
    const actionFunction = this.actionMap[event.action];
    if (actionFunction) {
      actionFunction();
    }
  }

  getRegistrationRequirementsData() {
    this.registrationRequirementsData = [
      {
        title: 'Personal Profile',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          // type: 'component',
          // action: '/personal-profile',
          type: 'dialog',
          action: 'surgeonProfileModal',
        },
        actionDisplay: 'View / Update my information',
        icon: 'fa-solid fa-address-card',
        status: Status.Completed,
      },
      {
        title: 'Training',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: 'component',
          action: '/training',
        },
        actionDisplay: 'View / Update my information',
        icon: 'fa-language fa-solid',
        status: Status.InProgress,
        recievedOn: new Date('2021-01-01'),
      },
      {
        title: 'Professional Activities and Privileges',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: 'component',
          action: '/professional-activities-and-privileges',
        },
        actionDisplay: 'View / Update my information',
        icon: 'fa-solid fa-user-doctor',
        status: Status.Completed,
      },
      {
        title: 'Medical License',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: 'component',
          action: '/medical-licence',
        },
        actionDisplay: 'View / Update my license',
        icon: 'fa-certificate fa-solid',
        status: Status.InProgress,
      },
      {
        title: 'ACGME Experience Report by Role',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: 'component',
          action: '/acgme-experience-report-by-role',
        },
        actionDisplay: 'View / Update my information',
        icon: 'fa-solid fa-stethoscope',
        status: Status.InProgress,
      },
      {
        title: 'Graduate Medical Education (GME)',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: 'component',
          action: '/gme',
        },
        actionDisplay: 'View / Update my information',
        icon: 'fa-solid fa-stethoscope',
        status: Status.Alert,
      },
      {
        title: 'Program Director Attestation',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: 'component',
          action: '/program-director-attestation',
        },
        actionDisplay: 'View / Update my information',
        icon: 'fa-solid fa-user-check',
        status: Status.Completed,
      },
      {
        title: 'Certification(s) Upload',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: 'component',
          action: '/certifications-upload',
        },
        actionDisplay: 'View / Update my information',
        icon: 'fa-solid fa-rectangle-list',
        status: Status.Completed,
      },
      {
        title: 'Application Fee',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: 'component',
          action: '/application-fee',
        },
        actionDisplay: 'View / Update my information',
        icon: 'fa-solid fa-cash-register',
        status: Status.Contingent,
      },
      {
        title: 'Special Accommodations',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: 'component',
          action: '/special-accommodations',
        },
        actionDisplay: 'View / Update my information',
        icon: 'fa-solid fa-star',
        status: Status.InProgress,
      },
    ];
    this.applyForAnExamActionCardData = {
      title: 'Apply for an Exam',
      description:
        'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
      action: {
        style: 2,
      },
      disabled: true,
      actionDisplay: 'Apply Now',
      icon: 'fa-solid fa-language',
    };
  }

  handleCardAction(action: string) {
    console.log('action', action);
    const actionFunction = this.actionMap[action];
    if (actionFunction) {
      actionFunction();
    }
  }
}
