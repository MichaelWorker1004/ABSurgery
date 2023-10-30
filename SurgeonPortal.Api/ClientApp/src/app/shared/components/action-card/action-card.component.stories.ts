import type { Meta, StoryObj } from '@storybook/angular';
import { applicationConfig, moduleMetadata } from '@storybook/angular';
import { DEFAULT_PROVIDERS } from 'src/stories/default-providers';
import { ActionCardComponent } from './action-card.component';

const meta: Meta<ActionCardComponent> = {
  title: 'Components/Action Card',
  tags: ['autodocs'],
  component: ActionCardComponent,
  render: (args: ActionCardComponent) => ({
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
      imports: [ActionCardComponent],
      declarations: [],
    }),
  ],
};

export default meta;
type Story = StoryObj<ActionCardComponent>;

export const Default: Story = {
  args: {
    title: "Action Card's Title",
    description: 'This is some cool description of what this card does',
    actionDisplay: 'Click here!',
    icon: 'fa-solid fa-id-card-clip',
    actionType: 'dialog',
  },
};

export const DefaultWithTranslationKeys: Story = {
  args: {
    titleKey: 'DASHBOARD.ACTION_CARDS.CCR_TITLE',
    descriptionKey: 'DASHBOARD.ACTION_CARDS.CCR_SUBTITLE',
    actionDisplayKey: 'DASHBOARD.ACTION_CARDS.CCR_BTN',
    icon: 'fa-solid fa-id-card-clip',
    actionType: 'dialog',
  },
};

export const Disabled: Story = {
  args: {
    ...Default.args,
    disabled: true,
    actionDisplay: 'Disabled!',
  },
};

export const WithStatus: Story = {
  args: {
    ...Default.args,
    status: 'completed',
    displayStatusText: true,
  },
};

export const WithStatusWithoutText: Story = {
  args: {
    ...WithStatus.args,
    displayStatusText: false,
  },
};

export const StatusWithDate: Story = {
  args: {
    ...WithStatus.args,
    recievedOn: new Date(),
  },
};

export const WithButtonCTA: Story = {
  args: {
    ...Default.args,
    actionStyle: 'button',
  },
};

export const WithAllOptions: Story = {
  args: {
    ...Default.args,
    ...StatusWithDate.args,
    ...WithButtonCTA.args,
  },
};
