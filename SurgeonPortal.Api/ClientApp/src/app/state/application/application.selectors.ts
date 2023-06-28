import { createPropertySelectors } from '@ngxs/store';
import { ApplicationState } from './application.state';

export class ApplicationSelectors {
  static slices = createPropertySelectors(ApplicationState);
}
