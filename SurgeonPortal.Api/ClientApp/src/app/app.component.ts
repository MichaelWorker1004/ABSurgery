import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { CommonModule } from '@angular/common';
import { Observable, Subscription } from 'rxjs';
import { NgxsModule, Select, Store } from '@ngxs/store';

import {
  AuthSelectors,
  GetUserProfile,
  IUserProfile,
  UserProfileSelectors,
} from './state';
import { SideNavigationComponent } from './side-navigation/side-navigation.component';
import { DashboardHeaderComponent } from './shared/components/dashboard-header/dashboard-header.component';
import { UserClaims } from './side-navigation/user-status.enum';

@Component({
  selector: 'abs-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  standalone: true,
  imports: [
    RouterOutlet,
    LoginComponent,
    CommonModule,
    NgxsModule,
    SideNavigationComponent,
    DashboardHeaderComponent,
  ],
})
export class AppComponent implements OnInit, OnDestroy {
  // TODO: MOve this logic into the auth guard
  @Select(AuthSelectors.isAuthenticated) isAuthenticated$:
    | Observable<boolean>
    | undefined;
  @Select(UserProfileSelectors.user) user$:
    | Observable<IUserProfile>
    | undefined;

  authSub: Subscription | undefined;
  userSub: Subscription | undefined;

  isSurgeon = false;

  isSideNavOpen = false;
  userData!: any;

  constructor(private _router: Router, private _store: Store) {
    this.authSub = this.isAuthenticated$?.subscribe((isAuthed) => {
      const loginUser = this._store.selectSnapshot(AuthSelectors.loginUser);
      const claims = this._store.selectSnapshot(AuthSelectors.claims);
      if (isAuthed && loginUser && claims) {
        if (claims.includes(UserClaims.surgeon)) {
          this.isSurgeon = true;
        }
        this._store.dispatch(new GetUserProfile(loginUser, claims));
      }
    });
  }

  ngOnInit(): void {
    this.fetchUserData();
  }

  ngOnDestroy(): void {
    this.authSub?.unsubscribe();
    this.userSub?.unsubscribe();
  }

  handleSideNavToggle() {
    this.isSideNavOpen = !this.isSideNavOpen;
  }

  fetchUserData() {
    this.userData = {
      name: 'John Doe, M.D',
      id: '00001',
      currentStatus: 'Certified - Clincally Active',
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
}
