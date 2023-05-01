import {
  Component,
  CUSTOM_ELEMENTS_SCHEMA,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Observable, Subject } from 'rxjs';
import {
  GetStateList,
  IPickListItem,
  PicklistsSelectors,
} from 'src/app/state/picklists';
import { IAdditionalTrainingModel, IStateReadOnlyModel } from 'src/app/api';
import { Select, Store } from '@ngxs/store';
import { InputSelectComponent } from '../../../shared/components/base-input/input-select.component';

@Component({
  selector: 'abs-training-add-edit-modal',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    InputSelectComponent,
  ],
  templateUrl: './training-add-edit-modal.component.html',
  styleUrls: ['./training-add-edit-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class TrainingAddEditModalComponent implements OnInit {
  @Input() showDialog = false;
  @Input() training$: Subject<IAdditionalTrainingModel> = new Subject();
  @Output() cancelDialog: EventEmitter<any> = new EventEmitter();
  @Output() saveDialog: EventEmitter<any> = new EventEmitter();

  @Select(PicklistsSelectors.slices.states) states$:
    | Observable<IStateReadOnlyModel[]>
    | undefined;

  additionalTrainingForm!: FormGroup;

  stateOptions: IPickListItem[] = [];

  constructor(private _store: Store, private formBuilder: FormBuilder) {
    this._store.dispatch(new GetStateList('500'));
    this.states$?.subscribe((value) => {
      this.stateOptions = value;
    });
  }

  ngOnInit() {
    this.additionalTrainingForm = this.formBuilder.group({
      trainingId: [''],
      typeOfTraining: ['', Validators.required],
      dateStarted: ['', Validators.required],
      dateEnded: ['', Validators.required],
      institutionId: [''],
      stateId: [''],
      city: [''],
      other: [''],
    });
    this.training$.subscribe((value) => {
      this.additionalTrainingForm.patchValue({ ...value });
      if (value.dateStarted) {
        const dateStarted = new Date(value.dateStarted)
          .toISOString()
          .split('T')[0];
        this.additionalTrainingForm.patchValue({ dateStarted: dateStarted });
      }
      if (value.dateEnded) {
        const dateEnded = new Date(value.dateEnded).toISOString().split('T')[0];
        this.additionalTrainingForm.patchValue({ dateEnded: dateEnded });
      }
    });
  }

  get typeOfTraining() {
    return this.additionalTrainingForm.get('typeOfTraining');
  }

  handleDefaultClose(event: any) {
    event.preventDefault();
  }

  cancel() {
    this.cancelDialog.emit({ show: false });
  }

  save() {
    this.saveDialog.emit({
      show: false,
      trainingRecord: this.additionalTrainingForm.value,
    });
  }

  trackByFn(
    index: number,
    item: IPickListItem
  ): string | number | null | undefined {
    return item.itemValue;
  }

  handleSelects($event: any) {
    const select = $event.target;
    const formControl = $event.target.getAttribute('data-controlname');
    const patchObj: any = {};
    patchObj[formControl] = select.value;
    this.additionalTrainingForm.patchValue(patchObj);
  }
}
