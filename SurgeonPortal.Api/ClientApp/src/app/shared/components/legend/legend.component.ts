import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ILegend } from './legend.model';

@Component({
  selector: 'abs-legend',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './legend.component.html',
  styleUrls: ['./legend.component.scss'],
})
export class LegendComponent {
  @Input() legendItems!: ILegend[];
}
