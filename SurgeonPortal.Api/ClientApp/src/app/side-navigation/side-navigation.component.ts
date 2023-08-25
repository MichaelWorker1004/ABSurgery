import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { IMenuItem } from 'src/web-components/menuItem';
import { NgxsModule, Store } from '@ngxs/store';
import { Logout } from '../state';
import {
  CERTIFIED_NAV_ITEMS,
  EXAMINER_NAV_ITEMS,
  TRAINEE_NAV_ITEMS,
} from './nav-items';

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
  @Input() isSurgeon = false;
  @Input() isExaminer = false;

  navItems: Array<IMenuItem> = TRAINEE_NAV_ITEMS;

  constructor(private _router: Router, private _store: Store) {}

  ngOnInit(): void {
    this.getNavItemsByUserRole();
  }

  getNavItemsByUserRole() {
    if (this.isSurgeon) {
      this.navItems = CERTIFIED_NAV_ITEMS;
    }

    if (this.isExaminer) {
      this.navItems = this.navItems.concat(EXAMINER_NAV_ITEMS);
    }
  }

  logout() {
    this._store.dispatch(new Logout());
  }

  toggleSideNav() {
    this.handleSideNavToggle.emit();
  }

  toggleSubNav(item: IMenuItem) {
    console.log('toggle side nav', item);
  }

  get router(): Router {
    return this._router;
  }
}
