import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Router, UrlTree } from '@angular/router';
import { Observable, map, skipWhile, take } from 'rxjs';
import { Select } from '@ngxs/store';
import { ApplicationSelectors } from './application.selectors';
import { IFeatureFlags } from './application.state';

interface CanActivate {
  canActivate(
    route: ActivatedRouteSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree;
}

@Injectable({
  providedIn: 'root',
})
export class FeatureToggleGuard implements CanActivate {
  @Select(ApplicationSelectors.slices.featureFlags)
  featureFlags$!: Observable<IFeatureFlags>;

  constructor(private router: Router) {}

  /**
   *
   * @param route
   * @returns
   */
  canActivate(
    route: ActivatedRouteSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    const requiredFeatures = route.data['requiredFeatures'] as string[];
    return this.featureFlags$.pipe(
      /**
       * Skip while the feature flags are not loaded
       */
      skipWhile((enabledFeatures) => !enabledFeatures),
      /**
       * Take one emission and complete the observable
       */
      take(1),
      /**
       * Map the enabled features to a boolean
       * If the required features are not empty, check if all required features are enabled
       * If there are no required features or if all required features are enabled, return true
       * else navigate to page not found
       */
      map((enabledFeatures) => {
        if (requiredFeatures && requiredFeatures.length > 0) {
          if (this.checkFeatures(enabledFeatures, requiredFeatures)) {
            return true;
          } else {
            this.router.navigate(['/page-not-found']);
            return false;
          }
        } else {
          return true;
        }
      })
    );
  }

  /**
   *
   * @param enabledFeatures
   * @param requiredFeatures
   * @returns
   */
  private checkFeatures(
    enabledFeatures: IFeatureFlags,
    requiredFeatures: string[]
  ) {
    return requiredFeatures.every(
      (feature) => enabledFeatures[feature as keyof IFeatureFlags]
    );
  }
}
