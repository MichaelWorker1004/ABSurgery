import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FellowshipAddEditModalComponent } from './fellowship-add-edit-modal.component';
import { surgeonPortalState } from 'src/app/state/surgeon-portal.state';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NgxsModule, Store } from '@ngxs/store';
import { GlobalDialogService } from 'src/app/shared/services/global-dialog.service';
import { SetUnsavedChanges } from 'src/app/state';

describe('FellowshipAddEditModalComponent', () => {
  let component: FellowshipAddEditModalComponent;
  let fixture: ComponentFixture<FellowshipAddEditModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        FellowshipAddEditModalComponent,
        NgxsModule.forRoot(surgeonPortalState),
        HttpClientTestingModule,
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(FellowshipAddEditModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize form controls and observables', () => {
    expect(component.fellowshipForm).toBeTruthy();
    expect(component.isEdit).toBe(false); // Modify as needed
    // Add more expectations as needed
  });

  it('should handle cancel without unsaved changes', () => {
    const spy = spyOn(component.cancelDialog, 'emit');
    component.hasUnsavedChanges = false;
    component.cancel();

    expect(spy).toHaveBeenCalled();
  });

  it('should handle cancel with unsaved changes', () => {
    //const spy = spyOn(component.cancelDialog, 'emit');
    const globalDialogService = TestBed.inject(GlobalDialogService);
    spyOn(globalDialogService, 'showConfirmation').and.returnValue(
      Promise.resolve(true)
    );

    component.hasUnsavedChanges = true;
    component.cancel();

    expect(globalDialogService.showConfirmation).toHaveBeenCalled();
    //expect(spy).toHaveBeenCalled();
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
