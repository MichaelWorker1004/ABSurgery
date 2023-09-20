import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { IMenuItem } from 'src/web-components/menuItem';
import { NgxsModule, Store } from '@ngxs/store';
import { TranslateService } from '@ngx-translate/core';
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

  navItems: Array<IMenuItem> = [];

  constructor(
    private _router: Router,
    private _store: Store,
    private _translateService: TranslateService
  ) {}

  ngOnInit(): void {
    this.getNavItemsByUserRole();
  }

  getNavItemsByUserRole() {
    if (this.isSurgeon) {
      this.navItems = CERTIFIED_NAV_ITEMS;
    } else {
      this.navItems = TRAINEE_NAV_ITEMS;
    }

    if (this.isExaminer) {
      this.navItems = this.navItems.concat(EXAMINER_NAV_ITEMS);
    }
    // let the page init before translating
    // TODO: [Joe] address this timing issue in a way that does not rely on a timeout
    setTimeout(() => {
      this.navItems = this.translateNavItem(this.navItems);
    }, 100);
  }

  logout() {
    this._store.dispatch(new Logout());
  }

  toggleSideNav() {
    this.handleSideNavToggle.emit();
  }

  featureToggle(item: any) {
    if (item.feature) {
      const feature = item.feature;
      return this._store.selectSnapshot((state) =>
        state.application?.featureFlags
          ? state.application.featureFlags[feature]
          : false
      );
    } else {
      return true;
    }
  }

  toggleSubNav(item: IMenuItem) {
    console.log('toggle side nav', item);
  }

  get router(): Router {
    return this._router;
  }

  /**
   *
   * @param alert
   * @returns
   */
  private translateNavItem(navItems: any[]): any[] {
    return navItems.map((navItem) => {
      if (navItem.displayKey) {
        // this safeguards against the timing issue of the page loading before the translations are ready
        // TODO: [Joe] address this timing issue in a way that does not rely on a timeout
        const newDisplay = this._translateService.instant(navItem.displayKey);
        if (newDisplay !== navItem.displayKey) {
          navItem.display = newDisplay;
        }
      }
      if (navItem.children) {
        navItem.children = this.translateNavItem(navItem.children);
      }
      return navItem;
    });
  }
}
