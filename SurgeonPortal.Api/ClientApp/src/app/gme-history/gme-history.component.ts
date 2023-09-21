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
  DeleteGraduateMedicalEducation,
  GetGraduateMedicalEducationDetails,
  ClearGraduateMedicalEducationDetails,
  IGraduateMedicalEducation,
  GetAllGraduateMedicalEducation,
  ClearGraduateMedicalEducationErrors,
  UpdateGraduateMedicalEducation,
  CreateGraduateMedicalEducation,
  GetDashboardProgramInformation,
  DashboardSelectors,
} from '../state';
import { Select, Store } from '@ngxs/store';
import {
  IRotationReadOnlyModel,
  IGmeSummaryReadOnlyModel,
  IRotationGapReadOnlyModel,
  IRotationModel,
} from 'src/app/api';
import { ButtonModule } from 'primeng/button';
import { GmeFormComponent } from './gme-form/gme-form.component';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { GetPicklists, PicklistsSelectors } from '../state/picklists';

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

interface IGmePicklistOptions {
  clinicalLevelOptions: any[] | undefined;
  clinicalActivityOptions: any[] | undefined;
}

@UntilDestroy()
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
    ButtonModule,
    GmeFormComponent,
  ],
})
export class GmeHistoryComponent implements OnInit, OnDestroy {
  @ViewChild('calendar') calendarComponent!: FullCalendarComponent;

  clearErrors = new ClearGraduateMedicalEducationErrors();

  @Select(GraduateMedicalEducationSelectors.graduateMedicalEducationList)
  gmeRotations$: Observable<IRotationReadOnlyModel[]> | undefined;

  @Select(GraduateMedicalEducationSelectors.graduateMedicalEducationAll)
  gmeAll$: Observable<IGraduateMedicalEducation> | undefined;

  @Select(GraduateMedicalEducationSelectors.graduateMedicalEducationSummary)
  gmeSummary$: Observable<IGmeSummaryReadOnlyModel[]> | undefined;

  @Select(GraduateMedicalEducationSelectors.graduateMedicalEducationDetails)
  selectedRotation$: Observable<IRotationModel> | undefined;

  @Select(GraduateMedicalEducationSelectors.errors)
  gmeErrors$: Observable<any> | undefined;

  gmeRotationsSubscription: Subscription | undefined;
  createGmeRotationSubscription: Subscription | undefined;
  updateGmeRotationSubscription: Subscription | undefined;
  gmeAllSubscription: Subscription | undefined;

  conflictingRecords: IRotationReadOnlyModel[] = [];
  gapData: IRotationGapReadOnlyModel | undefined;
  gapConflictDates: any;

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
        }`;

        innerContent += `<br>${info.event.extendedProps['eventTitle']}</div>
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
        const conflict = info.event.extendedProps['rawData'];
        const allRecords = [
          ...this.clinicalActivity,
          ...this.nonClinicalActivity,
        ];
        const conflictingRecords = {
          nextRotation: undefined,
          previousRotation: undefined,
        };
        if (conflict.nextRotationId) {
          conflictingRecords.nextRotation = allRecords.find(
            (x) => x.id === conflict.nextRotationId
          );
        }
        if (conflict.previousRotationId) {
          conflictingRecords.previousRotation = allRecords.find(
            (x) => x.id === conflict.previousRotationId
          );
        }
        this.viewConflictsToResolve(conflictingRecords, conflict);
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
  itemizedGmeCols = ITEMIZED_GME_COLS;
  itemizedGmeData!: IRotationReadOnlyModel[];

  gmeSummaryCols = GME_SUMMARY_COLS;
  gmeSummaryData!: any[];

  selectedGmeRotation: IRotationReadOnlyModel | undefined;

  showAddEditGmeRotation = false;
  isEditGmeRotation$ = new BehaviorSubject(false);

  minStartDate: Date | undefined;
  maxEndDate: Date | undefined;

  gmePicklistOptions: IGmePicklistOptions | undefined = {
    clinicalLevelOptions: [],
    clinicalActivityOptions: [],
  };

  userProgram: {
    programName?: string;
    clinicalLevel?: string;
    clinicalLevelId?: number;
  } = {};

  constructor(
    private _store: Store,
    private globalDialogService: GlobalDialogService
  ) {
    this.initRotationsData();
    this.initPicklistOptions();

    this.selectedRotation$?.pipe(untilDestroyed(this)).subscribe((rotation) => {
      this.selectedGmeRotation = undefined;
      const selectedRotation = {
        ...rotation,
        usingAffiliateOrganization: rotation?.alternateInstitutionName
          ? true
          : false,
        isClinicalActivity:
          !rotation?.clinicalActivity.includes('Non-Clinical'),
      };
      if (!rotation) {
        selectedRotation.startDate = this.maxEndDate?.toISOString() ?? '';
      }
      this.selectedGmeRotation = selectedRotation;
    });
  }

  initUserData() {
    this._store
      .dispatch(new GetDashboardProgramInformation())
      .pipe(untilDestroyed(this))
      .subscribe(() => {
        const userInfo = this._store.selectSnapshot(
          DashboardSelectors.dashboardProgramInformation
        );
        if (userInfo && userInfo.programs) {
          if (userInfo.programs.programName !== '') {
            this.userProgram.programName = userInfo.programs.programName;
          }
          if (userInfo.programs.clinicalLevel !== '') {
            switch (userInfo.programs.clinicalLevel) {
              case 'PGY1':
                this.userProgram.clinicalLevel = 'Clinical Level 1';
                break;
              case 'PGY2':
                this.userProgram.clinicalLevel = 'Clinical Level 2';
                break;
              case 'PGY3':
                this.userProgram.clinicalLevel = 'Clinical Level 3';
                break;
              case 'PGY4':
                this.userProgram.clinicalLevel = 'Clinical Level 4';
                break;
              case 'PGY5':
                this.userProgram.clinicalLevel = 'Clinical Level 5';
                break;
              case 'Research':
                this.userProgram.clinicalLevel = 'Research';
                break;
              case 'Other':
                this.userProgram.clinicalLevel = 'Other Clinical Fellowship';
                break;
              default:
                this.userProgram.clinicalLevel =
                  userInfo.programs.clinicalLevel;
                break;
            }
          }
        }
        if (this.userProgram.clinicalLevel) {
          const clinicalLevel =
            this.gmePicklistOptions?.clinicalLevelOptions?.find((level) => {
              return level.label === this.userProgram.clinicalLevel;
            });
          this.userProgram.clinicalLevelId = clinicalLevel?.value;
        }
      });
  }

  initPicklistOptions() {
    this.gmePicklistOptions = undefined;
    const gmePicklistOptions: IGmePicklistOptions = {
      clinicalLevelOptions: [],
      clinicalActivityOptions: [],
    };
    this._store
      .dispatch(new GetPicklists())
      .pipe(untilDestroyed(this))
      .subscribe(() => {
        gmePicklistOptions.clinicalLevelOptions = this._store.selectSnapshot(
          PicklistsSelectors.slices.clinicalLevels
        );
        if (gmePicklistOptions.clinicalLevelOptions) {
          gmePicklistOptions.clinicalLevelOptions =
            gmePicklistOptions.clinicalLevelOptions.map((level) => {
              return {
                label: level.name,
                value: level.id,
              };
            });
        }
        gmePicklistOptions.clinicalActivityOptions = this._store.selectSnapshot(
          PicklistsSelectors.slices.clinicalActivities
        );

        this.gmePicklistOptions = gmePicklistOptions;

        this.initUserData();
      });
  }

  initRotationsData() {
    this._store.dispatch(new GetAllGraduateMedicalEducation());

    this.gmeAllSubscription = this.gmeAll$?.subscribe((gmeAll) => {
      this.clinicalActivity = [];
      this.nonClinicalActivity = [];
      this.conflicts = [];

      if (gmeAll && (gmeAll.gmeRotations || gmeAll.gmeGaps)) {
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
              {
                label: 'Conflicts',
                value: { value: 'conflict', field: 'type' },
              },
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
        this.maxEndDate = undefined;
        this.minStartDate = undefined;
        gmeAll.gmeGaps.forEach((item, index) => {
          // build calendar items
          if (item.startDate === item.endDate) {
            // single day event
          }

          const endDate = new Date(item.endDate);
          endDate.setDate(endDate.getDate() + 1);
          const conflictItem: any = {
            id: 'conflict-' + index,
            start: item.startDate,
            class: 'conflict',
            classNames: ['clickable-event'],
            color: 'rgba(139, 4, 10, 0.25)',
            highlightColor: 'rgba(139, 4, 10, 1)',
            type: 'conflict',
            eventTitle: 'Rotation Conflict',
            allDay: true,
            rawData: item,
          };

          if (item.startDate !== item.endDate) {
            conflictItem.end = endDate;
          }
          this.conflicts.push(conflictItem);
        });
        gmeAll.gmeRotations.forEach((item) => {
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
          this.maxEndDate = new Date(
            this.maxEndDate.setDate(this.maxEndDate.getDate() + 1)
          );

          // build filter options for grid
          if (
            !clinicalFilterOptions.some(
              (x) => x.value === item.clinicalLevel?.replaceAll(' ', '_').trim()
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
            eventTitle: item.clinicalActivity,
            programName: item.programName,
            type: '',
            year: yearFilter.replaceAll(' ', '_').trim(),
            clinicalLevel: item.clinicalLevel?.replaceAll(' ', '_').trim(),
            allDay: true,
            rawData: item,
          };

          //if (item.isCredit) {
          if (!item.clinicalActivity.includes('Non-Clinical')) {
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
      }

      this.applyCalendarFilters();
    });
  }

  ngOnInit(): void {
    this.calendarFilter = undefined;
    setTimeout(() => {
      this.calendarOptions.eventSources = [
        this.getClinicalActivity(),
        this.getNonClinicalActivity(),
        this.getConflicts(),
      ];
      this.calendarReady = true;
    }, 0);
  }
  ngOnDestroy(): void {
    this.gmeRotationsSubscription?.unsubscribe();
    this.gmeAllSubscription?.unsubscribe();
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
      this.getConflicts(filters),
    ];
  }

  relaunchAddEditGmeRotation($event: any) {
    this.showConflictResolutionModal = false;
    if (this.showAddEditGmeRotation) {
      this.showAddEditGmeRotation = !this.showAddEditGmeRotation;
    }
    if ($event) {
      this.isEditGmeRotation$.next(true);
      this._store.dispatch(new GetGraduateMedicalEducationDetails($event));
      this.handleAddEditGmeRotation(true);
    }
  }

  handleAddEditGmeRotation(isEdit = false) {
    if (!isEdit) {
      this.isEditGmeRotation$.next(false);
      this._store.dispatch(new ClearGraduateMedicalEducationDetails());
      this.selectedGmeRotation = {
        programName: this.userProgram.programName ?? undefined,
        clinicalLevelId: this.userProgram.clinicalLevelId ?? undefined,
        startDate: this.maxEndDate?.toISOString() ?? '',
        isClinicalActivity: true,
        usingAffiliateOrganization: false,
      } as unknown as IRotationReadOnlyModel;
    }

    this.showAddEditGmeRotation = !this.showAddEditGmeRotation;
  }

  handleAddGmeGapRotation($event: any) {
    if ($event) {
      this.showConflictResolutionModal = false;
      this.isEditGmeRotation$.next(false);
      this.gapConflictDates = $event;
      this._store.dispatch(new ClearGraduateMedicalEducationDetails());
      this.selectedGmeRotation = {
        programName: this.userProgram.programName ?? undefined,
        clinicalLevelId: this.userProgram.clinicalLevelId ?? undefined,
        startDate: $event.startDate ?? '',
        endDate: $event.endDate ?? '',
        isClinicalActivity: true,
        usingAffiliateOrganization: false,
      } as unknown as IRotationReadOnlyModel;
      this.showAddEditGmeRotation = !this.showAddEditGmeRotation;
    }
  }

  handleGridAction($event: any) {
    const { data } = $event;
    if ($event.fieldKey === 'edit') {
      this.isEditGmeRotation$.next(true);
      this._store.dispatch(new GetGraduateMedicalEducationDetails(data.id));
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

  saveGmeRotation($event: any) {
    if ($event.data) {
      const formValues = $event.data;
      let startDate = '';
      let endDate = '';
      if (formValues.startDate) {
        startDate = new Date(formValues.startDate).toISOString();
      }
      if (formValues.endDate) {
        endDate = new Date(formValues.endDate).toISOString();
      }

      const newRotation = {
        id: this.selectedGmeRotation?.id ?? 0,
        startDate: startDate,
        endDate: endDate,
        clinicalLevelId: formValues.clinicalLevelId ?? 0,
        clinicalActivityId: formValues.clinicalActivityId ?? 0,
        programName: formValues.programName ?? '',
        nonSurgicalActivity: formValues.nonSurgicalActivity ?? '',
        alternateInstitutionName: formValues.alternateInstitutionName ?? '',
        isInternationalRotation: formValues.isInternationalRotation ?? false,
        other: formValues.other ?? '',
        fourMonthRotationExplain: formValues.fourMonthRotationExplain ?? '',
        nonPrimaryExplain: formValues.nonPrimaryExplain ?? '',
        nonClinicalExplain: formValues.nonClinicalExplain ?? '',
        isEssential: formValues.isEssential ?? false,
      } as unknown as IRotationModel;

      if ($event.isEdit) {
        this.updateGmeRotationSubscription = this._store
          .dispatch(new UpdateGraduateMedicalEducation(newRotation))
          .subscribe((res) => {
            if (!res.graduateMedicalEducation?.errors) {
              this.handleAddEditGmeRotation();
              this.updateGmeRotationSubscription?.unsubscribe();
            }
          });
      } else {
        this.createGmeRotationSubscription = this._store
          .dispatch(new CreateGraduateMedicalEducation(newRotation))
          .subscribe((res) => {
            if (!res.graduateMedicalEducation?.errors) {
              this.handleAddEditGmeRotation();
              this.updateGmeRotationSubscription?.unsubscribe();
            }
          });
      }
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

  viewConflictsToResolve(conflicts: any, gapData: IRotationGapReadOnlyModel) {
    this.conflictingRecords = [];
    this.gapData = undefined;
    const conflictRecords = [];
    if (conflicts.previousRotation) {
      conflictRecords.push(conflicts.previousRotation.rawData);
    }
    if (conflicts.nextRotation) {
      conflictRecords.push(conflicts.nextRotation.rawData);
    }

    this.conflictingRecords = conflictRecords;
    this.gapData = gapData;
    this.toggleConflictResolutionModal();
  }
  toggleConflictResolutionModal() {
    this.showConflictResolutionModal = !this.showConflictResolutionModal;
  }
}
