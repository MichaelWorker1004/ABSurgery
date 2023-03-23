import { CommonModule } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BehaviorSubject, Subject } from 'rxjs';
import { CollapsePanelComponent } from '../shared/components/collapse-panel/collapse-panel.component';
import { ProfileHeaderComponent } from '../shared/components/profile-header/profile-header.component';

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
    FormsModule,
  ],
})
export class ProfessionalStandingComponent implements OnInit {
  disableDescribe = true;
  editSanctionsAndEthics$: Subject<boolean> = new BehaviorSubject(false);
  editStateMedicalLiscense$: Subject<boolean> = new BehaviorSubject(false);
  editHospitalAppointmentsAndPrivileges$: Subject<boolean> =
    new BehaviorSubject(false);
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
}
