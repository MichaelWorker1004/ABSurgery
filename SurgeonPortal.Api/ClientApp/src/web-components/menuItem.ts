export interface IMenuItem {
  display: string;
  displayKey?: string;
  action: string;
  icon?: string;
  feature?: string;
  children?: Array<IMenuItem>;
  allowedClaims?: Array<string>;
  order?: number;
}
