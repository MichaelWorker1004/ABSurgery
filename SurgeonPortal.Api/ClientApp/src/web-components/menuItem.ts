export interface IMenuItem {
  display: string;
  action: string;
  icon?: string;
  feature?: string;
  children?: Array<IMenuItem>;
}
