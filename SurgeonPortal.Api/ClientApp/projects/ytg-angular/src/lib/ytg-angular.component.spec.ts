import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YtgAngularComponent } from './ytg-angular.component';

describe('YtgAngularComponent', () => {
  let component: YtgAngularComponent;
  let fixture: ComponentFixture<YtgAngularComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [YtgAngularComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(YtgAngularComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
