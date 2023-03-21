import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { ContinuousCertificationComponent } from './continuous-certification.component';

describe('ContinuousCertificationComponent', () => {
  let component: ContinuousCertificationComponent;
  let fixture: ComponentFixture<ContinuousCertificationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RouterTestingModule, ContinuousCertificationComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ContinuousCertificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
