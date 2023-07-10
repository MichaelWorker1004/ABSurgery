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
  ExamScoringSelectors,
  GetCaseContents,
  GetCaseRoster,
  UpdateCaseComment,
} from '../state';

interface ICaseDetailModel extends ICaseDetailReadOnlyModel {
  editComment: boolean;
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

  constructor(private _store: Store) {}

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
              };
            }) as ICaseDetailModel[];

          this.selectedCaseDetails = {
            ...caseData,
            sections: caseSections,
          };
        });
    }
  }

  saveSectionComment(section: ICaseDetailModel) {
    const newComment = {
      caseContentId: section.caseContentId,
      comments: section.comments,
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
  }
}
