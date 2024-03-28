import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { Observable } from 'rxjs';
import { Select, Store } from '@ngxs/store';
import { AuthSelectors } from './auth.selectors';
import { UntilDestroy } from '@ngneat/until-destroy';
import { UserClaims } from 'src/app/side-navigation/user-status.enum';

interface CanActivate {
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
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
export class AuthGuard implements CanActivate {
  @Select(AuthSelectors.slices.isAuthenticated) $isAuthenticated:
    | Observable<boolean>
    | undefined;
  @Select(AuthSelectors.slices.isPasswordReset) $isPasswordReset:
    | Observable<boolean>
    | undefined;
  @Select(AuthSelectors.claims) $claims: Observable<string[]> | undefined;
  constructor(private store: Store, private router: Router) {}

  // TODO: Explore using an async way to validate routes
  async test() {
    return 1;
  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    const requiredClaims = route.data['requiredClaims'] as string[];
    const requiredClaimsAtLeastOne = route.data[
      'requiredClaimsAtLeastOne'
    ] as string[];
    const isAuthenticated = this.store.selectSnapshot(
      AuthSelectors.slices.isAuthenticated
    );
    const userClaims = this.store.selectSnapshot(AuthSelectors.claims) ?? [];

    // first check if the user is authenticated
    if (isAuthenticated) {
      // if there are claims required for the route check them
      if (requiredClaims && requiredClaims.length > 0) {
        if (
          this.checkClaims(userClaims, requiredClaims) &&
          this.checkClaimsAtLeastOne(userClaims, requiredClaimsAtLeastOne)
        ) {
          // if the user has the required claims, allow the route
          return true;
        } else {
          // if the user does not have the required claims, redirect to unauthorized
          this.router.navigate(['/unauthorized']);
          return false;
        }
      } else {
        // if no claims are required, allow the route
        return true;
      }
    } else {
      // if not authenticated, redirect to login
      this.router.navigate(['/login'], {
        queryParams: { returnUrl: state.url },
      });
      return false;
    }
  }

  private checkClaims(userClaims: string[], requiredClaims: string[]) {
    return requiredClaims.every((claim) => userClaims.includes(claim));
  }

  private checkClaimsAtLeastOne(
    userClaims: string[],
    requiredClaimsAtLeastOne: string[]
  ) {
    const claimsWithOutUserPermission = userClaims.filter(
      (x) => x != UserClaims.user
    );

    // Check if there are any required claims to check against
    if (requiredClaimsAtLeastOne?.length > 0) {
      return requiredClaimsAtLeastOne.some((claim) =>
        claimsWithOutUserPermission.includes(claim)
      );
    }

    return true;
  }
}
