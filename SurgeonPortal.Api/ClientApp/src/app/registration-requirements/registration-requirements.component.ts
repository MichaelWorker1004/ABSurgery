import { CUSTOM_ELEMENTS_SCHEMA, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActionCardComponent } from '../shared/components/action-card/action-card.component';
import { Status } from '../shared/components/action-card/status.enum';
import { SurgeonProfileModalComponent } from './surgeon-profile-modal/surgeon-profile-modal.component';
import { MedicalLicenseModalComponent } from './medical-license-modal/medical-license-modal.component';
import { TrainingModalComponent } from './training-modal/training-modal.component';
import { ModalComponent } from '../shared/components/modal/modal.component';
import { AcgmeExperienceModalComponent } from './acgme-experience-modal/acgme-experience-modal.component';
import { GraduateMedicalEducationModalComponent } from './graduate-medical-education-modal/graduate-medical-education-modal.component';
import { SpecialAccommodationsModalComponent } from './special-accommodations-modal/special-accommodations-modal.component';

interface ActionMap {
  [key: string]: () => void;
}

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
  ],
  templateUrl: './registration-requirements.component.html',
  styleUrls: ['./registration-requirements.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class RegistrationRequirementsComponent implements OnInit {
  userData!: any;
  registrationRequirementsData!: Array<any>;
  applyForAnExamActionCardData!: any;
  showSurgeonProfile = false;
  showMedicalLicense = false;
  showACGMEexprience = false;
  showTraining = false;
  showGraduateMedicalEducation = false;
  showSpecialAccommodations = false;

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
      this.showSpecialAccommodations = !this.showSpecialAccommodations;
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
    console.log('event', event);
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
          type: 'dialog',
          action: 'trainingModal',
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
          type: 'dialog',
          action: 'medicalLicenseModal',
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
          type: 'dialog',
          action: 'ACGMEExperienceModal',
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
          type: 'dialog',
          action: 'graduateMedicalEducationModal',
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
          type: 'dialog',
          action: 'specialAccommodationsModal',
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
