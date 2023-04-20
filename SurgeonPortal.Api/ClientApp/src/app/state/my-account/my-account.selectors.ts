import { Selector } from '@ngxs/store';
import { IFormErrors } from '../../shared/common';
import { IUserCredential, MyAccountState } from './my-account.state';

export class MyAccountSelectors {
  @Selector([MyAccountState])
  static user(state: IUserCredential): IUserCredential {
    return state;
  }

  @Selector([MyAccountState])
  static errors(state: IUserCredential): IFormErrors | null {
    return <IFormErrors>state.errors;
  }
}
