import { Component, OnInit } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { NgxsModule, Select } from '@ngxs/store';
import { AuthSelectors } from './state/auth/auth.selectors';
import { SideNavigationComponent } from './side-navigation/side-navigation.component';
import { DashboardHeaderComponent } from './shared/components/dashboard-header/dashboard-header.component';

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
export class AppComponent implements OnInit {
  // TODO: MOve this logic into the auth guard
  @Select(AuthSelectors.isAuthenticated) isAuthenticated$:
    | Observable<boolean>
    | undefined;
  isLogIn = true;
  isSideNavOpen = false;
  userData!: any;

  constructor(private _router: Router) {
    // TODO: MOve this logic into the auth guard
    this.isAuthenticated$?.subscribe((isAuthed) => {
      if (isAuthed) {
        if (location.pathname === '/') {
          this._router.navigateByUrl('/dashboard');
        }
      }
    });
  }

  ngOnInit(): void {
    this.fetchUserData();
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
