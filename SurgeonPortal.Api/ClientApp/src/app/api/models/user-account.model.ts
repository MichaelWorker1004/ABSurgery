export interface IUser {
  givenName: string;
  surName: string;
  title: string;
  locale: string;
  address: IAddress;
  email: string;
  confirmEmail: string;
  password: string;
  confirmPassword: string;
  mailingList: boolean;
  id?: string;
  readonly status: string;
}

export interface IAddress {
  street: string;
  city: string;
  state: string;
  postalCode: string;
}

export interface IUserAccountModel {
  user: IUser;
}
