import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainingAddEditModalComponent } from './training-add-edit-modal.component';

describe('TrainingAddEditModalComponent', () => {
  let component: TrainingAddEditModalComponent;
  let fixture: ComponentFixture<TrainingAddEditModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TrainingAddEditModalComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(TrainingAddEditModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
