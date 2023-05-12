export interface IFormFields {
  label: string;
  subLabel?: string;
  value?: string;
  required: boolean;
  name: string;
  placeholder?: string;
  readonly?: boolean;
  type: string;
  size: string;
  options?: any[];
}
