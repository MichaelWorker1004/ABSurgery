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
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  standalone: true,
})
export class CmeRepositoryComponent implements OnInit {
  requirementsAndAdjustmentsCols = REQIUREMENTS_AND_ADJUSTMENTS_COLS;
  requirementsAndAdjustmentsData!: Array<any>;

  itemizedCmeCols = ITEMIZED_CME_COLS;
  itemizedCmeData!: Array<any>;

  cmeCreditsChartData!: any;

  cmeCreditsChartOptions = {
    plugins: {
      legend: {
        display: false,
      },
    },
    scales: {
      x: {
        ticks: {
          font: {
            size: 15,
            color: 'black',
            weight: 700,
          },
        },
        grid: {
          display: false,
        },
      },
      y: {
        ticks: {
          font: {
            size: 15,
            color: 'black',
            weight: 700,
          },
        },
      },
    },
  };

  ngOnInit(): void {
    this.getRequirementsAndAdjustmentsData();
    this.getItemizedCmeData();
    this.initbarGraph();
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

  getItemizedCmeData() {
    this.itemizedCmeData = [
      {
        date: new Date('10/27/2022'),
        description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit',
        credits: '24',
        cmeDirect: 'XXXXX',
      },
      {
        date: new Date('10/25/2022'),
        description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit',
        credits: '36',
        cmeDirect: 'XXXXX',
      },
      {
        date: new Date('9/10/2022'),
        description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit',
        credits: '12',
        cmeDirect: 'XXXXX',
      },
      {
        date: new Date('8/17/2022'),
        description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit',
        credits: '78',
        cmeDirect: 'XXXXX',
      },
    ];
  }

  initbarGraph() {
    this.cmeCreditsChartData = {
      labels: ['2018', '2019', '2020', '2021', '2022'],

      datasets: [
        {
          label: 'Category 1 Credits',
          backgroundColor: '#dbad6a',
          data: [40, 20, 30, 40, 20],
        },
        {
          label: 'Self Assessment Credits',
          backgroundColor: '#1c827d',
          data: [5, 15, 10, 5, 15],
        },
      ],
    };
  }
}
