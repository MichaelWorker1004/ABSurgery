import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccordionModule } from 'primeng/accordion';

@Component({
  selector: 'abs-expandable',
  standalone: true,
  imports: [CommonModule, AccordionModule],
  templateUrl: './expandable.component.html',
  styleUrls: ['./expandable.component.scss'],
})
export class ExpandableComponent {
  /**
   * Title of the Expandable
   * @type {string}
   */
  @Input() caseTitle!: string;

  /**
   * The ID of the case. Needed for scoring functionality
   * @type {number}
   */
  @Input() caseId!: number;

  /**
   * The index of the case. Needed for expanable functionality being controlled by the parent component
   * @type {number}
   */
  @Input() index = 0;

  /**
   * Whether the expandable is expanded or not
   * @type {boolean}
   */
  @Input() checked = true;

  /**
   * A custom title to display in the header of the expandable
   * @type {string}
   */
  @Input() customTitle!: string;

  /**
   * Converts a decimal number to a roman numeral
   * @type {number}
   */
  decimalToRoman(decimal: number) {
    const values = [
      { symbol: 'M', value: 1000 },
      { symbol: 'CM', value: 900 },
      { symbol: 'D', value: 500 },
      { symbol: 'CD', value: 400 },
      { symbol: 'C', value: 100 },
      { symbol: 'XC', value: 90 },
      { symbol: 'L', value: 50 },
      { symbol: 'XL', value: 40 },
      { symbol: 'X', value: 10 },
      { symbol: 'IX', value: 9 },
      { symbol: 'V', value: 5 },
      { symbol: 'IV', value: 4 },
      { symbol: 'I', value: 1 },
    ];
    let result = '';
    for (const { symbol, value } of values) {
      while (decimal >= value) {
        result += symbol;
        decimal -= value;
      }
    }
    return result;
  }
}
