export interface IActionCardReadOnlyModel {
  title: string;
  description: string;
  action?: {
    type: string;
    action?: string;
  };
  actionDisplay: string;
  icon: string;
  status?: string;
  disabled?: boolean;
}
