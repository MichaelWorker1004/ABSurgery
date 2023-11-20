import { ComponentFixture, TestBed, fakeAsync } from '@angular/core/testing';

import { GmeHistoryComponent } from './gme-history.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NgxsModule, Store } from '@ngxs/store';
import { surgeonPortalState } from '../state/surgeon-portal.state';
import { GlobalDialogService } from '../shared/services/global-dialog.service';

describe('GmeHistoryComponent', () => {
  let component: GmeHistoryComponent;
  let fixture: ComponentFixture<GmeHistoryComponent>;
  let store: Store;

  const mockState = {};

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        NgxsModule.forRoot(surgeonPortalState),
        GmeHistoryComponent,
      ],
    }).compileComponents();

    store = TestBed.inject(Store);
    store.reset({
      ...store.snapshot(),
      ...mockState,
    });
    fixture = TestBed.createComponent(GmeHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize component properties correctly', () => {
    expect(component.clearErrors).toBeDefined();
    expect(component.gmeRotations$).toBeDefined();
    expect(component.gmeAll$).toBeDefined();
    expect(component.gmeSummary$).toBeDefined();
    expect(component.selectedRotation$).toBeDefined();
    // Add more property initialization tests here
  });

  // the unit tests don't see the calendar component initialized on the page
  // it('should call getEventSources with filters', () => {
  //   spyOn(component, 'getEventSources');
  //   component.applyCalendarFilters();
  //   expect(component.getEventSources).toHaveBeenCalledWith(
  //     component.calendarFilter
  //   );
  // });

  // it('should add clinical activity events to the calendar', () => {
  //   const mockClinicalActivity = [
  //     { id: 1, start: '2023-01-01', end: '2023-01-02', type: 'clinical' },
  //   ];
  //   spyOn(component, 'getClinicalActivity').and.returnValue({
  //     events: mockClinicalActivity,
  //   });
  //   component.applyCalendarFilters();

  //   const calendarApi = component.calendarComponent.getApi();
  //   const events = calendarApi.getEvents();
  //   expect(events.length).toBe(1);
  //   expect(events[0].id).toBe('1');
  // });

  it('should handle add or edit GME rotation', () => {
    spyOn(component, 'handleAddEditGmeRotation');
    component.relaunchAddEditGmeRotation(true);
    expect(component.handleAddEditGmeRotation).toHaveBeenCalledWith(true);
  });

  it('should handle delete GME rotation', () => {
    const globalDialogService = TestBed.inject(GlobalDialogService);
    const globalDialogServiceSpy = spyOn(
      globalDialogService,
      'showConfirmation'
    ).and.returnValue(Promise.resolve(true));
    spyOn(store, 'dispatch');
    component.handleGridAction({ fieldKey: 'delete', data: { id: 1 } });

    expect(globalDialogServiceSpy).toHaveBeenCalled();
  });

  it('should handle saving a GME rotation', () => {
    const newRotation = {
      id: 1,
      startDate: '2023-01-01',
      endDate: '2023-01-02',
      clinicalLevelId: 2,
      clinicalActivityId: 3,
      programName: 'Test Program',
    };
    spyOn(store, 'dispatch');
    component.saveGmeRotation({ data: newRotation, isEdit: true });

    expect(store.dispatch).toHaveBeenCalled();
  });

  it('should toggle the conflict resolution modal', () => {
    component.showConflictResolutionModal = false;
    component.toggleConflictResolutionModal();
    expect(component.showConflictResolutionModal).toBe(true);

    component.showConflictResolutionModal = true;
    component.toggleConflictResolutionModal();
    expect(component.showConflictResolutionModal).toBe(false);
  });
});
