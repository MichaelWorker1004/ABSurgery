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
  args: {},
};
