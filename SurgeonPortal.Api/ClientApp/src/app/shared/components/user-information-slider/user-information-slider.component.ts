import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CarouselModule } from 'primeng/carousel';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'abs-user-information-slider',
  standalone: true,
  imports: [
    CommonModule,
    TranslateModule,
    CarouselModule,
    RouterLink,
    RouterLinkActive,
  ],
  templateUrl: './user-information-slider.component.html',
  styleUrls: ['./user-information-slider.component.scss'],
})
export class UserInformationSliderComponent {
  /**
   * Data to display in the slider
   * @type {any[]}
   * @example
   * sliderData = [
   * {
   * firstName: 'John',
   * lastName: 'Doe',
   * sessionNumber: 1,
   * startTime: '09:00:00',
   * endTime: '10:00:00',
   * },
   */
  @Input() sliderData: any = [];
  @Input() examinationWeek!: string;
  responsiveOptions = [
    {
      breakpoint: '1199px',
      numVisible: 1,
      numScroll: 1,
    },
    {
      breakpoint: '991px',
      numVisible: 2,
      numScroll: 1,
    },
    {
      breakpoint: '767px',
      numVisible: 1,
      numScroll: 1,
    },
  ];
}
