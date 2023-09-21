import { CommonModule } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit } from '@angular/core';
import { CollapsePanelComponent } from '../shared/components/collapse-panel/collapse-panel.component';
import { GridComponent } from '../shared/components/grid/grid.component';
import { ITEMIZED_CME_COLS } from './itemized-cme-cols';
import { REQIUREMENTS_AND_ADJUSTMENTS_COLS } from './requirments-and-adjustments-cols';
import { ChartModule } from 'primeng/chart';
import { ProgressBarComponent } from '../shared/components/progress-bar/progress-bar.component';
import { TooltipModule } from 'primeng/tooltip';
import { AlertComponent } from '../shared/components/alert/alert.component';
import { SUMMARY_CME_COLS } from './summary-cme-cols';
import { Select, Store } from '@ngxs/store';
import {
  ContinuingMedicalEducationSelectors,
  GetCmeSummary,
  ICmeAdjustment,
  ICmeCredit,
  ICmeSummaryRow,
  IDroppingCmeCredits,
} from '../state';
import { BehaviorSubject, Observable } from 'rxjs';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { TooltipComponent } from '../shared/components/tooltip/tooltip.component';

@UntilDestroy()
@Component({
  selector: 'abs-cme-repository',
  templateUrl: './cme-repository.component.html',
  styleUrls: ['./cme-repository.component.scss'],
  imports: [
    CommonModule,
    CollapsePanelComponent,
    GridComponent,
    ChartModule,
    ProgressBarComponent,
    TooltipModule,
    AlertComponent,
    TooltipComponent,
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  standalone: true,
})
export class CmeRepositoryComponent implements OnInit {
  @Select(ContinuingMedicalEducationSelectors.continuingMedicalEducationCredits)
  cmeCredits$: Observable<ICmeCredit[]> | undefined;

  @Select(
    ContinuingMedicalEducationSelectors.continuingMedicalEducationAdjustments
  )
  cmeAdjustments$: Observable<ICmeAdjustment[]> | undefined;

  @Select(ContinuingMedicalEducationSelectors.slices.cmeSummary)
  cmeSummary$: Observable<ICmeSummaryRow[]> | undefined;

  cmeAdjustmentsHeightChange$: BehaviorSubject<boolean> = new BehaviorSubject(
    true
  );
  cmeCreditsHeightChange$: BehaviorSubject<boolean> = new BehaviorSubject(true);

  droppingCredits?: IDroppingCmeCredits;

  cycleStartDate = new Date();
  cycleEndDate = new Date();

  summaryCmeCols = SUMMARY_CME_COLS;
  requirementsAndAdjustmentsCols = REQIUREMENTS_AND_ADJUSTMENTS_COLS;
  itemizedCmeCols = ITEMIZED_CME_COLS;

  constructor(private _store: Store) {
    this.initCmeData();
  }

  ngOnInit(): void {
    const today = new Date();
    this.cycleStartDate = new Date(today.getFullYear() - 5, 0, 1);
    this.cycleEndDate = new Date(today.getFullYear(), 11, 31);
  }

  initCmeData() {
    this._store
      .dispatch(new GetCmeSummary())
      .pipe(untilDestroyed(this))
      .subscribe(() => {
        this.droppingCredits = this._store.selectSnapshot(
          ContinuingMedicalEducationSelectors.slices.cmeDroppingCredits
        );
      });

    this.getItemizedCmeData();
    this.getRequirementsAndAdjustmentsData();
  }

  getRequirementsAndAdjustmentsData() {
    this.cmeAdjustments$?.pipe(untilDestroyed(this)).subscribe((data) => {
      this.cmeAdjustmentsHeightChange$.next(
        !this.cmeAdjustmentsHeightChange$.getValue()
      );
    });
  }

  getItemizedCmeData() {
    this.cmeCredits$?.pipe(untilDestroyed(this)).subscribe((data) => {
      this.cmeCreditsHeightChange$.next(
        !this.cmeCreditsHeightChange$.getValue()
      );
    });
  }
}
