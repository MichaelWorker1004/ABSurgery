import { NgIf } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgxMaskDirective } from 'ngx-mask';
import { NgxMaskPipe } from 'ngx-mask';
import { provideNgxMask } from 'ngx-mask';
import { ProfileHeaderComponent } from '../shared/components/profile-header/profile-header.component';
import { SuccessFailModalComponent } from '../shared/components/success-fail-modal/success-fail-modal.component';

@Component({
  selector: 'abs-personal-profile',
  templateUrl: './personal-profile.component.html',
  styleUrls: ['./personal-profile.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  standalone: true,
  imports: [
    NgIf,
    FormsModule,
    ProfileHeaderComponent,
    SuccessFailModalComponent,
    NgxMaskDirective,
    NgxMaskPipe,
  ],
  providers: [provideNgxMask()],
})
export class PersonalProfileComponent {
  // TODO: [Joe] fix unit test failing due to ngModel being used in form
  // TODO: [Joe] set up input masks
  // TODO: [Joe] set up validation
  // TODO: [Joe] set up national provider identifier (NPI) report button

  isEdit = false;

  // this is eventually getting moved to a service for universal modals
  call!: any;

  // TODO: [Joe] faked user data, replace with real data
  user = {
    profilePicture:
      'https://fastly.picsum.photos/id/91/3504/2336.jpg?hmac=tK6z7RReLgUlCuf4flDKeg57o6CUAbgklgLsGL0UowU',
    givenName: 'John',
    surName: 'Doe',
    title: 'M.D',
    email: 'email@test.io',
    status: 'Trainee',
  };
  profile = {
    firstName: 'John',
    middleInitial: 'J',
    lastName: 'Doe',
    certName: 'John James Doe VII',
    address: {
      street: '123 Main St',
      line2: 'Apt 1',
      city: 'New York',
      state: 'NY',
      zip: '12345',
      country: 'USA',
    },
    email: '',
    confirmEmail: '',
    phone: '123-456-7890',
    mobile: '123-456-7890',
    fax: '123-456-7890',
    placeOfBirth: {
      city: 'New York',
      state: 'NY',
      country: 'USA',
    },
    countryOfCitizenship: 'USA',
    absId: '123456',
    npi: '1234567890',
    ssn: '123456789',
    gender: 'male',
    dob: '01/01/1970',
    race: 'white',
    ethnicity: 'white',
    firstLanguage: 'English',
    bestLanguage: 'English',
  };
  formConfirmation = true;

  save() {
    this.call = {
      title: 'Success',
      message: 'Your profile has been updated.',
      isSuccess: true,
      showDialog: true,
    };
    //this.toggleDialog(true);

    this.isEdit = false;
  }

  toggleDialog($event: any) {
    console.log('toggleDialog', $event.show);
    this.call.showDialog = $event.show;
  }
}
