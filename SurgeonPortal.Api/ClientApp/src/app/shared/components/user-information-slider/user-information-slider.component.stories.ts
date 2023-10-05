import type { Meta, StoryObj } from '@storybook/angular';
import { applicationConfig, moduleMetadata } from '@storybook/angular';
import { DEFAULT_PROVIDERS } from 'src/stories/default-providers';
import { UserInformationSliderComponent } from './user-information-slider.component';

const meta: Meta<UserInformationSliderComponent> = {
  title: 'ABS/User Information Slider',
  tags: ['autodocs'],
  component: UserInformationSliderComponent,
  render: (args: UserInformationSliderComponent) => ({
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
      imports: [UserInformationSliderComponent],
      declarations: [],
    }),
  ],
};

export default meta;
type Story = StoryObj<UserInformationSliderComponent>;

export const Default: Story = {
  args: {
    sliderData: [
      {
        firstName: 'John',
        lastName: 'Doe',
        sessionNumber: 1,
        startTime: '09:00:00',
        endTime: '10:00:00',
      },
      {
        firstName: 'Jane',
        lastName: 'Doe',
        sessionNumber: 2,
        startTime: '10:00:00',
        endTime: '11:00:00',
      },
      {
        firstName: 'Alex',
        lastName: 'Doe',
        sessionNumber: 3,
        startTime: '11:00:00',
        endTime: '12:00:00',
      },
      {
        firstName: 'Mary',
        lastName: 'Doe',
        sessionNumber: 4,
        startTime: '12:00:00',
        endTime: '01:00:00',
      },
      {
        firstName: 'Hannah',
        lastName: 'Doe',
        sessionNumber: 5,
        startTime: '01:00:00',
        endTime: '02:00:00',
      },
    ],
  },
};
