import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'abs-examination-score-modal',
  standalone: true,
  imports: [CommonModule, ButtonModule],
  templateUrl: './examination-score-modal.component.html',
  styleUrls: ['./examination-score-modal.component.scss'],
})
export class ExaminationScoreModalComponent {
  @Input() candidateData: any;
  @Output() closeDialog = new EventEmitter();

  cancel() {
    this.closeDialog.emit();
  }
}
