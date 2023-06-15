import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  OnInit,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxMaskDirective } from 'ngx-mask';
import { provideNgxMask } from 'ngx-mask';
// import { GlobalDialogService } from 'src/app/shared/services/global-dialog.service';

import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { InputMaskModule } from 'primeng/inputmask';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'abs-surgeon-profile-modal',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    NgxMaskDirective,
    InputTextModule,
    DropdownModule,
    InputMaskModule,
    ButtonModule,
  ],
  templateUrl: './surgeon-profile-modal.component.html',
  styleUrls: ['./surgeon-profile-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [provideNgxMask()],
})
export class SurgeonProfileModalComponent implements OnInit {
  @Output() closeDialog: EventEmitter<any> = new EventEmitter();

  fakeOptions = [
    { itemDescription: 'Option 1', itemValue: 'option-1' },
    { itemDescription: 'Option 2', itemValue: 'option-2' },
    { itemDescription: 'Option 3', itemValue: 'option-3' },
  ];

  panels = ['personalInformation', 'contactInformation'];
  activePanel = 0;

  surgeonProfile: any;

  // constructor(private _globalDialogService: GlobalDialogService) {}

  ngOnInit() {
    this.getSurgeonProfile();
  }

  getSurgeonProfile() {
    this.surgeonProfile = {
      personalInfo: {
        cid: '123456789',
        fullName: 'Joe Bob Smith',
        firstName: 'Joe',
        middleName: 'Bob',
        lastName: 'Smith',
        certName: 'Joe Bob Smith, M.D.',
        gender: 'male',
        race: 'white',
        ethnicity: 'white',
        firstLanguage: 'english',
        bestLanguage: 'english',
      },
      contactInfo: {
        address: '123 Main St',
        address2: 'Apt 1',
        city: 'Philadelphia',
        state: 'PA',
        zip: '94105',
        country: 'USA',
        phone: '1234567890',
        mobile: '1234567890',
        fax: '1234567890',
        email: 'test@test.io',
        npid: '123456789',
      },
    };
  }

  handleDefaultShowTab(event: any) {
    this.activePanel = this.panels.indexOf(event.detail.name);
  }

  showTabPanel(panel: string) {
    const tabGroup: any | null = document.querySelector('#surgeonProfileTabs');
    tabGroup?.show(panel);
  }

  save() {
    if (this.activePanel === this.panels.length - 1) {
      this.close();
    } else {
      this.showTabPanel(this.panels[this.activePanel + 1]);
    }
  }

  close() {
    this.closeDialog.emit();
    // this._globalDialogService.closeOpenDialog();
    // timeout is needed to allow the modal to close before the tab panel is reset
    setTimeout(() => {
      this.showTabPanel(this.panels[0]);
    }, 500);
  }
}
