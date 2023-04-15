import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormErrorsComponent } from './form-errors.component';

import { NgxsModule, Store } from '@ngxs/store';

describe('FormErrorsComponent', () => {
  let component: FormErrorsComponent;
  let fixture: ComponentFixture<FormErrorsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormErrorsComponent, NgxsModule.forRoot()],
      providers: [Store],
    }).compileComponents();

    fixture = TestBed.createComponent(FormErrorsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
