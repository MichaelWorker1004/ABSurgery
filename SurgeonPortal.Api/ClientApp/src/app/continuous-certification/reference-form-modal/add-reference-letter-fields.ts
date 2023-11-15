export const ADD_REFERENCE_LETTER_FIELDS = [
  {
    label: 'Name of Authenticating Official',
    subLabel: '(Must be a Physician)',
    value: '',
    required: true,
    name: 'nameOfAuthenticatingOfficial',
    placeholder: 'Enter Official’s name',
    type: 'text',
    size: 'col-4',
  },
  {
    label: "Authenticating Official'/s Role",
    subLabel: '',
    value: '',
    required: true,
    name: 'authenticatingOfficialRole',
    placeholder: 'Choose their role',
    type: 'select',
    size: 'col-4',
    options: [
      {
        itemDescription: 'Chief of Staff',
        itemValue: 'chief',
      },
      {
        itemDescription: 'Medical Director',
        itemValue: 'medical',
      },
      {
        itemDescription: 'Program Director',
        itemValue: 'program',
      },
    ],
  },
  {
    label: 'Reason for Alternate Official',
    subLabel: '',
    value: '',
    required: false,
    name: 'reasonForAlternateOfficial',
    placeholder: 'Enter a reason',
    type: 'select',
    size: 'col-4',
    options: [
      {
        itemDescription: 'Option 1',
        itemValue: 'option1',
      },
      {
        itemDescription: 'Option 2',
        itemValue: 'option2',
      },
      {
        itemDescription: 'Option 3',
        itemValue: 'option3',
      },
    ],
  },
  {
    label: 'Authenticating Official’s Title',
    subLabel: '',
    value: '',
    required: false,
    name: 'authenticatingOfficialTitle',
    placeholder: 'Enter Official’s title',
    type: 'text',
    size: 'col-12',
  },
  {
    label: 'Authenticating Official’s Email Address',
    subLabel: '',
    value: '',
    required: true,
    name: 'authenticatingOfficialEmail',
    placeholder: 'Enter Official’s email address',
    type: 'text',
    size: 'col-4',
  },
  {
    label: 'Confirm Email Address',
    subLabel: '',
    value: '',
    required: true,
    name: 'confirmEmailAddress',
    placeholder: 'Enter Official’s email address again',
    type: 'text',
    size: 'col-4',
  },
  {
    label: 'Authenticating Official’s Phone Number',
    subLabel: '',
    value: '',
    required: true,
    name: 'authenticatingOfficialPhoneNumber',
    placeholder: '_ _ _ - _ _ _ - _ _ _ _',
    type: 'text',
    size: 'col-4',
  },
  {
    label: 'Name of Affiliated Institution',
    subLabel: '',
    value: '',
    required: false,
    name: 'nameOfAffiliatedInstitution',
    placeholder: 'Enter affiliated institution',
    type: 'text',
    size: 'col-12',
  },
  {
    label: 'City',
    subLabel: '',
    value: '',
    required: true,
    name: 'city',
    placeholder: 'Enter your city',
    type: 'text',
    size: 'col-4',
  },
  {
    label: 'State',
    subLabel: '',
    value: '',
    required: true,
    name: 'states',
    placeholder: 'Choose your state',
    type: 'select',
    size: 'col-4',
    options: [],
  },
  {
    label: 'Name',
    subLabel: '',
    value: '',
    required: false,
    readonly: true,
    name: 'name',
    placeholder: 'Enter your full name',
    type: 'text',
    size: 'col-12',
  },
];
