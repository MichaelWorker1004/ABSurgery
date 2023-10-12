import {
  applicationConfig,
  moduleMetadata,
  type Meta,
  type StoryObj,
} from '@storybook/angular';
import { ExaminationScoreCardComponent } from './examination-score-card.component';
import { DEFAULT_PROVIDERS } from 'src/stories/default-providers';

// More on how to set up stories at: https://storybook.js.org/docs/angular/writing-stories/introduction
const meta: Meta<ExaminationScoreCardComponent> = {
  title: 'ABS/Examination-Score-Card',
  component: ExaminationScoreCardComponent,
  tags: ['autodocs'],
  render: (args: ExaminationScoreCardComponent) => ({
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
      imports: [ExaminationScoreCardComponent],
      declarations: [],
    }),
  ],
};

export default meta;
type Story = StoryObj<ExaminationScoreCardComponent>;

// More on writing stories with args: https://storybook.js.org/docs/angular/writing-stories/args
export const Default: Story = {
  args: {
    case: {
      caseNumber: '1',
      endTime: '10:00:00',
      examCaseId: 1,
      examDate: '2023-10-11T00:00:00',
      examineeFirstName: 'Joe',
      examineeLastName: 'Smith',
      examineeMiddleName: 'M',
      examineeSuffix: '',
      examineeUserId: 1,
      examinerUserId: 2,
      isLocked: false,
      startTime: '09:00:00',
    },
    locked: false,
  },
};

export const Locked: Story = {
  args: {
    ...Default.args,
    locked: true,
  },
};
