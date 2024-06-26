export const ADD_EDIT_RECORD_FIELDS = [
  {
    label: 'Start Date',
    subLabel: '',
    value: '',
    required: true,
    minDate: null,
    maxDate: null,
    name: 'startDate',
    placeholder: 'MM/DD/YYYY',
    type: 'date',
    size: 'col-4',
    validators: {
      minDate: null,
      maxDate: null,
    },
  },
  {
    label: 'End Date',
    subLabel: '',
    required: true,
    minDate: null,
    maxDate: null,
    name: 'endDate',
    placeholder: 'MM/DD/YYYY',
    type: 'date',
    size: 'col-4',
    validators: {
      minDate: null,
      maxDate: null,
    },
  },
  {
    label: 'Week(s)',
    required: false,
    name: 'weeks',
    placeholder: '',
    readonly: true,
    type: 'text-readonly',
    size: 'col-4',
  },
  {
    label: 'Program Name',
    subLabel: '',
    required: true,
    name: 'programName',
    placeholder: 'Begin typing to search for a program...',
    type: 'autocomplete',
    size: 'col-6',
    options: [],
    filteredOptions: [],
  },
  {
    label: 'Affiliated Organization',
    subLabel: '',
    required: false,
    name: 'alternateInstitutionName',
    placeholder: 'Enter affiliated organization...',
    helpText:
      'This is only required if you could not find your program in the available list',
    type: 'text',
    size: 'col-6',
  },
  {
    label: 'Clinical Level',
    subLabel: '',
    required: true,
    name: 'clinicalLevelId',
    placeholder: 'Select clinical level...',
    type: 'select',
    size: 'col-6',
    options: [],
  },
  {
    label: 'Clinical Activity',
    subLabel: '',
    required: true,
    name: 'clinicalActivityId',
    placeholder: 'Select clinical activity...',
    type: 'grouped-select',
    size: 'col-6',
    options: [],
  },
  {
    label: 'Explain',
    subLabel: '',
    required: false,
    name: 'other',
    placeholder: 'Type your answer...',
    type: 'textarea',
    size: 'col-6',
  },
  {
    label: 'Description (Non-Surgical Only)',
    subLabel: '',
    required: false,
    name: 'nonSurgicalActivity',
    placeholder: 'Type your answer...',
    type: 'textarea',
    size: 'col-6',
  },
  {
    label: 'International Rotation',
    subLabel: '',
    value: '',
    required: true,
    name: 'isInternationalRotation',
    placeholder: 'Make a selection...',
    helpText:
      'If you selected yes please upload the ABS Approval letter under the training section',
    type: 'radio-group',
    size: 'col-6',
    options: [
      {
        label: 'Yes',
        value: true,
      },
      {
        label: 'No',
        value: false,
      },
    ],
  },
];
