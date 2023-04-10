import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReferenceFormModalComponent } from './reference-form-modal.component';

describe('ReferenceFormModalComponent', () => {
  let component: ReferenceFormModalComponent;
  let fixture: ComponentFixture<ReferenceFormModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReferenceFormModalComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ReferenceFormModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
