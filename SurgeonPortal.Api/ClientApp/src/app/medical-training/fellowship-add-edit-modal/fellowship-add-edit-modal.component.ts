import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { InputSelectComponent } from 'src/app/shared/components/base-input/input-select.component';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { CalendarModule } from 'primeng/calendar';
import { Observable, Subject } from 'rxjs';
import { IFellowshipReadOnlyModel } from 'src/app/api/models/medicaltraining/fellowship-read-only.model';
import { Select } from '@ngxs/store';
import { PicklistsSelectors } from 'src/app/state/picklists';
import { IFellowshipProgramReadOnlyModel } from 'src/app/api/models/picklists/fellowship-program-read-only.model';

@Component({
  selector: 'abs-fellowship-add-edit-modal',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    InputSelectComponent,
    AutoCompleteModule,
    InputTextModule,
    DropdownModule,
    CalendarModule,
  ],
  templateUrl: './fellowship-add-edit-modal.component.html',
  styleUrls: ['./fellowship-add-edit-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class FellowshipAddEditModalComponent implements OnInit {
  @Select(PicklistsSelectors.slices.fellowshipPrograms) fellowshipPrograms$:
    | Observable<IFellowshipProgramReadOnlyModel[]>
    | undefined;

  @Input() userId!: number;
  @Input() isEdit$: Subject<boolean> = new Subject();
  @Input() fellowship$: Subject<IFellowshipReadOnlyModel> = new Subject();
  @Output() cancelDialog: EventEmitter<any> = new EventEmitter();
  @Output() saveDialog: EventEmitter<any> = new EventEmitter();

  fellowshipPrograms!: IFellowshipProgramReadOnlyModel[];

  year = new Date().getFullYear();
  maxYear: Date = new Date();
  fellowshipId!: number;
  isEdit = false;

  fellowshipForm = new FormGroup({
    programName: new FormControl('', Validators.required),
    programOther: new FormControl(''),
    completionYear: new FormControl('', Validators.required),
  });

  ngOnInit(): void {
    this.maxYear.setFullYear(this.year);
    this.isEdit$.subscribe((isEdit) => {
      this.isEdit = isEdit;
    });
    this.setPicklistData();
    this.subscribeToRowData();
  }

  subscribeToRowData() {
    this.fellowship$.subscribe((formData) => {
      if (Object.keys(formData).length > 0) {
        this.fellowshipId = formData.id;
        this.fellowshipForm.patchValue({
          programName: formData.programName,
          programOther: formData.programOther,
          completionYear: formData.completionYear.toString(),
        });
      } else {
        this.fellowshipForm.reset();
      }
    });
  }

  setPicklistData() {
    this.fellowshipPrograms$?.subscribe(
      (fellowshipPrograms: IFellowshipProgramReadOnlyModel[]) => {
        this.fellowshipPrograms = fellowshipPrograms;
      }
    );
  }

  cancel() {
    this.cancelDialog.emit({ show: false });
  }

  save() {
    this.saveDialog.emit({
      edit: this.isEdit,
      show: false,
      fellowshipForm: this.fellowshipForm.value,
      fellowshipId: this.fellowshipId,
    });
  }
}
