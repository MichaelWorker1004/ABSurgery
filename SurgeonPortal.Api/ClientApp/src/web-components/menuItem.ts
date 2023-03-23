export interface IMenuItem {
  display: string;
  action: string;
  icon: string;
  children?: IMenuItem;
}
