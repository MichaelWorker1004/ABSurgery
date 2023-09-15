import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  HostListener,
  OnInit,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { DropdownModule } from 'primeng/dropdown';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { ButtonModule } from 'primeng/button';
import { GetScoringSessionList, PicklistsSelectors } from '../state/picklists';
import {
  ICaseCommentModel,
  ICaseDetailReadOnlyModel,
  ICaseRosterReadOnlyModel,
  IScoringSessionReadOnlyModel,
} from '../api';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Select, Store } from '@ngxs/store';
import {
  CreateCaseComment,
  CreateCaseFeedback,
  DeleteCaseComment,
  DeleteCaseFeedback,
  ExamScoringSelectors,
  GetCaseDetailsAndFeedback,
  GetCaseRoster,
  GetExamTitle,
  UpdateCaseComment,
  UpdateCaseFeedback,
  UserProfileSelectors,
} from '../state';
import { GlobalDialogService } from '../shared/services/global-dialog.service';
import { IExamTitleReadOnlyModel } from '../api/models/examinations/exam-title-read-only.model';
import { Observable } from 'rxjs';
import { ICaseFeedbackModel } from '../api/models/scoring/case-feedback.model';

interface ICaseDetailModel extends ICaseDetailReadOnlyModel {
  editComment: boolean;
  newComment?: string;
  newFeedback?: string;
}

@UntilDestroy()
@Component({
  selector: 'abs-examination-rosters',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule,
    DropdownModule,
    InputTextareaModule,
    ButtonModule,
  ],
  templateUrl: './examination-rosters.component.html',
  styleUrls: ['./examination-rosters.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class ExaminationRostersComponent implements OnInit {
  @HostListener('contextmenu', ['$event'])
  onRightClick(event: any) {
    event.preventDefault();
  }

  @Select(UserProfileSelectors.userId) user$: Observable<number> | undefined;

  @Select(ExamScoringSelectors.slices.examTitle) examTitle$:
    | Observable<IExamTitleReadOnlyModel>
    | undefined;

  @Select(ExamScoringSelectors.slices.selectedCaseFeedback)
  selectedCaseFeedback$: Observable<ICaseFeedbackModel> | undefined;

  userId!: number;

  examHeaderId = 491;
  selectedRoster: any = undefined;
  selectedCaseId: number | undefined = undefined;
  rosters: any = [];
  cases: any = [];

  caseFeedbackId!: number;
  caseFeedback!: string;
  caseFeedbackNewComment!: string;
  caseFeedbackEdit = false;
  isCaseFeedbackEditActive = false;

  scoringSessionsList: IScoringSessionReadOnlyModel[] = [];

  selectedCaseDetails: any = undefined;

  editActive!: boolean;

  constructor(
    private _store: Store,
    private _globalDialogService: GlobalDialogService
  ) {
    this._store.dispatch(new GetExamTitle(this.examHeaderId));
  }

  ngOnInit(): void {
    this.initPicklistValues();
    this.user$?.subscribe((userId) => {
      this.userId = userId;
    });
  }

  initPicklistValues() {
    // defaulting country code to 500 for US states
    this._store
      .dispatch(new GetScoringSessionList())
      .pipe(untilDestroyed(this))
      .subscribe(() => {
        this.scoringSessionsList = this._store.selectSnapshot(
          PicklistsSelectors.slices.scoringSessions
        ) as IScoringSessionReadOnlyModel[];

        if (this.scoringSessionsList?.length > 0) {
          this.selectedRoster = this.scoringSessionsList[0];
        }

        this.getCaseList();
      });
  }

  getCaseList() {
    this._store
      .dispatch(
        new GetCaseRoster(
          this.selectedRoster.session1Id,
          this.selectedRoster.session2Id
        )
      )
      .pipe(untilDestroyed(this))
      .subscribe(() => {
        this.cases = this._store.selectSnapshot(
          ExamScoringSelectors.slices.caseRoster
        ) as ICaseRosterReadOnlyModel[];

        if (this.cases?.length > 0) {
          this.selectCase(this.cases[0]);
        } else {
          this.selectedCaseId = undefined;
          this.selectedCaseDetails = undefined;
        }
      });
  }

  confirmRosterSelection(event: any) {
    if (this.editActive) {
      this._globalDialogService
        .showConfirmation(
          'Unsaved Changes',
          'Are you sure you want to navigate away from this case? Any unsaved changes will be lost.'
        )
        .then((result) => {
          if (result) {
            this.editActive = false;
            this.selectRoster(event);
          }
        });
    } else {
      this.selectRoster(event);
    }
  }

  selectRoster(event: any) {
    if (event.value) {
      this.selectedRoster = event.value;
      this.getCaseList();
    } else {
      this.selectedRoster = undefined;
      this.selectedCaseId = undefined;
      this.selectedCaseDetails = undefined;
    }
  }

  confirmCaseSelection(caseData: ICaseRosterReadOnlyModel) {
    if (this.editActive) {
      this._globalDialogService
        .showConfirmation(
          'Unsaved Changes',
          'Are you sure you want to navigate away from this case? Any unsaved changes will be lost.'
        )
        .then((result) => {
          if (result) {
            this.editActive = false;
            this.selectCase(caseData);
          }
        });
    } else {
      this.selectCase(caseData);
    }
  }

  selectCase(caseData: ICaseRosterReadOnlyModel) {
    if (this.selectedCaseId !== caseData.id) {
      this.selectedCaseId = caseData.id;
      this._store
        .dispatch(new GetCaseDetailsAndFeedback(caseData.id))
        .pipe(untilDestroyed(this))
        .subscribe(() => {
          const caseSections = this._store
            .selectSnapshot(ExamScoringSelectors.slices.selectedCaseContents)
            ?.map((val) => {
              return {
                ...val,
                editComment: false,
                newComment: '',
              };
            }) as ICaseDetailModel[];

          const caseFeedback = this._store.selectSnapshot(
            ExamScoringSelectors.slices.selectedCaseFeedback
          );

          this.selectedCaseDetails = {
            ...caseData,
            sections: caseSections,
            feedback: caseFeedback?.feedback,
            newFeedback: caseFeedback?.feedback ? caseFeedback.feedback : '',
            caseFeedbackId: caseFeedback?.id,
            editFeedback: false,
          };
        });
    }
  }

  toggleCommentSectionEdit(section: ICaseDetailModel) {
    section.editComment = !section.editComment;
    if (section.editComment) {
      section.newComment = section.comments;
      this.editActive = true;
    } else {
      section.newComment = '';
      this.editActive = false;
    }
  }

  toggleCaseFeedbackEdit() {
    this.selectedCaseDetails.editFeedback =
      !this.selectedCaseDetails.editFeedback;
    if (this.selectedCaseDetails.editFeedback) {
      this.selectedCaseDetails.newFeedback = this.selectedCaseDetails.feedback
        ? this.selectedCaseDetails.feedback
        : '';
      this.editActive = true;
    } else {
      this.selectedCaseDetails.newFeedback = '';
      this.editActive = false;
    }
  }

  saveSectionComment(section: ICaseDetailModel) {
    const newComment = {
      caseContentId: section.caseContentId,
      comments: section.newComment,
    } as unknown as ICaseCommentModel;
    if (section.caseCommentId) {
      newComment.id = section.caseCommentId;
      // call update case comment store action
      this._store
        .dispatch(new UpdateCaseComment(newComment))
        .pipe(untilDestroyed(this))
        .subscribe(() => {
          const caseComment = this._store.selectSnapshot(
            ExamScoringSelectors.slices.selectedCaseComment
          );
          if (caseComment) {
            section.caseCommentId = caseComment.id;
            section.comments = caseComment.comments;
          } else {
            this.selectCase(this.selectedCaseDetails);
          }
        });
    } else {
      // call add case comment store action
      this._store
        .dispatch(new CreateCaseComment(newComment))
        .pipe(untilDestroyed(this))
        .subscribe(() => {
          const caseComment = this._store.selectSnapshot(
            ExamScoringSelectors.slices.selectedCaseComment
          );
          if (caseComment) {
            section.caseCommentId = caseComment.id;
            section.comments = caseComment.comments;
          } else {
            this.selectCase(this.selectedCaseDetails);
          }
        });
    }
    section.editComment = false;
    this.editActive = false;
  }

  deleteSectionComment(section: ICaseDetailModel) {
    this._globalDialogService
      .showConfirmation(
        'Confirm Delete',
        'Are you sure you want to delete this section comment?'
      )
      .then((result) => {
        if (result) {
          this._store
            .dispatch(new DeleteCaseComment(section.caseCommentId))
            .pipe(untilDestroyed(this))
            .subscribe(() => {
              this.selectedCaseId = 0;
              this.selectCase(this.selectedCaseDetails);
            });
        }
      });
  }

  saveCaseFeedback() {
    const model = {
      userId: this.userId,
      feedback: this.selectedCaseDetails.newFeedback,
      caseHeaderId: this.selectedCaseDetails.id,
    } as unknown as ICaseFeedbackModel;
    if (this.selectedCaseDetails.caseFeedbackId) {
      model.id = this.selectedCaseDetails.caseFeedbackId;
      this._store
        .dispatch(new UpdateCaseFeedback(model))
        .pipe(untilDestroyed(this))
        .subscribe(() => {
          const caseFeedback = this._store.selectSnapshot(
            ExamScoringSelectors.slices.selectedCaseFeedback
          );

          if (caseFeedback) {
            this.selectedCaseDetails.caseFeedbackId = caseFeedback.id;
            this.selectedCaseDetails.feedback = caseFeedback.feedback;
          } else {
            this.selectCase(this.selectedCaseDetails);
          }
        });
    } else {
      this._store
        .dispatch(new CreateCaseFeedback(model))
        .pipe(untilDestroyed(this))
        .subscribe(() => {
          const caseFeedback = this._store.selectSnapshot(
            ExamScoringSelectors.slices.selectedCaseFeedback
          );
          if (caseFeedback) {
            this.selectedCaseDetails.caseFeedbackId = caseFeedback.id;
            this.selectedCaseDetails.feedback = caseFeedback.feedback;
          } else {
            this.selectCase(this.selectedCaseDetails);
          }
        });
    }
    this.selectedCaseDetails.editFeedback = false;
    this.editActive = false;
  }

  deleteCaseFeedback(id: number) {
    this._globalDialogService
      .showConfirmation(
        'Confirm Delete',
        'Are you sure you want to delete this case feedback?'
      )
      .then((result) => {
        if (result) {
          this._store
            .dispatch(new DeleteCaseFeedback(id))
            .pipe(untilDestroyed(this))
            .subscribe(() => {
              this.selectedCaseId = 0;
              this.selectCase(this.selectedCaseDetails);
            });
        }
      });
  }

  scrollToCaseFeedback() {
    if (!this.editActive) {
      this.selectedCaseDetails.editFeedback =
        !this.selectedCaseDetails.editFeedback;

      this.selectedCaseDetails.newFeedback = this.selectedCaseDetails.feedback
        ? this.selectedCaseDetails.feedback
        : '';
      this.editActive = true;
    }

    this.scrollToElementById('case-feedback');
    setTimeout(() => {
      const inputElement = document.getElementById('case-feedback-comment');
      if (inputElement) {
        inputElement.focus();
      }
    }, 500);
  }

  scrollToElementById(elementId: string) {
    const element = document.getElementById(elementId);
    if (element) {
      setTimeout(() => {
        element.scrollIntoView({ behavior: 'smooth' });
      }, 0);
    }
  }
}
