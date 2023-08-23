import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { RadioButtonModule } from 'primeng/radiobutton';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextareaModule } from 'primeng/inputtextarea';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { isObservable } from 'rxjs';

@Component({
  selector: 'abs-examination-score-card',
  standalone: true,
  imports: [
    CommonModule,
    RadioButtonModule,
    CheckboxModule,
    InputTextareaModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  templateUrl: './examination-score-card.component.html',
  styleUrls: ['./examination-score-card.component.scss'],
})
export class ExaminationScoreCardComponent implements OnInit, OnChanges {
  // TODO: [Joe] - add form-errors shared component

  @Input() case: any;
  @Input() locked = false;
  @Output() handleChange: EventEmitter<any> = new EventEmitter();

  localLocked = false;
  localData!: any;

  scoreOptions = [
    { label: 'Pass', value: '1' },
    { label: 'Equivocal', value: '2' },
    { label: 'Fail', value: '3' },
  ];

  scoringForm = new FormGroup({
    score: new FormControl(''),
    remarks: new FormControl(''),
    criticalFail: new FormControl(false),
  });

  ngOnInit() {
    this.scoringForm.valueChanges.subscribe((value: any) => {
      const caseData = { ...this.localData };

      if (caseData) {
        caseData.score = value?.score;
        caseData.remarks = value?.remarks;
        caseData.criticalFail = value?.criticalFail;

        this.handleFormChange(caseData);
      }
    });

    if (isObservable(this.case)) {
      this.case.subscribe((data) => {
        this.localData = data;
        this.setLocalData();
      });
    } else {
      this.localData = this.case;
      this.setLocalData();
    }
  }

  setLocalData() {
    this.scoringForm.get('score')?.setValue(this.localData?.score);
    this.scoringForm.get('remarks')?.setValue(this.localData?.remarks);
    this.scoringForm
      .get('criticalFail')
      ?.setValue(this.localData?.criticalFail);
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['case']) {
      const scores = this.case?.scores;
      if (scores) {
        for (const [key, value] of Object.entries(scores)) {
          this.scoringForm.get(key)?.setValue(value);
        }
      }
    }
    if (changes['locked']) {
      this.localLocked = this.locked;
      if (this.locked) {
        this.scoringForm.disable();
      } else {
        this.scoringForm.enable();
      }
    }
  }

  handleFormChange(caseData: any) {
    this.handleChange.emit({
      case: caseData,
    });
  }
}
