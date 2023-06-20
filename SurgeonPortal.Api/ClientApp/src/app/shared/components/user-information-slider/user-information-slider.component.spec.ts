import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserInformationSliderComponent } from './user-information-slider.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { NgxsModule } from '@ngxs/store';
import { surgeonPortalState } from 'src/app/state/surgeon-portal.state';

describe('UserInformationSliderComponent', () => {
  let component: UserInformationSliderComponent;
  let fixture: ComponentFixture<UserInformationSliderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        UserInformationSliderComponent,
        RouterTestingModule,
        NgxsModule.forRoot(surgeonPortalState),
        HttpClientTestingModule,
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(UserInformationSliderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
