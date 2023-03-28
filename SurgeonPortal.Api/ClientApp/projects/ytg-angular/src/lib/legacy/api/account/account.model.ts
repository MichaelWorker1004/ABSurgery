export class Account {
  public userId: number;
  public userName: string;
  public email: string;
  public roles: string[];
}

export class DefaultAccount extends Account {}
