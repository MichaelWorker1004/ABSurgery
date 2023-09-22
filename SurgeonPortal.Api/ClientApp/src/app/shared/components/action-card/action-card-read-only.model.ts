export interface IActionCardReadOnlyModel {
  title: string;
  titleKey?: string;
  description: string;
  descriptionKey?: string;
  actionType?: string;
  actionAction?: string;
  actionStyle?: number;
  action?: {
    type?: string;
    action?: string;
  };
  actionDisplay: string;
  actionDisplayKey?: string;
  icon: string;
  status?: string;
  disabled?: boolean;
}
