import type { Meta, StoryObj } from '@storybook/angular';
import { UnauthorizedComponent } from './unauthorized.component';

// More on how to set up stories at: https://storybook.js.org/docs/angular/writing-stories/introduction
const meta: Meta<UnauthorizedComponent> = {
  title: 'Pages/Unauthorized',
  component: UnauthorizedComponent,
  tags: ['autodocs'],
  render: (args: UnauthorizedComponent) => ({
    props: {
      ...args,
    },
  }),
};

export default meta;
type Story = StoryObj<UnauthorizedComponent>;

// More on writing stories with args: https://storybook.js.org/docs/angular/writing-stories/args
export const Default: Story = {
  args: {},
};
