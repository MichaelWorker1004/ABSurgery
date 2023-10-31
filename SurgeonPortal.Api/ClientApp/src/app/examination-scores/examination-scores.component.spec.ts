import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExaminationScoresComponent } from './examination-scores.component';
import { NgxsModule, Store } from '@ngxs/store';
import { surgeonPortalState } from '../state/surgeon-portal.state';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TranslateModule } from '@ngx-translate/core';
import { GetExamScoresList } from '../state';

describe('ExaminationScoresComponent', () => {
  let component: ExaminationScoresComponent;
  let fixture: ComponentFixture<ExaminationScoresComponent>;
  let store: Store;

  const mockState = {
    examScoring: {
      selectedExamScores: [
        {
          examScoringId: 1,
          examCaseId: 1,
          caseNumber: '1',
          examinerUserId: 1,
          examineeUserId: 1,
          examineeFirstName: 'John',
          examineeMiddleName: '',
          examineeLastName: 'Doe',
          examineeSuffix: '',
          examDate: '2021-01-01',
          startTime: '08:00',
          endTime: '12:00',
          score: 1,
          criticalFail: false,
          remarks: '',
          isLocked: false,
        },
        {
          examScoringId: 2,
          examCaseId: 2,
          caseNumber: '2',
          examinerUserId: 1,
          examineeUserId: 1,
          examineeFirstName: 'John',
          examineeMiddleName: '',
          examineeLastName: 'Doe',
          examineeSuffix: '',
          examDate: '2021-01-01',
          startTime: '08:00',
          endTime: '12:00',
          score: 2,
          criticalFail: false,
          remarks: '',
          isLocked: true,
        },
      ],
    },
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        ExaminationScoresComponent,
        NgxsModule.forRoot(surgeonPortalState),
        HttpClientTestingModule,
        TranslateModule.forRoot(),
      ],
    }).compileComponents();

    store = TestBed.inject(Store);
    store.reset({
      ...store.snapshot(),
    });
    fixture = TestBed.createComponent(ExaminationScoresComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize component properties', () => {
    expect(component.currentYear).toEqual(new Date().getFullYear());
    // Add more property initialization tests here
  });

  it('should call examSelected method on ngOnInit', () => {
    spyOn(component, 'examSelected');
    component.ngOnInit();
    expect(component.examSelected).toHaveBeenCalled();
  });

  it('should update lockedCases property correctly', () => {
    component.examSelected();
    expect(component.lockedCases).toBe(false);
  });

  it('should call GetExamScoresList when getExaminationScoresDate is called', () => {
    const getExamScoresListSpy = spyOn(store, 'dispatch');
    component.getExaminationScoresDate(1);
    expect(getExamScoresListSpy).toHaveBeenCalledWith(new GetExamScoresList(1));
  });

  it('should filter examinationScoresData correctly based on filterForm', () => {
    component.examinationScoresData = [
      { day: 'Day 1', status: 'Complete' },
      { day: 'Day 2', status: 'Incomplete' },
      { day: 'Day 1', status: 'Incomplete' },
    ];
    component.filterForm.setValue({ day: 'Day 1', status: 'Complete' });
    component.handleFilter();
    expect(component.filteredExaminationScoresData$.value.length).toBe(1);
  });

  it('should open the view modal and update selectedExamScheduleId$', () => {
    const examScheduleId = 123;
    const event = {
      data: { examScheduleId: examScheduleId, status: 'Complete' },
    };
    component.handleView(event);
    expect(component.showViewModal).toBe(true);
    expect(component.selectedExamScheduleId$.value).toBe(examScheduleId);
  });

  it('should close the view modal and dispatch GetExamScoresList when closeDialog is called', () => {
    const getExamScoresListSpy = spyOn(store, 'dispatch');
    component.closeDialog(true);
    expect(getExamScoresListSpy).toHaveBeenCalledWith(
      new GetExamScoresList(component.examHeaderId)
    );
    expect(component.showViewModal).toBe(false);
  });
});
