import {
  componentWrapperDecorator,
  type Meta,
  type StoryObj,
} from '@storybook/angular';
import { CollapsePanelComponent } from './collapse-panel.component';

interface CollapsePanelComponentArgs extends CollapsePanelComponent {
  panelTitle: string;
  panelContent: string;
}

// More on how to set up stories at: https://storybook.js.org/docs/angular/writing-stories/introduction
const meta: Meta<CollapsePanelComponentArgs> = {
  title: 'YTG/Collapse-Panel',
  tags: ['autodocs'],
  component: CollapsePanelComponent,
  decorators: [
    componentWrapperDecorator(
      //  using the contentWrapper allows you to define content that would appear in an <ng-content> tag
      (CollapsePanelComponent) => `${CollapsePanelComponent}`
    ),
  ],
  render: (args: CollapsePanelComponentArgs) => ({
    // The template in the render method can access component args that need to be rendered outside of the component tag
    // this allows the support of slot based content projection
    template: `<abs-collapse-panel [panelId]="panelId" [startExpanded]="startExpanded">
      <h5 class="mt-0 mb-2" panel-header>${args.panelTitle}</h5>
      <div [innerHTML]="'${args.panelContent}'"></div>
    </abs-collapse-panel>`,
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
    panelTitle: 'Panel Title',
    panelContent: 'Panel Content<br><br>More content.',
  },
};

export const StartOpen: Story = {
  args: {
    ...Default.args,
    panelId: 2,
  },
};

export const StartClosed: Story = {
  args: {
    ...Default.args,
    panelId: 3,
    startExpanded: false,
  },
};
