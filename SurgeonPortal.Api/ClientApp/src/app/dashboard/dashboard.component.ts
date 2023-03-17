import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { ActionCardComponent } from '../shared/components/action-card/action-card.component';
import { HighlightCardComponent } from '../shared/components/highlight-card/highlight-card.component';
import { UserInformationCardComponent } from '../shared/components/user-information-card/user-information-card.component';

@Component({
  selector: 'abs-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    ActionCardComponent,
    UserInformationCardComponent,
    HighlightCardComponent,
  ],
})
export class DashboardComponent implements OnInit {
  @Input() userData: any | undefined;
  alertsAndNotices: any | undefined;
  userActionCards: any | undefined;
  actionCardClass: string | undefined;

  ngOnInit(): void {
    this.fetchUserData();
    this.initDashboard();
  }

  initDashboard() {
    this.fetchActionCardsByUserId();
    this.fetchAlertsAndNoticesByUserId();
  }

  fetchUserData() {
    const userTrainee = {
      name: 'John Doe',
      id: '00001',
      currentStatus: 'Trainee',
      lastLogin: new Date().toLocaleString(),
      userInformation: [
        {
          title: 'Program Director',
          data: 'Lucy Generic, M.D',
        },
        {
          title: 'Program',
          data: 'University of Medicine Dept. of Surgery Philadelphia, PA',
        },
        {
          title: 'Clinical Level',
          data: 'PGY1',
        },
      ],
    };

    const userCertified = {
      name: 'John Doe, M.D',
      id: '00002',
      currentStatus: 'Certified - Clincally Active',
      lastLogin: new Date().toLocaleString(),
      userInformation: [
        {
          title: 'Surgical Critical Care',
          data: [
            {
              title: 'Certificate Number',
              data: 'XXXXXXXXX',
            },
            {
              title: 'Expiration Date',
              data: '12/2/2012',
            },
          ],
        },
        {
          title: 'Surgery of the Hand',
          data: [
            {
              title: 'Certificate Number',
              data: 'XXXXXXXXX',
            },
            {
              title: 'Next Assessment Due',
              data: '12/2/2012',
            },
          ],
        },
        {
          title: 'Surgery',
          data: [
            {
              title: 'Certificate Number',
              data: 'XXXXXXXXX',
            },
            {
              title: 'Expiration Date',
              data: '12/2/2012',
            },
          ],
        },
      ],
    };

    this.userData = userCertified;
  }

  fetchAlertsAndNoticesByUserId() {
    const alertsAndNoticesTrainee = [
      {
        title: 'Next General Surgery QE - 7/2024',
        content:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer egestas maximus turpis id pulvinar.',
        alert: true,
        image:
          'https://images.pexels.com/photos/6098057/pexels-photo-6098057.jpeg',
      },
      {
        title: 'Documents',
        content:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer egestas maximus turpis id pulvinar.',
        alert: false,
        image:
          'https://images.pexels.com/photos/4021775/pexels-photo-4021775.jpeg',
      },
    ];

    const alertsAndNoticesCertfiied = [
      {
        title: 'Issues with your GME',
        content:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer egestas maximus turpis id pulvinar.',
        alert: true,
        image:
          'https://images.pexels.com/photos/6098057/pexels-photo-6098057.jpeg',
      },
      {
        title: 'Documents',
        content:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer egestas maximus turpis id pulvinar.',
        alert: false,
        image:
          'https://images.pexels.com/photos/4021775/pexels-photo-4021775.jpeg',
      },
    ];

    this.alertsAndNotices = alertsAndNoticesCertfiied;
  }

  fetchActionCardsByUserId() {
    const userCardActionsTrainee = [
      {
        title: 'Apply for a Qualified Exam',
        description:
          'QE applications are not yet available. Check back on April 15th.',
        action: {
          type: 'component',
          action: '/apply',
        },
        actionDisplay: 'Coming Soon',
        icon: 'fa-solid fa-user-graduate',
        highlightColor: '#1C827D',
      },
      {
        title: 'Graduate Medical Education (GME)',
        description:
          'Add rotations to your GME history. Lorem ipsum dolor sit amet, consectetur adipiscing elit.',
        action: {
          type: 'component',
          action: '/gme-history',
        },
        actionDisplay: 'View Your GME',
        icon: 'fa-sharp fa-solid fa-file-waveform',
      },
    ];

    const userCardActionsCertified = [
      {
        title: 'Continuous Certification Requirements',
        description:
          'This is the mailing address that we currently have on record for you. You receive any paper communications from us this way.',
        action: {
          type: 'component',
          action: '/continuous-certification',
        },
        actionDisplay: 'See Requirements',
        icon: 'fa-solid fa-user-graduate',
        highlightColor: '#1C827D',
      },
      {
        title: 'Register for an Exam or Assessment',
        description:
          'This is basic information like your first and last name, title, etc.',
        action: {
          type: 'component',
          action: '/exam-proccess',
        },
        actionDisplay: 'Register For an Exam Now',
        icon: 'fa-sharp fa-solid fa-file-waveform',
      },
      {
        title: 'Apply for an Exam',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: 'component',
          action: '/apply',
        },
        actionDisplay: 'Apply Now',
        icon: 'fa-solid fa-list-check',
      },
    ];

    this.userActionCards = userCardActionsCertified;
    this.actionCardClass = `grid-item ${
      this.userActionCards.length >= 3 ? 'w-33' : 'w-50'
    }`;
  }
}
