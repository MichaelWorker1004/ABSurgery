import { ComponentFixture, TestBed } from '@angular/core/testing';

import {
  ExaminationRostersComponent,
  ICaseDetailModel,
} from './examination-rosters.component';
import { NgxsModule, Store } from '@ngxs/store';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { surgeonPortalState } from '../state/surgeon-portal.state';
import { TranslateModule } from '@ngx-translate/core';
import { UpdateCaseComment, UpdateCaseFeedback } from '../state';
import { ICaseCommentModel } from '../api';
import { GlobalDialogService } from '../shared/services/global-dialog.service';
import { ICaseFeedbackModel } from '../api/models/scoring/case-feedback.model';

describe('ExaminationRostersComponent', () => {
  let component: ExaminationRostersComponent;
  let fixture: ComponentFixture<ExaminationRostersComponent>;
  let store: Store;

  const mockState = {
    picklists: {
      scoringSessions: [
        {
          id: 1,
          name: 'session 1',
        },
        {
          id: 2,
          name: 'session 2',
        },
      ],
    },
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        ExaminationRostersComponent,
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

    fixture = TestBed.createComponent(ExaminationRostersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  // it('should initialize picklist values', () => {
  //   component.initPicklistValues();

  //   expect(component.scoringSessionsList?.length).toBe(2);
  // });

  it('should select a roster', () => {
    const event = { value: 'your-selected-roster' };
    spyOn(component, 'getCaseList').and.stub();

    component.selectRoster(event);

    expect(component.selectedRoster).toEqual(event.value);
    expect(component.getCaseList).toHaveBeenCalled();
  });

  it('should select a case', () => {
    const caseData = {
      id: 1,
      caseNumber: '1',
      description: 'description',
      title: 'title',
    };
    const spy = spyOn(component, 'selectCase');

    component.confirmCaseSelection(caseData);

    // Assert that the selected case details are set correctly
    expect(spy).toHaveBeenCalledWith(caseData);
  });

  it('should toggle comment section edit', () => {
    const section = {
      editComment: false,
      comments: 'Old comment',
      newComment: '',
    } as unknown as ICaseDetailModel;
    component.editActive = false;

    component.toggleCommentSectionEdit(section);

    expect(section.editComment).toBe(true);
    expect(section.newComment).toBe(section.comments);
    expect(component.editActive).toBe(true);
  });

  it('should save section comment', () => {
    const spy = spyOn(store, 'dispatch');
    const section = {
      caseCommentId: 1,
      caseContentId: 1,
      newComment: 'New comment',
    } as unknown as ICaseDetailModel;

    component.saveSectionComment(section);

    expect(spy).toHaveBeenCalledWith(
      new UpdateCaseComment({
        id: section.caseCommentId,
        caseContentId: section.caseContentId,
        comments: section.newComment,
      } as unknown as ICaseCommentModel)
    );
  });

  it('should delete section comment', () => {
    const globalDialogService = TestBed.inject(GlobalDialogService);
    const spy = spyOn(globalDialogService, 'showConfirmation').and.returnValue(
      Promise.resolve(true)
    );

    const section = { caseCommentId: 1 } as unknown as ICaseDetailModel;

    component.deleteSectionComment(section);

    expect(spy).toHaveBeenCalled();
  });

  it('should save case feedback', () => {
    const spy = spyOn(store, 'dispatch');
    component.selectedCaseDetails = {
      caseFeedbackId: 1,
      id: 1,
      newFeedback: 'New feedback',
    };

    component.saveCaseFeedback();

    expect(spy).toHaveBeenCalledWith(
      new UpdateCaseFeedback({
        id: 1,
        feedback: component.selectedCaseDetails.newFeedback,
        caseHeaderId: component.selectedCaseDetails.id,
      } as unknown as ICaseFeedbackModel)
    );

    // Assert that the feedback is updated and stored correctly
  });

  it('should delete case feedback', () => {
    const globalDialogService = TestBed.inject(GlobalDialogService);
    spyOn(globalDialogService, 'showConfirmation').and.returnValue(
      Promise.resolve(true)
    );
    const feedbackId = 1;

    component.deleteCaseFeedback(feedbackId);

    expect(globalDialogService.showConfirmation).toHaveBeenCalled();
  });
});
