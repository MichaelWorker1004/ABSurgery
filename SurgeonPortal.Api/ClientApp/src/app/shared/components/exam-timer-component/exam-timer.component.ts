import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnDestroy,
  OnInit,
  Output,
  SimpleChanges,
} from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'abs-exam-timer',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './exam-timer.component.html',
  styleUrls: ['./exam-timer.component.scss'],
})
export class ExamTimerComponent implements OnInit, OnDestroy, OnChanges {
  /**
   * label for the total timer
   */
  @Input() timerLabel = 'Total';

  /**
   * label for the increment timer
   */
  @Input() incrementLabel = 'Increment';

  /**
   * how many increments go into the total time
   */
  @Input() totalIncrements = 4;

  /**
   * how many minutes are in an increment
   */
  @Input() incrementDuration = 7;

  /**
   * what is the current increment
   */
  @Input() currentIncrement = 1;

  /**
   * event emitter for when the increment timer ends
   */
  @Output() incrementEnd: EventEmitter<any> = new EventEmitter();

  /**
   * event emitter for when the total timer ends
   */
  @Output() timerEnd: EventEmitter<any> = new EventEmitter();

  localIncrementDuration!: number;
  localTotalIncrements!: number;
  localCurrentIncrement!: number;

  incrementNegative = false;

  totalTimerValue = 0;
  totalTimer = {
    minutes: 0,
    seconds: 0,
  };
  incrementTimerValue = 0;
  incrementTimer = {
    minutes: 0,
    seconds: 0,
  };

  private totalTimerInterval: any;

  ngOnInit(): void {
    this.localCurrentIncrement = this.currentIncrement;
    this.localIncrementDuration = this.incrementDuration;
    this.localTotalIncrements = this.totalIncrements;
    this.startTimers();
  }
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['currentIncrement']) {
      if (
        this.localTotalIncrements >= changes['currentIncrement'].currentValue
      ) {
        this.localCurrentIncrement = changes['currentIncrement'].currentValue;
      } else {
        this.localCurrentIncrement = this.localTotalIncrements;
      }

      this.incrementTimerValue = this.localIncrementDuration * 60;
      if (
        this.incrementTimerValue >= this.totalTimerValue ||
        this.localCurrentIncrement === this.localTotalIncrements
      ) {
        this.incrementTimerValue = this.totalTimerValue;
      }
      this.incrementNegative = false;
    }
    if (changes['incrementDuration']) {
      this.localIncrementDuration = changes['incrementDuration'].currentValue;
      this.startTimers();
    }
    if (changes['totalIncrements']) {
      this.localTotalIncrements = changes['totalIncrements'].currentValue;
      this.startTimers();
    }
  }
  ngOnDestroy(): void {
    this.stopTimers();
  }

  startTimers() {
    if (this.totalTimerInterval) {
      clearInterval(this.totalTimerInterval);
    }
    this.totalTimerValue =
      this.localIncrementDuration *
      60 *
      (this.localTotalIncrements - this.localCurrentIncrement + 1);
    this.totalTimer.minutes = Math.floor(this.totalTimerValue / 60);
    this.totalTimer.seconds = this.totalTimerValue % 60;

    this.incrementTimerValue = this.localIncrementDuration * 60;
    if (this.incrementTimerValue >= this.totalTimerValue) {
      this.incrementTimerValue = this.totalTimerValue;
    }
    this.incrementTimer.minutes = Math.floor(this.incrementTimerValue / 60);
    this.incrementTimer.seconds = this.incrementTimerValue % 60;

    this.totalTimerInterval = setInterval(() => {
      //total timer
      if (this.totalTimerValue > 0) {
        this.totalTimerValue--;
        this.totalTimer.minutes = Math.floor(this.totalTimerValue / 60);
        this.totalTimer.seconds = this.totalTimerValue % 60;

        //increment timer
        if (this.incrementTimerValue > 0) {
          this.incrementTimerValue--;
          this.incrementTimer.minutes = Math.floor(
            this.incrementTimerValue / 60
          );
          this.incrementTimer.seconds = this.incrementTimerValue % 60;
        } else {
          if (!this.incrementNegative) {
            // only fire the increment end event once per increment
            this.incrementEnd.emit();
          }
          if (this.localCurrentIncrement >= this.localTotalIncrements) {
            this.stopTimers();
          } else {
            // allow the increment time to go negative if they take longer on an increment
            this.incrementNegative = true;
            this.incrementTimerValue--;
            this.incrementTimer.minutes = Math.floor(
              Math.abs(this.incrementTimerValue) / 60
            );
            this.incrementTimer.seconds =
              Math.abs(this.incrementTimerValue) % 60;

            // uncomment to have to increment timer start again automatically
            // this.localCurrentIncrement++;
            // this.incrementTimerValue = this.localIncrementDuration * 60 - 1;
            // if (this.incrementTimerValue >= this.totalTimerValue) {
            //   this.incrementTimerValue = this.totalTimerValue;
            // }
            // this.incrementTimer.minutes = Math.floor(
            //   this.incrementTimerValue / 60
            // );
            // this.incrementTimer.seconds = this.incrementTimerValue % 60;
          }
        }
      } else {
        this.stopTimers();
        this.incrementNegative = false;
        this.incrementTimer.minutes = 0;
        this.incrementTimer.seconds = 0;
      }
    }, 1000);
  }

  stopTimers() {
    clearInterval(this.totalTimerInterval);
    this.timerEnd.emit();
    this.incrementEnd.emit();
  }
}
