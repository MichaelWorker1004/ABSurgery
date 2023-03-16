import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
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
  userData: any | undefined;
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
    this.userData = {
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
  }

  fetchAlertsAndNoticesByUserId() {
    this.alertsAndNotices = [
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
  }

  fetchActionCardsByUserId() {
    this.userActionCards = [
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
    this.actionCardClass = `grid-item ${
      this.userActionCards.length >= 3 ? 'w-33' : 'w-50'
    }`;
  }
}
