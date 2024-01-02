import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  ActivatedRoute,
  Router,
  RouterOutlet,
  RouterStateSnapshot,
} from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { NgxsModule, Select, Store } from '@ngxs/store';
import { MessagesModule } from 'primeng/messages';
import { Observable } from 'rxjs';
import packageInfo from '../../package.json';
import { LoginComponent } from './login/login.component';

import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Message } from 'primeng/api';
import { IMenuItem } from 'src/web-components/menuItem';
import { IAppUserReadOnlyModel } from './api';
import { AlertComponent } from './shared/components/alert/alert.component';
import { DashboardHeaderComponent } from './shared/components/dashboard-header/dashboard-header.component';
import { SideNavigationComponent } from './side-navigation/side-navigation.component';
import { UserClaims } from './side-navigation/user-status.enum';
import {
  AuthSelectors,
  GetUserProfile,
  IUserProfile,
  UserProfileSelectors,
} from './state';
import { LoadApplication } from './state/application/application.actions';
import { ALL_NAV_ITEMS } from './nav-items';
import { ButtonModule } from 'primeng/button';

@UntilDestroy()
@Component({
  selector: 'abs-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  standalone: true,
  imports: [
    RouterOutlet,
    LoginComponent,
    CommonModule,
    TranslateModule,
    NgxsModule,
    SideNavigationComponent,
    DashboardHeaderComponent,
    MessagesModule,
    AlertComponent,
    ButtonModule,
  ],
})
export class AppComponent implements OnInit {
  // TODO: MOve this logic into the auth guard
  @Select(AuthSelectors.slices.isAuthenticated) isAuthenticated$:
    | Observable<boolean>
    | undefined;
  @Select(UserProfileSelectors.user) user$:
    | Observable<IUserProfile>
    | undefined;
  @Select(AuthSelectors.loginUser)
  loginUser$: Observable<IAppUserReadOnlyModel> | undefined;

  version = packageInfo.buildId;

  isAuthenticated = false;
  isPasswordReset = false;

  isSurgeon = false;
  isExaminer = false;
  isSideNavOpen = false;
  sideNavLogoPath = '../../assets/img/abs-logo.svg';
  applicationName = 'The American Board Of Surgery';

  navItems: Array<IMenuItem> = [];
  allNavItems: Array<IMenuItem> = ALL_NAV_ITEMS;
  userClaims: string[] = [];
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

  currentYear = new Date().getFullYear();

  keysPressed = new Set();

  preventScreenshot = false;
  messages!: Message[];

  constructor(
    private _store: Store,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.isAuthenticated$?.pipe(untilDestroyed(this)).subscribe((isAuthed) => {
      this.isAuthenticated = isAuthed;
      this.isPasswordReset = this._store.selectSnapshot(
        AuthSelectors.slices.isPasswordReset
      );
      const routerStateSnapshot: RouterStateSnapshot =
        this.router.routerState.snapshot;
      const loginUser = this._store.selectSnapshot(AuthSelectors.loginUser);
      const claims = this._store.selectSnapshot(AuthSelectors.claims);
      if (isAuthed && loginUser && claims) {
        this._store.dispatch(new LoadApplication());
        this.userClaims = claims;
        this.isSurgeon =
          claims.includes(UserClaims.surgeon) &&
          !claims.includes(UserClaims.trainee);

        this.navItems = this.allNavItems.filter((item) => {
          if (item.allowedClaims) {
            if (item.children && item.children.length > 0) {
              item.children = item.children.filter((child) => {
                if (child.allowedClaims) {
                  return child.allowedClaims.some((claim) =>
                    this.userClaims.includes(
                      UserClaims[claim as keyof typeof UserClaims]
                    )
                  );
                }
                return true;
              });

              item.children.sort(
                (a, b) => (a.order ? a.order : 0) - (b.order ? b.order : 0)
              );
            }
            return item.allowedClaims.some((claim) =>
              this.userClaims.includes(
                UserClaims[claim as keyof typeof UserClaims]
              )
            );
          }
          return true;
        });

        this.navItems.sort(
          (a, b) => (a.order ? a.order : 0) - (b.order ? b.order : 0)
        );

        this._store.dispatch(new GetUserProfile(loginUser, claims));
      }

      if (!isAuthed) {
        const returnUrl = routerStateSnapshot.url
          ? routerStateSnapshot.url.includes('oral-examinations/exam/')
            ? '/dashboard'
            : routerStateSnapshot.url
          : '/dashboard';

        if (!window.location.href.includes('forgot-password')) {
          this.router.navigate(['/login'], {
            queryParams: { returnUrl: returnUrl },
          });
        }
      }
    });
  }

  ngOnInit(): void {
    document.addEventListener('keydown', (event) => {
      this.keysPressed.add(event.keyCode);
      this.checkMacOsScreenshotV1();
    });

    document.addEventListener('keyup', (event) => {
      this.keysPressed.delete(event.keyCode);
      if (event.keyCode === 44) {
        this.preventScreenshot = true;
      }
      this.preventScreenshot = false;
    });
  }

  checkMacOsScreenshotV1() {
    const windowsKeyOrCommand = 91;
    const windowsR = 91;
    const commanR = 93;
    const shiftKey = 16;
    const prntScreen = 44;
    const sKey = 83;

    if (
      (this.keysPressed.has(windowsKeyOrCommand) &&
        this.keysPressed.has(shiftKey)) ||
      (this.keysPressed.has(commanR) && this.keysPressed.has(shiftKey)) ||
      (this.keysPressed.has(windowsR) &&
        this.keysPressed.has(shiftKey) &&
        this.keysPressed.has(sKey)) ||
      this.keysPressed.has(prntScreen) ||
      (this.keysPressed.has(windowsKeyOrCommand) &&
        this.keysPressed.has(shiftKey)) ||
      (this.keysPressed.has(windowsKeyOrCommand) &&
        this.keysPressed.has(shiftKey) &&
        this.keysPressed.has(sKey))
    ) {
      this.preventScreenshot = true;
    } else {
      this.preventScreenshot = false;
    }
  }

  handleSideNavToggle() {
    this.isSideNavOpen = !this.isSideNavOpen;
  }
}
