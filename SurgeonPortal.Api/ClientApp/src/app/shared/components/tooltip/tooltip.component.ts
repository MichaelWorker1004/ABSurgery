import {
  AfterViewInit,
  Component,
  ElementRef,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'abs-tooltip',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './tooltip.component.html',
  styleUrls: ['./tooltip.component.scss'],
})
export class TooltipComponent implements OnInit, AfterViewInit, OnChanges {
  @ViewChild('tooltip', { static: false }) tooltipRef!: ElementRef;
  @ViewChild('icon', { static: false }) iconRef!: ElementRef;

  @Input() tooltipText: string | undefined;
  @Input() position: 'top' | 'bottom' | 'left' | 'right' = 'top';
  @Input() containerId: string | undefined;

  localPosition: 'top' | 'bottom' | 'left' | 'right' = 'top';

  ngOnInit(): void {
    this.localPosition = this.position;
  }

  ngAfterViewInit(): void {
    this.adjustPosition();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['tooltipText']) {
      this.adjustPosition();
      //check that the adjusted position is still valid
      setTimeout(() => {
        this.adjustPosition();
      }, 100);
    }
  }

  adjustPosition() {
    const windowWidth = window.innerWidth;
    const windowHeight = window.innerHeight;

    const tooltipRect = this.tooltipRef.nativeElement.getBoundingClientRect();
    const iconRect = this.iconRef.nativeElement.getBoundingClientRect();

    if (iconRect.top - tooltipRect.height < 0) {
      //tooltip spills over top of page
      this.localPosition = 'bottom';
    } else if (iconRect.bottom + tooltipRect.height > windowHeight) {
      //tooltip spills over bottom of page
      this.localPosition = 'top';
    } else if (iconRect.left - tooltipRect.width < 0) {
      //tooltip spills over left of page
      this.localPosition = 'right';
    } else if (iconRect.right + tooltipRect.width > windowWidth) {
      //tooltip spills over right of page
      this.localPosition = 'left';
    }

    // TODO - allow the user to set a container value to check against instead of window
  }
}
