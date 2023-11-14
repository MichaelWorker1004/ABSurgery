import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConflictResolutionModalComponent } from './conflict-resolution-modal.component';
import { IRotationGapReadOnlyModel, IRotationReadOnlyModel } from 'src/app/api';

describe('ConflictResolutionModalComponent', () => {
  let component: ConflictResolutionModalComponent;
  let fixture: ComponentFixture<ConflictResolutionModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ConflictResolutionModalComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ConflictResolutionModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize conflictingRecords and gapData properties', () => {
    const conflictingRecords: IRotationReadOnlyModel[] = [
      /* provide test data */
    ];
    const gapData: IRotationGapReadOnlyModel = {
      startDate: '2023-01-01',
      endDate: '2023-01-31',
      previousRotationId: 123,
      nextRotationId: 456,
    };

    component.conflictingRecords = conflictingRecords;
    component.gapData = gapData;

    expect(component.conflictingRecords).toEqual(conflictingRecords);
    expect(component.gapData).toEqual(gapData);
  });

  it('should handle ngOnChanges and update localConflictsData', () => {
    const changes: any = {
      conflictingRecords: {
        currentValue: [
          /* provide test data */
        ],
      },
    };
    component.ngOnChanges(changes);
    expect(component.localConflictsData).toEqual(
      changes.conflictingRecords.currentValue
    );
  });

  it('should emit editRecord event when calling girdAction with fieldKey "edit"', () => {
    const editRecordSpy = jasmine.createSpy();
    component.editRecord.subscribe(editRecordSpy);
    const eventData = { fieldKey: 'edit', data: { id: 123 } };
    component.girdAction(eventData);
    expect(editRecordSpy).toHaveBeenCalledWith(eventData.data.id);
  });

  it('should log unhandled action when calling girdAction with an unhandled fieldKey', () => {
    spyOn(console, 'log'); // Spy on the console.log method
    const eventData = { fieldKey: 'unhandled', data: {} };
    component.girdAction(eventData);
    expect(console.log).toHaveBeenCalledWith('unhandled action', eventData);
  });

  it('should emit addRecord event when calling addNewRecord', () => {
    const addRecordSpy = jasmine.createSpy();
    component.addRecord.subscribe(addRecordSpy);
    component.gapData = {
      startDate: '2023-01-01',
      endDate: '2023-01-31',
      previousRotationId: 123,
      nextRotationId: 456,
    };
    component.addNewRecord();
    expect(addRecordSpy).toHaveBeenCalledWith({
      startDate: component.gapData?.startDate,
      endDate: component.gapData?.endDate,
    });
  });

  it('should emit closeDialog event when calling close', () => {
    const closeDialogSpy = jasmine.createSpy();
    component.closeDialog.subscribe(closeDialogSpy);
    component.close();
    expect(closeDialogSpy).toHaveBeenCalledWith({
      action: 'ACGMEExperienceModal',
    });
  });
});
