export class GetCMECredits {
  static readonly type = '[CME] get list of cme credits by user';

  //constructor() {}
}

export class GetCMECreditDetails {
  static readonly type = '[CME] get details for specific cme credit';

  constructor(public id: number) {}
}

export class GetCMEAdjustments {
  static readonly type = '[CME] get list of cme adjustments by user';

  //constructor() {}
}

export class GetCmeSummary {
  static readonly type = '[CME] get all data for the cme summary page';

  //constructor() {}
}

export class ClearCMEErrors {
  static readonly type = '[CME] clear cme errors';

  //constructor() {}
}
