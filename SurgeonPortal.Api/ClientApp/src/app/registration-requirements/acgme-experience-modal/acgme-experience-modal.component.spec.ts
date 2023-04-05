import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AcgmeExperienceModalComponent } from './acgme-experience-modal.component';

describe('AcgmeExperienceModalComponent', () => {
  let component: AcgmeExperienceModalComponent;
  let fixture: ComponentFixture<AcgmeExperienceModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AcgmeExperienceModalComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(AcgmeExperienceModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
