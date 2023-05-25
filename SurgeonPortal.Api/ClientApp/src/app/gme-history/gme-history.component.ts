import {
  Component,
  CUSTOM_ELEMENTS_SCHEMA,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { FullCalendarModule } from '@fullcalendar/angular';
import { CalendarOptions } from '@fullcalendar/core';
import multiMonthPlugin from '@fullcalendar/multimonth';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { GlobalDialogService } from '../shared/services/global-dialog.service';
import { CollapsePanelComponent } from '../shared/components/collapse-panel/collapse-panel.component';
import { InputSelectComponent } from '../shared/components/base-input/input-select.component';
import { AlertComponent } from '../shared/components/alert/alert.component';
import { ITEMIZED_GME_COLS } from './itemized-gme-cols';
import { GME_SUMMARY_COLS } from './gme-summary-cols';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import { GridComponent } from '../shared/components/grid/grid.component';
import { ModalComponent } from '../shared/components/modal/modal.component';
import { ConflictResolutionModalComponent } from './conflict-resolution-modal/conflict-resolution-modal.component';
import { IGridOptions } from '../shared/components/grid/grid-options.model';
import { AbsFilterType } from '../shared/components/grid/abs-grid.enum';
import { AddRecordModalComponent } from './add-record-modal/add-record-modal.component';
import { DropdownModule } from 'primeng/dropdown';

import { FullCalendarComponent } from '@fullcalendar/angular';

import {
  GraduateMedicalEducationSelectors,
  GetGraduateMedicalEducationList,
  DeleteGraduateMedicalEducation,
} from '../state';
import { Select, Store } from '@ngxs/store';
import { IRotationReadOnlyModel, IGmeSummaryReadOnlyModel } from 'src/app/api';

export interface ICalendarFilterValue {
  value: string;
  field: string;
}
export interface ICalendarFilter {
  label: string;
  value: ICalendarFilterValue;
}
export interface ICalendarFilterOptions {
  label: string;
  items: ICalendarFilter[];
}

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
    DropdownModule,
  ],
})
export class GmeHistoryComponent implements OnInit, OnDestroy {
  @ViewChild('calendar') calendarComponent!: FullCalendarComponent;

  @Select(GraduateMedicalEducationSelectors.graduateMedicalEducationList)
  gmeRotations$: Observable<IRotationReadOnlyModel[]> | undefined;

  @Select(GraduateMedicalEducationSelectors.graduateMedicalEducationSummary)
  gmeSummary$: Observable<IGmeSummaryReadOnlyModel[]> | undefined;

  gmeRotationsSubscription: Subscription | undefined;
  gmeSummarySubscription: Subscription | undefined;

  calendarReady = false;
  calendarFilterOptions: ICalendarFilterOptions[] = [];
  calendarFilter: ICalendarFilterValue | undefined;

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
          <br>${info.event.extendedProps['programName']}
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
  itemizedGmeData!: IRotationReadOnlyModel[];

  gmeSummaryCols = GME_SUMMARY_COLS;
  summaryGme$: BehaviorSubject<boolean> = new BehaviorSubject(true);
  gmeSummaryData!: any[];

  showAddEditGmeRotation = false;
  isEditGmeRotation$ = new BehaviorSubject(false);
  selectedGmeRotationId$ = new BehaviorSubject<
    { id?: number; nextStart: string } | undefined
  >(undefined);

  minStartDate: Date | undefined;
  maxEndDate: Date | undefined;

  constructor(
    private _store: Store,
    private globalDialogService: GlobalDialogService
  ) {
    this.initRotationsData();
    this.initSummaryData();
  }

  initRotationsData() {
    this._store.dispatch(new GetGraduateMedicalEducationList());

    this.gmeRotationsSubscription = this.gmeRotations$?.subscribe(
      (gmeRotations) => {
        this.clinicalActivity = [];
        this.nonClinicalActivity = [];
        this.conflicts = [];

        if (gmeRotations) {
          this.calendarFilterOptions = [
            {
              label: 'Activity Types',
              items: [
                {
                  label: 'Clinical',
                  value: { value: 'clinical', field: 'type' },
                },
                {
                  label: 'Non-Clinical',
                  value: { value: 'non-clinical', field: 'type' },
                },
                //{ label: 'Conflicts', value: { value: 'conflict', field: 'type' }, },
              ],
            },
            {
              label: 'Clinical Levels',
              items: [],
            },
          ];

          // set filter options for grid
          const clinicalFilterOptions: { value: string; label: string }[] = [];
          const yearFilterOptions: ICalendarFilter[] = [];
          gmeRotations.forEach((item) => {
            //get min start date
            if (this.minStartDate) {
              if (new Date(item.startDate) < new Date(this.minStartDate)) {
                this.minStartDate = new Date(item.startDate);
              }
            } else {
              this.minStartDate = new Date(item.startDate);
            }

            //get max end date
            if (this.maxEndDate) {
              if (new Date(item.endDate) > new Date(this.maxEndDate)) {
                this.maxEndDate = new Date(item.endDate);
              }
            } else {
              this.maxEndDate = new Date(item.endDate);
            }

            // build filter options for grid
            if (
              !clinicalFilterOptions.some(
                (x) =>
                  x.value === item.clinicalLevel?.replaceAll(' ', '_').trim()
              )
            ) {
              clinicalFilterOptions.push({
                value: item.clinicalLevel?.replaceAll(' ', '_').trim(),
                label: item.clinicalLevel,
              });
              this.calendarFilterOptions[1].items.push({
                label: item.clinicalLevel,
                value: {
                  value: item.clinicalLevel?.replaceAll(' ', '_').trim(),
                  field: 'clinicalLevel',
                },
              });
            }

            const itemMonth = new Date(item.startDate).getMonth();
            const itemYear = new Date(item.startDate).getFullYear().toString();
            let yearFilter = '';
            // hardcoded to 5 for June
            if (itemMonth >= 5) {
              yearFilter = itemYear.concat(
                ' - ',
                (parseInt(itemYear) + 1).toString()
              );
            } else {
              yearFilter = (parseInt(itemYear) - 1)
                .toString()
                .concat(' - ', itemYear);
            }
            if (!yearFilterOptions.some((x) => x.label === yearFilter)) {
              yearFilterOptions.push({
                label: yearFilter,
                value: {
                  value: yearFilter?.replaceAll(' ', '_').trim(),
                  field: 'year',
                },
              });
            }

            // build calendar items
            const endDate = new Date(item.endDate);
            endDate.setDate(endDate.getDate() + 1);
            const calendarItem = {
              id: item.id,
              start: item.startDate,
              end: endDate,
              class: '',
              color: '',
              highlightColor: '',
              eventTitle: item.clinicalLevel,
              programName: item.programName,
              type: '',
              year: yearFilter.replaceAll(' ', '_').trim(),
              clinicalLevel: item.clinicalLevel?.replaceAll(' ', '_').trim(),
              allDay: true,
              rawData: item,
            };

            if (item.isCredit) {
              // clinical activity
              calendarItem.class = 'clinical';
              calendarItem.color = 'rgba(28, 130, 125, 0.25)';
              calendarItem.highlightColor = 'rgba(28, 130, 125, 1)';
              calendarItem.type = 'clinical';
              this.clinicalActivity.push(calendarItem);
            } else {
              // non clinical activity
              calendarItem.class = 'non-clinical';
              calendarItem.color = 'rgba(219, 173, 106, 0.25)';
              calendarItem.highlightColor = 'rgba(219, 173, 106, 1)';
              calendarItem.type = 'non-clinical';
              this.nonClinicalActivity.push(calendarItem);
            }
          });

          clinicalFilterOptions.sort((a, b) => {
            return a.label > b.label ? 1 : -1;
          });
          this.calendarFilterOptions.push({
            label: 'Years',
            items: yearFilterOptions,
          });
          this.calendarFilterOptions.forEach((filterOption) => {
            if (filterOption.label !== 'Activity Types') {
              filterOption.items.sort(
                (a: ICalendarFilter, b: ICalendarFilter) => {
                  return a.label > b.label ? 1 : -1;
                }
              );
            }
          });

          this.itemizedGridOptions.filterOptions = clinicalFilterOptions;

          this.itemizedGme$.next(!this.itemizedGme$.getValue());
        }

        this.applyCalendarFilters();
      }
    );
  }

  initSummaryData() {
    this.gmeSummarySubscription = this.gmeSummary$?.subscribe((gmeSummary) => {
      console.log('gmeSummary', gmeSummary);
      if (gmeSummary) {
        this.summaryGme$.next(!this.summaryGme$.getValue());
      }
    });
  }

  ngOnInit(): void {
    this.calendarFilter = undefined;
    setTimeout(() => {
      this.calendarOptions.eventSources = [
        this.getClinicalActivity(),
        this.getNonClinicalActivity(),
        //this.getConflicts(),
      ];
      this.calendarReady = true;
    }, 0);
  }
  ngOnDestroy(): void {
    this.gmeRotationsSubscription?.unsubscribe();
    this.gmeSummarySubscription?.unsubscribe();
  }

  getClinicalActivity(filters?: ICalendarFilterValue) {
    return {
      events: this.clinicalActivity.filter((event) => {
        if (filters) {
          return filters.value === event[filters.field];
        } else {
          return true;
        }
      }),
    };
  }
  getNonClinicalActivity(filters?: ICalendarFilterValue) {
    return {
      events: this.nonClinicalActivity.filter((event) => {
        if (filters) {
          return filters.value === event[filters.field];
        } else {
          return true;
        }
      }),
    };
  }
  getConflicts(filters?: ICalendarFilterValue) {
    // class: 'conflict',
    // classNames: ['clickable-event'],
    // color: 'rgba(139, 4, 10, 0.25)',
    // highlightColor: 'rgba(139, 4, 10, 1)',
    // eventTitle: 'Rotation Conflict',
    // type: 'conflict',
    return {
      events: this.conflicts.filter((event) => {
        if (filters) {
          return filters.value === event[filters.field];
        } else {
          return true;
        }
      }),
    };
  }
  getEventSources(filters?: ICalendarFilterValue) {
    return [
      this.getClinicalActivity(filters),
      this.getNonClinicalActivity(filters),
      //this.getConflicts(filters),
    ];
  }

  handleAddEditGmeRotation(isEdit = false) {
    if (!isEdit) {
      this.isEditGmeRotation$.next(false);
      this.selectedGmeRotationId$.next({
        nextStart: this.maxEndDate?.toISOString() ?? '',
      });
    }

    this.showAddEditGmeRotation = !this.showAddEditGmeRotation;
    this.itemizedGme$.next(!this.itemizedGme$.getValue());
  }

  handleGridAction($event: any) {
    const { data } = $event;
    if ($event.fieldKey === 'edit') {
      this.isEditGmeRotation$.next(true);
      this.selectedGmeRotationId$.next({
        id: data.id,
        nextStart: this.maxEndDate?.toISOString() ?? '',
      });
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

  handleCalendarFilterChange($event: any) {
    if ($event.value) {
      this.calendarFilter = $event.value;
    } else {
      this.calendarFilter = undefined;
    }

    this.applyCalendarFilters();
  }

  applyCalendarFilters() {
    if (this.calendarComponent) {
      const calendarApi = this.calendarComponent.getApi();
      // clear calendar of events
      calendarApi.getEventSources().forEach((eventSource) => {
        eventSource.remove();
      });
      // refetch all calendar events from sources using new filter value
      const sources = this.getEventSources(this.calendarFilter);
      sources.forEach((source) => {
        calendarApi.addEventSource(source);
      });

      let firstDate: Date | string | null = null;
      calendarApi.getEvents().forEach((event) => {
        if (!firstDate) {
          firstDate = event.start;
        } else {
          if (event.start && event.start < firstDate) {
            firstDate = event.start;
          }
        }
      });
      if (firstDate && this.calendarFilter) {
        calendarApi.gotoDate(firstDate);
      } else {
        calendarApi.today();
      }
    }
  }

  viewConflictsToResolve(conflictList: any[]) {
    console.log(conflictList);
    this.toggleConflictResolutionModal();
  }
  toggleConflictResolutionModal() {
    this.showConflictResolutionModal = !this.showConflictResolutionModal;
  }
}
