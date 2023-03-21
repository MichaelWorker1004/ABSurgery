import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActionCardComponent } from '../shared/components/action-card/action-card.component';

@Component({
  selector: 'abs-continuous-certification',
  templateUrl: './continuous-certification.component.html',
  styleUrls: ['./continuous-certification.component.scss'],
  imports: [CommonModule, ActionCardComponent],
  standalone: true,
})
export class ContinuousCertificationComponent implements OnInit {
  userData!: any;
  continousCertificationData!: any;

  ngOnInit(): void {
    this.getUserData();
    this.getContinuousCertificationData();
  }

  getUserData() {
    this.userData = {
      name: 'John Doe, M.D',
    };
  }

  getContinuousCertificationData() {
    this.continousCertificationData = [
      {
        title: 'Personal Profile',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: 'component',
          action: '/personal-profile',
        },
        actionDisplay: 'View / Update my information',
        icon: 'fa-solid fa-address-card',
        status: 'completed',
      },
      {
        title: 'Outcomes Registries / Quality Assessment Programs',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: 'component',
          action: '/apply',
        },
        actionDisplay: 'View / Update my information',
        icon: 'fa-solid fa-stethoscope',
        status: 'completed',
      },
      {
        title: 'Medical Training',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: 'component',
          action: '/apply',
        },
        actionDisplay: 'View / Update my training',
        icon: 'fa-solid fa-language',
        status: 'completed',
      },
      {
        title: 'Professional Standing',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: 'component',
          action: '/apply',
        },
        actionDisplay: 'View / Update my activities',
        icon: 'fa-solid fa-certificate',
        status: 'in-progress',
      },
      {
        title: 'CME Repository',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: 'component',
          action: '/personal-profile',
        },
        actionDisplay: 'View CMEs',
        icon: 'fa-solid fa-id-card-clip',
        status: 'in-progress',
      },
      {
        title: 'Pay Fee',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: 'component',
          action: '/apply',
        },
        actionDisplay: 'View / Pay Fee',
        icon: 'fa-solid fa-language',
        status: 'in-progress',
      },

      {
        title: 'Reference Forms',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: 'component',
          action: '/apply',
        },
        actionDisplay: 'View / Update my activities',
        icon: 'fa-solid fa-rectangle-list',
        status: 'in-progress',
      },
      {
        title: 'Attestation',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {},
        disabled: true,
        actionDisplay: 'Not available yet',
        icon: 'fa-solid fa-user-check',
        status: 'contingent',
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
}
