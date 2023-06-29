import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CarouselModule } from 'primeng/carousel';
import {
  RouterLink,
  RouterLinkActive,
  Router,
  ActivatedRoute,
} from '@angular/router';

@Component({
  selector: 'abs-user-information-slider',
  standalone: true,
  imports: [CommonModule, CarouselModule, RouterLink, RouterLinkActive],
  templateUrl: './user-information-slider.component.html',
  styleUrls: ['./user-information-slider.component.scss'],
})
export class UserInformationSliderComponent {
  @Input() sliderData!: any[];
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

  constructor(private _router: Router, private route: ActivatedRoute) {}

  routeToExam(examinationId: string) {
    this._router.navigate([`oral-examinations/exam/${examinationId}`], {
      relativeTo: this.route,
    });
  }

  get router(): Router {
    return this._router;
  }
}
