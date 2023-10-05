import type { Meta, StoryObj } from '@storybook/angular';
import { applicationConfig, moduleMetadata } from '@storybook/angular';
import { UserInformationCardComponent } from './user-information-card.component';
import { DEFAULT_PROVIDERS } from 'src/stories/default-providers';

const meta: Meta<UserInformationCardComponent> = {
  title: 'ABS/User Information Card',
  tags: ['autodocs'],
  component: UserInformationCardComponent,
  render: (args: UserInformationCardComponent) => ({
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
      imports: [UserInformationCardComponent],
      declarations: [],
    }),
  ],
};

export default meta;
type Story = StoryObj<UserInformationCardComponent>;

export const Default: Story = {
  args: {
    userInformation: [
      {
        certificateId: '9999',
        status: 'Active',
        speciality: 'Orthopaedic Surgery',
        initialCertificationDate:
          'Initial Certification Date: October 11, 2021',
        endDateDisplay: 'Next Assessment Due: December 31, 2026',
      },
      {
        certificateId: '1111',
        status: 'Active',
        speciality: 'Hand Surgery',
        initialCertificationDate:
          'Initial Certification Date: October 11, 2021',
        endDateDisplay: 'Next Assessment Due: December 31, 2026',
      },
    ],
    currentStatus: 'Certified',
    isSurgeon: true,
  },
};

export const NotCertified: Story = {
  args: {
    userInformation: {
      programDirector: 'Tim Yocum',
      programName: 'Angular Rockstars',
      city: 'Allentown',
      state: 'PA',
      clinicalLevel: 'The Best',
    },
    currentStatus: 'Trainee',
    isSurgeon: false,
  },
};
