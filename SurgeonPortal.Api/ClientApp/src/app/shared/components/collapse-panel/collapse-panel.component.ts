import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Subject } from 'rxjs';

@Component({
  selector: 'abs-collapse-panel',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './collapse-panel.component.html',
  styleUrls: ['./collapse-panel.component.scss'],
})
export class CollapsePanelComponent implements OnInit {
  @Input() panelId!: number;
  @Input() startExpanded = false;
  @Input() heightToggle: Subject<boolean> = new Subject();

  ngOnInit() {
    this.heightToggle.subscribe(() => {
      this.resetHeight();
    });
    if (this.startExpanded) {
      // setTimeout is needed to wait for the DOM to be ready
      setTimeout(() => this.togglePanel(), 0);
    }
  }

  togglePanel() {
    const panel = document.querySelector<HTMLElement>('#panel-' + this.panelId);
    const panelBody = document.querySelector<HTMLElement>(
      '#panel-body-' + this.panelId
    );

    panel?.classList.toggle('active');
    if (panelBody!.style.maxHeight && panelBody!.style.maxHeight !== '0px') {
      panelBody!.style.maxHeight = '0px';
    } else {
      panelBody!.style.maxHeight = panelBody!.scrollHeight + 200 + 'px';
    }
  }

  resetHeight() {
    const panelBody = document.querySelector<HTMLElement>(
      '#panel-body-' + this.panelId
    );
    if (panelBody?.style.maxHeight && panelBody?.style.maxHeight !== '0px') {
      // setTimeout is needed to wait for the DOM to update with new content
      setTimeout(() => {
        panelBody!.style.maxHeight = panelBody!.scrollHeight + 200 + 'px';
      }, 0);
    }
  }
}
