import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NgxsModule, Store } from '@ngxs/store';
import { MedicalTrainingComponent } from './medical-training.component';
import { surgeonPortalState } from '../state/surgeon-portal.state';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { BehaviorSubject, of } from 'rxjs';
import { SetUnsavedChanges } from '../state';
import { IMedicalTrainingModel } from '../api/models/medicaltraining/medical-training.model';

describe('MedicalTrainingComponent', () => {
  let component: MedicalTrainingComponent;
  let store: Store;
  let fixture: ComponentFixture<MedicalTrainingComponent>;

  // let medicalTrainingSubject: BehaviorSubject<IMedicalTrainingModel> =
  //   new BehaviorSubject<IMedicalTrainingModel>(
  //     {} as unknown as IMedicalTrainingModel
  //   );

  const medicalTrainingData: IMedicalTrainingModel = {
    id: 1,
    userId: 1,
    graduateProfileId: 1,
    graduateProfileDescription: 'test',
    medicalSchoolName: 'test',
    medicalSchoolCity: 'test',
    medicalSchoolStateId: 'test',
    medicalSchoolStateName: 'test',
    medicalSchoolCountryId: 'test',
    medicalSchoolCountryName: 'test',
    degreeId: 1,
    degreeName: 'test',
    medicalSchoolCompletionYear: 'test',
    residencyProgramName: 'test',
    residencyCompletionYear: 'test',
    residencyProgramOther: 'test',
  } as unknown as IMedicalTrainingModel;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        MedicalTrainingComponent,
        ReactiveFormsModule,
        NgxsModule.forRoot(surgeonPortalState),
        HttpClientTestingModule,
        TranslateModule.forRoot(),
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(MedicalTrainingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();

    store = TestBed.inject(Store);
    spyOn(store, 'dispatch').and.returnValue(of({}));

    //medicalTrainingSubject = new BehaviorSubject<IMedicalTrainingModel>(medicalTrainingData);
    //component.medicalTraining$ = medicalTrainingSubject.asObservable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize form controls', () => {
    expect(component.medicalTrainingForm).toBeDefined();
    // Add more specific tests for form controls if needed
  });

  it('should set edit mode to true', () => {
    component.toggleFormEdit(true);
    expect(component.isEdit).toBe(true);
    expect(store.dispatch).toHaveBeenCalledWith(new SetUnsavedChanges(true));
  });

  it('should set edit mode to false', () => {
    component.toggleFormEdit(false);
    expect(component.isEdit).toBe(false);
    expect(store.dispatch).toHaveBeenCalledWith(new SetUnsavedChanges(false));
  });

  it('should launch or close the fellowship modal', () => {
    component.showFellowshipModal(true);
    expect(component.showFellowshipAddEdit).toBe(true);

    component.showFellowshipModal(false);
    expect(component.showFellowshipAddEdit).toBe(false);
  });

  it('should launch or close the Training modal', () => {
    component.showTrainingModal(true);
    expect(component.showTrainingAddEdit).toBe(true);

    component.showTrainingModal(false);
    expect(component.showTrainingAddEdit).toBe(false);
  });

  it('should launch or close the Other Certificate modal', () => {
    component.showOtherCertificaions(true);
    expect(component.showRPVICertificatesAddEdit).toBe(true);

    component.showOtherCertificaions(false);
    expect(component.showRPVICertificatesAddEdit).toBe(false);
  });

  // it('should set initial value of the form', () => {
  //   //component.ngOnInit();
  //   medicalTrainingSubject.next(medicalTrainingData);

  //   expect(component.createMode).toBe(false);
  // });
});
