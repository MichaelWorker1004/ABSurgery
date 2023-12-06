export interface IFormFields {
  label: string;
  subLabel?: string;
  helpText?: string;
  value?: string;
  required: boolean;
  name: string;
  placeholder?: string;
  readonly?: boolean;
  type: string;
  size: string;
  options?: any[];
  filteredOptions?: any[];
  validators?: any;
  errorText?: string;
  helpTextArray?: string[];
  overlapId?: number;
  disabled?: boolean;
}
