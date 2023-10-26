import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CurrentAppointmentFormComponent } from './current-appointment-form.component';

describe('CurrentAppointmentFormComponent', () => {
  let component: CurrentAppointmentFormComponent;
  let fixture: ComponentFixture<CurrentAppointmentFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CurrentAppointmentFormComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(CurrentAppointmentFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize the medicalLicenseForm', () => {
    expect(component.currentAppointmentForm).toBeDefined();
  });

  it('should set form values in ngOnInit', () => {
    const formData = {
      clinicallyActive: 1,
      primaryPracticeId: 1,
      organizationTypeId: 2,
      explanationOfNonPrivileges: 'Sample Explanation',
      explanationOfNonClinicalActivities: 'Sample Explanation',
    };

    component.formData = formData;
    component.ngOnInit();

    expect(component.originalFormValues).toEqual(formData);
    expect(component.currentAppointmentForm.getRawValue()).toEqual({
      ...formData,
      clinicallyActive: true,
    });
  });

  it('should update originalFormValues and localEdit on formData change', () => {
    const newFormData = {
      clinicallyActive: 1,
      primaryPracticeId: 1,
      organizationTypeId: 2,
      explanationOfNonPrivileges: 'Sample Explanation',
      explanationOfNonClinicalActivities: 'Sample Explanation',
    };

    component.formData = newFormData;
    component.ngOnChanges({
      formData: {
        currentValue: newFormData,
        previousValue: undefined,
        firstChange: false,
        isFirstChange: function (): boolean {
          throw new Error('Function not implemented.');
        },
      },
    });

    expect(component.originalFormValues).toEqual(newFormData);
    expect(component.currentAppointmentForm.getRawValue()).toEqual({
      ...newFormData,
      clinicallyActive: true,
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
      primaryPracticeOptions: ['State1', 'State2'],
      organizationTypeOptions: ['Type1', 'Type2'],
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

  it('should emit saveDialog event on onSubmit', () => {
    const spy = spyOn(component.saveForm, 'emit');
    const formValue = {
      clinicallyActive: 1,
      primaryPracticeId: 1,
      organizationTypeId: 2,
      explanationOfNonPrivileges: 'Sample Explanation',
      explanationOfNonClinicalActivities: 'Sample Explanation',
    };

    component.formData = formValue;
    component.ngOnInit();

    component.localEdit = true;
    component.onSubmit();

    expect(spy).toHaveBeenCalledWith({
      show: false,
      data: {
        ...formValue,
        clinicallyActive: true,
      },
      isEdit: true,
    });
  });

  it('should emit closeDialog event on close', () => {
    const spy = spyOn(component.cancelForm, 'emit');
    component.close();
    expect(spy).toHaveBeenCalledWith({ show: false });
  });

  afterEach(() => {
    TestBed.resetTestingModule();
  });
});
