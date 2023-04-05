import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { GridComponent } from '../../shared/components/grid/grid.component';
import { TrainingAddEditModalComponent } from '../../shared/components/training-add-edit-modal/training-add-edit-modal.component';
import { MEDICAL_TRAINING_COLS } from '../../shared/gridDefinitions/medical-training-cols';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'abs-training-modal',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    GridComponent,
    TrainingAddEditModalComponent,
  ],
  templateUrl: './training-modal.component.html',
  styleUrls: ['./training-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class TrainingModalComponent {
  @Output() closeDialog: EventEmitter<any> = new EventEmitter();

  trainingCols = MEDICAL_TRAINING_COLS;
  showTrainingAddEdit = false;
  tempTraining$: BehaviorSubject<any> = new BehaviorSubject({});
  panels = [
    'medicalSchool',
    'residency',
    'additionalTraining',
    'additionalDocuments',
  ];
  activePanel = 0;

  medicalTraining: any;

  ngOnInit() {
    this.getMedicalTraining();
  }

  getMedicalTraining() {
    this.medicalTraining = {
      medicalSchool: {
        statement: 'USA',
        name: 'Johns Hopkins University',
        city: 'Baltimore',
        state: 'MD',
        country: 'USA',
        degree: 'MD',
        yearGraduated: '2010',
      },
      residency: {
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
      },
      additionalTraining: [
        {
          id: 1,
          type: 'Advanced 01 Surgery',
          state: 'NY',
          city: 'New York',
          institution: 'York Hospital',
          other: '-',
          from: new Date('7/01/2007'),
          to: new Date('7/01/2008'),
        },
        {
          id: 2,
          type: 'Advanced 01 Surgery',
          state: 'NY',
          city: 'New York',
          institution: 'York Hospital',
          other: '-',
          from: new Date('7/01/2007'),
          to: new Date('7/01/2008'),
        },
        {
          id: 3,
          type: 'Advanced 01 Surgery',
          state: 'NY',
          city: 'New York',
          institution: 'York Hospital',
          other: '-',
          from: new Date('7/01/2007'),
          to: new Date('7/01/2008'),
        },
      ],
      additionalDocuments: {
        multiplePrograms: null,
        completionDocuments: [
          {
            file: null,
            fileName: null,
            uploadDate: null,
          },
        ],
        recievedABSApprovalLetter: null,
        approvalLetters: [
          {
            file: null,
            fileName: null,
            uploadDate: null,
          },
        ],
      },
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

  showTrainingModal(training: any) {
    if (training) {
      console.log(training);
      this.tempTraining$.next(training);
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

  handleGridAction($event: any) {
    if ($event.fieldKey === 'edit') {
      this.showTrainingModal($event.data);
    } else {
      console.log('unhandled action', $event);
    }
  }
}
