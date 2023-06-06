export class GetDashboardProgramInformation {
  static readonly type = '[Dashboard] Get the dashboard program information';
}

export class GetDashboardCertificationInformation {
  static readonly type =
    '[Dashboard] Get the dashboard certificate information';

  constructor(public absId: string) {}
}
