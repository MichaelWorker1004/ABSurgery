import { Selector } from '@ngxs/store';
import { DashboardState, IDashboardState } from './dashboard.state';

export class DashboardSelectors {
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
}
