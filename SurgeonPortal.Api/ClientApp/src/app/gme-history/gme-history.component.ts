import {
  Component,
  CUSTOM_ELEMENTS_SCHEMA,
  OnDestroy,
  OnInit,
} from '@angular/core';
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

import { GlobalDialogService } from '../shared/services/global-dialog.service';
import { CollapsePanelComponent } from '../shared/components/collapse-panel/collapse-panel.component';
import { InputSelectComponent } from '../shared/components/base-input/input-select.component';
import { AlertComponent } from '../shared/components/alert/alert.component';
import { ITEMIZED_GME_COLS } from './itemized-gme-cols';
import { GME_SUMMARY_COLS } from './gme-summary-cols';
import {
  BehaviorSubject,
  filter,
  Observable,
  Subject,
  Subscription,
} from 'rxjs';
import { GridComponent } from '../shared/components/grid/grid.component';
import { ModalComponent } from '../shared/components/modal/modal.component';
import { ConflictResolutionModalComponent } from './conflict-resolution-modal/conflict-resolution-modal.component';
import { IGridOptions } from '../shared/components/grid/grid-options.model';
import { AbsFilterType } from '../shared/components/grid/abs-grid.enum';
import { AddRecordModalComponent } from './add-record-modal/add-record-modal.component';

import {
  GraduateMedicalEducationSelectors,
  GetGraduateMedicalEducationList,
  GetGraduateMedicalEducationDetails,
  UpdateGraduateMedicalEducation,
  CreateGraduateMedicalEducation,
  DeleteGraduateMedicalEducation,
} from '../state';
import { GraduateMedicalEducationState } from '../state';
import { Select, Store } from '@ngxs/store';
import { IRotationModel, IRotationReadOnlyModel } from 'src/app/api';

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
export class GmeHistoryComponent implements OnInit, OnDestroy {
  @Select(GraduateMedicalEducationSelectors.graduateMedicalEducationList)
  gmeRotations$: Observable<IRotationReadOnlyModel[]> | undefined;

  gmeRotationsSubscription: Subscription | undefined;

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
  itemizedGme$: BehaviorSubject<boolean> = new BehaviorSubject(true);
  itemizedGmeCols = ITEMIZED_GME_COLS;
  itemizedGmeData!: any[];

  gmeSummaryCols = GME_SUMMARY_COLS;
  gmeSummaryData!: any[];

  showAddEditGmeRotation = false;
  isEditGmeRotation$ = new BehaviorSubject(false);
  selectedGmeRotationId$ = new BehaviorSubject<number | undefined>(undefined);

  constructor(
    private _store: Store,
    private globalDialogService: GlobalDialogService
  ) {
    this.initRotationsData();
  }

  initRotationsData() {
    this._store.dispatch(new GetGraduateMedicalEducationList());

    this.gmeRotationsSubscription = this.gmeRotations$?.subscribe(
      (gmeRotations) => {
        if (gmeRotations) {
          // set filter options for grid
          const filterOptions: { value: any; label: any }[] | undefined = [];
          gmeRotations.forEach((item) => {
            if (
              !filterOptions.some(
                (x) => x.value === item.clinicalLevel?.replace(' ', '_').trim()
              )
            ) {
              filterOptions.push({
                value: item.clinicalLevel?.replace(' ', '_').trim(),
                label: item.clinicalLevel,
              });
            }
          });
          // need to remove dups here
          this.itemizedGridOptions.filterOptions = filterOptions;

          // get the value from the observable to inject into the calendar
          this.itemizedGme$.next(!this.itemizedGme$.getValue());
        }
      }
    );
  }

  ngOnInit(): void {
    setTimeout(() => {
      this.calendarOptions.eventSources = [
        this.getClinicalActivity(),
        this.getNonClinicalActivity(),
        this.getConflicts(),
      ];
      this.calendarReady = true;
    }, 0);

    this.getGMESummaryData();
  }
  ngOnDestroy(): void {
    this.gmeRotationsSubscription?.unsubscribe();
  }

  getGMESummaryData() {
    const gmeSummaryData = [
      {
        residencyYear: '1',
        clinicalRotation: 12,
        clincalYears1To3: 11,
        clincalYears4To6: 1,
        essentialContentArea: 6,
        primaryChief: 1,
        nonClinicalActivity: 0,
      },
      {
        residencyYear: '2',
        clinicalRotation: 5,
        clincalYears1To3: 7,
        clincalYears4To6: 3,
        essentialContentArea: 0,
        primaryChief: 0,
        nonClinicalActivity: 12,
      },
      {
        residencyYear: '3',
        clinicalRotation: 4,
        clincalYears1To3: 12,
        clincalYears4To6: 8,
        essentialContentArea: 3,
        primaryChief: 4,
        nonClinicalActivity: 10,
      },
      {
        residencyYear: '4',
        clinicalRotation: 6,
        clincalYears1To3: 7,
        clincalYears4To6: 3,
        essentialContentArea: 10,
        primaryChief: 12,
        nonClinicalActivity: 4,
      },
      {
        residencyYear: '5',
        clinicalRotation: 6,
        clincalYears1To3: 7,
        clincalYears4To6: 3,
        essentialContentArea: 10,
        primaryChief: 12,
        nonClinicalActivity: 4,
      },
      {
        residencyYear: '6',
        clinicalRotation: 6,
        clincalYears1To3: 7,
        clincalYears4To6: 3,
        essentialContentArea: 10,
        primaryChief: 12,
        nonClinicalActivity: 4,
      },
    ];
    const gmeSummaryTotals = {
      residencyYear: 'Total Weeks',
      clinicalRotation: this.getTotals(gmeSummaryData, 'clinicalRotation'),
      clincalYears1To3: this.getTotals(gmeSummaryData, 'clincalYears1To3'),
      clincalYears4To6: this.getTotals(gmeSummaryData, 'clincalYears4To6'),
      essentialContentArea: this.getTotals(
        gmeSummaryData,
        'essentialContentArea'
      ),
      primaryChief: this.getTotals(gmeSummaryData, 'primaryChief'),
      nonClinicalActivity: this.getTotals(
        gmeSummaryData,
        'nonClinicalActivity'
      ),
      rowStyle: {
        'font-weight': 'bold',
        'background-color': '#1F3758',
        color: '#FFF',
      },
    };
    const gmeSummaryAverages = {
      residencyYear: 'Avg Weeks',
      clinicalRotation: this.getAverages(gmeSummaryData, 'clinicalRotation'),
      clincalYears1To3: this.getAverages(gmeSummaryData, 'clincalYears1To3'),
      clincalYears4To6: this.getAverages(gmeSummaryData, 'clincalYears4To6'),
      essentialContentArea: this.getAverages(
        gmeSummaryData,
        'essentialContentArea'
      ),
      primaryChief: this.getAverages(gmeSummaryData, 'primaryChief'),
      nonClinicalActivity: this.getAverages(
        gmeSummaryData,
        'nonClinicalActivity'
      ),
      rowStyle: {
        'font-weight': 'bold',
        'background-color': '#1F3758',
        color: '#FFF',
      },
    };

    this.gmeSummaryData = [
      ...gmeSummaryData,
      gmeSummaryTotals,
      gmeSummaryAverages,
    ];
  }

  getTotals(items: any[], prop: string) {
    return items.reduce((a, b) => a + b[prop], 0);
  }
  getAverages(items: any[], prop: string) {
    const avg = items.reduce((a, b) => a + b[prop], 0) / items.length;
    return Math.round(avg * 10) / 10;
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
      this.isEditGmeRotation$.next(false);
    }

    this.showAddEditGmeRotation = !this.showAddEditGmeRotation;
    this.itemizedGme$.next(!this.itemizedGme$.getValue());
  }

  handleGridAction($event: any) {
    const { data } = $event;
    if ($event.fieldKey === 'edit') {
      this.isEditGmeRotation$.next(true);
      this.selectedGmeRotationId$.next(data.id);
      this.handleAddEditGmeRotation(true);
    } else if ($event.fieldKey === 'delete') {
      this.globalDialogService
        .showConfirmation(
          'Confirm Delete',
          'Are you sure you want to delete this record?'
        )
        .then((result) => {
          if (result) {
            this.deleteGmeRotation(data.id);
          }
        });
    }
  }

  deleteGmeRotation(id: number) {
    this._store.dispatch(new DeleteGraduateMedicalEducation(id));
  }

  viewConflictsToResolve(conflictList: any[]) {
    this.toggleConflictResolutionModal();
  }
  toggleConflictResolutionModal() {
    this.showConflictResolutionModal = !this.showConflictResolutionModal;
  }
}
