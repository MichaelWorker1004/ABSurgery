import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectWrapperComponent } from './select-wrapper.component';

describe('SelectWrapperComponent', () => {
  let component: SelectWrapperComponent;
  let fixture: ComponentFixture<SelectWrapperComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SelectWrapperComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(SelectWrapperComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
