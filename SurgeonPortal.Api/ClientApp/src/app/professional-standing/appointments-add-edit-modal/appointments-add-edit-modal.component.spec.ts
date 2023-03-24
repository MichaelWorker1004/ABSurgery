import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppointmentsAddEditModalComponent } from './appointments-add-edit-modal.component';

describe('AppointmentsAddEditModalComponent', () => {
  let component: AppointmentsAddEditModalComponent;
  let fixture: ComponentFixture<AppointmentsAddEditModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AppointmentsAddEditModalComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(AppointmentsAddEditModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
