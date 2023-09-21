import type { Meta, StoryObj } from '@storybook/angular';
import { TooltipComponent } from './tooltip.component';

// More on how to set up stories at: https://storybook.js.org/docs/angular/writing-stories/introduction
const meta: Meta<TooltipComponent> = {
  title: 'Components/Tooltip',
  component: TooltipComponent,
  tags: ['autodocs'],
  render: (args: TooltipComponent) => ({
    props: {
      ...args,
    },
  }),
};

export default meta;
type Story = StoryObj<TooltipComponent>;

// More on writing stories with args: https://storybook.js.org/docs/angular/writing-stories/args
export const Default: Story = {
  args: {
    tooltipText: 'This is a tooltip',
    position: 'top',
  },
};

export const DislayBottom: Story = {
  args: {
    tooltipText: 'This is a tooltip',
    position: 'bottom',
  },
};

export const DislayLeft: Story = {
  args: {
    tooltipText: 'This is a tooltip',
    position: 'left',
  },
};

export const DislayRight: Story = {
  args: {
    tooltipText: 'This is a tooltip',
    position: 'right',
  },
};
