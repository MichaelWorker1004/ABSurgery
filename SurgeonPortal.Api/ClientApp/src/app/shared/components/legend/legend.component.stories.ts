import type { Meta, StoryObj } from '@storybook/angular';
import { moduleMetadata } from '@storybook/angular';

import { LegendComponent } from './legend.component';

const meta: Meta<LegendComponent> = {
  title: 'Components/Legend',
  tags: ['autodocs'],
  component: LegendComponent,
  render: (args: LegendComponent) => ({
    props: {
      ...args,
    },
  }),
  decorators: [
    moduleMetadata({
      imports: [LegendComponent],
      declarations: [],
    }),
  ],
};

export default meta;
type Story = StoryObj<LegendComponent>;

export const Default: Story = {
  args: {
    legendItems: [
      {
        text: 'Not Submitted',
        color: '#7f7f7f',
      },
      {
        text: 'Current Session',
        color: '#dbad6a',
      },
      {
        text: 'Submitted',
        color: '#1c827d',
      },
    ],
  },
};
