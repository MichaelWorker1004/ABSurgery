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
import { IPickListItem, PicklistsSelectors } from 'src/app/state/picklists';
import { IFellowshipProgramReadOnlyModel } from 'src/app/api/models/picklists/fellowship-program-read-only.model';
import { GlobalDialogService } from 'src/app/shared/services/global-dialog.service';

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

  fellowshipPrograms: string[] = [];
  filteredFellowshipPrograms: string[] = [];

  year = new Date().getFullYear();
  maxYear: Date = new Date();
  fellowshipId!: number;
  isEdit = false;

  hasUnsavedChanges = false;

  fellowshipForm = new FormGroup({
    programName: new FormControl(''),
    programOther: new FormControl(''),
    completionYear: new FormControl(''),
  });

  constructor(private globalDialogService: GlobalDialogService) {}

  ngOnInit(): void {
    this.maxYear.setFullYear(this.year);
    this.isEdit$.subscribe((isEdit) => {
      this.isEdit = isEdit;
    });
    this.setPicklistData();
    this.subscribeToRowData();

    this.fellowshipForm.valueChanges.subscribe(() => {
      const isDirty = this.fellowshipForm.dirty;
      if (isDirty && !this.hasUnsavedChanges) {
        this.hasUnsavedChanges = true;
      }
    });
  }

  subscribeToRowData() {
    this.fellowship$.subscribe((formData) => {
      if (Object.keys(formData).length > 0) {
        const programName = this.fellowshipPrograms.find(
          (i) => i === formData.programName.toString()
        );
        this.fellowshipId = formData.id;

        this.fellowshipForm.patchValue({
          programName: programName,
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
        fellowshipPrograms.forEach((fellowshipProgram) => {
          this.fellowshipPrograms.push(fellowshipProgram.programName);
        });
      }
    );
  }

  onInstitutionSelect() {
    this.fellowshipForm.get('programOther')?.patchValue('');
    this.fellowshipForm.get('programOther')?.disable();
  }

  clearAutoComplete() {
    this.fellowshipForm.get('programName')?.patchValue('');
    this.fellowshipForm.get('programOther')?.enable();
  }

  filterItems($event: any) {
    const value = $event.query;
    this.filteredFellowshipPrograms = this.fellowshipPrograms.filter((i) => {
      return i?.toLowerCase().includes(value.toLowerCase());
    });
  }

  cancel() {
    if (this.hasUnsavedChanges) {
      this.globalDialogService
        .showConfirmation('Unsaved Changes', 'Do you want to navigate away')
        .then((result) => {
          if (result) {
            this.cancelDialog.emit({ show: false });
          }
        });
    } else {
      this.cancelDialog.emit({ show: false });
    }
  }

  save() {
    this.saveDialog.emit({
      edit: this.isEdit,
      show: false,
      fellowshipForm: this.fellowshipForm.value,
      fellowshipId: this.fellowshipId,
    });
    this.hasUnsavedChanges = false;
  }
}
