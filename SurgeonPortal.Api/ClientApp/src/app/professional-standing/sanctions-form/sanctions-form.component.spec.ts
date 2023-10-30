import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SanctionsFormComponent } from './sanctions-form.component';
import { SimpleChange } from '@angular/core';

describe('SanctionsFormComponent', () => {
  let component: SanctionsFormComponent;
  let fixture: ComponentFixture<SanctionsFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SanctionsFormComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(SanctionsFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize form controls', () => {
    expect(component.sanctionsEthicsForm).toBeTruthy();
    // Add more expectations to check if form controls are properly initialized.
  });

  it('should handle ngOnChanges', () => {
    const formData = {
      hadDrugAlchoholTreatment: true,
      hadHospitalPrivilegesDenied: false,
      hadLicenseRestricted: true,
      hadHospitalPrivilegesRestricted: false,
      hadFelonyConviction: true,
      hadCensure: false,
      explanation: 'Sample Explanation',
    };

    const isEdit = true;
    const picklists = { somePicklist: [] };
    const changes: SimpleChange[] = [
      new SimpleChange(null, formData, true),
      new SimpleChange(null, isEdit, true),
      new SimpleChange(null, picklists, true),
    ];

    component.ngOnChanges({
      formData: changes[0],
      isEdit: changes[1],
      picklists: changes[2],
    });

    expect(component.originalFormValues).toEqual(formData);
    expect(component.localEdit).toBe(isEdit);
    expect(component.optionLists).toEqual({
      ...component.optionLists,
      ...picklists,
    });
    // Add more expectations to check if the component handles ngOnChanges correctly.
  });

  it('should checkSantionsAndEthics enable explanation', () => {
    const formData = {
      hadDrugAlchoholTreatment: true,
      hadHospitalPrivilegesDenied: false,
      hadLicenseRestricted: true,
      hadHospitalPrivilegesRestricted: false,
      hadFelonyConviction: true,
      hadCensure: false,
      explanation: 'Sample Explanation',
    };

    component.sanctionsEthicsForm.patchValue(formData);

    component.checkSantionsAndEthics();

    expect(component.sanctionsEthicsForm.controls['explanation'].disabled).toBe(
      false
    );
  });

  it('should checkSantionsAndEthics disable explanation', () => {
    const formData = {
      hadDrugAlchoholTreatment: false,
      hadHospitalPrivilegesDenied: false,
      hadLicenseRestricted: false,
      hadHospitalPrivilegesRestricted: false,
      hadFelonyConviction: false,
      hadCensure: false,
      explanation: 'Sample Explanation',
    };

    component.sanctionsEthicsForm.patchValue(formData);

    component.checkSantionsAndEthics();

    expect(component.sanctionsEthicsForm.controls['explanation'].disabled).toBe(
      true
    );
  });

  it('should onSubmit', () => {
    const saveFormSpy = spyOn(component.saveForm, 'emit');
    const formData = {
      hadDrugAlchoholTreatment: true,
      hadHospitalPrivilegesDenied: false,
      hadLicenseRestricted: true,
      hadHospitalPrivilegesRestricted: false,
      hadFelonyConviction: true,
      hadCensure: false,
      explanation: 'Sample Explanation',
    };

    component.sanctionsEthicsForm.patchValue(formData);
    component.localEdit = true;
    component.onSubmit();

    expect(saveFormSpy).toHaveBeenCalledWith({
      show: false,
      data: formData,
      isEdit: true,
    });
  });

  it('should close', () => {
    const cancelFormSpy = spyOn(component.cancelForm, 'emit');
    component.close();
    expect(cancelFormSpy).toHaveBeenCalledWith({ show: false });
  });

  afterEach(() => {
    TestBed.resetTestingModule();
  });
});
