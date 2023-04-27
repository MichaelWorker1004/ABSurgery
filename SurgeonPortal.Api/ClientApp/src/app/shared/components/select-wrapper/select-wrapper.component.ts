import {
  Component,
  CUSTOM_ELEMENTS_SCHEMA,
  EventEmitter,
  Input,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { SlChangeEvent } from '@shoelace-style/shoelace';

@Component({
  selector: 'abs-select-wrapper',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './select-wrapper.component.html',
  styleUrls: ['./select-wrapper.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class SelectWrapperComponent {
  @Output() valueChange: EventEmitter<string | undefined> = new EventEmitter<
    string | undefined
  >();
  // eslint-disable-next-line @angular-eslint/no-output-native
  @Output() change: EventEmitter<SlChangeEvent> =
    new EventEmitter<SlChangeEvent>();
  // @Output() label: string | undefined;
  // @Output() name: string | undefined;

  _value: string | undefined;

  @Input()
  get value(): string | undefined {
    // console.log('GET', this._value);
    return this._value;
  }
  set value(value: string | undefined) {
    // console.log('SET', this._value, value);
    this._value = value;
    this.valueChange.emit(value);
  }

  dispatchChange(event: SlChangeEvent) {
    // console.log('DISPATCH', event);
    this.change.emit(event);
  }

  changeBtn() {
    this.value = 'option-1';
  }
}
