import { Selector, createPropertySelectors } from '@ngxs/store';
import { DashboardState, IDashboardState } from './dashboard.state';

export class DashboardSelectors {
  static slices = createPropertySelectors<IDashboardState>(DashboardState);

  @Selector([DashboardState])
  static dashboardProgramInformation(
    state: IDashboardState
  ): IDashboardState | undefined {
    if (state.programs) {
      return state;
    }
    return;
  }

  @Selector([DashboardState])
  static dashboardRegistrationStatus(
    state: IDashboardState
  ): IDashboardState | undefined {
    if (state.registrationStatus) {
      return state;
    }
    return;
  }

  @Selector([DashboardState])
  static dashboardCertificateInformation(
    state: IDashboardState
  ): IDashboardState | undefined {
    if (state.certificates) {
      return state;
    }
    return;
  }

  @Selector([DashboardState])
  static dashboardAlertsAndNotices(
    state: IDashboardState
  ): IDashboardState | undefined {
    if (state.alertsAndNotices) {
      return state;
    }
    return;
  }
}
