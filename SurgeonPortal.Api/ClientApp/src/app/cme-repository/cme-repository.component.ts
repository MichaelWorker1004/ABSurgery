/* eslint-disable prettier/prettier */
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
import { Observable } from 'rxjs';
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

  droppingCredits?: IDroppingCmeCredits;

  cycleStartDate = new Date();
  cycleEndDate = new Date();

  summaryCmeCols = SUMMARY_CME_COLS;
  requirementsAndAdjustmentsCols = REQIUREMENTS_AND_ADJUSTMENTS_COLS;
  itemizedCmeCols = ITEMIZED_CME_COLS;
  CMEGridPaging = true;
  ABSGridPaging = true;
  iconDisplay = 'inline';

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
  }

  Print() {
    this.CMEGridPaging = false;
    this.ABSGridPaging = false;
    this.iconDisplay = 'none';

    setTimeout(function ()
    {
      const printContent = document.getElementById("pintHtml");
      const w = window.open('', '', 'left=0,top=0,width=900,height=900,toolbar=0,scrollbars=0,status=0');
      w!.document.write('<html><head><title>CME Repository</title>');
      w!.document.write('<link rel="stylesheet" type="text/css" href="https://cdn.jsdelivr.net/npm/@shoelace-style/shoelace@2.2.0/dist/themes/light.css">');
      w!.document.write('<link rel="stylesheet" type="text/css" href="assets/styles.scss">');
      w!.document.write('<link rel="stylesheet" type="text/css" href="assets/grid.component.scss">');
      w!.document.write('</head><body><main><div class="app-container-fluid: false; flex: false; flex-column: false">');
      w!.document.write(printContent!.innerHTML);
      w!.document.write('<script type="text/javascript">addEventListener("load", () => { print(); close(); })</script></div></main></body></html>');
      w!.document.close();
      w!.focus();
    }, 2000);

    setTimeout( () => {
      this.CMEGridPaging = true;
      this.ABSGridPaging = true;
      this.iconDisplay = 'inline';
    }, 3000);
   
  }
}
