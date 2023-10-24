import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NgxsModule, Store } from '@ngxs/store';
import { surgeonPortalState } from '../../../state/surgeon-portal.state';

import { TrainingAddEditModalComponent } from './training-add-edit-modal.component';
import { ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { GlobalDialogService } from '../../services/global-dialog.service';
import { SetUnsavedChanges } from 'src/app/state';

describe('TrainingAddEditModalComponent', () => {
  let component: TrainingAddEditModalComponent;
  let fixture: ComponentFixture<TrainingAddEditModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        ReactiveFormsModule,
        TrainingAddEditModalComponent,
        NgxsModule.forRoot(surgeonPortalState),
        TranslateModule.forRoot(),
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(TrainingAddEditModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize form controls and observables', () => {
    expect(component.additionalTrainingForm).toBeTruthy();
    expect(component.isEdit).toBeFalsy();
    // Add more expectations as needed
  });

  it('should handle cancel without unsaved changes', () => {
    const spy = spyOn(component.cancelDialog, 'emit');
    component.hasUnsavedChanges = false;
    component.cancel();

    expect(spy).toHaveBeenCalled();
  });

  it('should handle cancel with unsaved changes', () => {
    const globalDialogService = TestBed.inject(GlobalDialogService);
    spyOn(globalDialogService, 'showConfirmation').and.returnValue(
      Promise.resolve(true)
    );

    component.hasUnsavedChanges = true;
    component.cancel();

    expect(globalDialogService.showConfirmation).toHaveBeenCalled();
    // figure out how to trigger a confirm click on the dialog
    //expect(component.hasUnsavedChanges).toBe(false);
    // Add more expectations as needed
  });

  it('should handle save', () => {
    const spy = spyOn(component.saveDialog, 'emit');
    const store = TestBed.inject(Store);
    spyOn(store, 'dispatch');
    component.isEdit = true;
    const formValue = {
      /* mock form values */
    };

    component.save();

    expect(component.hasUnsavedChanges).toBe(false);
    expect(store.dispatch).toHaveBeenCalledWith(new SetUnsavedChanges(false));
    expect(spy).toHaveBeenCalled();
    // Add more expectations as needed
  });
});
