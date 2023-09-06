import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { GridComponent } from '../../shared/components/grid/grid.component';
import { TrainingAddEditModalComponent } from '../../shared/components/training-add-edit-modal/training-add-edit-modal.component';
import { FileUploadButtonComponent } from 'src/app/shared/components/file-upload-button/file-upload-button.component';
import { ModalComponent } from 'src/app/shared/components/modal/modal.component';
import { MEDICAL_TRAINING_COLS } from '../../shared/gridDefinitions/medical-training-cols';
import { BASIC_DOCUMENT_COLS } from '../../shared/gridDefinitions/basic-document-cols';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import {
  UserProfileSelectors,
  GQAdditionalTrainingSelectors,
  GetAdditionalTrainingList,
  GetAdditionalTrainingDetails,
  UpdateAdditionalTraining,
  CreateAdditionalTraining,
} from '../../state';
import { Select, Store } from '@ngxs/store';
import {
  IAdditionalTrainingModel,
  IAdditionalTrainingReadOnlyModel,
} from 'src/app/api';
import { IAdvancedTrainingModel } from 'src/app/api/models/medicaltraining/advanced-training.model';

import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { DropdownModule } from 'primeng/dropdown';
import { CheckboxModule } from 'primeng/checkbox';
import { RadioButtonModule } from 'primeng/radiobutton';
import { ButtonModule } from 'primeng/button';

interface MedicalTrainingData {
  medicalSchool: any;
  residency: any;
  additionalDocuments: any;
}

@Component({
  selector: 'abs-training-modal',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    GridComponent,
    TrainingAddEditModalComponent,
    FileUploadButtonComponent,
    ModalComponent,
    InputTextModule,
    InputTextareaModule,
    DropdownModule,
    CheckboxModule,
    RadioButtonModule,
    ButtonModule,
  ],
  templateUrl: './training-modal.component.html',
  styleUrls: ['./training-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class TrainingModalComponent implements OnInit, OnDestroy {
  @Output() closeDialog: EventEmitter<any> = new EventEmitter();
  @Select(UserProfileSelectors.userId) userId$: Observable<number> | undefined;

  @Select(GQAdditionalTrainingSelectors.additionalTrainingList)
  additionalTrainingList$:
    | Observable<IAdditionalTrainingReadOnlyModel[]>
    | undefined;

  @Select(GQAdditionalTrainingSelectors.selectedAdditionalTrainingDetails)
  selectedTraining$: Observable<any> | undefined;

  fakeOptions = [
    { itemDescription: 'Option 1', itemValue: 'option-1' },
    { itemDescription: 'Option 2', itemValue: 'option-2' },
    { itemDescription: 'Option 3', itemValue: 'option-3' },
    { itemDescription: 'Option 4', itemValue: 'option-4' },
    { itemDescription: 'Option 5', itemValue: 'option-5' },
    { itemDescription: 'Option 6', itemValue: 'option-6' },
    { itemDescription: 'Option 7', itemValue: 'option-7' },
    { itemDescription: 'Option 8', itemValue: 'option-8' },
    { itemDescription: 'Option 9', itemValue: 'option-9' },
    { itemDescription: 'Option 101', itemValue: 'option-101' },
  ];

  userSubscription: Subscription | undefined;
  selectedTrainingSubscription: Subscription | undefined;

  trainingCols = MEDICAL_TRAINING_COLS;
  documentCols = BASIC_DOCUMENT_COLS;
  showTrainingAddEdit = false;
  trainingAddEditTitle: string | undefined;
  emptyTraining: IAdvancedTrainingModel = {
    id: 0,
    userId: 0,
    trainingTypeId: 0,
    trainingType: '',
    programId: 0,
    institutionName: '',
    city: '',
    state: '',
    other: '',
    startDate: '',
    endDate: '',
    createdByUserId: 0,
    createdAtUtc: '',
    lastUpdatedAtUtc: '',
    lastUpdatedByUserId: 0,
  };
  tempTraining$: BehaviorSubject<IAdvancedTrainingModel> = new BehaviorSubject(
    this.emptyTraining
  );
  panels = [
    'medicalSchool',
    'residency',
    'additionalTraining',
    'additionalDocuments',
  ];
  activePanel = 0;

  medicalTraining: MedicalTrainingData = {
    medicalSchool: {},
    residency: {},
    additionalDocuments: {},
  };
  userId: number | undefined;

  constructor(private _store: Store) {
    this.initTrainingData();
  }
  ngOnDestroy(): void {
    this.userSubscription?.unsubscribe();
    this.selectedTrainingSubscription?.unsubscribe();
  }

  ngOnInit() {
    this.getMedicalTraining();
  }

  initTrainingData() {
    this._store.dispatch(new GetAdditionalTrainingList());

    this.selectedTrainingSubscription = this.selectedTraining$?.subscribe(
      (selectedTraining) => {
        if (selectedTraining?.trainingId > -1) {
          this.tempTraining$.next(selectedTraining);
        }
      }
    );
  }

  getMedicalTraining() {
    this.medicalTraining.medicalSchool = {
      statement: 'USA',
      name: 'Johns Hopkins University',
      city: 'Baltimore',
      state: 'MD',
      country: 'USA',
      degree: 'MD',
      yearGraduated: '2010',
    };
    this.medicalTraining.residency = {
      programName: 'General Surgery',
      yearOfCompletion: '2015',
      programNotListed: 'an explanation goes here',
      completedInCanada: null,
      postResidencyPlans: null,
      postFellowshipPlans: null,
      vascularTraining: null,
      vascularFellowship: null,
      thoracicTraining: null,
      thoracicFellowship: null,
    };
    this.medicalTraining.additionalDocuments = {
      multiplePrograms: null,
      completionDocuments: [
        {
          file: new Blob(['Hello, world!'], { type: 'text/plain' }),
          fileName: 'this_is_a_file_name.txt',
          fileType: 'lines',
          uploadDate: new Date('7/01/2022'),
        },
      ],
      recievedABSApprovalLetter: null,
      approvalLetters: [
        {
          file: new Blob(['Hello, world!'], { type: 'text/plain' }),
          fileName: 'this_is_a_file_name.txt',
          fileType: 'lines',
          uploadDate: new Date('7/01/2022'),
        },
      ],
    };
  }

  handleDefaultShowTab(event: any) {
    this.activePanel = this.panels.indexOf(event.detail.name);
  }

  showTabPanel(panel: string) {
    const tabGroup: any | null = document.querySelector('#trainingTabs');
    tabGroup?.show(panel);
  }

  save() {
    if (this.activePanel === this.panels.length - 1) {
      this.close();
    } else {
      this.showTabPanel(this.panels[this.activePanel + 1]);
    }
  }

  close() {
    this.closeDialog.emit();
    // timeout is needed to allow the modal to close before the tab panel is reset
    setTimeout(() => {
      this.showTabPanel(this.panels[0]);
    }, 500);
  }

  uploadFile(event: any, fileList: any[]) {
    if (event.file) {
      fileList.push(event.file);
    }
  }

  showTrainingModal(trainingId?: number | undefined) {
    if (trainingId) {
      this._store.dispatch(new GetAdditionalTrainingDetails(trainingId));
      const selectedTraining = this._store.selectSnapshot<any>(
        GQAdditionalTrainingSelectors.selectedAdditionalTrainingDetails
      );

      this.trainingAddEditTitle = 'Edit Additional / Advanced Training';
    } else {
      this.tempTraining$.next(this.emptyTraining);
      this.trainingAddEditTitle = 'Add Additional / Advanced Training';
    }
    this.showTrainingAddEdit = true;
  }
  saveTraining($event: any) {
    $event.trainingRecord.institutionId = parseInt(
      $event.trainingRecord.institutionId
    );
    if ($event.trainingRecord.dateEnded) {
      $event.trainingRecord.dateEnded = new Date(
        $event.trainingRecord.dateEnded
      );
    }
    if ($event.trainingRecord.dateStarted) {
      $event.trainingRecord.dateStarted = new Date(
        $event.trainingRecord.dateStarted
      );
    }
    // TODO: [Joe] show the universal success/error message
    if ($event.trainingRecord.trainingId > -1) {
      this._store.dispatch(new UpdateAdditionalTraining($event.trainingRecord));
    } else {
      this._store.dispatch(new CreateAdditionalTraining($event.trainingRecord));
    }
    this.showTrainingAddEdit = $event.show;
    this.tempTraining$.next(this.emptyTraining);
  }
  cancelAddEditTraining($event: any) {
    this.showTrainingAddEdit = $event.show;
    this.tempTraining$.next(this.emptyTraining);
  }

  handleGridAction($event: any, fileList?: any[] | undefined) {
    if ($event.fieldKey === 'edit') {
      this.showTrainingModal($event.data.trainingId);
    } else if ($event.fieldKey === 'delete') {
      // TODO: [Joe] once we have the ngx-store implemented see if this can be done cleaner without passing in the fileList
      if (fileList) {
        const index = fileList.indexOf($event.data);
        if (index > -1) {
          fileList.splice(index, 1);
        }
      } else {
        console.log('handle delete', $event);
      }
    } else if ($event.fieldKey === 'download') {
      const link = document.createElement('a');
      link.setAttribute('href', URL.createObjectURL($event.data.file));
      link.setAttribute('download', $event.data.fileName);
      link.style.display = 'none';
      document.body.appendChild(link);
      link.click();
      document.body.removeChild(link);
    } else {
      console.log('unhandled action', $event);
    }
  }
}
