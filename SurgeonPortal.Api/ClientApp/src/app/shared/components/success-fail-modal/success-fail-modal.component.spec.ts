import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SuccessFailModalComponent } from './success-fail-modal.component';

describe('SuccessFailModalComponent', () => {
  let component: SuccessFailModalComponent;
  let fixture: ComponentFixture<SuccessFailModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SuccessFailModalComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(SuccessFailModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
