import { createPropertySelectors } from '@ngxs/store';
import { ApplicationState, IApplicationState } from './application.state';

export class ApplicationSelectors {
  static slices = createPropertySelectors<IApplicationState>(ApplicationState);
}
