export interface IMenuItem {
  display: string;
  action: any;
  icon: string;
  children?: IMenuItem;
}
