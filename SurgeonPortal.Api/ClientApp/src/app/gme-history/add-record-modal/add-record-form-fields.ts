export const ADD_EDIT_RECORD_FIELDS = [
  {
    label: 'Start Date',
    subLabel: '',
    value: '',
    required: true,
    name: 'from',
    placeholder: 'MM/DD/YYYY',
    type: 'date',
    size: 'col-4',
  },
  {
    label: 'End Date',
    subLabel: '',
    required: true,
    name: 'to',
    placeholder: 'MM/DD/YYYY',
    type: 'date',
    size: 'col-4',
  },
  {
    label: 'Week(s)',
    required: false,
    name: 'weeks',
    placeholder: '',
    readonly: true,
    type: 'text',
    size: 'col-4',
  },
  {
    label: 'Program Name',
    subLabel: '',
    required: true,
    name: 'programName',
    placeholder: 'Type your answer...',
    type: 'text',
    size: 'col-6',
  },
  {
    label: 'Affiliated Organization',
    subLabel: '',
    required: true,
    name: 'affiliatedOrganization',
    placeholder: 'Type your answer...',
    type: 'text',
    size: 'col-6',
  },
  {
    label: 'Clinical Level',
    subLabel: '',
    required: true,
    name: 'clinicalLevel',
    placeholder: 'Type your answer...',
    type: 'text',
    size: 'col-12',
  },
  {
    label: 'Explain',
    subLabel: '',
    required: true,
    name: 'explain',
    placeholder: 'Type your answer...',
    type: 'textarea',
    size: 'col-6',
  },
  {
    label: 'Description (Non-Surgical Only)',
    subLabel: '',
    required: true,
    name: 'description',
    placeholder: 'Type your answer...',
    type: 'textarea',
    size: 'col-6',
  },
  {
    label: 'International Rotation',
    subLabel: '',
    value: '',
    required: true,
    name: 'internationalRotation',
    placeholder: 'Make a selection...',
    type: 'select',
    size: 'col-4',
    options: [
      {
        label: 'Yes',
        value: 'Yes',
      },
      {
        label: 'No',
        value: 'No',
      },
    ],
  },
];