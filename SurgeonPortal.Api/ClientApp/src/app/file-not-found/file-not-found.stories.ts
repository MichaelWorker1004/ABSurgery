import type { Meta, StoryObj } from '@storybook/angular';
import { FileNotFoundComponent } from './file-not-found.component';

// More on how to set up stories at: https://storybook.js.org/docs/angular/writing-stories/introduction
const meta: Meta<FileNotFoundComponent> = {
  title: 'Pages/File-Not-Found',
  component: FileNotFoundComponent,
  tags: ['autodocs'],
  render: (args: FileNotFoundComponent) => ({
    props: {
      ...args,
    },
  }),
};

export default meta;
type Story = StoryObj<FileNotFoundComponent>;

// More on writing stories with args: https://storybook.js.org/docs/angular/writing-stories/args
export const Default: Story = {
  args: {},
};
