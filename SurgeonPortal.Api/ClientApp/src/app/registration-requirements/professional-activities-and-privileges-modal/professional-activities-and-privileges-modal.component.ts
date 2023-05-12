import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  OnInit,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { provideNgxMask } from 'ngx-mask';
import { HOSPOITAL_APPOINTMENTS_COLS } from './hospital-appointments-cols';
import { GridComponent } from 'src/app/shared/components/grid/grid.component';
import { InputTextareaModule } from 'primeng/inputtextarea';

@Component({
  selector: 'abs-professional-activities-and-privileges-modal',
  standalone: true,
  imports: [CommonModule, GridComponent, InputTextareaModule],
  templateUrl: './professional-activities-and-privileges-modal.component.html',
  styleUrls: ['./professional-activities-and-privileges-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [provideNgxMask()],
})
export class ProfessionalActivitiesAndPrivilegesModalComponent
  implements OnInit
{
  @Output() closeDialog: EventEmitter<any> = new EventEmitter();

  panels = [
    'hospitalAppointments',
    'explanationOfLackOfPrivileges',
    'nonClinicalActivities',
  ];
  activePanel = 0;

  surgeonProfile: any;

  hospitalAppointmentsCols = HOSPOITAL_APPOINTMENTS_COLS;
  hospitalAppointmentsData!: any;

  explanationOfLackOfPrivilegesForm = '';
  nonClincalActivitiesForm = '';

  ngOnInit() {
    console.log('init');
    this.getProfessionalActivitiesAndPrivilegesData();
  }

  handleGridAction(event: any) {
    console.log(event);
  }

  getProfessionalActivitiesAndPrivilegesData() {
    this.hospitalAppointmentsData = [
      {
        practiceType: 'Administration (Exclusively)',
        apptType: 'Other',
        organizationType: 'Organization Type',
        city: 'York',
        state: 'PA',
        institution: 'York Hospital [6228]',
        other: '-',
        authOfficial: 'ME',
      },
      {
        practiceType: 'Clinical Practice In Surgery',
        apptType: 'Active Staff',
        organizationType: 'Governmental (Military, VA, State, etc.)',
        city: 'York',
        state: 'PA',
        institution: 'Other Institution',
        other: 'ABS',
        authOfficial: 'Frank Lewis, Jr.',
      },
    ];
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
