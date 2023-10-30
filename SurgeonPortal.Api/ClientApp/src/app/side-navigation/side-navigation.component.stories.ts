import type { Meta, StoryObj } from '@storybook/angular';
import { applicationConfig, moduleMetadata } from '@storybook/angular';
import { SideNavigationComponent } from './side-navigation.component';
import { DEFAULT_PROVIDERS } from 'src/stories/default-providers';

const meta: Meta<SideNavigationComponent> = {
  title: 'YTG/Side Navigation',
  tags: ['autodocs'],
  component: SideNavigationComponent,
  render: (args: SideNavigationComponent) => ({
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
      imports: [SideNavigationComponent],
      declarations: [],
    }),
  ],
};

export default meta;
type Story = StoryObj<SideNavigationComponent>;

export const Default: Story = {
  args: {
    applicationName: 'The American Board Of Surgery',
    logoPath:
      'https://surgeonportaldev.azurewebsites.net/assets/img/abs-logo.svg',
    navItems: [
      {
        display: 'No Children',
        action: 'dashboard',
        icon: 'fa-solid fa-chart-line',
      },
      {
        display: 'Has Children',
        action: 'apply-and-resgister',
        icon: 'fa-solid fa-list-check',
        children: [
          {
            display: 'Registration Requirements',
            action: 'registration-requirements',
          },
          {
            display: 'Exam Registration',
            action: 'exam-registration',
          },
        ],
      },
    ],
  },
};
