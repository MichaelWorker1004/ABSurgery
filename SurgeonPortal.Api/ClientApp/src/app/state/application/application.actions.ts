export class LoadApplication {
  static readonly type = '[Application] Load the application';
}

export class SetUnsavedChanges {
  static readonly type = '[Application] toggle unsaved changes flag';

  constructor(public hasUnsavedChanges: boolean) {}
}

export class SetExamInProgress {
  static readonly type = '[Application] toggle exam in progress flag';

  constructor(public examInProgress: boolean) {}
}

export class CloseApplication {
  static readonly type = '[Application] Close the application';
}
