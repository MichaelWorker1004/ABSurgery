import { CUSTOM_ELEMENTS_SCHEMA, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'abs-exam-process',
  templateUrl: './exam-process.component.html',
  styleUrls: ['./exam-process.component.scss'],
  standalone: true,
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  imports: [CommonModule, RouterLink],
})
export class ExamProcessComponent implements OnInit {
  availableApplications: any[] = [];

  ngOnInit(): void {
    this.getApplications();
  }

  getApplications() {
    this.availableApplications = [
      {
        name: 'Pediatric Surgery Qualifying Exam',
        progress: 'not started',
        continuousCertNeeded: true,
        status: 'not-started',
        deadline: new Date('5/10/2022'),
      },
      {
        name: 'General Surgery Qualifying Exam',
        progress: '0/10 completed',
        continuousCertNeeded: false,
        status: 'in-progress',
        deadline: new Date('5/10/2022'),
      },
    ];
  }
}
