import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OralExaminationsComponent } from './oral-examinations.component';

describe('OralExaminationsComponent', () => {
  let component: OralExaminationsComponent;
  let fixture: ComponentFixture<OralExaminationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OralExaminationsComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(OralExaminationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
