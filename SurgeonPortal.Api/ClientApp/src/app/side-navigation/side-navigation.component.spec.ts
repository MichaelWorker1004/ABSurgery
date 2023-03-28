import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { SideNavigationComponent } from './side-navigation.component';
import { NgxsModule } from '@ngxs/store';
import { surgeonPortalState } from '../state/surgeon-portal.state';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('SideNavigationComponent', () => {
  let component: SideNavigationComponent;
  let fixture: ComponentFixture<SideNavigationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        SideNavigationComponent,
        HttpClientTestingModule,
        NgxsModule.forRoot(surgeonPortalState),
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(SideNavigationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
