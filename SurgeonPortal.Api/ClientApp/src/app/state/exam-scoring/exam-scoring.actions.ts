import { s } from '@fullcalendar/core/internal-common';
import { ICaseCommentModel, ICaseScoreModel } from 'src/app/api';
import { IExamScoreModel } from 'src/app/api/models/ce/exam-score.model';
import { ICaseFeedbackModel } from 'src/app/api/models/scoring/case-feedback.model';

export class GetActiveExamId {
  static readonly type = '[Exam-Scoring] get active exam id';
}

export class GetExamTitle {
  static readonly type = '[Exam-Scoring] get title of exam by id';

  constructor(public id: number) {}
}

export class GetCaseRoster {
  static readonly type =
    '[Exam-Scoring] get list of cases for examination roster';

  constructor(public id1: number, public id2?: number) {}
}

export class GetCaseContents {
  static readonly type = '[Exam-Scoring] get contents of exam case by id';

  constructor(public id: number) {}
}

export class GetCaseComment {
  static readonly type = '[Exam-Scoring] get comments of exam case by id';

  constructor(public id: number) {}
}

export class CreateCaseComment {
  static readonly type = '[Exam-Scoring] create comment for exam case';

  constructor(public comment: ICaseCommentModel) {}
}

export class UpdateCaseComment {
  static readonly type = '[Exam-Scoring] update comment of exam case by id';

  constructor(public comment: ICaseCommentModel) {}
}

export class DeleteCaseComment {
  static readonly type = '[Exam-Scoring] delete comment of exam case by id';

  constructor(public id: number) {}
}

export class GetExamineeList {
  static readonly type = '[Exam-Scoring] get list of examinees by day';

  constructor(public date: string) {}
}

export class GetExaminee {
  static readonly type = '[Exam-Scoring] get examinee by id';

  constructor(public examScheduleId: number) {}
}

export class GetActiveExamination {
  static readonly type = '[Exam-Scoring] get active examination';

  // need an API call for this
  constructor(public id: number) {}
}

export class CreateExamScore {
  static readonly type = '[Exam-Scoring] create score for exam';

  constructor(public model: IExamScoreModel, public navigate = true) {}
}

export class GetExamScoresList {
  static readonly type = '[Exam-Scoring] get list of exam scores';

  constructor(public id: number) {}
}

export class GetSelectedExamScores {
  static readonly type = '[Exam-Scoring] get selected exam score';

  //currently no API call for this
  constructor(public id: number) {}
}

export class CreateCaseScore {
  static readonly type = '[Exam-Scoring] create score for exam case';

  constructor(public score: ICaseScoreModel, public showLoading = true) {}
}

export class UpdateCaseScore {
  static readonly type = '[Exam-Scoring] update score of exam case by id';

  constructor(public score: ICaseScoreModel, public showLoading = true) {}
}

export class DeleteCaseScore {
  static readonly type = '[Exam-Scoring] delete score of exam case by id';

  constructor(public id: number) {}
}

export class GetRoster {
  static readonly type = '[Exam-Scoring] get roster';

  constructor(public examinerUserId: number, public examDate: string) {}
}

export class SkipExam {
  static readonly type = '[Exam-Scoring] skip exam';

  constructor(
    public examScheduleId: number,
    public examDate: string,
    public showLoading = true
  ) {}
}

export class ClearExamScoringErrors {
  static readonly type = '[Exam-Scoring] Clear Erros';
}

export class ResetCaseCommentsData {
  static readonly type = '[Exam-Scoring] Reset case comments data';
}

export class ResetExamScoringData {
  static readonly type = '[Exam-Scoring] Reset exam scoring data';
}
export class CreateCaseFeedback {
  static readonly type = '[Exam-Scoring] Create case feedback';

  constructor(public model: ICaseFeedbackModel) {}
}

export class GetCaseFeedback {
  static readonly type = '[Exam-Scoring] Get case feedback';

  constructor(public id: number) {}
}

export class UpdateCaseFeedback {
  static readonly type = '[Exam-Scoring] Update case feedback';

  constructor(public model: ICaseFeedbackModel) {}
}

export class DeleteCaseFeedback {
  static readonly type = '[Exam-Scoring] Delete case feedback';

  constructor(public id: number) {}
}

export class GetCaseDetailsAndFeedback {
  static readonly type = '[Exam-Scoring] Get case details and feedback';

  constructor(public id: number) {}
}

export class GetExaminerAgenda {
  static readonly type = '[Exam-Scoring] Get examiner agenda';

  constructor(public examHeaderId: number) {}
}

export class GetExaminerConflict {
  static readonly type = '[Exam-Scoring] Get examiner conflict';

  constructor(public examHeaderId: number) {}
}

export class ClearExamineeData {
  static readonly type = '[Exam-Scoring] Clear examinee data';
}
