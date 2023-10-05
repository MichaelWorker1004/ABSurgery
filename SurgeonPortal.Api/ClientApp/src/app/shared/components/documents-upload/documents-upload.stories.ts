import {
  moduleMetadata,
  type Meta,
  type StoryObj,
  applicationConfig,
} from '@storybook/angular';
import { DocumentsUploadComponent } from './documents-upload.component';

import { DOCUMENTS_COLS } from 'src/app/documents/documents-col';
import { DEFAULT_PROVIDERS } from 'src/stories/default-providers';

// More on how to set up stories at: https://storybook.js.org/docs/angular/writing-stories/introduction
const meta: Meta<DocumentsUploadComponent> = {
  title: 'Components/Documents-Upload',
  component: DocumentsUploadComponent,
  tags: ['autodocs'],
  render: (args: DocumentsUploadComponent) => ({
    props: {
      ...args,
    },
  }),
  decorators: [
    applicationConfig({
      providers: [...DEFAULT_PROVIDERS],
    }),
    moduleMetadata({
      imports: [DocumentsUploadComponent],
      declarations: [],
    }),
  ],
};

export default meta;
type Story = StoryObj<DocumentsUploadComponent>;

// More on writing stories with args: https://storybook.js.org/docs/angular/writing-stories/args
export const Default: Story = {
  args: {
    allowUpload: false,
    gridCols: DOCUMENTS_COLS,
    documentsData: [],
    dropdownOptions: undefined,
    filterOn: 'documentName',
    showFilter: true,
    userId: 1,
  },
};

export const NoFilter: Story = {
  args: {
    ...Default.args,
    showFilter: false,
  },
};

export const WithData: Story = {
  args: {
    ...Default.args,
    documentsData: [
      {
        documentName: 'Document 1',
        documentType: 'Type 1',
        uploadedDateUtc: new Date('2021-01-01'),
        uploadedBy: 'User 1',
        id: 1,
      },
      {
        documentName: 'Document 2',
        documentType: 'Type 2',
        uploadedDateUtc: new Date('2021-01-02'),
        uploadedBy: 'User 2',
        id: 2,
      },
    ],
  },
};

export const AllowUpload: Story = {
  args: {
    ...Default.args,
    allowUpload: true,
  },
};

export const AllowUploadWithOptions: Story = {
  args: {
    ...Default.args,
    allowUpload: true,
    dropdownOptions: [
      { itemDescription: 'Item 1', itemValue: 1 },
      { itemDescription: 'Item 2', itemValue: 2 },
      { itemDescription: 'Item 3', itemValue: 3 },
    ],
  },
};
