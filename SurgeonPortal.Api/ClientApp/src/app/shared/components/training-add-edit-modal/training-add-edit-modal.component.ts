import {
  Component,
  CUSTOM_ELEMENTS_SCHEMA,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Subject } from 'rxjs';

@Component({
  selector: 'abs-training-add-edit-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './training-add-edit-modal.component.html',
  styleUrls: ['./training-add-edit-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class TrainingAddEditModalComponent implements OnInit {
  @Input() showDialog = false;
  @Input() training: Subject<any> = new Subject();
  @Output() cancelDialog: EventEmitter<any> = new EventEmitter();
  @Output() saveDialog: EventEmitter<any> = new EventEmitter();

  localTraining: any = {};

  ngOnInit() {
    this.training.subscribe((value) => {
      this.localTraining = value;
      if (value.from) {
        // TODO: [Joe] convert to string format yyyy-MM-dd
        this.localTraining.from = new Date(value.from)
          .toISOString()
          .split('T')[0];
      }
      if (value.to) {
        this.localTraining.to = new Date(value.to).toISOString().split('T')[0];
      }
    });
  }

  handleDefaultClose(event: any) {
    event.preventDefault();
  }

  cancel() {
    this.cancelDialog.emit({ show: false });
  }

  save() {
    this.saveDialog.emit({ show: false, license: this.localTraining });
  }
}
