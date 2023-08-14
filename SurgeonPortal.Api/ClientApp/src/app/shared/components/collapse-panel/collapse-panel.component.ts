import {
  Component,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { Subject } from 'rxjs';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

@UntilDestroy()
@Component({
  selector: 'abs-collapse-panel',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './collapse-panel.component.html',
  styleUrls: ['./collapse-panel.component.scss'],
})
export class CollapsePanelComponent implements OnInit, OnChanges {
  /**
   * the panel id, used to find the correct panel to expand/collapse when multiple are used on a page
   * @type {number}
   */
  @Input() panelId = 1;

  /**
   * the starting state of the panel, whether it is expanded or collapsed
   * @type {boolean}
   */
  @Input() startExpanded = false;

  /**
   * a bit flag that tells the component that the content within the panel has changed
   * and it should recalculate the height of the uncollapsed panel
   * @type {Subject<boolean>}
   */
  @Input() heightToggle: Subject<boolean> = new Subject();

  /**
   * TODO - implement this instead of the heightToggle
   * a bit flag that tells the component that the content within the panel has changed
   * and it should recalculate the height of the uncollapsed panel
   * @type {boolean}
   */
  @Input() contentToggle = false;

  ngOnInit() {
    this.heightToggle.pipe(untilDestroyed(this)).subscribe(() => {
      this.resetHeight();
    });
    if (this.startExpanded) {
      // setTimeout is needed to wait for the DOM to be ready
      setTimeout(() => this.togglePanel(), 0);
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['contentToggle']) {
      console.log('here');
      this.resetHeight();
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
