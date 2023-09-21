export interface IActionCardReadOnlyModel {
  title: string;
  titleKey?: string;
  description: string;
  descriptionKey?: string;
  action?: {
    type: string;
    action?: string;
  };
  actionDisplay: string;
  actionDisplayKey?: string;
  icon: string;
  status?: string;
  disabled?: boolean;
}
