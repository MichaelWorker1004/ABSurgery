import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CeScoringAppComponent } from './ce-scoring.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { NgxsModule, Store } from '@ngxs/store';
import { surgeonPortalState } from '../state/surgeon-portal.state';
import { TranslateModule } from '@ngx-translate/core';
import { of } from 'rxjs';
import { IAgendaReadOnlyModel } from '../api/models/examiners/agenda-read-only.model';
import { IConflictReadOnlyModel } from '../api/models/examiners/conflict-read-only.model';
import { IDashboardRosterReadOnlyModel } from '../api/models/scoring/dashboard-roster-read-only.model';

describe('CeScoringAppComponent', () => {
  let component: CeScoringAppComponent;
  let fixture: ComponentFixture<CeScoringAppComponent>;
  let store: Store;

  const mockState = {
    application: {
      featureFlags: {
        ceScoreTesting: true,
        ceScoreTestingDate: '2023-10-16',
      },
    },
    examScoring: {
      examinerAgenda: {
        id: 1,
        documentName: 'Agenda Document',
      },
      examinerConflict: {
        id: 2,
        documentName: 'Conflict Document',
      },
      dashboardRoster: [
        {
          firstName: 'first',
          middleName: '',
          lastName: 'last',
          sessionNumber: 1,
          startTime: '08:00',
          endTime: '12:00',
        },
        {
          firstName: 'first',
          middleName: '',
          lastName: 'last',
          sessionNumber: 2,
          startTime: '13:00',
          endTime: '17:00',
        },
      ],
    },
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        CeScoringAppComponent,
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
    fixture = TestBed.createComponent(CeScoringAppComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize feature flags', () => {
    component.ngOnInit();

    expect(component.ceScoreTesting).toBe(true);
    expect(component.examinationDate).toBe('2023-10-16');
  });

  it('should fetch and set CE dashboard date', () => {
    component.fetchCEDashboardDate();

    expect(component.alertsAndNotices).toBeDefined();
    expect(component.alertsAndNotices?.length).toBe(2);
  });

  it('should handle card action', () => {
    const downloadEvent = {
      type: 'download',
      documentId: 1,
      documentName: 'Document Name',
    };
    const store = TestBed.inject(Store);
    const dispatchSpy = spyOn(store, 'dispatch');

    component.handleCardAction(downloadEvent);

    expect(dispatchSpy).toHaveBeenCalledWith(jasmine.any(Object));
  });

  it('should reset case comments data', () => {
    const store = TestBed.inject(Store);
    const dispatchSpy = spyOn(store, 'dispatch');

    component.resetCaseCommentsData();

    expect(dispatchSpy).toHaveBeenCalledWith(jasmine.any(Object));
  });

  it('should reset exam scoring data', () => {
    const store = TestBed.inject(Store);
    const dispatchSpy = spyOn(store, 'dispatch');

    component.resetExamScoringData();

    expect(dispatchSpy).toHaveBeenCalledWith(jasmine.any(Object));
  });
});
