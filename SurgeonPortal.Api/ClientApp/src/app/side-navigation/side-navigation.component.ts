import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { NgxsModule, Store } from '@ngxs/store';
import { IMenuItem } from 'src/web-components/menuItem';
import { CloseApplication } from '../state/application/application.actions';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { distinctUntilChanged } from 'rxjs';

@UntilDestroy()
@Component({
  selector: 'abs-side-navigation',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive, NgxsModule],
  templateUrl: './side-navigation.component.html',
  styleUrls: ['./side-navigation.component.scss'],
})
export class SideNavigationComponent implements OnInit {
  /**
   * Emits an event when the side navigation is toggled
   * @type {EventEmitter<any>}
   */
  @Output() handleSideNavToggle: EventEmitter<any> = new EventEmitter();

  /**
   * The path to the logo image
   * @type {string}
   */
  @Input() logoPath!: string;

  /**
   * The name of the application
   * @type {string}
   */
  @Input() applicationName!: string;

  /**
   * The Navigtion Items
   * @type {Array<IMenuItem>}
   */
  @Input() navItems: Array<IMenuItem> = [];

  localNavItems: Array<IMenuItem> = [];

  constructor(
    private _router: Router,
    private _store: Store,
    private _translateService: TranslateService
  ) {}
  ngOnInit(): void {
    this.localNavItems = this.translateNavItem(this.navItems);
  }

  logout() {
    this._store.dispatch(new CloseApplication());
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
        this._translateService
          .get(navItem.displayKey)
          .pipe(distinctUntilChanged(), untilDestroyed(this))
          .subscribe((res) => {
            const newDisplay = res;
            if (newDisplay !== navItem.displayKey) {
              navItem.display = newDisplay;
            }
          });
      }
      if (navItem.children) {
        navItem.children = this.translateNavItem(navItem.children);
      }
      return navItem;
    });
  }
}
