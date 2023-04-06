import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SpecialAccommodationsModalComponent } from './special-accommodations-modal.component';

describe('SpecialAccommodationsModalComponent', () => {
  let component: SpecialAccommodationsModalComponent;
  let fixture: ComponentFixture<SpecialAccommodationsModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SpecialAccommodationsModalComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(SpecialAccommodationsModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
