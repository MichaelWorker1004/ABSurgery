export class GetDashboardCertificationStatus {
  static readonly type = '[Dashboard] Get the dashboard certification status';
}

export class GetDashboardProgramInformation {
  static readonly type = '[Dashboard] Get the dashboard program information';
}

export class GetDashboardCertificationInformation {
  static readonly type =
    '[Dashboard] Get the dashboard certificate information';
}

export class GetTraineeRegistrationStatus {
  static readonly type =
    '[Dashboard] Get the trainee registration status information';

  constructor(public examCode: string) {}
}

export class GetAlertsAndNotices {
  static readonly type = '[Dashboard] Get the alerts and notices information';
}
