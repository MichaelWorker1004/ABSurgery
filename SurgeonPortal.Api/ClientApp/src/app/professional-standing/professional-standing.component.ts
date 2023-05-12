import { CommonModule } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BehaviorSubject, map, Observable, of, Subject } from 'rxjs';
import { CollapsePanelComponent } from '../shared/components/collapse-panel/collapse-panel.component';
import { ProfileHeaderComponent } from '../shared/components/profile-header/profile-header.component';
import { AppointmentsAddEditModalComponent } from './appointments-add-edit-modal/appointments-add-edit-modal.component';
import { LicenseAddEditModalComponent } from './license-add-edit-modal/license-add-edit-modal.component';
import { GridComponent } from '../shared/components/grid/grid.component';
import { APPOINTMENTS_PRIVILEGES_COLS } from './appointments-privileges-cols';
import { LICENSES_COLS } from './licenses-cols';
import { ModalComponent } from '../shared/components/modal/modal.component';

import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { RadioButtonModule } from 'primeng/radiobutton';

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
    AppointmentsAddEditModalComponent,
    LicenseAddEditModalComponent,
    GridComponent,
    FormsModule,
    ModalComponent,
    InputTextModule,
    DropdownModule,
    InputTextareaModule,
    RadioButtonModule,
  ],
})
export class ProfessionalStandingComponent implements OnInit {
  fakeOptions = [
    { itemDescription: 'Option 1', itemValue: 'option-1' },
    { itemDescription: 'Option 2', itemValue: 'option-2' },
    { itemDescription: 'Option 3', itemValue: 'option-3' },
  ];

  appointmentsPrivilegesCols = APPOINTMENTS_PRIVILEGES_COLS;
  licensesCols = LICENSES_COLS;

  disableDescribe = true;
  editSanctionsAndEthics$: Subject<boolean> = new BehaviorSubject(true);
  editStateMedicalLiscense$: Subject<boolean> = new BehaviorSubject(true);
  editHospitalAppointmentsAndPrivileges$: Subject<boolean> =
    new BehaviorSubject(true);
  stateMedicalLicenseTitle: string | undefined;
  appointmentsTitle: string | undefined;
  // TODO: [Joe] faked user data, replace with real data
  user = {
    profilePicture:
      'https://fastly.picsum.photos/id/91/3504/2336.jpg?hmac=tK6z7RReLgUlCuf4flDKeg57o6CUAbgklgLsGL0UowU',
    fullName: 'John Doe',
    givenName: 'John',
    surName: 'Doe',
    title: 'M.D',
    emailAddress: 'email@test.io',
    status: 'Trainee',
  };

  showAppointmentsAddEdit = false;
  showLicensesAddEdit = false;
  tempAppointment$: Subject<any> = new BehaviorSubject({});
  tempLicense$: Subject<any> = new BehaviorSubject({});
  profile = {
    medicalLicenses: [
      {
        id: 1,
        state: 'Pennsylvania',
        number: '123456',
        type: 'Full',
        issueDate: new Date('10/24/1986'),
        expireDate: new Date('10/24/2024'),
        reportingOrg: 'ABS',
      },
      {
        id: 2,
        state: 'California',
        number: '098765',
        type: 'Full',
        issueDate: new Date('10/24/1986'),
        expireDate: new Date('10/24/2024'),
        reportingOrg: 'Self',
      },
      {
        id: 3,
        state: 'Maryland',
        number: '111222',
        type: 'Full',
        issueDate: new Date('10/24/1986'),
        expireDate: new Date('10/24/2024'),
        reportingOrg: 'ABS',
      },
      {
        id: 4,
        state: 'Pennsylvania',
        number: '333444',
        type: 'Full',
        issueDate: new Date('10/24/1986'),
        expireDate: new Date('10/24/2024'),
        reportingOrg: 'Self',
      },
    ],
    appointmentsAndPrivileges: {
      primaryPractice: '',
      primaryPracticeOrg: '',
      lackOfHospitalPrivilegesReason: '',
      nonClinicalActivities: '',
      list: [
        {
          id: 1,
          practiceType: 'Practice_1',
          appointmentType: 'Appointment_1',
          oranizationType: 'Organization_1',
          state: 'PA',
          institution: 'LVHN',
          other: '-',
          official: 'ABE',
        },
        {
          id: 2,
          practiceType: 'Practice_2',
          appointmentType: 'Appointment_2',
          oranizationType: 'Organization_2',
          state: 'PA',
          institution: 'LVHN',
          other: '-',
          official: 'ABE',
        },
        {
          id: 3,
          practiceType: 'Practice_3',
          appointmentType: 'Appointment_3',
          oranizationType: 'Organization_3',
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

  handleLicensesGridAction($event: any) {
    if ($event.fieldKey === 'edit') {
      this.showLicenseModal($event.data);
    } else {
      console.log('unhandled action', $event);
    }
  }

  showLicenseModal(license: any) {
    if (license) {
      this.tempLicense$.next(license);
      this.stateMedicalLicenseTitle = 'Edit License';
    } else {
      this.tempLicense$.next({
        state: null,
        number: null,
        type: null,
        issueDate: null,
        expireDate: null,
        reportingOrg: null,
      });
      this.stateMedicalLicenseTitle = 'Add License';
    }
    this.showLicensesAddEdit = true;
  }
  saveLicense($event: any) {
    // TODO: [Joe] handle the save call
    // TODO: [Joe] handle the update call
    // TODO: [Joe] show the universal success/error message
    this.showLicensesAddEdit = $event.show;
    this.tempLicense$.next({});
  }
  cancelAddEditLicense($event: any) {
    this.showLicensesAddEdit = $event.show;
    this.tempLicense$.next({});
  }

  handleAppointementsGridAction($event: any) {
    if ($event.fieldKey === 'edit') {
      this.showAppointmentModal($event.data);
    } else if ($event.fieldKey === 'delete') {
      // TODO: [Joe] show confirmation modal
      // TODO: [Joe] handle the delete call
      console.log('delete', $event.data);
    } else {
      console.log('unhandled action', $event);
    }
  }
  showAppointmentModal(appointment: any) {
    if (appointment) {
      this.tempAppointment$.next(appointment);
      this.appointmentsTitle = 'Edit Appointment';
    } else {
      this.tempAppointment$.next({
        practiceType: null,
        appointmentType: null,
        oranizationType: null,
        state: null,
        institution: null,
        other: null,
        official: null,
      });
      this.appointmentsTitle = 'Add Appointment';
    }
    this.showAppointmentsAddEdit = true;
  }
  saveAppointment($event: any) {
    // TODO: [Joe] handle the save call
    // TODO: [Joe] handle the update call
    // TODO: [Joe] show the universal success/error message
    this.showAppointmentsAddEdit = $event.show;
    this.tempAppointment$.next({});
  }
  cancelAddEditAppointment($event: any) {
    this.showAppointmentsAddEdit = $event.show;
    this.tempAppointment$.next({});
  }
}
