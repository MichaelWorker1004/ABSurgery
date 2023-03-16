import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { IMenuItem } from 'src/web-components/menuItem';

@Component({
  selector: 'abs-side-navigation',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './side-navigation.component.html',
  styleUrls: ['./side-navigation.component.scss'],
})
export class SideNavigationComponent implements OnInit {
  @Output() handleSideNavToggle: EventEmitter<any> = new EventEmitter();
  @Input() currentStatus: string | undefined;
  navItems: Array<IMenuItem> = [];

  constructor(private _router: Router) {}

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
    ];

    if (this.currentStatus === 'trainee') {
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

  toggleSideNav() {
    this.handleSideNavToggle.emit();
  }

  get router(): Router {
    return this._router;
  }
}
