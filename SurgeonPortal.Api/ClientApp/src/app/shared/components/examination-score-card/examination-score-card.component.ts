import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
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
export class ExaminationScoreCardComponent implements OnInit {
  @Input() case: any;
  @Output() handleChange: EventEmitter<any> = new EventEmitter();

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
  }

  handleFormChange() {
    this.handleChange.emit({
      scoreValues: this.scoringForm.value,
      case: this.case,
    });
  }
}
