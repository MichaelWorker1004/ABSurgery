import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CmeRepositoryComponent } from './cme-repository.component';
import { surgeonPortalState } from '../state/surgeon-portal.state';
import { NgxsModule, Store } from '@ngxs/store';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { GetCmeSummary } from '../state';
import { of } from 'rxjs';

describe('CmeRepositoryComponent', () => {
  let component: CmeRepositoryComponent;
  let fixture: ComponentFixture<CmeRepositoryComponent>;
  let store: Store;

  const mockState = {};

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        CmeRepositoryComponent,
        NgxsModule.forRoot(surgeonPortalState),
        HttpClientTestingModule,
      ],
    }).compileComponents();

    store = TestBed.inject(Store);
    store.reset({
      ...store.snapshot(),
      ...mockState,
    });
    fixture = TestBed.createComponent(CmeRepositoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize CME data on component creation', () => {
    const getCmeSummarySpy = spyOn(store, 'dispatch').and.returnValue(of({}));
    component.initCmeData();
    expect(getCmeSummarySpy).toHaveBeenCalledWith(new GetCmeSummary());
  });

  it('should set cycle start and end dates on component initialization', () => {
    component.ngOnInit();
    const today = new Date();
    const expectedStartDate = new Date(today.getFullYear() - 5, 0, 1);
    const expectedEndDate = new Date(today.getFullYear(), 11, 31);
    expect(component.cycleStartDate).toEqual(expectedStartDate);
    expect(component.cycleEndDate).toEqual(expectedEndDate);
  });

  it('should set summaryCmeCols, requirementsAndAdjustmentsCols, and itemizedCmeCols properties', () => {
    expect(component.summaryCmeCols).toBeDefined();
    expect(component.requirementsAndAdjustmentsCols).toBeDefined();
    expect(component.itemizedCmeCols).toBeDefined();
  });
});
