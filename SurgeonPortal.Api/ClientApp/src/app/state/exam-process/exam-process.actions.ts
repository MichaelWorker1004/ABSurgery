import { IExamFeeTransactionModel } from 'src/app/api/models/billing/exam-fee-transaction.mode';
import { IExamIntentionsModel } from 'src/app/api/models/examinations/exam-intentions.model';

export class GetExamDirectory {
  static readonly type =
    '[ExamProcess] Get list of available exam applications for user';
}

export class GetExamFees {
  static readonly type = '[ExamProcess] Get list of exam fees for user';
}

export class ExamFeeTransaction {
  static readonly type = '[ExamProcess] Get exam fee transaction token';

  constructor(public model: IExamFeeTransactionModel) {}
}

export class GetExamFeeByExamId {
  static readonly type = '[ExamProcess] Get exam fee by exam id';

  constructor(public examId: number) {}
}

export class GetApplicationFee {
  static readonly type = '[ExamProcess] Get application fee';

  constructor(public examId: number) {}
}

export class GetSiteSelection {
  static readonly type = '[ExamProcess] Get site selection';

  constructor(public examId: number) {}
}

export class SetSiteSelection {
  static readonly type = '[ExamProcess] Set site selection';

  constructor(public siteSelection: string) {}
}

export class GetExamIntentions {
  static readonly type = '[ExamProcess] Get exam intentions';

  constructor(public examId: number) {}
}

export class UpdateExamIntentions {
  static readonly type = '[ExamProcess] Update exam intentions';

  constructor(public model: IExamIntentionsModel) {}
}
