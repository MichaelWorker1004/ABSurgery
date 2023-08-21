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
import { AutoCompleteModule } from 'primeng/autocomplete';
import { CalendarModule } from 'primeng/calendar';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { InputSelectComponent } from 'src/app/shared/components/base-input/input-select.component';
import { Observable, Subject } from 'rxjs';
import { Select } from '@ngxs/store';
import { ICertificateTypeReadOnlyModel } from 'src/app/api/models/picklists/certificate-type-read-only.model';
import { PicklistsSelectors } from 'src/app/state/picklists';
import { GlobalDialogService } from 'src/app/shared/services/global-dialog.service';

@Component({
  selector: 'abs-other-certificates-add-edit-modal',
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
  templateUrl: './other-certificates-add-edit-modal.component.html',
  styleUrls: ['./other-certificates-add-edit-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class OtherCertificatesAddEditModalComponent implements OnInit {
  //TODO: [Joe] - add form-errors shared component

  @Select(PicklistsSelectors.slices.certificateTypes) certificateTypes$:
    | Observable<ICertificateTypeReadOnlyModel[]>
    | undefined;

  @Input() userId!: number;
  @Input() isEdit$: Subject<boolean> = new Subject();
  @Input() otherCertificate$: Subject<any> = new Subject();
  @Output() cancelDialog: EventEmitter<any> = new EventEmitter();
  @Output() saveDialog: EventEmitter<any> = new EventEmitter();

  certificateTypes!: ICertificateTypeReadOnlyModel[];

  otherCertificateId!: number;
  isEdit!: boolean;

  hasUnsavedChanges = false;

  otherCertificatesForm = new FormGroup({
    id: new FormControl(0),
    certificateTypeId: new FormControl(
      { value: 6, disabled: true },
      Validators.required
    ),
    certificateNumber: new FormControl(''),
    issueDate: new FormControl('', [Validators.required]),
  });

  constructor(private globalDialogService: GlobalDialogService) {}

  ngOnInit(): void {
    this.isEdit$.subscribe((isEdit) => {
      this.isEdit = isEdit;
    });
    this.subscribeToRowData();
    this.certificateTypes$?.subscribe(
      (certificateTypes: ICertificateTypeReadOnlyModel[]) => {
        this.certificateTypes = certificateTypes;
      }
    );

    this.otherCertificatesForm.valueChanges.subscribe(() => {
      const isDirty = this.otherCertificatesForm.dirty;
      if (isDirty && !this.hasUnsavedChanges) {
        this.hasUnsavedChanges = true;
      }
    });
  }

  subscribeToRowData() {
    this.otherCertificate$.subscribe((res) => {
      if (Object.keys(res.length > 0)) {
        for (const [key, value] of Object.entries(res)) {
          this.otherCertificatesForm.get(key)?.setValue(value);
          this.otherCertificateId = res.id;
        }
        this.otherCertificatesForm
          .get('issueDate')
          ?.setValue(new Date(res.issueDate).toLocaleDateString());
      }
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
      otherCertificateForm: this.otherCertificatesForm.value,
      otherCertificateId: this.otherCertificateId,
    });
  }
}
