import { of } from 'rxjs';

import { AppStateService } from '../state';
import { stateAuth, stateAccount } from '../state';

import { Account } from './account';

export function Authorize(roles?: string[]) {
  return function (
    targetedClass: Object,
    propertyName: string,
    descriptor: PropertyDescriptor
  ) {
    decorator(propertyName, propertyName, targetedClass, descriptor, roles);
  };
}

const unauthorizedError = <any>{
  success: false,
  errors: [
    {
      field: 'Unauthorized',
      errors: [' Please login again. '],
    },
  ],
};

const permissionsError = <any>{
  success: false,
  errors: [
    {
      field: 'AuthorizationError',
      errors: [' You do not have permissions to do this. '],
    },
  ],
};

function decorator(
  key: string,
  propertyName: string,
  targetedClass: Object,
  descriptor: PropertyDescriptor | undefined,
  roles?: string[]
): void {
  key = key || propertyName;
  if (descriptor === undefined) {
    descriptor = Object.getOwnPropertyDescriptor(targetedClass, key);
  }
  if (descriptor) {
    const originalMethod = descriptor.value;
    descriptor.value = function () {
      let authorized = AppStateService.getValue<boolean>(stateAuth);
      if (!authorized) {
        return of(unauthorizedError);
      }
      if (!checkRoles(roles)) {
        return of(permissionsError);
      }
      return originalMethod.apply(this, arguments);
    };
  }
}

function checkRoles(roles?: string[]): boolean {
  if (roles == null || roles.length === 0) {
    return true;
  }
  let account = AppStateService.getValue<Account>(stateAccount);

  // tslint:disable-next-line:forin
  for (const role in roles) {
    for (const checkRole in account.roles) {
      if (role.toLowerCase() === checkRole.toLowerCase()) {
        return true;
      }
    }
  }
  return false;
}
