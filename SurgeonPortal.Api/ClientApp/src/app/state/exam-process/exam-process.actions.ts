import { IExamFeeTransactionModel } from 'src/app/api/models/billing/exam-fee-transaction.mode';

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
