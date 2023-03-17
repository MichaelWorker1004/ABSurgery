import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { CommonModule } from '@angular/common';
import { SideNavigationComponent } from './side-navigation/side-navigation.component';
import { DashboardHeaderComponent } from './dashboard-header/dashboard-header.component';

@Component({
  selector: 'abs-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  standalone: true,
  imports: [
    RouterOutlet,
    LoginComponent,
    CommonModule,
    SideNavigationComponent,
    DashboardHeaderComponent,
  ],
})
export class AppComponent implements OnInit {
  isLogIn = false;
  isSideNavOpen = false;
  userData!: any;

  ngOnInit(): void {
    this.fetchUserData();
  }

  handleSideNavToggle() {
    this.isSideNavOpen = !this.isSideNavOpen ? true : false;
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
