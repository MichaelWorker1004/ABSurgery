export class GetDashboardProgramInformation {
  static readonly type = '[Dashboard] Get the dashboard program information';

  constructor(public userId: number) {}
}

export class GetDashboardCertificationInformation {
  static readonly type =
    '[Dashboard] Get the dashboard certificate information';

  constructor(public absId: string) {}
}
