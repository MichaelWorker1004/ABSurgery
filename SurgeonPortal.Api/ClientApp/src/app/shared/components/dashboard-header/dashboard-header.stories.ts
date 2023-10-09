import {
  moduleMetadata,
  type Meta,
  type StoryObj,
  applicationConfig,
} from '@storybook/angular';
import { DashboardHeaderComponent } from './dashboard-header.component';

import { DEFAULT_PROVIDERS } from 'src/stories/default-providers';

// More on how to set up stories at: https://storybook.js.org/docs/angular/writing-stories/introduction
const meta: Meta<DashboardHeaderComponent> = {
  title: 'Components/Dashboard-Header',
  component: DashboardHeaderComponent,
  tags: ['autodocs'],
  render: (args: DashboardHeaderComponent) => ({
    props: {
      ...args,
    },
  }),
  decorators: [
    applicationConfig({
      providers: [...DEFAULT_PROVIDERS],
    }),
    moduleMetadata({
      imports: [DashboardHeaderComponent],
      declarations: [],
    }),
  ],
};

export default meta;
type Story = StoryObj<DashboardHeaderComponent>;

// More on writing stories with args: https://storybook.js.org/docs/angular/writing-stories/args
export const Default: Story = {
  args: {
    showSearch: true,
    headerLinks: [
      {
        display: 'News',
        action: 'https://www.absurgery.org/default.jsp?news_home_mb',
      },
      {
        display: 'EPAs',
        action: 'https://www.absurgery.org/default.jsp?epahome',
      },
      {
        display: 'About',
        action: 'https://www.absurgery.org/default.jsp?abouthome',
      },
      {
        display: 'Contact',
        action: 'https://www.absurgery.org/default.jsp?aboutcontact',
      },
    ],
  },
};

export const CustomLinks: Story = {
  args: {
    ...Default.args,
    headerLinks: [
      {
        display: 'Google',
        action: 'https://www.google.com',
      },
      {
        display: 'YouTube',
        action: 'https://www.youtube.com',
      },
    ],
  },
};

export const NoSearch: Story = {
  args: {
    ...Default.args,
    showSearch: false,
  },
};
