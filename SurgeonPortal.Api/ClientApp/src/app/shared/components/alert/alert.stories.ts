import {
  componentWrapperDecorator,
  type Meta,
  type StoryObj,
} from '@storybook/angular';
import { AlertComponent } from './alert.component';

export interface AlertComponentArgs extends AlertComponent {
  alertContent: string;
}

// More on how to set up stories at: https://storybook.js.org/docs/angular/writing-stories/introduction
const meta: Meta<AlertComponentArgs> = {
  title: 'Components/Alert',
  component: AlertComponent,
  tags: ['autodocs'],
  decorators: [
    componentWrapperDecorator(
      //  using the contentWrapper allows you to define content that would appear in an <ng-content> tag
      (AlertComponent) => `${AlertComponent}`
    ),
  ],
  render: (args: AlertComponentArgs) => ({
    // The template in the render method can access component args that need to be rendered outside of the component tag
    // this allows the support of slot based content projection
    template: `<abs-alert [alertType]="alertType" [hideIcon]="hideIcon" [fontSize]="fontSize">
      <div [innerHTML]="'${args.alertContent}'"></div>
    </abs-alert>`,
    props: {
      ...args,
    },
  }),
};

export default meta;
type Story = StoryObj<AlertComponentArgs>;

// More on writing stories with args: https://storybook.js.org/docs/angular/writing-stories/args
export const Default: Story = {
  args: {
    alertContent: 'This is an alert',
    alertType: 'success',
    hideIcon: false,
    fontSize: 1.25,
  },
};

export const SuccessAlert: Story = {
  args: {
    ...Default.args,
    alertType: 'success',
  },
};

export const InfoAlert: Story = {
  args: {
    ...Default.args,
    alertType: 'info',
  },
};

export const WarningAlert: Story = {
  args: {
    ...Default.args,
    alertType: 'warning',
  },
};

export const DangerAlert: Story = {
  args: {
    ...Default.args,
    alertType: 'danger',
  },
};

export const HideIcon: Story = {
  args: {
    ...Default.args,
    hideIcon: true,
  },
};

export const LargeFont: Story = {
  args: {
    ...Default.args,
    fontSize: 2,
  },
};

export const SmallFont: Story = {
  args: {
    ...Default.args,
    fontSize: 0.75,
  },
};

export const HTMLContent: Story = {
  args: {
    ...Default.args,
    alertContent: 'This is an alert with <i>HTML</i> content',
  },
};
