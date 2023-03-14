export class IncrementValue {
  static readonly type = '[Simple Value] Increment value by one';
}

export class DecrementValue {
  static readonly type = '[Simple Value] Decrement value by one';
}

export class ResetValue {
  static readonly type = '[Simple Value] Reset value';
}

export class SetValue {
  static readonly type = '[Simple Value] Set value defined by user';
  constructor(public payload: number) {}
}

export class Save {
  // call to service to save
}
