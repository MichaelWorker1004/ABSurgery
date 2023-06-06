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

  otherCertificatesForm = new FormGroup({
    certificationType: new FormControl(''),
    certificateNumner: new FormControl(''),
    issueDate: new FormControl(''),
  });

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
  }

  subscribeToRowData() {
    this.otherCertificate$.subscribe((res) => {
      console.log(res);
    });
  }

  cancel() {
    this.cancelDialog.emit({ show: false });
  }

  save() {
    this.saveDialog.emit({
      edit: this.isEdit,
      show: false,
      fellowshipForm: this.otherCertificatesForm.value,
      fellowshipId: this.otherCertificateId,
    });
  }
}
