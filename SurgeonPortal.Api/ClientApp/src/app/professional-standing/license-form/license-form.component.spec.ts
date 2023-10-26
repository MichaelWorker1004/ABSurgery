import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LicenseFormComponent } from './license-form.component';

describe('LicenseFormComponent', () => {
  let component: LicenseFormComponent;
  let fixture: ComponentFixture<LicenseFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LicenseFormComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(LicenseFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize the medicalLicenseForm', () => {
    expect(component.medicalLicenseForm).toBeDefined();
  });

  it('should set form values in ngOnInit', () => {
    const formData = {
      issuingStateId: 1,
      licenseNumber: '12345',
      licenseTypeId: 2,
      issueDate: '10/25/2023',
      expireDate: '11/25/2023',
    };

    component.formData = formData;
    component.ngOnInit();

    expect(component.originalFormValues).toEqual(formData);
    expect(component.medicalLicenseForm.value).toEqual(formData);
  });

  it('should update originalFormValues and localEdit on formData change', () => {
    const newFormData = {
      issuingStateId: 3,
      licenseNumber: '54321',
      licenseTypeId: 4,
      issueDate: '10/26/2023',
      expireDate: '11/26/2023',
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
    expect(component.medicalLicenseForm.value).toEqual(newFormData);
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
      licenseStateOptions: ['State1', 'State2'],
      licenseTypeOptions: ['Type1', 'Type2'],
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
    const spy = spyOn(component.saveDialog, 'emit');
    const formValue = {
      issuingStateId: 1,
      licenseNumber: '12345',
      licenseTypeId: 2,
      issueDate: '10/25/2023',
      expireDate: '11/25/2023',
    };

    component.formData = formValue;
    component.ngOnInit();

    component.localEdit = true;
    component.onSubmit();

    expect(spy).toHaveBeenCalledWith({
      show: false,
      data: formValue,
      isEdit: true,
    });
  });

  it('should emit closeDialog event on close', () => {
    const spy = spyOn(component.closeDialog, 'emit');
    component.close();
    expect(spy).toHaveBeenCalledWith({ show: false });
  });

  afterEach(() => {
    TestBed.resetTestingModule();
  });
});
