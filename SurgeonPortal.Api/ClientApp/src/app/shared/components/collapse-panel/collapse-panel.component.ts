import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'abs-collapse-panel',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './collapse-panel.component.html',
  styleUrls: ['./collapse-panel.component.scss'],
})
export class CollapsePanelComponent implements OnInit {
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
   * whether or not to show the +/- icon on the panel header
   * @type {boolean}
   */
  @Input() showIcon = true;

  /**
   * an event to let the parent know when the panel has been expanded or collapsed
   * @type {EventEmitter<any>}
   */
  @Output() togglePanelEvent = new EventEmitter();

  ngOnInit() {
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
    if (panel?.classList.contains('active')) {
      this.togglePanelEvent.emit({ panelId: this.panelId, expanded: true });
    } else {
      this.togglePanelEvent.emit({ panelId: this.panelId, expanded: false });
    }
    if (panelBody!.style.maxHeight && panelBody!.style.maxHeight !== '0px') {
      // reset the panel maxHeight since the css animation will not work with a maxHeight = unset
      panelBody!.style.maxHeight = panelBody!.scrollHeight + 'px';
      setTimeout(() => {
        // set the height to 0 to trigger the collapse animation
        panelBody!.style.maxHeight = '0px';
      }, 0);
    } else {
      // set the maxHeight to the scrollHeight to allow for the animation to expand the panel
      panelBody!.style.maxHeight = panelBody!.scrollHeight + 'px';
      setTimeout(() => {
        // set the maxHeight to unset after the animation to account for future content changes
        panelBody!.style.maxHeight = 'unset';
      }, 501);
    }
  }
}
