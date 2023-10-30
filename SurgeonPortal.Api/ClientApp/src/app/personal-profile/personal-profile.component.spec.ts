import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NgxsModule, Store } from '@ngxs/store';
import { TranslateModule } from '@ngx-translate/core';

import {
  PersonalProfileComponent,
  IDisplayUserProfile,
} from './personal-profile.component';
import { surgeonPortalState } from '../state/surgeon-portal.state';
import { of } from 'rxjs';
import { UpdateUserProfile } from '../state';

describe('PersonalProfileComponent', () => {
  let component: PersonalProfileComponent;
  let store: Store;
  let fixture: ComponentFixture<PersonalProfileComponent>;

  const userData: IDisplayUserProfile = {
    absId: '888885',
    bestLanguageId: '1',
    birthCity: 'New York',
    birthCountry: '500',
    birthDate: '2000-01-01T05:00:00.000Z',
    //birthState: 'NY',
    city: 'New York',
    country: '500',
    countryCitizenship: '500',
    displayName: 'Joseph Mayberry',
    emailAddress: 'josephs@absurgery.com',
    ethnicity: 'N',
    firstLanguageId: '1',
    firstName: 'Joseph',
    genderId: '1',
    lastName: 'Mayberry',
    middleName: 'A',
    mobilePhoneNumber: '',
    npi: '',
    officePhoneNumber: '(111) 222-3333',
    profilePicture:
      'https://fastly.picsum.photos/id/91/3504/2336.jpg?hmac=tK6z7RReLgUlCuf4flDKeg57o6CUAbgklgLsGL0UowU',
    race: 'W',
    receiveComms: false,
    //state: 'NY',
    street1: '111 Street Way',
    street2: '',
    suffix: '',
    userConfirmed: true,
    zipCode: '11111     ',
  } as unknown as IDisplayUserProfile;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        ReactiveFormsModule,
        NgxsModule.forRoot(surgeonPortalState),
        PersonalProfileComponent,
        TranslateModule.forRoot(),
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(PersonalProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();

    store = TestBed.inject(Store);
    spyOn(store, 'dispatch').and.returnValue(of({}));
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize form controls', () => {
    expect(component.userProfileForm).toBeDefined();
    // Add more specific tests for form controls if needed
  });

  it('should toggle edit mode', () => {
    component.isEdit = false;
    component.toggleEdit();
    expect(component.isEdit).toBe(true);
  });

  it('should reset form defaults', () => {
    const userDataAlt: any = userData;
    userDataAlt.birthDate = new Date(Date.parse(userData.birthDate));

    // Assuming you have test data in this.user
    component.user = userData;
    component.resetFormDefaults();
    expect(component.userProfileForm.value).toEqual(userData || userDataAlt);
  });

  it('should handle form submission', () => {
    // Mock your service and NgRx Store to handle the dispatch call
    // spyOn(store, 'dispatch').and.returnValue({
    //   subscribe: () => {},
    // });

    component.user = userData;
    component.resetFormDefaults();

    const model = userData;
    model.birthDate = new Date(model.birthDate).toISOString();

    component.onSubmit();

    // Expect that the dispatch method was called with the correct action and data
    expect(store.dispatch).toHaveBeenCalledWith(new UpdateUserProfile(model));
  });
});
