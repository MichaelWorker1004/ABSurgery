import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  Input,
  OnInit,
} from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'abs-progress-bar',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './progress-bar.component.html',
  styleUrls: ['./progress-bar.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class ProgressBarComponent implements OnInit {
  @Input() currentNumber!: number;
  @Input() totalNumber!: number;
  @Input() statusLabel!: string;
  @Input() barColor!: string;
  @Input() toolTipText!: string;

  remainingNumber!: number;
  progressBarWidth!: number;

  ngOnInit(): void {
    this.remainingNumber = this.totalNumber - this.currentNumber;
    this.progressBarWidth = Math.round(
      (this.currentNumber / this.totalNumber) * 100
    );
  }
}
