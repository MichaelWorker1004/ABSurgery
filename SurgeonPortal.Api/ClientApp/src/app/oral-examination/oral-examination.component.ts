import { CUSTOM_ELEMENTS_SCHEMA, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { ExpandableComponent } from '../shared/components/expandable/expandable.component';
import { ButtonModule } from 'primeng/button';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { ExaminationScoreCardComponent } from '../shared/components/examination-score-card/examination-score-card.component';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'abs-oral-examination',
  standalone: true,
  imports: [
    CommonModule,
    ExpandableComponent,
    ButtonModule,
    InputTextareaModule,
    ExaminationScoreCardComponent,
  ],
  templateUrl: './oral-examination.component.html',
  styleUrls: ['./oral-examination.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class OralExaminationsComponent implements OnInit {
  currentYear = new Date().getFullYear();
  examinationId!: string | null;

  candidateName!: string;
  dayTime!: string;
  cases: BehaviorSubject<any> = new BehaviorSubject([]);

  activeCase = 0;

  scores!: any[];

  candidateCaseScores = {} as any;

  candidateScores: any[] = [];

  constructor(private activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe((params) => {
      this.examinationId = params['examinationId'];
    });

    this.getExaminationData();
  }

  getExaminationData() {
    this.candidateName = 'John Doe';
    this.dayTime = '03/08/2023, 10:00–10:30AM EST';
    this.cases.next([
      {
        caseId: '38623',
        sections: [
          {
            title: 'Testing Points',
            htmlContent: `<ol><li>Lorem Ipsum</li><li>adipiscing elit</li><li>vitae mauris consequat</li></ol>`,
            showEdit: true,
            comment: undefined,
            editComment: false,
          },
          {
            title: 'Stem',
            htmlContent: `<p>Donec blandit feugiat ligula. Done hendrerit, felis et imperdiet euismod, purus ipsum pretium metus, in lacinia nulla nisl eget sapien. Donec ut est in lectus consequat consequat. Etiam get dui. Aliquam erat volutpat. Sed at lorem in nunc porta tristique. Proin nec augue. Quisque aliquam tempor magna. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.</p>`,
          },
          {
            title: 'Pertinent Diagnostics',
            htmlContent: `<ul><li>Lorem ipsum dolor sit amet</li><li>Donec laoreet nonummy augue. Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc</li><li>Done laoreet nonummy augue. Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc</li></ul>`,
          },
          {
            title: 'Management Points',
            htmlContent: `<b>Medical/Preoperative</b>
                          <ul>
                          <li>Lorem ipsum dolor sit amet</li>
                          <li>Donec laoreet nonummy augue. Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc</li>
                          <li>Done laoreet nonummy augue. Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc</li>
                          </ul>
                          <b>Operative Technical Details</b>
                          <ul>
                          <li>Lorem ipsum dolor sit amet</li>
                          <li>Donec laoreet nonummy augue. Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc</li>
                          <li>Done laoreet nonummy augue. Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc</li>
                          </ul>`,
          },
        ],
      },
      {
        caseId: '58471',
        sections: [
          {
            title: 'Testing Points',
            htmlContent: `<ol><li>Lorem Ipsum</li><li>adipiscing elit</li><li>vitae mauris consequat</li></ol>`,
            comment: undefined,
            editComment: false,
          },
          {
            title: 'Stem',
            htmlContent: `<p>Donec blandit feugiat ligula. Done hendrerit, felis et imperdiet euismod, purus ipsum pretium metus, in lacinia nulla nisl eget sapien. Donec ut est in lectus consequat consequat. Etiam get dui. Aliquam erat volutpat. Sed at lorem in nunc porta tristique. Proin nec augue. Quisque aliquam tempor magna. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.</p>`,
            comment: undefined,
            editComment: false,
          },
          {
            title: 'Pertinent Diagnostics',
            htmlContent: `<ul><li>Lorem ipsum dolor sit amet</li><li>Donec laoreet nonummy augue. Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc</li><li>Done laoreet nonummy augue. Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc</li></ul>`,
            comment: 'this is an existing comment',
            editComment: false,
          },
          {
            title: 'Management Points',
            htmlContent: `<b>Medical/Preoperative</b>
                          <ul>
                          <li>Lorem ipsum dolor sit amet</li>
                          <li>Donec laoreet nonummy augue. Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc</li>
                          <li>Done laoreet nonummy augue. Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc</li>
                          </ul>
                          <b>Operative Technical Details</b>
                          <ul>
                          <li>Lorem ipsum dolor sit amet</li>
                          <li>Donec laoreet nonummy augue. Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc</li>
                          <li>Done laoreet nonummy augue. Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc</li>
                          </ul>`,
            comment: undefined,
            editComment: false,
          },
        ],
      },
      {
        caseId: '38022',
        sections: [
          {
            title: 'Testing Points',
            htmlContent: `<ol><li>Lorem Ipsum</li><li>adipiscing elit</li><li>vitae mauris consequat</li></ol>`,
            comment: undefined,
            editComment: false,
          },
          {
            title: 'Stem',
            htmlContent: `<p>Donec blandit feugiat ligula. Done hendrerit, felis et imperdiet euismod, purus ipsum pretium metus, in lacinia nulla nisl eget sapien. Donec ut est in lectus consequat consequat. Etiam get dui. Aliquam erat volutpat. Sed at lorem in nunc porta tristique. Proin nec augue. Quisque aliquam tempor magna. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.</p>`,
            comment: undefined,
            editComment: false,
          },
          {
            title: 'Pertinent Diagnostics',
            htmlContent: `<ul><li>Lorem ipsum dolor sit amet</li><li>Donec laoreet nonummy augue. Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc</li><li>Done laoreet nonummy augue. Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc</li></ul>`,
            comment: 'this is an existing comment',
            editComment: false,
          },
          {
            title: 'Management Points',
            htmlContent: `<b>Medical/Preoperative</b>
                          <ul>
                          <li>Lorem ipsum dolor sit amet</li>
                          <li>Donec laoreet nonummy augue. Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc</li>
                          <li>Done laoreet nonummy augue. Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc</li>
                          </ul>
                          <b>Operative Technical Details</b>
                          <ul>
                          <li>Lorem ipsum dolor sit amet</li>
                          <li>Donec laoreet nonummy augue. Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc</li>
                          <li>Done laoreet nonummy augue. Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc</li>
                          </ul>`,
            comment: undefined,
            editComment: false,
          },
        ],
      },
      {
        caseId: '10110',
        sections: [
          {
            title: 'Testing Points',
            htmlContent: `<ol><li>Lorem Ipsum</li><li>adipiscing elit</li><li>vitae mauris consequat</li></ol>`,
            comment: undefined,
            editComment: false,
          },
          {
            title: 'Stem',
            htmlContent: `<p>Donec blandit feugiat ligula. Done hendrerit, felis et imperdiet euismod, purus ipsum pretium metus, in lacinia nulla nisl eget sapien. Donec ut est in lectus consequat consequat. Etiam get dui. Aliquam erat volutpat. Sed at lorem in nunc porta tristique. Proin nec augue. Quisque aliquam tempor magna. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.</p>`,
            comment: undefined,
            editComment: false,
          },
          {
            title: 'Pertinent Diagnostics',
            htmlContent: `<ul><li>Lorem ipsum dolor sit amet</li><li>Donec laoreet nonummy augue. Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc</li><li>Done laoreet nonummy augue. Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc</li></ul>`,
            comment: 'this is an existing comment',
            editComment: false,
          },
          {
            title: 'Management Points',
            htmlContent: `<b>Medical/Preoperative</b>
                          <ul>
                          <li>Lorem ipsum dolor sit amet</li>
                          <li>Donec laoreet nonummy augue. Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc</li>
                          <li>Done laoreet nonummy augue. Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc</li>
                          </ul>
                          <b>Operative Technical Details</b>
                          <ul>
                          <li>Lorem ipsum dolor sit amet</li>
                          <li>Donec laoreet nonummy augue. Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc</li>
                          <li>Done laoreet nonummy augue. Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc</li>
                          </ul>`,
            comment: undefined,
            editComment: false,
          },
        ],
      },
    ]);
  }

  handleChange(event: any, caseIndex: number) {
    let candidateCases = [] as any;
    this.cases.subscribe((cases) => {
      candidateCases = cases;
    });

    candidateCases[caseIndex]['scores'] = event.scoreValues;

    this.cases.next(candidateCases);

    // this.cases[caseIndex]['scores'] = event.scoreValues;
  }

  handleSave(caseIndex: number) {
    this.activeCase = this.activeCase + 1;
  }

  handleSubmit() {
    // console.log(this.cases);
  }
}
