import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GmeFormComponent } from './gme-form.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NgxsModule } from '@ngxs/store';
import { surgeonPortalState } from 'src/app/state/surgeon-portal.state';

describe('GmeFormComponent', () => {
  let component: GmeFormComponent;
  let fixture: ComponentFixture<GmeFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        NgxsModule.forRoot(surgeonPortalState),
        GmeFormComponent,
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(GmeFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize form controls', () => {
    const gemRotationForm = component.gemRotationForm;
    expect(gemRotationForm.get('programName')).toBeDefined();
    expect(gemRotationForm.get('clinicalLevelId')).toBeDefined();
    // Add more assertions for other form controls
  });

  it('should handle ngOnChanges and update form data', () => {
    const formData = {
      programName: 'test program name',
      clinicalLevelId: 123,
    };
    component.ngOnChanges({
      formData: {
        currentValue: formData,
        previousValue: undefined,
        firstChange: false,
        isFirstChange: function (): boolean {
          throw new Error('Function not implemented.');
        },
      },
    });

    const gemRotationForm = component.gemRotationForm.value;

    expect(gemRotationForm['programName']).toEqual(formData.programName);
    expect(gemRotationForm['clinicalLevelId']).toEqual(
      formData.clinicalLevelId
    );

    expect(component.originalFormValues).toEqual(formData);
  });

  it('should handle ngOnChanges and update isEdit', () => {
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

    expect(component.localEdit).toEqual(true);
  });

  it('should handle ngOnChanges and update picklists', () => {
    const newPicklists = {
      clinicalLevelOptions: ['value 1', 'value 2'],
    };
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

    expect(component.optionLists).toEqual({
      ...component.optionLists,
      ...newPicklists,
    });
  });

  it('should emit saveForm event when onSubmit is called', () => {
    const saveFormSpy = jasmine.createSpy();
    component.saveForm.subscribe(saveFormSpy);
    component.onSubmit();
    // Expect saveForm event to be emitted with the correct data
    expect(saveFormSpy).toHaveBeenCalledWith({
      show: false,
      data: jasmine.any(Object), // Add expected data here
      isEdit: jasmine.any(Boolean), // Add expected boolean value here
    });
  });

  it('should emit relaunchDialog event when changeModalData is called', () => {
    const relaunchDialogSpy = jasmine.createSpy();
    component.relaunchDialog.subscribe(relaunchDialogSpy);
    const id = 123; // Provide a sample id
    component.changeModalData(id);
    expect(relaunchDialogSpy).toHaveBeenCalledWith(id);
  });

  it('should emit cancelForm event when close is called', () => {
    const cancelFormSpy = jasmine.createSpy();
    component.cancelForm.subscribe(cancelFormSpy);
    component.close();
    expect(cancelFormSpy).toHaveBeenCalledWith({ show: false });
  });
});
