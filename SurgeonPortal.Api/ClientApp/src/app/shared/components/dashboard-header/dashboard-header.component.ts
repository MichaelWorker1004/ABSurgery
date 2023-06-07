import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'abs-dashboard-header',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive, FormsModule],
  templateUrl: './dashboard-header.component.html',
  styleUrls: ['./dashboard-header.component.scss'],
})
export class DashboardHeaderComponent {
  @Output() handleSideNavToggle: EventEmitter<any> = new EventEmitter();

  searchText: string | undefined;
  headerLinks = [
    {
      display: 'News',
      action: 'https://www.absurgery.org/default.jsp?news_home_mb',
    },
    {
      display: 'EPAs',
      action: 'https://www.absurgery.org/default.jsp?epahome',
    },
    {
      display: 'About',
      action: 'https://www.absurgery.org/default.jsp?abouthome',
    },
    {
      display: 'Contact',
      action: 'https://www.absurgery.org/default.jsp?aboutcontact',
    },
  ];

  constructor(private _router: Router) {}

  handleSearch() {
    // search handler
  }

  onKey(event: any) {
    this.searchText = event.target.value;
  }

  toggleSideNav() {
    this.handleSideNavToggle.emit();
  }

  get router(): Router {
    return this._router;
  }
}
