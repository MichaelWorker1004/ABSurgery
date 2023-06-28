import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { ExaminationScoreCardComponent } from 'src/app/shared/components/examination-score-card/examination-score-card.component';

@Component({
  selector: 'abs-examination-score-modal',
  standalone: true,
  imports: [CommonModule, ButtonModule, ExaminationScoreCardComponent],
  templateUrl: './examination-score-modal.component.html',
  styleUrls: ['./examination-score-modal.component.scss'],
})
export class ExaminationScoreModalComponent {
  @Input() candidateData: any;
  @Output() closeDialog = new EventEmitter();

  candidateCaseScores = {} as any;

  handleChange(event: any) {
    this.candidateCaseScores[event.case.caseId] = event;
  }

  saveScores() {
    console.log(this.candidateCaseScores);
  }

  cancel() {
    this.closeDialog.emit();
  }
}
