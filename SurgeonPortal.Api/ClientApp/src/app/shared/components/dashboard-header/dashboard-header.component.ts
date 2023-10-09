import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { FormsModule } from '@angular/forms';

interface HeaderLink {
  display: string;
  action: string;
}

@Component({
  selector: 'abs-dashboard-header',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive, FormsModule],
  templateUrl: './dashboard-header.component.html',
  styleUrls: ['./dashboard-header.component.scss'],
})
export class DashboardHeaderComponent {
  /**
   * The links to display in the header
   * @type {HeaderLink[]}
   */
  @Input() headerLinks: HeaderLink[] = [];
  /**
   * Whether or not to show the search bar
   * @type {boolean}
   */
  @Input() showSearch = true;

  @Output() handleSideNavToggle: EventEmitter<any> = new EventEmitter();
  searchText: string | undefined;

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
