import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CurrentAppointmentFormComponent } from './current-appointment-form.component';

describe('CurrentAppointmentFormComponent', () => {
  let component: CurrentAppointmentFormComponent;
  let fixture: ComponentFixture<CurrentAppointmentFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CurrentAppointmentFormComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(CurrentAppointmentFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
