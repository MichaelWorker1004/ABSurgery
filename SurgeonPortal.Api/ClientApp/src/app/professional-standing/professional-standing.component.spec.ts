import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfessionalStandingComponent } from './professional-standing.component';
import { NgxsModule, Store } from '@ngxs/store';
import { surgeonPortalState } from '../state/surgeon-portal.state';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { take } from 'rxjs';
import { GlobalDialogService } from '../shared/services/global-dialog.service';
import {
  GetPSAppointmentsAndPrivilegesList,
  GetPSMedicalLicenseList,
  GetProfessionalStandingSanctionsDetails,
  GetUserProfessionalStandingDetails,
} from '../state';

describe('ProfessionalStandingComponent', () => {
  let component: ProfessionalStandingComponent;
  let fixture: ComponentFixture<ProfessionalStandingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        ProfessionalStandingComponent,
        NgxsModule.forRoot(surgeonPortalState),
        HttpClientTestingModule,
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(ProfessionalStandingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize picklist values', () => {
    // Test that the component initializes picklist values correctly.
    component.initPicklistValues();
    // You can use expectations here to check if the picklists are properly populated.
    expect(component.medicalLicensePickLists.licenseStateOptions).toBeDefined();
    expect(component.medicalLicensePickLists.licenseTypeOptions).toBeDefined();

    expect(
      component.appointmentsPrivilegesPickLists.stateCodeOptions
    ).toBeDefined();
    expect(
      component.appointmentsPrivilegesPickLists.practiceTypeOptions
    ).toBeDefined();
    expect(
      component.appointmentsPrivilegesPickLists.organizationTypeOptions
    ).toBeDefined();
    expect(
      component.appointmentsPrivilegesPickLists.organizationOptions
    ).toBeDefined();
    expect(
      component.appointmentsPrivilegesPickLists.appointmentTypeOptions
    ).toBeDefined();

    expect(
      component.currentAppointmentPickLists.organizationTypeOptions
    ).toBeDefined();
    expect(
      component.currentAppointmentPickLists.primaryPracticeOptions
    ).toBeDefined();
    // Similar tests for other picklists.
  });

  it('should get initial data', () => {
    const store = TestBed.inject(Store);
    spyOn(store, 'dispatch');
    // You can test that the component properly dispatches the action to get medical licenses
    // and handles the result.
    component.initProfileData();

    expect(store.dispatch).toHaveBeenCalledWith(new GetPSMedicalLicenseList());
    expect(store.dispatch).toHaveBeenCalledWith(
      new GetUserProfessionalStandingDetails()
    );
    expect(store.dispatch).toHaveBeenCalledWith(
      new GetPSAppointmentsAndPrivilegesList()
    );
    expect(store.dispatch).toHaveBeenCalledWith(
      new GetProfessionalStandingSanctionsDetails()
    );
    // Mock the action and check if it's dispatched and if the component handles it.
    // You'll need to use a testing library or a mock store to do this.
  });

  it('should handle licenses grid action', () => {
    const spy = spyOn(component, 'showLicenseModal');
    // Test the handleLicensesGridAction method, passing in an event.
    const event = { fieldKey: 'edit', data: {} };
    component.handleLicensesGridAction(event);
    // Check that the component handles the event correctly.
    // You can use expectations to verify the resulting state or actions dispatched.
    expect(spy).toHaveBeenCalled();
  });

  it('should handle Appointements grid action edit', () => {
    const spy = spyOn(component, 'showAppointmentModal');
    // Test the handleLicensesGridAction method, passing in an event.
    const event = { fieldKey: 'edit', data: {} };
    component.handleAppointementsGridAction(event);
    // Check that the component handles the event correctly.
    // You can use expectations to verify the resulting state or actions dispatched.
    expect(spy).toHaveBeenCalled();
  });

  it('should handle Appointements grid action delete', () => {
    const globalDialogService = TestBed.inject(GlobalDialogService);
    spyOn(globalDialogService, 'showConfirmation').and.returnValue(
      Promise.resolve(true)
    );
    // Test the handleLicensesGridAction method, passing in an event.
    const event = { fieldKey: 'delete', data: {} };
    component.handleAppointementsGridAction(event);
    // Check that the component handles the event correctly.
    // You can use expectations to verify the resulting state or actions dispatched.
    expect(globalDialogService.showConfirmation).toHaveBeenCalled();
  });

  it('should toggle edit', () => {
    // Test the toggleEdit method, passing in an observable and a value.
    component.toggleEdit(component.editSanctionsAndEthics$, true);
    component.editSanctionsAndEthics$.pipe(take(1)).subscribe((value) => {
      expect(value).toBe(true);
    });

    component.toggleEdit(component.editSanctionsAndEthics$, false);
    component.editSanctionsAndEthics$.pipe(take(1)).subscribe((value) => {
      expect(value).toBe(false);
    });
    // Use expectations to verify that the observable is updated.
  });

  afterEach(() => {
    TestBed.resetTestingModule();
  });
});
