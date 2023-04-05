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

@Component({
  selector: 'abs-surgeon-profile-modal',
  standalone: true,
  imports: [CommonModule, FormsModule, NgxMaskDirective],
  templateUrl: './surgeon-profile-modal.component.html',
  styleUrls: ['./surgeon-profile-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [provideNgxMask()],
})
export class SurgeonProfileModalComponent implements OnInit {
  @Output() closeDialog: EventEmitter<any> = new EventEmitter();

  panels = ['personalInformation', 'contactInformation'];
  activePanel = 0;

  surgeonProfile: any;

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
        phone: '123-456-7890',
        mobile: '123-456-7890',
        fax: '123-456-7890',
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
    // timeout is needed to allow the modal to close before the tab panel is reset
    setTimeout(() => {
      this.showTabPanel(this.panels[0]);
    }, 500);
  }
}
