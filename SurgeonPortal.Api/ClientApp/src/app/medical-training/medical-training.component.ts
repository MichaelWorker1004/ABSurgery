import { CommonModule } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ProfileHeaderComponent } from '../shared/components/profile-header/profile-header.component';

@Component({
  selector: 'abs-medical-training',
  templateUrl: './medical-training.component.html',
  styleUrls: ['./medical-training.component.scss'],
  standalone: true,
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  imports: [CommonModule, FormsModule, ProfileHeaderComponent],
})
export class MedicalTrainingComponent {
  isEdit = true;
  // TODO: [Joe] faked user data, replace with real data
  user = {
    profilePicture:
      'https://fastly.picsum.photos/id/91/3504/2336.jpg?hmac=tK6z7RReLgUlCuf4flDKeg57o6CUAbgklgLsGL0UowU',
    fullName: 'John Doe',
    title: 'M.D',
    emailAddress: 'email@test.io',
    status: 'Trainee',
  };

  // TODO: [Joe] faked user data, replace with real data
  medicalTraining = {
    statement: 'USA',
    medicalSchool: {
      name: 'Johns Hopkins University',
      city: 'Baltimore',
      state: 'MD',
      country: 'United States',
      degree: 'MD',
      yearGraduated: '2010',
    },
    residency: {
      programName: 'General Surgery',
      yearOfCompletion: '2015',
      programNotListed: 'an explanation goes here',
    },
    fellowship: {
      programName: 'General Surgery',
      yearOfCompletion: '2015',
      programNotListed: 'an explanation goes here',
    },
    certifications: {
      flsCert: null,
      fesCert: null,
      registeredPhysician: 'Dr. Steve Smith Jr.',
      issueDate: new Date('10/24/2022'),
    },
    additionalTraining: [
      {
        type: 'Advanced 01 Surgery',
        state: 'NY',
        city: 'New York',
        institution: 'York Hospital',
        other: '-',
        from: new Date('7/01/2007'),
        to: new Date('7/01/2008'),
      },
    ],
  };
}
