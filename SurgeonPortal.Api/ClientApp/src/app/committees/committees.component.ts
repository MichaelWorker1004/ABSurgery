import { Component, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

@Component({
  selector: 'abs-committees',
  templateUrl: './committees.component.html',
  styleUrls: ['./committees.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  standalone: true,
})
export class CommitteesComponent {}
