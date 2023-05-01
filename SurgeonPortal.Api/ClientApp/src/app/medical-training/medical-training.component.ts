import { CommonModule } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BehaviorSubject } from 'rxjs';
import { ProfileHeaderComponent } from '../shared/components/profile-header/profile-header.component';
import { GridComponent } from '../shared/components/grid/grid.component';
import { TrainingAddEditModalComponent } from '../shared/components/training-add-edit-modal/training-add-edit-modal.component';
import { MEDICAL_TRAINING_COLS } from '../shared/gridDefinitions/medical-training-cols';

import { GlobalDialogService } from '../shared/services/global-dialog.service';
import { ModalComponent } from '../shared/components/modal/modal.component';

@Component({
  selector: 'abs-medical-training',
  templateUrl: './medical-training.component.html',
  styleUrls: ['./medical-training.component.scss'],
  standalone: true,
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  imports: [
    CommonModule,
    FormsModule,
    ProfileHeaderComponent,
    GridComponent,
    TrainingAddEditModalComponent,
    GridComponent,
    ModalComponent,
  ],
})
export class MedicalTrainingComponent {
  isEdit = true;
  trainingCols = MEDICAL_TRAINING_COLS;
  showTrainingAddEdit = false;
  trainingAddEditTitle: string | undefined;
  tempTraining$: BehaviorSubject<any> = new BehaviorSubject({});
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
      flsCert: {
        file: null,
        fileName: null,
        newFile: null,
        newFileName: null,
      },
      fesCert: {
        file: null,
        fileName: null,
        newFile: null,
        newFileName: null,
      },
      registeredPhysician: 'Dr. Steve Smith Jr.',
      issueDate: new Date('10/24/2022'),
    },
    additionalTraining: [
      {
        trainingId: 1,
        typeOfTraining: 'Advanced 01 Surgery',
        state: 'NY',
        city: 'New York',
        institutionName: 'York Hospital',
        other: '-',
        dateStarted: new Date('7/01/2007'),
        dateEnded: new Date('7/01/2008'),
      },
      {
        trainingId: 1,
        typeOfTraining: 'Advanced 01 Surgery',
        state: 'NY',
        city: 'New York',
        institutionName: 'York Hospital',
        other: '-',
        dateStarted: new Date('7/01/2007'),
        dateEnded: new Date('7/01/2008'),
      },
      {
        trainingId: 1,
        typeOfTraining: 'Advanced 01 Surgery',
        state: 'NY',
        city: 'New York',
        institutionName: 'York Hospital',
        other: '-',
        dateStarted: new Date('7/01/2007'),
        dateEnded: new Date('7/01/2008'),
      },
    ],
  };

  constructor(private globalDialogService: GlobalDialogService) {}

  handleGridAction($event: any) {
    if ($event.fieldKey === 'edit') {
      this.showTrainingModal($event.data);
    } else {
      console.log('unhandled action', $event);
    }
  }
  showTrainingModal(training: any) {
    if (training) {
      this.tempTraining$.next(training);
      this.trainingAddEditTitle = 'Edit Additional / Advanced Training';
    } else {
      this.tempTraining$.next({
        type: null,
        state: null,
        city: null,
        institution: null,
        other: null,
        from: null,
        to: null,
      });
      this.trainingAddEditTitle = 'Add Additional / Advanced Training';
    }
    this.showTrainingAddEdit = true;
  }
  saveTraining($event: any) {
    // TODO: [Joe] handle the save call
    // TODO: [Joe] handle the update call
    // TODO: [Joe] show the universal success/error message
    this.showTrainingAddEdit = $event.show;
    this.tempTraining$.next({});
  }
  cancelAddEditTraining($event: any) {
    this.showTrainingAddEdit = $event.show;
    this.tempTraining$.next({});
  }

  toggleFormEdit(toggle: boolean) {
    if (toggle) {
      this.clearUploadFile(this.medicalTraining.certifications.flsCert);
      this.clearUploadFile(this.medicalTraining.certifications.fesCert);
    }
    this.isEdit = toggle;
  }

  save() {
    this.globalDialogService
      .showConfirmation('Confirmation', 'Are you sure?')
      .then((result) => {
        if (result) {
          this.globalDialogService.showSuccessError(
            'Success',
            'Medical Training Saved',
            true
          );
        } else {
          this.globalDialogService.showSuccessError(
            'Error',
            'Medical Training not Saved',
            false
          );
        }
      });

    // this.globalDialogService.showSuccessError(
    //   'Success',
    //   'Medical Training Saved',
    //   true
    // );
    console.log('save', this.medicalTraining);
    this.toggleFormEdit(false);
  }

  onFileAdded(event: any, file: any) {
    if (event.target.files[0]) {
      file.newFile = event.target.files[0];
      file.newFileName = event.target.files[0].name;
    }
  }

  uploadFile(file: any) {
    file.file = file.newFile;
    file.fileName = file.newFileName;
    file.newFile = null;
    file.newFileName = null;
  }

  removeFile(file: any) {
    file.file = null;
    file.fileName = null;
  }
  clearUploadFile(file: any) {
    file.newFile = null;
    file.newFileName = null;
  }
}
