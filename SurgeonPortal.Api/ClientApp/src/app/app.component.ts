import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  ActivatedRoute,
  Router,
  RouterOutlet,
  RouterStateSnapshot,
} from '@angular/router';
import { LoginComponent } from './login/login.component';
import { CommonModule } from '@angular/common';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { Observable, Subscription, take } from 'rxjs';
import { NgxsModule, Select, Store } from '@ngxs/store';
import packageInfo from '../../package.json';
import { MessagesModule } from 'primeng/messages';

import {
  AuthSelectors,
  GetUserProfile,
  IUserProfile,
  UserProfileSelectors,
} from './state';
import { SideNavigationComponent } from './side-navigation/side-navigation.component';
import { DashboardHeaderComponent } from './shared/components/dashboard-header/dashboard-header.component';
import { UserClaims } from './side-navigation/user-status.enum';
import { Message } from 'primeng/api';
import { AlertComponent } from './shared/components/alert/alert.component';
import { LoadApplication } from './state/application/application.actions';
import { IAppUserReadOnlyModel } from './api';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

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

  currentYear = new Date().getFullYear();

  keysPressed = new Set();

  preventScreenshot = false;
  messages!: Message[];

  constructor(
    private _store: Store,
    private translate: TranslateService,
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
        this.isSurgeon =
          claims.includes(UserClaims.surgeon) &&
          !claims.includes(UserClaims.trainee);
        this.isExaminer = claims.includes(UserClaims.examiner);
        this._store.dispatch(new GetUserProfile(loginUser, claims));
      }

      if (!isAuthed) {
        const returnUrl = routerStateSnapshot.url
          ? routerStateSnapshot.url
          : '/dashboard';
        this.router.navigate(['/login'], {
          queryParams: { returnUrl: returnUrl },
        });
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
