import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { GridComponent } from 'src/app/shared/components/grid/grid.component';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { REFERENCE_FORMS_COLS } from './refrence-forms-cols';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { CheckboxModule } from 'primeng/checkbox';
import { IFormFields } from 'src/app/shared/models/form-fields/form-fields';
import { Select } from '@ngxs/store';
import { PicklistsSelectors } from 'src/app/state/picklists';
import { Observable } from 'rxjs';
import { IStateReadOnlyModel } from 'src/app/api';
import { ButtonModule } from 'primeng/button';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { ADD_REFERENCE_LETTER_FIELDS } from './add-reference-letter-fields';
import { CollapsePanelComponent } from 'src/app/shared/components/collapse-panel/collapse-panel.component';

@UntilDestroy()
@Component({
  selector: 'abs-reference-form-modal',
  standalone: true,
  imports: [
    CommonModule,
    GridComponent,
    FormsModule,
    ReactiveFormsModule,
    InputTextModule,
    DropdownModule,
    CheckboxModule,
    ButtonModule,
    CollapsePanelComponent,
  ],
  templateUrl: './reference-form-modal.component.html',
  styleUrls: ['./reference-form-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class ReferenceFormModalComponent implements OnInit {
  @ViewChild('referenceRequestPanel')
  referenceRequestPanel!: CollapsePanelComponent;

  @Select(PicklistsSelectors.slices.states) states$:
    | Observable<IStateReadOnlyModel[]>
    | undefined;

  @Output() closeDialog: EventEmitter<any> = new EventEmitter();

  lapsedPath = true; // TODO - will be set from user data
  formExpanded = false;

  referenceLetterForm = new FormGroup({
    nameOfAuthenticatingOfficial: new FormControl('', [Validators.required]),
    authenticatingOfficialRole: new FormControl('', [Validators.required]),
    reasonForAlternateOfficial: new FormControl(''),
    authenticatingOfficialTitle: new FormControl(''),
    authenticatingOfficialEmail: new FormControl('', [Validators.required]),
    confirmEmailAddress: new FormControl('', [Validators.required]),
    authenticatingOfficialPhoneNumber: new FormControl('', [
      Validators.required,
    ]),
    nameOfAffiliatedInstitution: new FormControl(''),
    city: new FormControl('', [Validators.required]),
    states: new FormControl('', [Validators.required]),
    name: new FormControl({ value: '', readonly: true }),
  });

  referenceAttestationsForm = new FormGroup({});

  userData = {
    diplayName: 'John Doe, M.D',
  };

  referenceFormsCols = REFERENCE_FORMS_COLS;
  referenceLetterFields: IFormFields[] = ADD_REFERENCE_LETTER_FIELDS;
  refrenceGridData = [
    {
      referenceFormId: 'MD19143',
      affiliatedInstitution: 'ABS',
      authenticatingOfficial: 'John Doe, M.D.',
      date: new Date('09/21/2019'),
      status: 'Requested',
    },
    {
      referenceFormId: 'MD08221',
      affiliatedInstitution: 'ABS',
      authenticatingOfficial: 'Mary Joseph',
      date: new Date('08/12/2019'),
      status: 'Approved',
    },
    {
      referenceFormId: 'MD12345',
      affiliatedInstitution: 'ABS',
      authenticatingOfficial: 'John Dorian',
      date: new Date('8/1/2019'),
      status: 'Approved',
    },
  ];

  ngOnInit(): void {
    this.setPicklists();
    // note to all future developers, never do this, it is stupid and hacky
    // but also it was the only thing that worked, blame shoelace
    document.addEventListener('sl-after-hide', ($event: any) => {
      if ($event?.srcElement?.innerHTML.includes('Reference Form')) {
        this.referenceRequestPanel.collaspsePanel();
      }
    });
  }

  setPicklists() {
    this.states$?.pipe(untilDestroyed(this)).subscribe((states) => {
      this.referenceLetterFields.filter((field) => {
        if (field.name === 'states') {
          field.options = states;
        }
      });
    });
  }

  handleGridAction(event: any) {
    console.log('unhandled action', event);
  }

  onSubmitPanel(formFields: any) {
    console.log('unhandled submit', formFields);
    this.referenceRequestPanel.collaspsePanel();
  }

  closePanel() {
    this.referenceRequestPanel.collaspsePanel();
  }

  onSubmit(formFields: any) {
    console.log('unhandled submit', formFields);
  }

  close() {
    this.closeDialog.emit();
  }

  togglePanelHandler(event: any) {
    this.formExpanded = event.expanded;
    if (event.expanded) {
      // handle form reset as part of expand
      console.log('panel expanded', event);
    } else {
      // handle form cancel as part of collapse
      console.log('panel collapsed', event);
    }
  }
}
