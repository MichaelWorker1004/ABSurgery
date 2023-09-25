import {
  componentWrapperDecorator,
  type Meta,
  type StoryObj,
} from '@storybook/angular';
import { ModalComponent } from './modal.component';

interface ModalComponentArgs extends ModalComponent {
  modalContent: string;
  // openModal: (data: any) => void;
}

// More on how to set up stories at: https://storybook.js.org/docs/angular/writing-stories/introduction
const meta: Meta<ModalComponentArgs> = {
  title: 'Components/Modal',
  tags: ['autodocs'],
  component: ModalComponent,
  decorators: [
    componentWrapperDecorator(
      //  using the contentWrapper allows you to define content that would appear in an <ng-content> tag
      (ModalComponent) => `${ModalComponent}`
    ),
  ],
  render: (args: ModalComponentArgs) => ({
    // The template in the render method can access component args that need to be rendered outside of the component tag
    // this allows the support of slot based content projection
    template: `<p>Toggle the value of "open" in the inputs below to display the modal.</p>
    <abs-modal [open]="open" [title]="title" [status]="status" [modalName]="modalName" [preventOverlayClose]="preventOverlayClose" [hideClose]="hideClose" [width]="width">
      <div [innerHTML]="'${args.modalContent}'"></div>
    </abs-modal>`,
    props: {
      ...args,
    },
  }),
};

export default meta;
type Story = StoryObj<ModalComponentArgs>;

// More on writing stories with args: https://storybook.js.org/docs/angular/writing-stories/args
export const Default: Story = {
  args: {
    // openModal: (data: any) => {
    //   console.log(data);
    // },
    open: false,
    title: 'Modal Title',
    modalName: 'modalName',
    modalContent: 'Modal Content<br><br>More content.',
    width: 70,
  },
};

export const StatusComplete: Story = {
  args: {
    ...Default.args,
    status: 'completed',
  },
};

export const StatusInProgress: Story = {
  args: {
    ...Default.args,
    status: 'in-progress',
  },
};

export const StatusAlert: Story = {
  args: {
    ...Default.args,
    status: 'alert',
  },
};

export const PreventOverlayClose: Story = {
  args: {
    ...Default.args,
    preventOverlayClose: true,
  },
};

export const HideCloseButton: Story = {
  args: {
    ...Default.args,
    hideClose: true,
  },
};

export const WIdthSmall: Story = {
  args: {
    ...Default.args,
    width: 40,
  },
};

export const WidthLarge: Story = {
  args: {
    ...Default.args,
    width: 90,
  },
};
