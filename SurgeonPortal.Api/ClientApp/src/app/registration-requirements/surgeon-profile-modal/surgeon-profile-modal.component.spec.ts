import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SurgeonProfileModalComponent } from './surgeon-profile-modal.component';

describe('SurgeonProfileModalComponent', () => {
  let component: SurgeonProfileModalComponent;
  let fixture: ComponentFixture<SurgeonProfileModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SurgeonProfileModalComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(SurgeonProfileModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
