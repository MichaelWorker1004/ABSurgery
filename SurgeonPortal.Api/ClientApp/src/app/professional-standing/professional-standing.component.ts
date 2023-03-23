import { CommonModule } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BehaviorSubject, Subject } from 'rxjs';
import { CollapsePanelComponent } from '../shared/components/collapse-panel/collapse-panel.component';
import { ProfileHeaderComponent } from '../shared/components/profile-header/profile-header.component';
import { GridComponent } from '../shared/components/grid/grid.component';
import { APPOINTMENTS_PRIVILEGES_COLS } from './appointments-privileges-cols';

@Component({
  selector: 'abs-professional-standing',
  templateUrl: './professional-standing.component.html',
  styleUrls: ['./professional-standing.component.scss'],
  standalone: true,
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  imports: [
    CommonModule,
    CollapsePanelComponent,
    ProfileHeaderComponent,
    GridComponent,
    FormsModule,
  ],
})
export class ProfessionalStandingComponent implements OnInit {
  appointmentsPrivilegesCols = APPOINTMENTS_PRIVILEGES_COLS;

  disableDescribe = true;
  editSanctionsAndEthics$: Subject<boolean> = new BehaviorSubject(true);
  editStateMedicalLiscense$: Subject<boolean> = new BehaviorSubject(true);
  editHospitalAppointmentsAndPrivileges$: Subject<boolean> =
    new BehaviorSubject(true);
  // TODO: [Joe] faked user data, replace with real data
  user = {
    profilePicture:
      'https://fastly.picsum.photos/id/91/3504/2336.jpg?hmac=tK6z7RReLgUlCuf4flDKeg57o6CUAbgklgLsGL0UowU',
    givenName: 'John',
    surName: 'Doe',
    title: 'M.D',
    email: 'email@test.io',
    status: 'Trainee',
  };

  profile = {
    appointmentsAndPrivileges: {
      primaryPractice: '',
      primaryPracticeOrg: '',
      lackOfHospitalPrivilegesReason: '',
      nonClinicalActivities: '',
      list: [
        {
          practiceType: 'Practice 1',
          appointmentType: 'Appointment 1',
          oranizationType: 'Organization 1',
          state: 'PA',
          institution: 'LVHN',
          other: '-',
          official: 'ABE',
        },
        {
          practiceType: 'Practice 2',
          appointmentType: 'Appointment 2',
          oranizationType: 'Organization 2',
          state: 'PA',
          institution: 'LVHN',
          other: '-',
          official: 'ABE',
        },
        {
          practiceType: 'Practice 3',
          appointmentType: 'Appointment 3',
          oranizationType: 'Organization 3',
          state: 'PA',
          institution: 'LVHN',
          other: '-',
          official: 'ABE',
        },
      ],
    },
    sanctionsEthics: {
      drugOrAlchohol: true,
      hospitalPrivilegesRevoked: false,
      liscensureRevoked: null,
      hospitalStaffPrivilegesRevoked: null,
      felony: null,
      censured: null,
      describe: '',
    },
  };

  ngOnInit() {
    this.checkSantionsAndEthics();
  }

  toggleEdit(observable$: Subject<boolean>, value: boolean) {
    observable$.next(value);
  }

  checkSantionsAndEthics() {
    // TODO: [Joe] figure out why sl-radio-group is not updating the value on change
    this.disableDescribe = !Object.values(this.profile.sanctionsEthics).some(
      (value) => value === true
    );
  }

  handleGridAction($event: any) {
    // do action
    console.log($event);
  }
}
