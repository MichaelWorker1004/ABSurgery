import type { Meta, StoryObj } from '@storybook/angular';
import { applicationConfig, moduleMetadata } from '@storybook/angular';
import { DEFAULT_PROVIDERS } from 'src/stories/default-providers';
import { HighlightCardComponent } from './highlight-card.component';

const meta: Meta<HighlightCardComponent> = {
  title: 'Components/Highlight Card',
  tags: ['autodocs'],
  component: HighlightCardComponent,
  render: (args: HighlightCardComponent) => ({
    props: {
      ...args,
    },
  }),
  decorators: [
    applicationConfig({
      // Provides necesarry utilities for PrimeNG components
      providers: [...DEFAULT_PROVIDERS],
    }),
    moduleMetadata({
      imports: [HighlightCardComponent],
      declarations: [],
    }),
  ],
};

export default meta;
type Story = StoryObj<HighlightCardComponent>;

export const Default: Story = {
  args: {
    title: "Action Card's Title",
    content: 'This is some cool description of what this card does',
    image:
      'https://images.pexels.com/photos/13548722/pexels-photo-13548722.jpeg',
  },
};

export const AsAlert: Story = {
  args: {
    ...Default.args,
    alert: true,
  },
};

export const WithAction: Story = {
  args: {
    ...Default.args,
    actionText: 'Click here!',
    actionType: 'action',
    actionAction: 'action',
  },
};
