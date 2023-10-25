import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { NgxsModule, Store } from '@ngxs/store';
import { surgeonPortalState } from '../state/surgeon-portal.state';

import { DashboardComponent } from './dashboard.component';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { of } from 'rxjs';
import {
  CERTIFIED_ACTION_CARDS,
  TRAINEE_ACTION_CARDS,
} from './user-action-cards';

describe('DashboardComponent', () => {
  let component: DashboardComponent;
  let fixture: ComponentFixture<DashboardComponent>;
  let store: Store;
  let translateService: TranslateService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        DashboardComponent,
        NgxsModule.forRoot(surgeonPortalState),
        HttpClientTestingModule,
        TranslateModule.forRoot(),
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(DashboardComponent);
    component = fixture.componentInstance;
    store = TestBed.inject(Store);
    translateService = TestBed.inject(TranslateService);

    store = TestBed.inject(Store);
    spyOn(store, 'dispatch').and.returnValue(of({}));
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should set userActionCards based on isSurgeon', () => {
    // Set isSurgeon to true and check if userActionCards is set accordingly
    component.isSurgeon = true;
    component.setActionCardsByUserClaims(component.isSurgeon);
    expect(component.userActionCards).toEqual(CERTIFIED_ACTION_CARDS);

    // Set isSurgeon to false and check if userActionCards is set accordingly
    component.isSurgeon = false;
    component.setActionCardsByUserClaims(component.isSurgeon);
    expect(component.userActionCards).toEqual(TRAINEE_ACTION_CARDS);
  });
});
