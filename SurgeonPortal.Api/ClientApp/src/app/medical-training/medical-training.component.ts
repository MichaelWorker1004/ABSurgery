import { CommonModule } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BehaviorSubject, Observable } from 'rxjs';
import { ProfileHeaderComponent } from '../shared/components/profile-header/profile-header.component';
import { GridComponent } from '../shared/components/grid/grid.component';
import { TrainingAddEditModalComponent } from '../shared/components/training-add-edit-modal/training-add-edit-modal.component';
import { MEDICAL_TRAINING_COLS } from '../shared/gridDefinitions/medical-training-cols';

import { GlobalDialogService } from '../shared/services/global-dialog.service';
import { ModalComponent } from '../shared/components/modal/modal.component';
import { UserProfileSelectors } from '../state';
import { Select } from '@ngxs/store';
import { AdvancedTrainingService } from '../api/services/medicaltraining/advanced-training.service';
import { IAdvancedTrainingReadOnlyModel } from '../api/models/medicaltraining/advanced-training-read-only.model';

import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { CalendarModule } from 'primeng/calendar';

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

    InputTextModule,
    DropdownModule,
    InputTextareaModule,
    CalendarModule,
  ],
})
export class MedicalTrainingComponent implements OnInit {
  @Select(UserProfileSelectors.userId) userId$: Observable<number> | undefined;
  userId!: number;
  isEdit = true;
  trainingCols = MEDICAL_TRAINING_COLS;
  showTrainingAddEdit = false;
  trainingAddEditTitle: string | undefined;
  tempTraining$: BehaviorSubject<any> = new BehaviorSubject({});
  isAdditionalAdvancedEdit$: BehaviorSubject<boolean> = new BehaviorSubject(
    false
  );
  advancedTraining$: BehaviorSubject<IAdvancedTrainingReadOnlyModel[]> =
    new BehaviorSubject<IAdvancedTrainingReadOnlyModel[]>([]);

  fakeOptions = [
    { itemDescription: 'Option 1', itemValue: 'option-1' },
    { itemDescription: 'Option 2', itemValue: 'option-2' },
    { itemDescription: 'Option 3', itemValue: 'option-3' },
  ];

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
        state: 'MN',
        city: 'Minneapolis',
        institutionName: 'Abbott-Northwestern (Vascular Surgery)',
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

  constructor(
    private globalDialogService: GlobalDialogService,
    private advancedTrainingService: AdvancedTrainingService
  ) {}

  ngOnInit(): void {
    this.userId$?.subscribe((id) => {
      this.userId = id;
    });
    this.getAdvancedTrainingGridData();
  }

  getAdvancedTrainingGridData() {
    this.advancedTrainingService
      .retrieveAdvancedTrainingReadOnly_GetByUserId()
      .subscribe((res: IAdvancedTrainingReadOnlyModel[]) => {
        this.advancedTraining$.next(res);
      });
  }

  handleGridAction($event: any) {
    const data = $event.data;

    if ($event.fieldKey === 'edit') {
      this.isAdditionalAdvancedEdit$.next(true);
      this.tempTraining$.next(data);
      this.showTrainingModal(true);
    } else {
      console.log('unhandled action', $event);
    }
  }

  showTrainingModal(isEdit = false) {
    this.isAdditionalAdvancedEdit$.next(isEdit);
    this.showTrainingAddEdit = !this.showTrainingAddEdit;
  }

  saveTraining($event: any) {
    const formValues = $event.trainingRecord;
    const programId: number | undefined = parseInt(
      formValues.institutionName?.itemValue ?? ''
    );
    const trainingTypeId: number | undefined = parseInt(
      formValues.trainingType ?? ''
    );

    const createModel = {
      trainingTypeId: trainingTypeId ?? null,
      programId: programId ?? null,
      other: formValues.other ?? undefined,
      startDate: new Date(formValues.startDate ?? '') ?? null,
      endDate: new Date(formValues.endDate ?? '') ?? null,
    };

    if ($event.edit === true && $event.trainingId) {
      this.advancedTrainingService
        .updateAdvancedTraining($event.trainingId, createModel)
        .subscribe(() => {
          this.getAdvancedTrainingGridData();
        });
    }

    if ($event.edit === false) {
      this.advancedTrainingService
        .createAdvancedTraining(createModel)
        .subscribe(() => {
          this.getAdvancedTrainingGridData();
        });
    }

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
