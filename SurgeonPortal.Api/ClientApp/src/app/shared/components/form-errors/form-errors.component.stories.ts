import type { Meta, StoryObj } from '@storybook/angular';
import { applicationConfig, moduleMetadata } from '@storybook/angular';
import { DEFAULT_PROVIDERS } from 'src/stories/default-providers';
import { FormErrorsComponent } from './form-errors.component';

const meta: Meta<FormErrorsComponent> = {
  title: 'YTG/Form Errors',
  tags: ['autodocs'],
  component: FormErrorsComponent,
  render: (args: FormErrorsComponent) => ({
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
      imports: [FormErrorsComponent],
      declarations: [],
    }),
  ],
};

export default meta;
type Story = StoryObj<FormErrorsComponent>;

export const Default: Story = {
  args: {
    errors: {
      name: ['Name is required'],
      email: ['Email is invalid'],
    },
  },
};
