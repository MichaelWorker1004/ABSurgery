/* @typescript-eslint/ban-types: 0 */
import { AppStateService } from '../state.service';

export enum StateMode {
  Read,
  ReadWrite,
}

export function AppState(key?: string, mode = StateMode.Read) {
  // eslint-disable-next-line @typescript-eslint/ban-types
  return function (targetedClass: Object, propertyName: string) {
    decorator(key, propertyName, targetedClass, mode);
  };
}

function decorator(
  key: string,
  propertyName: string,
  // eslint-disable-next-line @typescript-eslint/ban-types
  targetedClass: Object,
  mode: StateMode
) {
  key = key || propertyName;
  Object.defineProperty(targetedClass, propertyName, {
    get: function () {
      return AppStateService.getValue(key);
    },
    set: function (value) {
      if (mode !== StateMode.ReadWrite) {
        throw new Error('This property is readonly');
      }
      AppStateService.setValue(key, value);
    },
  });
}
