import { CommonModule } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit } from '@angular/core';
import { CollapsePanelComponent } from '../shared/components/collapse-panel/collapse-panel.component';
import { GridComponent } from '../shared/components/grid/grid.component';
import { REQIUREMENTS_AND_ADJUSTMENTS_COLS } from './requirments-and-adjustments-cols';

@Component({
  selector: 'abs-cme-repository',
  templateUrl: './cme-repository.component.html',
  styleUrls: ['./cme-repository.component.scss'],
  imports: [CommonModule, CollapsePanelComponent, GridComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  standalone: true,
})
export class CmeRepositoryComponent implements OnInit {
  requirementsAndAdjustmentsCols = REQIUREMENTS_AND_ADJUSTMENTS_COLS;
  requirementsAndAdjustmentsData!: Array<any>;

  ngOnInit(): void {
    this.getRequirementsAndAdjustmentsData();
  }

  getRequirementsAndAdjustmentsData() {
    this.requirementsAndAdjustmentsData = [
      {
        date: new Date('10/27/2022'),
        description: 'Waiver',
        category1: '8',
        saCredits: '8',
        issuedBy: 'ABS',
      },
      {
        date: new Date('10/27/2022'),
        description: 'Waiver',
        category1: '10',
        saCredits: '10',
        issuedBy: 'ABS',
      },
    ];
  }
}
