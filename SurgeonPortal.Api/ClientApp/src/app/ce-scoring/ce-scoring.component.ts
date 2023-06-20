import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActionCardComponent } from '../shared/components/action-card/action-card.component';
import { ACTION_CARDS } from './user-action-cards';
import { HighlightCardComponent } from '../shared/components/highlight-card/highlight-card.component';
import { UserInformationSliderComponent } from '../shared/components/user-information-slider/user-information-slider.component';

@Component({
  selector: 'abs-ce-scoring',
  standalone: true,
  imports: [
    CommonModule,
    ActionCardComponent,
    HighlightCardComponent,
    UserInformationSliderComponent,
  ],
  templateUrl: './ce-scoring.component.html',
  styleUrls: ['./ce-scoring.component.scss'],
})
export class CeScoringAppComponent implements OnInit {
  currentYear = new Date().getFullYear();
  userActionCards = ACTION_CARDS;
  alertsAndNotices: any[] | undefined;
  oralExaminations!: any[];
  examinationWeek!: string;

  ngOnInit(): void {
    this.fetchCEDashboardDate();
  }

  fetchCEDashboardDate() {
    this.alertsAndNotices = [
      {
        title: 'Your Weekly Agenda is Ready to Review',
        content:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer egestas maximus turpis id pulvinar.',
        alert: false,
        image:
          'https://images.pexels.com/photos/13548722/pexels-photo-13548722.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1',
      },
    ];

    this.oralExaminations = [
      {
        examineeName: 'Karla Africa',
        examinationId: '123456',
        sessionNumber: 1,
        times: '10:00–10:30AM EST',
      },
      {
        examineeName: 'Nkiruka Iseigas',
        examinationId: '456',
        sessionNumber: 1,
        times: '10:35–11:05AM EST',
      },
      {
        examineeName: 'Daniel Fuentes',
        examinationId: '666441',
        sessionNumber: 1,
        times: '11:10–11:40AM EST',
      },
      {
        examineeName: 'John Ayala',
        examinationId: '444564',
        sessionNumber: 1,
        times: '12:15–12:45PM EST',
      },
      {
        examineeName: 'John Doe',
        examinationId: '444564',
        sessionNumber: 1,
        times: '1:15–1:45PM EST',
      },
      {
        examineeName: 'John Ayala',
        examinationId: '444564',
        sessionNumber: 1,
        times: '12:15–12:45PM EST',
      },

      {
        examineeName: 'John Doe',
        examinationId: '444564',
        sessionNumber: 1,
        times: '1:15–1:45PM EST',
      },
      {
        examineeName: 'John Doe',
        examinationId: '444564',
        sessionNumber: 1,
        times: '1:15–1:45PM EST',
      },
      {
        examineeName: 'John Ayala',
        examinationId: '444564',
        sessionNumber: 1,
        times: '12:15–12:45PM EST',
      },

      {
        examineeName: 'John Doe',
        examinationId: '444564',
        sessionNumber: 1,
        times: '1:15–1:45PM EST',
      },
    ];

    this.examinationWeek = '03/08/2023';
  }
}
