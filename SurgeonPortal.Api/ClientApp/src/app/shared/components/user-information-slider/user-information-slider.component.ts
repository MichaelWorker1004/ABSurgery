import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CarouselModule } from 'primeng/carousel';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { IRosterReadOnlyModel } from 'src/app/api/models/scoring/roster-read-only.model';

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
  @Input() sliderData: IRosterReadOnlyModel[] = [];
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
