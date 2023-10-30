import type { Meta, StoryObj } from '@storybook/angular';
import { GridComponent } from './grid.component';
import {
  GRID_DEFAULT_COLS,
  GRID_DEFAULT_COLS_EXPAND,
} from './defaults/grid-default-cols';
import { GRID_DEFAULT_DATA } from './defaults/grid-dafault-data';
import { DEFAULT_DROPDOWN_OPTIONS } from './defaults/grid-default-dropdown';
import {
  applicationConfig,
  componentWrapperDecorator,
  moduleMetadata,
} from '@storybook/angular';
import { DEFAULT_PROVIDERS } from 'src/stories/default-providers';

const meta: Meta<GridComponent> = {
  title: 'YTG/Grid',
  tags: ['autodocs'],
  component: GridComponent,
  render: (args: GridComponent) => ({
    template: `<abs-grid [data]="data" [columns]="columns" [title]="title" [noResultsMessage]="noResultsMessage" [showFilter]="showFilter" [filterOn]="filterOn" [filterType]="filterType" [filterOptions]="filterOptions" [noFilteredResultsMessage]="noFilteredResultsMessage" [pagination]="pagination" [currentPage]="currentPage" [itemsPerPage]="itemsPerPage" [expandTemplate]="expandedRow">
      <ng-template #expandedRow let-r>{{r.expandedRowContent}}</ng-template>
    </abs-grid>`,
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
      imports: [GridComponent],
      declarations: [],
    }),
    componentWrapperDecorator((GridComponent) => `${GridComponent}`),
  ],
};

export default meta;
type Story = StoryObj<GridComponent>;

export const Default: Story = {
  args: {
    data: GRID_DEFAULT_DATA,
    columns: GRID_DEFAULT_COLS,
    title: 'Grid Title',
    noResultsMessage: 'There are no results to display.',
    showFilter: false,
  },
};

export const Pagination: Story = {
  args: {
    ...Default.args,
    pagination: true,
    currentPage: 1,
    itemsPerPage: 3,
  },
};

export const TextFilter: Story = {
  args: {
    ...Default.args,
    showFilter: true,
    filterOn: 'name',
    filterType: 'text',
    noFilteredResultsMessage: 'There are no results to display.',
  },
};

export const DrowdownFilter: Story = {
  args: {
    ...Default.args,
    showFilter: true,
    filterOn: 'title',
    filterType: 'dropdown',
    filterOptions: DEFAULT_DROPDOWN_OPTIONS,
    noFilteredResultsMessage: 'There are no results to display.',
  },
};

export const ExpandRow: Story = {
  args: {
    ...Default.args,
    expandTemplate: 'expandedRow',
    columns: GRID_DEFAULT_COLS_EXPAND,
  },
};
