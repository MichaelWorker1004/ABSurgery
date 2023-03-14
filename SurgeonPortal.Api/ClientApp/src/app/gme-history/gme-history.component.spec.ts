import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GmeHistoryComponent } from './gme-history.component';

describe('GmeHistoryComponent', () => {
  let component: GmeHistoryComponent;
  let fixture: ComponentFixture<GmeHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GmeHistoryComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(GmeHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
