import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserInformationCardComponent } from './user-information-card.component';

describe('UserInformationCardComponent', () => {
  let component: UserInformationCardComponent;
  let fixture: ComponentFixture<UserInformationCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserInformationCardComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(UserInformationCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
