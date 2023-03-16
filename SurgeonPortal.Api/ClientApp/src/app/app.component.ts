import { Component } from '@angular/core';
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
export class AppComponent {
  isLogIn = false;
  isSideNavOpen = false;
  currentStatus = 'trainee';

  handleSideNavToggle() {
    this.isSideNavOpen = !this.isSideNavOpen ? true : false;
  }
}
