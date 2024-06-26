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
import { debounceTime, distinctUntilChanged, isObservable, take } from 'rxjs';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

@UntilDestroy()
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
    TranslateModule,
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

  disabledCriticalFail = true;

  scoreOptions = [
    { label: 'Pass', value: '3' },
    { label: 'Equivocal', value: '2' },
    { label: 'Fail', value: '1' },
  ];

  scoringForm = new FormGroup({
    score: new FormControl(''),
    remarks: new FormControl(''),
    criticalFail: new FormControl({ value: false, disabled: true }),
  });

  constructor(private _translateService: TranslateService) {}

  ngOnInit() {
    this.scoringForm.valueChanges
      .pipe(debounceTime(250), distinctUntilChanged(), untilDestroyed(this))
      .subscribe((value: any) => {
        const caseData = { ...this.localData };
        if (caseData) {
          caseData.score = value?.score ?? 0;
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
      const localData = { ...this.case };
      localData['caseNumber'] = this.case?.sections
        ? this.case?.sections[0].caseNumber
        : this.case?.caseNumber;

      if (this.case?.sections) {
        this._translateService
          .get('EXAMSCORING.EXAMINATION.SCORE_CARD.REMARKS_TITLE', {
            caseTitle: this.case?.sections[0].caseTitle,
            caseNumber: this.case?.sections[0].caseNumber,
          })
          .pipe(distinctUntilChanged(), untilDestroyed(this))
          .subscribe((res) => {
            localData['remarksTitle'] = res;
          });
      } else {
        this._translateService
          .get('EXAMSCORING.EXAMINATION.SCORE_CARD.REMARKS_TITLE_NO_CASE', {
            caseNumber: this.case?.caseNumber,
          })
          .pipe(distinctUntilChanged(), untilDestroyed(this))
          .subscribe((res) => {
            localData['remarksTitle'] = res;
          });
      }

      this.localData = localData;
      this.setLocalData();
    }
  }

  handleScoreSelect(value: any) {
    if (value === '1' || value === 1) {
      this.scoringForm.get('criticalFail')?.enable();
      this.disabledCriticalFail = false;
    } else {
      this.scoringForm.get('criticalFail')?.disable();
      this.scoringForm.get('criticalFail')?.setValue(false);
      this.disabledCriticalFail = true;
    }
  }

  setLocalData() {
    this.scoringForm.get('score')?.setValue(this.localData?.score);
    this.scoringForm.get('remarks')?.setValue(this.localData?.remarks);
    this.scoringForm
      .get('criticalFail')
      ?.setValue(this.localData?.criticalFail);

    this.handleScoreSelect(this.scoringForm.get('score')?.value);
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
