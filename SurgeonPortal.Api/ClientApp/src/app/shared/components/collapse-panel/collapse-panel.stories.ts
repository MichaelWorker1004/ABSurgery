import {
  componentWrapperDecorator,
  type Meta,
  type StoryObj,
} from '@storybook/angular';
import { CollapsePanelComponent } from './collapse-panel.component';
import { BehaviorSubject, of, Subject } from 'rxjs';

interface CollapsePanelComponentArgs extends CollapsePanelComponent {
  panelTitle: string;
  panelContent: string;
}

// More on how to set up stories at: https://storybook.js.org/docs/angular/writing-stories/introduction
const meta: Meta<CollapsePanelComponentArgs> = {
  title: 'Components/Collapse-Panel',
  component: CollapsePanelComponent,
  decorators: [
    componentWrapperDecorator(
      (CollapsePanelComponent) => `${CollapsePanelComponent}`
    ),
  ],
  tags: ['autodocs'],
  render: (args: CollapsePanelComponentArgs) => ({
    template: `<abs-collapse-panel [panelId]="${args.panelId}" [startExpanded]="${args.startExpanded}" [contentToggle]="${args.contentToggle}"><h5 class="mt-0 mb-2" panel-header>${args.panelTitle}</h5>${args.panelContent}</abs-collapse-panel>`,
    props: {
      ...args,
    },
  }),
};

export default meta;
type Story = StoryObj<CollapsePanelComponentArgs>;

// More on writing stories with args: https://storybook.js.org/docs/angular/writing-stories/args
export const Default: Story = {
  args: {
    panelId: 1,
    startExpanded: true,
    heightToggle: of(true) as Subject<boolean>,
    panelTitle: 'Panel Title',
    panelContent: 'Panel Content',
  },
};

export const StartOpen: Story = {
  args: {
    panelId: 2,
    startExpanded: true,
    heightToggle: new BehaviorSubject(true),
    contentToggle: false,
    panelTitle: 'Panel Title',
    panelContent: 'Panel Content',
  },
};

export const StartClosed: Story = {
  args: {
    panelId: 3,
    startExpanded: false,
    heightToggle: new BehaviorSubject(true),
    contentToggle: false,
    panelTitle: 'Panel Title',
    panelContent: 'Panel Content',
  },
};

// this story needs to be fixed by converting the observable to a boolean with an ngOnChanges
export const ContentHeightChanging: Story = {
  args: {
    panelId: 4,
    startExpanded: false,
    heightToggle: new BehaviorSubject(true),
    contentToggle: true,
    panelTitle: 'Panel Title',
    panelContent: 'Panel Content',
  },
};
