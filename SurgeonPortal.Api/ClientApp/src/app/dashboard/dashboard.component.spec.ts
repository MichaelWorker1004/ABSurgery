import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Store } from '@ngxs/store';
import { of } from 'rxjs';

import {
  IUserProfile,
  UserProfileState,
} from '../state/user-profile/user-profile.state';
import { UserProfileSelectors } from '../state/user-profile/user-profile.selectors';
import { DashboardComponent } from './dashboard.component';

describe('DashboardComponent', () => {
  let component: DashboardComponent;
  let fixture: ComponentFixture<DashboardComponent>;
  let store: Store;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DashboardComponent],
      imports: [],
      providers: [Store],
    });

    fixture = TestBed.createComponent(DashboardComponent);
    component = fixture.componentInstance;
    store = TestBed.inject(Store);

    // Mock the store selectors using NgxsStateMock:
    store.reset({
      [UserProfileState]: {
        user: { displayName: 'John Doe', lastLoginDateUtc: new Date() },
      },
    });

    spyOn(store, 'select').and.callFake((selector) => {
      if (selector === UserProfileSelectors.user) {
        return of({ displayName: 'John Doe', lastLoginDateUtc: new Date() });
      } else if (selector === UserProfileSelectors.userClaims) {
        return of([]);
      } else {
        return of(null);
      }
    });
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize user data', () => {
    const user = { displayName: 'John Doe', lastLoginDateUtc: new Date() };
    store.reset({ [UserProfileState]: { user } });
    fixture.detectChanges();

    expect(component.userData).toEqual(user as unknown as IUserProfile);
  });

  it('should set userActionCards based on user claims', () => {
    spyOn(store, 'select').and.returnValue(of(['surgeon']));
    fixture.detectChanges();

    expect(component.isSurgeon).toBeTruthy();
    expect(component.userActionCards).toEqual(component.certifiedCards);

    spyOn(store, 'select').and.returnValue(of(['trainee']));
    fixture.detectChanges();

    expect(component.isSurgeon).toBeFalsy();
    expect(component.userActionCards).toEqual(component.traineeCards);
  });

  // You can write more tests for other component behaviors.

  afterEach(() => {
    TestBed.resetTestingModule();
  });
});
