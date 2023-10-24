import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OtherCertificatesAddEditModalComponent } from './other-certificates-add-edit-modal.component';
import { NgxsModule, Store } from '@ngxs/store';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { surgeonPortalState } from 'src/app/state/surgeon-portal.state';
import { ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { GlobalDialogService } from 'src/app/shared/services/global-dialog.service';
import { SetUnsavedChanges } from 'src/app/state';

describe('OtherCertificatesAddEditModalComponent', () => {
  let component: OtherCertificatesAddEditModalComponent;
  let fixture: ComponentFixture<OtherCertificatesAddEditModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        OtherCertificatesAddEditModalComponent,
        ReactiveFormsModule,
        NgxsModule.forRoot(surgeonPortalState),
        HttpClientTestingModule,
        TranslateModule.forRoot(),
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(OtherCertificatesAddEditModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize form controls and observables', () => {
    expect(component.otherCertificatesForm).toBeTruthy();
    expect(component.isEdit).toBeFalsy(); // Modify as needed
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
