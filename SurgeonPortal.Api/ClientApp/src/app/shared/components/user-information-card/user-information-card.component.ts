import {
  Component,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
} from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'abs-user-information-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './user-information-card.component.html',
  styleUrls: ['./user-information-card.component.scss'],
})
export class UserInformationCardComponent implements OnInit, OnChanges {
  @Input() userInformation: any;
  @Input() currentStatus!: string;
  @Input() isSurgeon: boolean | undefined;

  localIsSurgeon!: boolean;

  ngOnInit(): void {
    this.localIsSurgeon = this.isSurgeon || false;
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['isSurgeon']) {
      this.localIsSurgeon = changes['isSurgeon'].currentValue;
    }
  }
}
