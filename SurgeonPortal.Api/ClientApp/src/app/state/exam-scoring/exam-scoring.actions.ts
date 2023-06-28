export class GetExamRosters {
  static readonly type = '[Exam-Scoring] get list of rosters';
}

export class GetExamCases {
  static readonly type = '[Exam-Scoring] get list of exam cases';

  constructor(public id1: number, public id2: number) {}
}

export class GetExamScores {
  static readonly type = '[Exam-Scoring] get list of exam case scores';

  constructor(public id: number) {}
}

export class GetCaseContents {
  static readonly type = '[Exam-Scoring] get contents of exam case by id';

  constructor(public id: number) {}
}

export class GetCaseComments {
  static readonly type = '[Exam-Scoring] get comments of exam case by id';

  constructor(public id: number) {}
}

export class ClearExamScoringErrors {
  static readonly type = '[Exam-Scoring] Clear Erros';
}
