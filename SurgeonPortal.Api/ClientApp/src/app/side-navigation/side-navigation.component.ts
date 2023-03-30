import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { IMenuItem } from 'src/web-components/menuItem';
import { NgxsModule, Store } from '@ngxs/store';
import { Logout } from '../state/auth/auth.actions';

@Component({
  selector: 'abs-side-navigation',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive, NgxsModule],
  templateUrl: './side-navigation.component.html',
  styleUrls: ['./side-navigation.component.scss'],
})
export class SideNavigationComponent implements OnInit {
  @Output() handleSideNavToggle: EventEmitter<any> = new EventEmitter();
  @Input() currentStatus: string | undefined;
  navItems: Array<IMenuItem> = [];

  constructor(private _router: Router, private _store: Store) {}

  ngOnInit(): void {
    this.getNavItemsByUserRoll();
  }

  getNavItemsByUserRoll() {
    this.navItems = [
      {
        display: 'Dashboard',
        action: 'dashboard',
        icon: 'fa-solid fa-chart-line',
      },
      {
        display: 'Personal Profile',
        action: 'personal-profile',
        icon: 'fa-solid fa-circle-info',
      },
      {
        display: 'Medical Training',
        action: 'medical-training',
        icon: 'fa-solid fa-graduation-cap',
      },
      {
        display: 'Professional Standing',
        action: 'professional-standing',
        icon: 'fa-solid fa-stethoscope',
      },
      {
        display: 'CME History',
        action: 'cme-repository',
        icon: 'fa-regular fa-folder-open',
      },
      {
        display: 'Exam Process',
        action: 'exam-process',
        icon: 'fa-solid fa-list-check',
        children: [
          {
            display: 'Registration Requirements',
            action: 'registration-requirements',
          },
          {
            display: 'Exam Registration',
            action: 'exam-registration',
          },
        ],
      },
      {
        display: 'Examination History',
        action: 'examination-history',
        icon: 'fa-sharp fa-solid fa-file-waveform',
      },
      {
        display: 'Continuous Certification',
        action: 'continuous-certification',
        icon: 'fa-solid fa-user-graduate',
      },
      {
        display: 'Payment History',
        action: 'payment-history',
        icon: 'fa-regular fa-credit-card',
      },
      {
        display: 'Documents',
        action: 'documents',
        icon: 'fa-solid fa-file-lines',
      },
      {
        display: 'Committees',
        action: 'committees',
        icon: 'fa-solid fa-users',
      },
    ];

    if (this.currentStatus === 'Trainee') {
      this.navItems = [
        {
          display: 'Dashboard',
          action: 'dashboard',
          icon: 'fa-solid fa-chart-line',
        },
        {
          display: 'Personal Profile',
          action: 'personal-profile',
          icon: 'fa-solid fa-circle-info',
        },
        {
          display: 'Medical Training',
          action: 'medical-training',
          icon: 'fa-solid fa-graduation-cap',
        },
        {
          display: 'GME History',
          action: 'gme-history',
          icon: 'fa-regular fa-folder-open',
        },
        {
          display: 'Documents',
          action: 'documents',
          icon: 'fa-solid fa-file-lines',
        },
      ];
    }
  }

  logout() {
    this._store.dispatch(new Logout());
  }

  toggleSideNav() {
    this.handleSideNavToggle.emit();
  }

  toggleSubNav(item: IMenuItem) {
    console.log(item);
  }

  get router(): Router {
    return this._router;
  }
}
