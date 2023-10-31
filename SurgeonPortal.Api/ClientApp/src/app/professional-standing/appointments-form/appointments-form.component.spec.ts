import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppointmentsFormComponent } from './appointments-form.component';

describe('AppointmentsFormComponent', () => {
  let component: AppointmentsFormComponent;
  let fixture: ComponentFixture<AppointmentsFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AppointmentsFormComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(AppointmentsFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize the hospitalAppointmentForm', () => {
    expect(component.hospitalAppointmentForm).toBeDefined();
  });

  it('should set form values in ngOnInit', () => {
    const formData = {
      practiceTypeId: 1,
      appointmentTypeId: 2,
      organizationTypeId: 3,
      stateCode: 'CA',
      organizationId: 4,
      other: '',
      authorizingOfficial: 'John Doe',
    };

    component.formData = formData;
    component.ngOnInit();

    expect(component.originalFormValues).toEqual(formData);
    expect(component.hospitalAppointmentForm.value).toEqual({
      ...formData,
      organizationId: undefined,
    });
  });

  it('should update localEdit on isEdit change', () => {
    component.isEdit = true;
    component.ngOnChanges({
      isEdit: {
        currentValue: true,
        previousValue: undefined,
        firstChange: false,
        isFirstChange: function (): boolean {
          throw new Error('Function not implemented.');
        },
      },
    });
    expect(component.localEdit).toBe(true);
  });

  it('should update optionLists on picklists change', () => {
    const newPicklists = {
      practiceTypeOptions: ['Type1', 'Type2'],
      appointmentTypeOptions: ['Appointment1', 'Appointment2'],
      organizationTypeOptions: ['OrgType1', 'OrgType2'],
      stateCodeOptions: ['CA', 'NY'],
      organizationOptions: [{ itemValue: 1, itemDescription: 'Org1' }],
      filteredOrganizationOptions: [{ itemValue: 2, itemDescription: 'Org2' }],
    };

    component.picklists = newPicklists;
    component.ngOnChanges({
      picklists: {
        currentValue: newPicklists,
        previousValue: undefined,
        firstChange: false,
        isFirstChange: function (): boolean {
          throw new Error('Function not implemented.');
        },
      },
    });

    expect(component.optionLists).toEqual(newPicklists);
  });

  it('should disable "other" field when organizationId has a non-empty value', () => {
    // Simulate changing organizationId to a non-empty value
    component.hospitalAppointmentForm.get('organizationId')?.setValue(1);
    expect(component.hospitalAppointmentForm.get('other')?.disabled).toBe(true);
    expect(
      component.hospitalAppointmentForm.get('other')?.hasError('required')
    ).toBe(false);
  });

  it('should enable "other" field when organizationId is empty', () => {
    // Simulate changing organizationId to an empty value
    component.hospitalAppointmentForm.get('organizationId')?.setValue('');
    expect(component.hospitalAppointmentForm.get('other')?.disabled).toBe(
      false
    );
    expect(
      component.hospitalAppointmentForm.get('other')?.hasError('required')
    ).toBe(true);
  });

  it('should emit saveForm event on onSubmit', () => {
    const saveFormSpy = spyOn(component.saveForm, 'emit');
    const formData = {
      practiceTypeId: 1,
      appointmentTypeId: 2,
      organizationTypeId: 3,
      stateCode: 'CA',
      organizationId: 4,
      other: '',
      authorizingOfficial: 'John Doe',
    };

    component.formData = formData;
    component.ngOnInit();

    component.localEdit = true;
    component.onSubmit();

    expect(saveFormSpy).toHaveBeenCalledWith({
      show: false,
      data: {
        ...formData,
        organizationId: undefined,
      },
      isEdit: true,
    });
  });

  it('should emit cancelForm event on close', () => {
    const cancelFormSpy = spyOn(component.cancelForm, 'emit');
    component.close();
    expect(cancelFormSpy).toHaveBeenCalledWith({ show: false });
  });

  afterEach(() => {
    TestBed.resetTestingModule();
  });
});
