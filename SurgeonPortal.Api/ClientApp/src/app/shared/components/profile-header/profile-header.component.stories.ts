import type { Meta, StoryObj } from '@storybook/angular';
import { applicationConfig, moduleMetadata } from '@storybook/angular';
import { DEFAULT_PROVIDERS } from 'src/stories/default-providers';
import { ProfileHeaderComponent } from './profile-header.component';

const meta: Meta<ProfileHeaderComponent> = {
  title: 'YTG/Profile Header',
  tags: ['autodocs'],
  component: ProfileHeaderComponent,
  render: (args: ProfileHeaderComponent) => ({
    props: {
      ...args,
    },
  }),
  decorators: [
    applicationConfig({
      providers: [...DEFAULT_PROVIDERS],
    }),
    moduleMetadata({
      imports: [ProfileHeaderComponent],
      declarations: [],
    }),
  ],
};

export default meta;
type Story = StoryObj<ProfileHeaderComponent>;

export const Default: Story = {
  args: {
    profilePicture: 'https://cdn-icons-png.flaticon.com/512/3135/3135715.png',
  },
};
