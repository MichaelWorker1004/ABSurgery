import { CUSTOM_ELEMENTS_SCHEMA, Component, OnInit } from '@angular/core';
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
import { Store } from '@ngxs/store';
import {
  CreateCaseComment,
  DeleteCaseComment,
  ExamScoringSelectors,
  GetCaseContents,
  GetCaseRoster,
  UpdateCaseComment,
} from '../state';
import { GlobalDialogService } from '../shared/services/global-dialog.service';

interface ICaseDetailModel extends ICaseDetailReadOnlyModel {
  editComment: boolean;
  newComment?: string;
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
  examHeaderId = 491;
  selectedRoster: any = undefined;
  selectedCaseId: number | undefined = undefined;
  rosters: any = [];
  cases: any = [];

  scoringSessionsList: IScoringSessionReadOnlyModel[] = [];

  selectedCaseDetails: any = undefined;

  editActive!: boolean;

  constructor(
    private _store: Store,
    private _globalDialogService: GlobalDialogService
  ) {}

  ngOnInit(): void {
    this.initPicklistValues();
  }

  initPicklistValues() {
    // defaulting country code to 500 for US states
    this._store
      .dispatch(new GetScoringSessionList(this.examHeaderId))
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
        .dispatch(new GetCaseContents(caseData.id))
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

          this.selectedCaseDetails = {
            ...caseData,
            sections: caseSections,
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
}
