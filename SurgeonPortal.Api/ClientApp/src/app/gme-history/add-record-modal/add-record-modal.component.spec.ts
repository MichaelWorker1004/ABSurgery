import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddRecordModalComponent } from './add-record-modal.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NgxsModule, Store } from '@ngxs/store';
import { surgeonPortalState } from '../../state/surgeon-portal.state';

describe('AddRecordModalComponent', () => {
  let component: AddRecordModalComponent;
  let fixture: ComponentFixture<AddRecordModalComponent>;
  let store: Store;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        NgxsModule.forRoot(surgeonPortalState),
        AddRecordModalComponent,
      ],
    }).compileComponents();

    store = TestBed.inject(Store);
    fixture = TestBed.createComponent(AddRecordModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call ngOnInit and initialize data', () => {
    spyOn(component, 'fetchDropdownData');
    spyOn(component, 'fetchFormData');
    spyOn(component, 'onChanges');

    component.ngOnInit();

    expect(component.fetchDropdownData).toHaveBeenCalled();
    expect(component.fetchFormData).toHaveBeenCalled();
    expect(component.onChanges).toHaveBeenCalled();
  });

  it('should fetch dropdown data', () => {
    spyOn(component['_store'], 'dispatch');

    component.fetchDropdownData();

    // Check that GetClinicalLevelList, GetClinicalActivityList, GetAccreditedProgramInstitutionsList were dispatched.
    expect(component['_store'].dispatch).toHaveBeenCalledTimes(3);
  });

  it('should validate the form controls', () => {
    const startDateControl = component.addEditRecordsForm.get('startDate');
    const endDateControl = component.addEditRecordsForm.get('endDate');

    startDateControl?.setValue('2023-01-01');
    endDateControl?.setValue('2023-01-10');

    expect(startDateControl?.valid).toBe(true);
    expect(endDateControl?.valid).toBe(true);
  });

  it('should submit the form', () => {
    spyOn(store, 'dispatch');

    const mockFormValues = {
      startDate: '2023-01-01',
      endDate: '2023-01-10',
      clinicalLevelId: 1,
      clinicalActivityId: 2,
      programName: 'Program',
      nonSurgicalActivity: '',
      alternateInstitutionName: '',
      isInternationalRotation: false,
      other: '',
      weeks: '',
    };

    component.addEditRecordsForm.setValue(mockFormValues);
    component.isEditLocal = true; // For update action

    component.onSubmit();

    expect(store.dispatch).toHaveBeenCalled();
  });

  it('should emit relaunchDialog and closeDialog events', () => {
    spyOn(component.closeDialog, 'emit');
    spyOn(component.relaunchDialog, 'emit');

    component.changeModalData(123);
    expect(component.relaunchDialog.emit).toHaveBeenCalledWith(123);

    component.close();
    expect(component.closeDialog.emit).toHaveBeenCalled();
  });
});
