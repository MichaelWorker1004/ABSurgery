import { CommonModule } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { CollapsePanelComponent } from '../shared/components/collapse-panel/collapse-panel.component';
import { ProfileHeaderComponent } from '../shared/components/profile-header/profile-header.component';
import { GridComponent } from '../shared/components/grid/grid.component';
import { APPOINTMENTS_PRIVILEGES_COLS } from './appointments-privileges-cols';
import { LICENSES_COLS } from './licenses-cols';
import { ModalComponent } from '../shared/components/modal/modal.component';

import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { RadioButtonModule } from 'primeng/radiobutton';
import { ButtonModule } from 'primeng/button';
import {
  ProfessionalStandingSelectors,
  GetPSMedicalLicenseList,
  GetPSMedicalLicenseDetails,
  CreatePSMedicalLicense,
  UpdatePSMedicalLicense,
  GetUserProfessionalStandingDetails,
  UpdateUserProfessionalStandingDetails,
  CreateUserProfessionalStandingDetails,
  GetPSAppointmentsAndPrivilegesList,
  GetProfessionalStandingSanctionsDetails,
  CreateProfessionalStandingSanctionsDetails,
  UpdateProfessionalStandingSanctionsDetails,
  UpdatePSAppointmentAndPrivilege,
  CreatePSAppointmentAndPrivilege,
  GetPSAppointmentAndPrivilegeDetails,
  DeletePSAppointmentAndPrivilege,
  ClearProfessionalStandingErrors,
} from '../state';
import {
  GetPicklists,
  IPickListItemNumber,
  PicklistsSelectors,
} from '../state/picklists';
import { Select, Store } from '@ngxs/store';
import {
  IMedicalLicenseReadOnlyModel,
  IMedicalLicenseModel,
  IStateReadOnlyModel,
  IUserProfessionalStandingModel,
  ISanctionsModel,
  IUserAppointmentModel,
  IUserAppointmentReadOnlyModel,
} from '../api';
import { LicenseFormComponent } from './license-form/license-form.component';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { SanctionsFormComponent } from './sanctions-form/sanctions-form.component';
import { AppointmentsFormComponent } from './appointments-form/appointments-form.component';
import { GlobalDialogService } from '../shared/services/global-dialog.service';
import { CurrentAppointmentFormComponent } from './current-appointment-form/current-appointment-form.component';
interface IMedicalLicensePickLists {
  licenseStateOptions: IStateReadOnlyModel[] | undefined;
  licenseTypeOptions: IPickListItemNumber[] | undefined;
}

interface IProfessionalStandingPickLists {
  organizationTypeOptions: IPickListItemNumber[] | undefined;
  primaryPracticeOptions: IPickListItemNumber[] | undefined;
}

interface IAppointementsPrivilegesPickLists {
  stateCodeOptions: IStateReadOnlyModel[] | undefined;
  practiceTypeOptions: IPickListItemNumber[] | undefined;
  organizationTypeOptions: IPickListItemNumber[] | undefined;
  organizationOptions: IPickListItemNumber[] | undefined;
  appointmentTypeOptions: IPickListItemNumber[] | undefined;
}

interface IMedicalLicense extends IMedicalLicenseReadOnlyModel {
  showEdit: boolean;
}

@UntilDestroy()
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
    ModalComponent,
    InputTextModule,
    DropdownModule,
    InputTextareaModule,
    RadioButtonModule,
    ButtonModule,
    LicenseFormComponent,
    SanctionsFormComponent,
    AppointmentsFormComponent,
    CurrentAppointmentFormComponent,
  ],
})
export class ProfessionalStandingComponent implements OnInit {
  /* error variables */
  clearErrors = new ClearProfessionalStandingErrors(); // may need to break this into dividual error clearing actions
  @Select(ProfessionalStandingSelectors.slices.medicalLicenseErrors)
  medicalLicenseErrors$: Observable<any> | undefined;
  @Select(ProfessionalStandingSelectors.slices.appointmentErrors)
  appointmentErrors$: Observable<any> | undefined;
  @Select(ProfessionalStandingSelectors.slices.professionalStandingErrors)
  currentAppointmentErrors$: Observable<any> | undefined;
  @Select(ProfessionalStandingSelectors.slices.sanctionsErrors)
  sanctionsErrors$: Observable<any> | undefined;

  /* Medical License variables */
  @Select(ProfessionalStandingSelectors.slices.medicalLiscenseList)
  medicalLicenses$: Observable<IMedicalLicense[]> | undefined;
  @Select(ProfessionalStandingSelectors.slices.selectedMedicalLicense)
  selectedMedicalLicense$: Observable<IMedicalLicenseModel> | undefined;

  extendedMedicalLicenses$: Subject<IMedicalLicense[]> | undefined =
    new BehaviorSubject([] as any);

  licensesCols = LICENSES_COLS;
  selectedMedicalLicense: IMedicalLicenseModel | undefined;
  stateMedicalLicenseTitle: string | undefined;
  showLicensesAddEdit = false;
  medicalLicensePickLists: IMedicalLicensePickLists = {
    licenseStateOptions: [],
    licenseTypeOptions: [],
  };

  /* Sanctions and Ethics variables */
  @Select(ProfessionalStandingSelectors.slices.sanctions)
  sanctionsAndEthics$: Observable<ISanctionsModel> | undefined;
  editSanctionsAndEthics$: Subject<boolean> = new BehaviorSubject(false);

  sanctionsAndEthics: ISanctionsModel | undefined;

  /* Current Appointments and Privileges variables */
  @Select(ProfessionalStandingSelectors.slices.userProfessionalStandingDetails)
  currentAppointments$: Observable<IUserProfessionalStandingModel> | undefined;
  currentAppointments: any;
  currentAppointmentPickLists: IProfessionalStandingPickLists = {
    organizationTypeOptions: [],
    primaryPracticeOptions: [],
  };

  /* Appointments and Privileges variables */
  @Select(ProfessionalStandingSelectors.slices.allAppointments)
  allAppointments$: Observable<IUserAppointmentReadOnlyModel[]> | undefined;
  @Select(ProfessionalStandingSelectors.slices.selectedAppointment)
  selectedAppointment$: Observable<IUserAppointmentModel> | undefined;
  editHospitalAppointmentsAndPrivileges$: Subject<boolean> =
    new BehaviorSubject(false);

  appointmentsPrivilegesCols = APPOINTMENTS_PRIVILEGES_COLS;
  appointmentsTitle: string | undefined;
  selectedAppointment: IUserAppointmentModel | undefined;
  showAppointmentsAddEdit = false;
  appointmentsPrivilegesPickLists: IAppointementsPrivilegesPickLists = {
    stateCodeOptions: [],
    practiceTypeOptions: [],
    organizationTypeOptions: [],
    organizationOptions: [],
    appointmentTypeOptions: [],
  };

  constructor(
    private _store: Store,
    private globalDialogService: GlobalDialogService
  ) {
    this.initProfileData();
  }

  ngOnInit() {
    this.initPicklistValues();
    this.setStateMedicalLicenseEdit();
  }

  setStateMedicalLicenseEdit() {
    this.medicalLicenses$?.subscribe((medicalLicenses: IMedicalLicense[]) => {
      const extendedLicenses: IMedicalLicense[] = medicalLicenses.map(
        (license) => ({
          ...license,
          showEdit: license.reportingOrganization === 'Self',
        })
      );
      this.extendedMedicalLicenses$?.next(extendedLicenses);
    });
  }

  initPicklistValues() {
    // defaulting country code to 500 for US states
    this._store
      .dispatch(new GetPicklists('500'))
      .pipe(untilDestroyed(this))
      .subscribe(() => {
        //create new objects to trigger change detection
        const newMedicalLicensePickLists: IMedicalLicensePickLists = {
          licenseStateOptions: [],
          licenseTypeOptions: [],
        };
        const newAppointmentsPrivilegesPickLists: IAppointementsPrivilegesPickLists =
          {
            stateCodeOptions: [],
            practiceTypeOptions: [],
            organizationTypeOptions: [],
            organizationOptions: [],
            appointmentTypeOptions: [],
          };
        const newProfessionalStandingPickLists: IProfessionalStandingPickLists =
          {
            organizationTypeOptions: [],
            primaryPracticeOptions: [],
          };

        //medical license picklists
        newMedicalLicensePickLists.licenseStateOptions =
          this._store.selectSnapshot(PicklistsSelectors.slices.states);
        newMedicalLicensePickLists.licenseTypeOptions =
          this._store.selectSnapshot(PicklistsSelectors.slices.licenseTypes);

        this.medicalLicensePickLists = newMedicalLicensePickLists;

        //appointments and privileges picklists
        newAppointmentsPrivilegesPickLists.stateCodeOptions =
          this._store.selectSnapshot(PicklistsSelectors.slices.states);
        newAppointmentsPrivilegesPickLists.practiceTypeOptions =
          this._store.selectSnapshot(PicklistsSelectors.slices.practiceTypes);
        newAppointmentsPrivilegesPickLists.organizationTypeOptions =
          this._store.selectSnapshot(
            PicklistsSelectors.slices.organizationTypes
          );
        newAppointmentsPrivilegesPickLists.organizationOptions =
          this._store.selectSnapshot(
            PicklistsSelectors.slices.jcahoOrganizations
          );
        newAppointmentsPrivilegesPickLists.appointmentTypeOptions =
          this._store.selectSnapshot(
            PicklistsSelectors.slices.appointmentTypes
          );
        this.appointmentsPrivilegesPickLists =
          newAppointmentsPrivilegesPickLists;

        //professional standing picklists
        newProfessionalStandingPickLists.organizationTypeOptions =
          this._store.selectSnapshot(
            PicklistsSelectors.slices.organizationTypes
          );
        newProfessionalStandingPickLists.primaryPracticeOptions =
          this._store.selectSnapshot(
            PicklistsSelectors.slices.primaryPractices
          );
        this.currentAppointmentPickLists = newProfessionalStandingPickLists;
      });
  }

  initProfileData() {
    this.getMedicalLicenses();
    this.getCurrentAppointmentDetails();
    this.getPreviousAppointmentsAndPrivileges();
    this.getSanctionsAndEthicsDetails();
  }

  getCurrentAppointmentDetails() {
    this._store
      .dispatch(new GetUserProfessionalStandingDetails())
      .pipe(untilDestroyed(this))
      .subscribe(() => {
        this.currentAppointments$?.subscribe((res) => {
          this.currentAppointments = res;
          if (!res) {
            this.editHospitalAppointmentsAndPrivileges$.next(true);
          }
        });
      });
  }

  getSanctionsAndEthicsDetails() {
    this._store
      .dispatch(new GetProfessionalStandingSanctionsDetails())
      .pipe(untilDestroyed(this))
      .subscribe(() => {
        this.sanctionsAndEthics$?.subscribe((res) => {
          this.sanctionsAndEthics = res;
          if (!res) {
            this.editSanctionsAndEthics$.next(true);
          }
        });
      });
  }

  getPreviousAppointmentsAndPrivileges() {
    this._store.dispatch(new GetPSAppointmentsAndPrivilegesList());
  }

  getAppointmentDetails(appointment: IUserAppointmentModel) {
    if (appointment.apptId) {
      this._store
        .dispatch(new GetPSAppointmentAndPrivilegeDetails(appointment.apptId))
        .pipe(untilDestroyed(this))
        .subscribe(() => {
          this.selectedAppointment = this._store.selectSnapshot(
            ProfessionalStandingSelectors.slices.selectedAppointment
          );
        });
    }
  }

  getMedicalLicenses() {
    this._store.dispatch(new GetPSMedicalLicenseList());
  }

  getMedicalLicenseDetails(license: IMedicalLicenseReadOnlyModel) {
    if (license.licenseId) {
      this._store
        .dispatch(new GetPSMedicalLicenseDetails(license.licenseId))
        .pipe(untilDestroyed(this))
        .subscribe(() => {
          this.selectedMedicalLicense = this._store.selectSnapshot(
            ProfessionalStandingSelectors.slices.selectedMedicalLicense
          );
        });
    }
  }

  /* Medical License Functions */
  handleLicensesGridAction($event: any) {
    if ($event.fieldKey === 'edit') {
      this.showLicenseModal($event.data);
    } else {
      console.log('unhandled action', $event);
    }
  }

  showLicenseModal(license: IMedicalLicenseReadOnlyModel | null | undefined) {
    if (license) {
      this.getMedicalLicenseDetails(license);
      this.stateMedicalLicenseTitle = 'Edit Medical License';
    } else {
      this.selectedMedicalLicense = undefined;
      this.stateMedicalLicenseTitle = 'Add Medical License';
    }
    this.showLicensesAddEdit = true;
  }

  saveLicense($event: any) {
    let issueDate = '';
    let expireDate = '';
    if ($event.data.issueDate) {
      issueDate = new Date($event.data.issueDate).toISOString();
    }
    if ($event.data.expireDate) {
      expireDate = new Date($event.data.expireDate).toISOString();
    }
    const newLicense = {
      licenseId: this.selectedMedicalLicense?.licenseId ?? 0,
      issuingStateId: $event.data.issuingStateId ?? '',
      licenseNumber: $event.data.licenseNumber ?? '',
      licenseTypeId: $event.data.licenseTypeId ?? 0,
      issueDate: issueDate,
      expireDate: expireDate,
      reportingOrganization:
        this.selectedMedicalLicense?.reportingOrganization ?? 'Self',
    } as unknown as IMedicalLicenseModel;

    if ($event.isEdit) {
      this._store
        .dispatch(new UpdatePSMedicalLicense(newLicense))
        .pipe(untilDestroyed(this))
        .subscribe((res) => {
          if (!res.professionalStanding?.medicalLicenseErrors) {
            this.showLicensesAddEdit = $event.show;
            this.selectedMedicalLicense = undefined;
          }
        });
    } else {
      this._store
        .dispatch(new CreatePSMedicalLicense(newLicense))
        .pipe(untilDestroyed(this))
        .subscribe((res) => {
          if (!res.professionalStanding?.medicalLicenseErrors) {
            this.showLicensesAddEdit = $event.show;
            this.selectedMedicalLicense = undefined;
          }
        });
    }
  }

  cancelAddEditLicense($event: any) {
    this.showLicensesAddEdit = $event.show;
  }

  /* Sanctions and Ethics Functions */
  saveSanctionsAndEthics($event: any) {
    const newSanctionsAndEthics = {
      ...$event.data,
    } as unknown as ISanctionsModel;

    if (this.sanctionsAndEthics) {
      this._store
        .dispatch(
          new UpdateProfessionalStandingSanctionsDetails(newSanctionsAndEthics)
        )
        .pipe(untilDestroyed(this))
        .subscribe((res) => {
          if (!res.professionalStanding?.sanctionsErrors) {
            this.toggleEdit(this.editSanctionsAndEthics$, false);
          }
        });
    } else {
      this._store
        .dispatch(
          new CreateProfessionalStandingSanctionsDetails(newSanctionsAndEthics)
        )
        .pipe(untilDestroyed(this))
        .subscribe((res) => {
          if (!res.professionalStanding?.sanctionsErrors) {
            this.toggleEdit(this.editSanctionsAndEthics$, false);
          }
        });
    }
  }

  /* Current Appointments Functions */
  saveCurrentAppointments($event: any) {
    const newCurrentAppointments = {
      ...$event.data,
      clinicallyActive: $event.data.clinicallyActive ? 1 : 0,
    } as unknown as IUserProfessionalStandingModel;

    if (this.currentAppointments) {
      this._store
        .dispatch(
          new UpdateUserProfessionalStandingDetails(newCurrentAppointments)
        )
        .pipe(untilDestroyed(this))
        .subscribe((res) => {
          if (!res.professionalStanding?.professionalStandingErrors) {
            this.toggleEdit(this.editHospitalAppointmentsAndPrivileges$, false);
          }
        });
    } else {
      // create
      this._store
        .dispatch(
          new CreateUserProfessionalStandingDetails(newCurrentAppointments)
        )
        .pipe(untilDestroyed(this))
        .subscribe((res) => {
          if (!res.professionalStanding?.professionalStandingErrors) {
            this.toggleEdit(this.editHospitalAppointmentsAndPrivileges$, false);
          }
        });
    }
  }

  /* Appointments List Functions */
  handleAppointementsGridAction($event: any) {
    if ($event.fieldKey === 'edit') {
      this.showAppointmentModal($event.data);
    } else if ($event.fieldKey === 'delete') {
      this.globalDialogService
        .showConfirmation(
          'Confirm Delete',
          'Are you sure you want to delete this record?'
        )
        .then((result) => {
          if (result) {
            this.deleteAppointment($event.data.apptId);
          }
        });
    } else {
      console.log('unhandled action', $event);
    }
  }
  showAppointmentModal(appointment: any) {
    if (appointment) {
      this.getAppointmentDetails(appointment);
      this.appointmentsTitle = 'Edit Appointment';
    } else {
      this.selectedAppointment = undefined;
      this.appointmentsTitle = 'Add Appointment';
    }
    this.showAppointmentsAddEdit = true;
  }

  saveAppointment($event: any) {
    // get orgId from autocomplete object
    const orgId = $event.data.organizationId?.itemValue ?? 0;
    const newAppointment = {
      apptId: this.selectedAppointment?.apptId ?? 0,
      practiceTypeId: $event.data.practiceTypeId ?? 0,
      appointmentTypeId: $event.data.appointmentTypeId ?? 0,
      organizationTypeId: $event.data.organizationTypeId ?? 0,
      authorizingOfficial: $event.data.authorizingOfficial ?? '',
      organizationId: orgId,
      stateCode: $event.data.stateCode ?? '',
      other: $event.data.other ?? '',
    } as unknown as IUserAppointmentModel;
    if ($event.isEdit) {
      this._store
        .dispatch(new UpdatePSAppointmentAndPrivilege(newAppointment))
        .pipe(untilDestroyed(this))
        .subscribe((res) => {
          if (!res.professionalStanding?.appointmentErrors) {
            this.showAppointmentsAddEdit = $event.show;
            this.selectedAppointment = undefined;
          }
        });
    } else {
      this._store
        .dispatch(new CreatePSAppointmentAndPrivilege(newAppointment))
        .pipe(untilDestroyed(this))
        .subscribe((res) => {
          if (!res.professionalStanding?.appointmentErrors) {
            this.showAppointmentsAddEdit = $event.show;
            this.selectedAppointment = undefined;
          }
        });
    }
  }

  deleteAppointment(apptId: number) {
    this._store.dispatch(new DeletePSAppointmentAndPrivilege(apptId));
  }

  cancelAddEditAppointment($event: any) {
    this.showAppointmentsAddEdit = $event.show;
    this.selectedAppointment = undefined;
  }

  /* on page form helper functions */
  toggleEdit(observable$: Subject<boolean>, value: boolean) {
    observable$.next(value);
  }
}
