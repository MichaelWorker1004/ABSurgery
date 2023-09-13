import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Router, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { Store } from '@ngxs/store';
import { UntilDestroy } from '@ngneat/until-destroy';
import { ApplicationSelectors } from '../application/application.selectors';

interface CanActivate {
  canActivate(
    route: ActivatedRouteSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree;
}

@UntilDestroy()
@Injectable({
  providedIn: 'root',
})
export class FeatureToggleGuard implements CanActivate {
  constructor(private store: Store, private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    const requiredFeatures = route.data['requiredFeatures'] as string[];
    const enabledFeatures =
      this.store.selectSnapshot(ApplicationSelectors.slices.featureFlags) ?? {};

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
  }

  private checkFeatures(enabledFeatures: any, requiredFeatures: string[]) {
    return requiredFeatures.every((feature) => enabledFeatures[feature]);
  }
}
