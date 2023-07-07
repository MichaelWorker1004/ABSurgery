import type { Meta, StoryObj } from '@storybook/angular';
import { ExamTimerComponent } from './exam-timer.component';

// More on how to set up stories at: https://storybook.js.org/docs/angular/writing-stories/introduction
const meta: Meta<ExamTimerComponent> = {
  title: 'Components/Exam-Timer',
  component: ExamTimerComponent,
  tags: ['autodocs'],
  render: (args: ExamTimerComponent) => ({
    props: {
      ...args,
    },
  }),
};

export default meta;
type Story = StoryObj<ExamTimerComponent>;

// More on writing stories with args: https://storybook.js.org/docs/angular/writing-stories/args
export const Default: Story = {
  args: {
    timerLabel: 'Total',
    incrementLabel: 'Case',
    totalIncrements: 4,
    incrementDuration: 7,
    currentIncrement: 1,
  },
};

export const ShortTimer: Story = {
  args: {
    timerLabel: 'Total',
    incrementLabel: 'Case',
    totalIncrements: 4,
    incrementDuration: 0.25,
    currentIncrement: 1,
  },
};

export const StartInMiddle: Story = {
  args: {
    timerLabel: 'Total',
    incrementLabel: 'Case',
    totalIncrements: 4,
    incrementDuration: 7,
    currentIncrement: 3,
  },
};
