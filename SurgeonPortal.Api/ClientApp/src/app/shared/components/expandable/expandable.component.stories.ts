import {
  componentWrapperDecorator,
  type Meta,
  type StoryObj,
} from '@storybook/angular';
import { ExpandableComponent } from './expandable.component';

interface ExpandableComponentArgs extends ExpandableComponent {
  caseTitle: string;
  content: string;
}

const meta: Meta<ExpandableComponentArgs> = {
  title: 'ABS/Expandable',
  tags: ['autodocs'],
  component: ExpandableComponent,
  decorators: [
    componentWrapperDecorator(
      (ExpandableComponent) => `${ExpandableComponent}`
    ),
  ],
  render: (args: ExpandableComponentArgs) => ({
    template: `
      <abs-expandable [caseTitle]="caseTitle" [index]="index" [checked]="checked" [customTitle]="customTitle" [caseId]="caseId">
        <div>${args.content}</div>
      </abs-expandable>
    `,
    props: {
      ...args,
    },
  }),
};

export default meta;
type Story = StoryObj<ExpandableComponentArgs>;

export const Default: Story = {
  args: {
    caseTitle: 'Panel Title',
    index: 5,
    checked: true,
    content:
      '<strong>Lorem ipsum dolor</strong> sit amet, consectetur adipiscing elit. Fusce porta lacus et interdum varius. Nunc imperdiet lorem vitae lorem aliquam, ut consequat sapien dapibus. Morbi sodales consectetur varius. Cras vulputate rhoncus leo, facilisis imperdiet neque sodales quis. Etiam convallis placerat aliquam. Nunc faucibus nunc vitae lacus ullamcorper lobortis. Quisque euismod nec felis ultricies porttitor. Maecenas at orci bibendum, iaculis eros sed, aliquet mi. Aenean imperdiet purus et turpis aliquet, vel dictum lectus lacinia. Morbi eget sapien sed mi accumsan gravida. Quisque ac ligula lorem. Proin sagittis dui id tempus dignissim. Aliquam semper sapien eget libero malesuada commodo.',
  },
};

export const Closed: Story = {
  args: {
    ...Default.args,
    checked: false,
  },
};

export const CustomTitle: Story = {
  args: {
    ...Default.args,
    customTitle: 'This is a custom Title!',
  },
};
