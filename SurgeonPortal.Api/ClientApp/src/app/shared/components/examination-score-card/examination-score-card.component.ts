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
  @Input() case: any;
  @Output() handleChange: EventEmitter<any> = new EventEmitter();

  localData!: any;

  scoreOptions = [
    { label: 'Pass', value: '1' },
    { label: 'Equivocal', value: '2' },
    { label: 'Fail', value: '3' },
  ];

  scoringForm = new FormGroup({
    score: new FormControl(''),
    comment: new FormControl(''),
    criticalFail: new FormControl(''),
  });

  ngOnInit() {
    this.scoringForm.valueChanges.subscribe((value: any) => {
      this.handleFormChange();
    });

    if (isObservable(this.case)) {
      this.case.subscribe((data) => {
        this.localData = data;
      });
    } else {
      this.localData = this.case;
    }
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['case']) {
      console.log(this.case.scores);
      const scores = this.case.scores;
      if (scores) {
        for (const [key, value] of Object.entries(scores)) {
          console.log(key, value);
          this.scoringForm.get(key)?.setValue(value);
        }
      }
    }
  }

  handleFormChange() {
    this.handleChange.emit({
      scoreValues: this.scoringForm.value,
      case: this.case,
    });
  }
}
