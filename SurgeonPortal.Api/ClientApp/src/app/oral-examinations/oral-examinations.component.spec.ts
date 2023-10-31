import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OralExaminationsComponent } from './oral-examinations.component';
import { NgxsModule, Store } from '@ngxs/store';
import { surgeonPortalState } from '../state/surgeon-portal.state';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TranslateModule } from '@ngx-translate/core';
import { Observable } from 'rxjs';
import { ORAL_EXAMINATION_COLS } from './oral-examination-cols';
import { IExamSessionReadOnlyModel } from '../api';
import { GlobalDialogService } from '../shared/services/global-dialog.service';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('OralExaminationsComponent', () => {
  let component: OralExaminationsComponent;
  let fixture: ComponentFixture<OralExaminationsComponent>;
  let store: Store;

  const mockState = {
    examScoring: {
      examineeList: [
        {
          examScheduleId: 1,
          firstName: 'John',
          lastName: 'Doe',
          startTime: '08:00',
          endTime: '12:00',
          meetingLink: 'https://zoomlink.com',
          isSubmitted: false,
          isCurrentSession: true,
          sessionNumber: 1,
          isLocked: false,
        },
      ],
    },
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        OralExaminationsComponent,
        NgxsModule.forRoot(surgeonPortalState),
        HttpClientTestingModule,
        TranslateModule.forRoot(),
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    }).compileComponents();

    store = TestBed.inject(Store);
    store.reset({
      ...store.snapshot(),
      ...mockState,
    });
    fixture = TestBed.createComponent(OralExaminationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize component properties', () => {
    expect(component.examDate).toEqual(jasmine.any(Date));
    expect(component.examDateDisplay).toEqual(jasmine.any(Date));
    expect(component.zoomLink).toBe('https://zoomlink.com');
    expect(component.oralExaminations$).toEqual(jasmine.any(Observable));
    expect(component.oralExaminationCols).toEqual(ORAL_EXAMINATION_COLS);
  });

  it('should get oral examinations', () => {
    //const mockExamineeList: IExamSessionReadOnlyModel[] = [];
    spyOn(component, 'setExaminationStatus').and.returnValue('not-started');
    component.getOralExaminations();
    expect(component.oralExaminations$.value.length).toBeGreaterThan(0);
    expect(component.zoomLink).toBe('https://zoomlink.com');
  });

  it('should set examination status', () => {
    const mockExam = {
      isCurrentSession: true,
      isSubmitted: false,
    } as unknown as IExamSessionReadOnlyModel;
    const status = component.setExaminationStatus(mockExam);
    expect(status).toEqual('current-session');
  });

  it('should handle grid action for starting the exam', () => {
    const globalDialogService = TestBed.inject(GlobalDialogService);
    const spy = spyOn(globalDialogService, 'showConfirmation').and.returnValue(
      Promise.resolve(true)
    );
    const event = {
      fieldKey: 'startExam',
      data: { examScheduleId: 1, fullName: 'John Doe' },
    };
    spyOn(window, 'confirm').and.returnValue(true); // Simulate user confirmation
    component.handleGridAction(event);
    expect(spy).toHaveBeenCalled();
  });

  it('should copy text from input', () => {
    const element = { value: 'test' };
    spyOn(navigator.clipboard, 'writeText');
    component.copyFromTextInput(element);
    expect(navigator.clipboard.writeText).toHaveBeenCalledWith('test');
  });

  it('should open Zoom link', () => {
    component.zoomLink = 'https://zoomlink.com';
    spyOn(window, 'open');
    component.openZoomLink();
    expect(window.open).toHaveBeenCalledWith('https://zoomlink.com', '_blank');
  });
});
