import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  OnInit,
  //ViewEncapsulation,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { DropdownModule } from 'primeng/dropdown';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'abs-examination-rosters',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule,
    DropdownModule,
    InputTextareaModule,
    ButtonModule,
  ],
  templateUrl: './examination-rosters.component.html',
  styleUrls: ['./examination-rosters.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  //encapsulation: ViewEncapsulation.None,
})
export class ExaminationRostersComponent implements OnInit {
  selectedRoster: any = undefined;
  selectedCaseId: number | undefined = undefined;
  rosters: any = [];
  cases: any = [];

  selectedCaseDetails: any = undefined;

  ngOnInit(): void {
    // setTimeout(() => {
    //   this.getRosterList();
    //   this.getCaseList();
    // }, 5000);
    // this.getRosterList();
    this.getCaseList();
  }

  getCaseList() {
    this.cases = [
      {
        caseId: 38232,
        modifier: 'T',
      },
      {
        caseId: 38247,
        modifier: null,
      },
      {
        caseId: 38019,
        modifier: null,
      },
      {
        caseId: 38157,
        modifier: 'T',
      },
      {
        caseId: 38512,
        modifier: 'P',
      },
      {
        caseId: 38426,
        modifier: null,
      },
      {
        caseId: 38553,
        modifier: null,
      },
      {
        caseId: 38,
        modifier: 'T',
      },
      {
        caseId: 39,
        modifier: null,
      },
      {
        caseId: 40,
        modifier: null,
      },
      {
        caseId: 41,
        modifier: null,
      },
      {
        caseId: 42,
        modifier: null,
      },
    ];

    if (this.cases?.length > 0) {
      this.selectCase(this.cases[0].caseId);
    } else {
      this.selectedCaseId = undefined;
      this.selectedCaseDetails = undefined;
    }
  }

  // getRosterList() {
  //   // call store action to get roster list
  //   this.rosters = [
  //     {
  //       displayName: 'Day 1: Session 1 & 2 Roster D',
  //       cases: [
  //         {
  //           caseId: 38232,
  //           modifier: 'T',
  //         },
  //         {
  //           caseId: 38247,
  //           modifier: null,
  //         },
  //         {
  //           caseId: 38019,
  //           modifier: null,
  //         },
  //         {
  //           caseId: 38157,
  //           modifier: 'T',
  //         },
  //         {
  //           caseId: 38512,
  //           modifier: 'P',
  //         },
  //         {
  //           caseId: 38426,
  //           modifier: null,
  //         },
  //         {
  //           caseId: 38553,
  //           modifier: null,
  //         },
  //         {
  //           caseId: 38,
  //           modifier: 'T',
  //         },
  //         {
  //           caseId: 39,
  //           modifier: null,
  //         },
  //         {
  //           caseId: 40,
  //           modifier: null,
  //         },
  //         {
  //           caseId: 41,
  //           modifier: null,
  //         },
  //         {
  //           caseId: 42,
  //           modifier: null,
  //         },
  //       ],
  //     },
  //     {
  //       displayName: 'Day 1: Session 1 & 2 Roster E',
  //       cases: [
  //         {
  //           caseId: 38,
  //           modifier: 'T',
  //         },
  //         {
  //           caseId: 39,
  //           modifier: null,
  //         },
  //         {
  //           caseId: 40,
  //           modifier: null,
  //         },
  //       ],
  //     },
  //     {
  //       displayName: 'Day 1: Session 1 & 2 Roster E',
  //       cases: [],
  //     },
  //   ];

  //   if (this.rosters?.length > 0) {
  //     this.selectedRoster = this.rosters[0];
  //     if (this.selectedRoster.cases?.length > 0) {
  //       this.selectCase(this.rosters[0].cases[0].caseId);
  //     } else {
  //       this.selectedCaseId = undefined;
  //       this.selectedCaseDetails = undefined;
  //     }
  //   }
  // }

  selectRoster(event: any) {
    if (event.value?.cases?.length > 0) {
      this.selectCase(event.value.cases[0].caseId);
    } else {
      this.selectedCaseId = undefined;
      this.selectedCaseDetails = undefined;
    }
  }

  selectCase(caseId: number) {
    if (this.selectedCaseId !== caseId) {
      this.selectedCaseId = caseId;
      // call store action to get case details

      this.selectedCaseDetails = {
        caseId: caseId,
        modifier: null,
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
      };
    }
  }

  saveCaseComments() {
    // call store action to save case comments
    console.log('saveCaseComments');
    this.selectedCaseDetails.sections.forEach((section: any) => {
      section.editComment = false;
    });
  }
}
