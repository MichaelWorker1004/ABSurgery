import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit } from '@angular/core';
import { FullCalendarModule } from '@fullcalendar/angular';
import { Calendar, CalendarOptions } from '@fullcalendar/core';
import multiMonthPlugin from '@fullcalendar/multimonth';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';

import { CollapsePanelComponent } from '../shared/components/collapse-panel/collapse-panel.component';
import { InputSelectComponent } from '../shared/components/base-input/input-select.component';
import { AlertComponent } from '../shared/components/alert/alert.component';
import { ITEMIZED_GME_COLS } from './itemized-gme-cols';
import { BehaviorSubject, filter, Subject } from 'rxjs';
import { GridComponent } from '../shared/components/grid/grid.component';
import { ModalComponent } from '../shared/components/modal/modal.component';
import { ConflictResolutionModalComponent } from './conflict-resolution-modal/conflict-resolution-modal.component';
import { IGridOptions } from '../shared/components/grid/grid-options.model';
import { AbsFilterType } from '../shared/components/grid/abs-grid.enum';
import { AddRecordModalComponent } from './add-record-modal/add-record-modal.component';

@Component({
  selector: 'abs-gme-history',
  templateUrl: './gme-history.component.html',
  styleUrls: ['./gme-history.component.scss'],
  standalone: true,
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  imports: [
    FormsModule,
    CommonModule,
    FullCalendarModule,
    CollapsePanelComponent,
    InputSelectComponent,
    AlertComponent,
    GridComponent,
    ModalComponent,
    ConflictResolutionModalComponent,
    AddRecordModalComponent,
    ModalComponent,
  ],
})
export class GmeHistoryComponent implements OnInit {
  calendar: any;
  calendarReady = false;
  calendarFilter = 'current-year';

  clinicalActivity!: any[];
  nonClinicalActivity!: any[];
  conflicts!: any[];

  showConflictResolutionModal = false;

  calendarOptions: CalendarOptions = {
    height: 'auto',
    headerToolbar: {
      start: 'prev',
      center: '',
      end: 'next',
    },
    eventDisplay: 'background',
    plugins: [multiMonthPlugin],
    initialView: 'multiMonthThreeMonth',
    views: {
      multiMonthThreeMonth: {
        type: 'multiMonth',
        duration: { months: 3 },
        multiMonthMinWidth: 100,
        multiMonthTitleFormat: { year: 'numeric', month: 'long' },
        showNonCurrentDates: false,
      },
    },
    eventContent(info) {
      let content;
      let realEnd;
      if (info.event.end !== null) {
        realEnd = new Date(info.event.end?.getTime());
        realEnd.setDate(realEnd.getDate() - 1);
      }
      if (info.event.display !== 'list-item') {
        content = document.createElement('sl-tooltip');
        let innerContent = `<div slot="content">${info.event.start?.toLocaleDateString()}${
          info.event.end ? ' - ' + realEnd?.toLocaleDateString() : ''
        }
          <br>${info.event.extendedProps['eventTitle']}</div>
          <div style="width: 100%; height: 100%;display:flex;">`;

        if (info.isStart) {
          innerContent += `<div class="gme-calendar-highlight ${
            info.event.extendedProps['class']
          }">${info.event.start?.getDate()}</div>`;
        }
        if (info.isEnd && info.event.end !== null) {
          innerContent += `<div class="gme-calendar-highlight ${
            info.event.extendedProps['class']
          } ml-auto">${realEnd?.getDate()}</div>`;
        }
        innerContent += `</div>`;
        content.innerHTML = innerContent;
        const domNodes = [content];
        return { domNodes: domNodes };
      } else {
        content = `<div class="fc-daygrid-event-dot" style="border-color: rgb(139, 4, 10);"></div><div class="fc-event-title">${info.event.title}</div>`;
        return { html: content };
      }
    },
    eventClick: (info) => {
      if (info.event.extendedProps['class'] === 'conflict') {
        const conflicts: any[] = [];
        this.viewConflictsToResolve(conflicts);
      }
    },
  };

  itemizedGridOptions: IGridOptions = {
    showFilter: true,
    filterType: AbsFilterType.Dropdown,
    placeholder: 'All Clinical Levels',
    filterOn: 'clinicalLevel',
    filterOptions: [],
  };
  itemizedGme$: Subject<boolean> = new BehaviorSubject(true);
  itemizedGmeCols = ITEMIZED_GME_COLS;
  itemizedGmeData!: any[];

  showAddEditGmeRotation = false;
  isEditGmeRotation!: boolean;

  ngOnInit(): void {
    setTimeout(() => {
      this.calendarOptions.eventSources = [
        this.getClinicalActivity(),
        this.getNonClinicalActivity(),
        this.getConflicts(),
      ];
      this.calendarReady = true;
    }, 0);

    this.getItemizedGmeData();
  }

  getItemizedGmeData() {
    const filterOptions: { value: any; label: any }[] | undefined = [];

    this.itemizedGmeData = [
      {
        from: new Date('09/29/15'),
        to: new Date('10/29/15'),
        weeks: 4,
        programName: 'AZ - University of Arizona [0017]',
        affiliatedInstitute: '',
        clinicalLevel: 'Level 1',
        description: 'Internal Medicine',
        intlRotation: 'No',
      },
      {
        from: new Date('10/30/15'),
        to: new Date('11/27/15'),
        weeks: 4,
        programName: 'AZ - University of Arizona [0017]',
        affiliatedInstitute: '',
        clinicalLevel: 'Level 2',
        description: 'Internal Medicine',
        intlRotation: 'No',
      },
      {
        from: new Date('12/02/15'),
        to: new Date('12/23/15'),
        weeks: 4,
        programName: 'AZ - University of Arizona [0017]',
        affiliatedInstitute: '',
        clinicalLevel: 'Level 3',
        description: 'Internal Medicine',
        intlRotation: 'No',
      },
      {
        from: new Date('12/02/15'),
        to: new Date('12/23/15'),
        weeks: 4,
        programName: 'AZ - University of Arizona [0017]',
        affiliatedInstitute: '',
        clinicalLevel: 'Level 4',
        description: 'Internal Medicine',
        intlRotation: 'No',
      },
      {
        from: new Date('09/29/15'),
        to: new Date('10/29/15'),
        weeks: 4,
        programName: 'AZ - University of Arizona [0017]',
        affiliatedInstitute: '',
        clinicalLevel: 'Level 5',
        description: 'Internal Medicine',
        intlRotation: 'No',
      },
      {
        from: new Date('10/30/15'),
        to: new Date('11/27/15'),
        weeks: 4,
        programName: 'AZ - University of Arizona [0017]',
        affiliatedInstitute: '',
        clinicalLevel: 'Level 6',
        description: 'Internal Medicine',
        intlRotation: 'No',
      },
      {
        from: new Date('12/02/15'),
        to: new Date('12/23/15'),
        weeks: 4,
        programName: 'AZ - University of Arizona [0017]',
        affiliatedInstitute: '',
        clinicalLevel: 'Level 7',
        description: 'Internal Medicine',
        intlRotation: 'No',
      },
      {
        from: new Date('12/02/15'),
        to: new Date('12/23/15'),
        weeks: 4,
        programName: 'AZ - University of Arizona [0017]',
        affiliatedInstitute: '',
        clinicalLevel: 'Level 8',
        description: 'Internal Medicine',
        intlRotation: 'No',
      },
    ];

    this.itemizedGmeData.forEach((item) => {
      filterOptions.push({
        value: item.clinicalLevel.replace(' ', '_'),
        label: item.clinicalLevel,
      });
    });

    this.itemizedGridOptions.filterOptions = filterOptions;
  }

  getClinicalActivity() {
    this.clinicalActivity = [
      {
        id: 'a1',
        start: '2023-06-01',
        end: '2023-06-08', //created by adding 1 to the date of the .to date
        allDay: true,
        class: 'clinical',
        backgroundColor: 'rgba(28, 130, 125, 0.25)',
        highlightColor: 'rgba(28, 130, 125, 1)',
        eventTitle: 'Clinical Rotation',
      },
      {
        id: 'a2',
        start: '2023-05-19',
        end: '2023-05-24',
        allDay: true,
        class: 'clinical',
        backgroundColor: 'rgba(28, 130, 125, 0.25)',
        highlightColor: 'rgba(28, 130, 125, 1)',
        eventTitle: 'Clinical Rotation',
      },
      {
        id: 'a3',
        start: '2023-05-01',
        end: '2023-05-08',
        allDay: true,
        class: 'clinical',
        backgroundColor: 'rgba(28, 130, 125, 0.25)',
        highlightColor: 'rgba(28, 130, 125, 1)',
        eventTitle: 'Clinical Rotation',
      },
    ];
    return {
      events: this.clinicalActivity,
    };
  }

  getNonClinicalActivity() {
    this.nonClinicalActivity = [
      {
        id: 'b1',
        start: '2023-06-09',
        end: '2023-06-17',
        class: 'non-clinical',
        color: 'rgba(219, 173, 106, 0.25)',
        highlightColor: 'rgba(219, 173, 106, 1)',
        eventTitle: 'Non-Clinical Rotation',
      },
      {
        id: 'b2',
        start: '2023-05-09',
        end: '2023-05-17',
        class: 'non-clinical',
        color: 'rgba(219, 173, 106, 0.25)',
        highlightColor: 'rgba(219, 173, 106, 1)',
        eventTitle: 'Non-Clinical Rotation',
      },
    ];
    return {
      events: this.nonClinicalActivity,
    };
  }

  getConflicts() {
    this.conflicts = [
      {
        id: 'c1',
        start: '2023-05-08',
        allDay: true,
        class: 'conflict',
        classNames: ['clickable-event'],
        color: 'rgba(139, 4, 10, 0.25)',
        highlightColor: 'rgba(139, 4, 10, 1)',
        eventTitle: 'Rotation Conflict',
      },
      {
        id: 'c2',
        start: '2023-06-08',
        allDay: true,
        class: 'conflict',
        classNames: ['clickable-event'],
        color: 'rgba(139, 4, 10, 0.25)',
        highlightColor: 'rgba(139, 4, 10, 1)',
        eventTitle: 'Rotation Conflict',
      },
      {
        id: 'c3',
        start: '2023-05-17',
        end: '2023-05-19',
        class: 'conflict',
        classNames: ['clickable-event'],
        color: 'rgba(139, 4, 10, 0.25)',
        highlightColor: 'rgba(139, 4, 10, 1)',
        eventTitle: 'Rotation Conflict',
      },
    ];
    return {
      events: this.conflicts,
    };
  }

  handleAddEditGmeRotation(isEdit = false) {
    if (!isEdit) {
      this.isEditGmeRotation = false;
    }
    this.showAddEditGmeRotation = !this.showAddEditGmeRotation;
  }

  handleGridAction($event: any) {
    console.log($event);
    if ($event.fieldKey === 'edit') {
      this.isEditGmeRotation = true;
      this.handleAddEditGmeRotation(true);
    }
  }

  viewConflictsToResolve(conflictList: any[]) {
    this.toggleConflictResolutionModal();
  }
  toggleConflictResolutionModal() {
    this.showConflictResolutionModal = !this.showConflictResolutionModal;
  }
}
