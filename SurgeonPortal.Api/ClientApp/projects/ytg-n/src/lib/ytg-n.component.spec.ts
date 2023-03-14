import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YtgNComponent } from './ytg-n.component';

describe('YtgNComponent', () => {
  let component: YtgNComponent;
  let fixture: ComponentFixture<YtgNComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [YtgNComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(YtgNComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
