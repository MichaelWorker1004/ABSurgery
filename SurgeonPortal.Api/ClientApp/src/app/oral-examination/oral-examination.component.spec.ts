import {
  ComponentFixture,
  TestBed,
  fakeAsync,
  tick,
} from '@angular/core/testing';

import { OralExaminationsComponent } from './oral-examination.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { NgxsModule, Store } from '@ngxs/store';
import { surgeonPortalState } from '../state/surgeon-portal.state';
import { TranslateModule } from '@ngx-translate/core';
import {
  ClearExamineeData,
  CreateExamScore,
  SetExamInProgress,
  SkipExam,
} from '../state';
import { IExamScoreModel } from '../api/models/ce/exam-score.model';

describe('OralExaminationsComponent', () => {
  let component: OralExaminationsComponent;
  let fixture: ComponentFixture<OralExaminationsComponent>;
  let store: Store;

  const mockState = {
    examScoring: {
      examinee: {
        examScheduleId: 1,
        fullName: 'John Doe',
        examDate: 'Oct 16 2023 12:50PM',
        cases: [
          {
            id: 1,
            title: 'case 1',
            score: 1,
          },
          {
            id: 2,
            title: 'case 2',
          },
        ],
        examineeUserId: 1,
        examScoringId: 1,
      },
    },
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        OralExaminationsComponent,
        RouterTestingModule,
        NgxsModule.forRoot(surgeonPortalState),
        HttpClientTestingModule,
        TranslateModule.forRoot(),
      ],
    }).compileComponents();

    store = TestBed.inject(Store);
    store.reset({
      ...store.snapshot(),
      ...mockState,
    });
    fixture = TestBed.createComponent(OralExaminationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize component properties', () => {
    expect(component.currentYear).toBe(new Date().getFullYear());
    expect(component.activeCase).toBe(0);
    expect(component.currentIncrement).toBe(1);
    // Add property initialization tests here
  });

  it('should clear previously selected examinee data on init', () => {
    const clearExamineeDataSpy = spyOn(store, 'dispatch');
    component.ngOnInit();
    expect(clearExamineeDataSpy).toHaveBeenCalledWith(new ClearExamineeData());
  });

  it('should handle right-click event', () => {
    const event = {
      preventDefault: jasmine.createSpy('preventDefault'),
    };
    component.onRightClick(event);
    expect(event.preventDefault).toHaveBeenCalled();
  });

  it('should get examination data and set active case', () => {
    store.reset({
      ...store.snapshot(),
      ...mockState,
    });
    component.getExaminationData();

    expect(component.activeCase).toBe(1);
    expect(component.casesLength).toBe(2);
    expect(component.candidateName).toBe('John Doe');
  });

  it('should handle next case and set active case', () => {
    // Mock activeCase and casesLength properties
    component.activeCase = 0;
    component.casesLength = 3;

    component.handleNextCase();

    // Add expectations to check if active case is updated correctly
    expect(component.activeCase).toBe(1);
  });

  it('should handle save and submit later', async () => {
    store.reset({
      ...store.snapshot(),
      ...mockState,
    });
    component.getExaminationData();

    const submitLaterSpy = spyOn(store, 'dispatch');
    // Mock updateScores function and related properties
    const updateScoresSpy = spyOn(component, 'updateScores').and.returnValue(
      Promise.resolve()
    );

    component.handleSaveAndSubmitLater();

    await updateScoresSpy();

    expect(updateScoresSpy).toHaveBeenCalled();

    expect(submitLaterSpy).toHaveBeenCalledWith(new SetExamInProgress(false));
    expect(submitLaterSpy).toHaveBeenCalledWith(
      new SkipExam(component.examScheduleId, '2023-10-16T04:00:00.000Z')
    );
  });

  it('should handle submit', async () => {
    const submitSpy = spyOn(store, 'dispatch');
    const updateScoresSpy = spyOn(component, 'updateScores').and.returnValue(
      Promise.resolve()
    );
    component.handleSubmit();

    await updateScoresSpy();

    expect(updateScoresSpy).toHaveBeenCalled();

    expect(submitSpy).toHaveBeenCalledWith(new SetExamInProgress(false));
    expect(submitSpy).toHaveBeenCalledWith(
      new CreateExamScore(
        {
          examScheduleId: component.examScheduleId,
        } as unknown as IExamScoreModel,
        false
      )
    );
  });

  afterEach(() => {
    TestBed.resetTestingModule();
  });
});
