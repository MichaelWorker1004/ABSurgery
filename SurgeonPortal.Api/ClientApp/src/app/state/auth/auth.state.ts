import { Injectable } from '@angular/core';
import { catchError, tap } from 'rxjs';
import { Action, State, StateContext, StateToken, Store } from '@ngxs/store';
import {
  Login,
  Logout,
  ClearAuthErrors,
  RefreshToken,
  ResetPassword,
  ForgotUsername,
  ForgotPassword,
  ResetForgotPassword,
} from './auth.actions';
import { AuthService, IAppUserReadOnlyModel } from '../../api';
import {
  AuthStateModel,
  IAuthState,
  IError,
  IAuthCredentials,
  IRefreshToken,
} from './auth.interfaces';
import { GlobalDialogService } from 'src/app/shared/services/global-dialog.service';

/**
 * The state token for the auth state
 */
export const AUTH_STATE_TOKEN = new StateToken<IAuthState>('auth');

@State<IAuthState>({
  name: AUTH_STATE_TOKEN,
  defaults: {
    access_token: null,
    refresh_token: null,
    token_type: null,
    userName: null,
    expiration: null,
    expires_in_minutes: null,
    user: null,
    claims: null,
    errors: null,
    forgotPasswordErrors: null,
    isBusy: false,
    isPasswordReset: false,
    passwordResetComplete: false,
    isAuthenticated: false,
  },
})
@Injectable()
export class AuthState {
  /**
   * Timer for refreshing the token
   */
  refreshTimer: any;
  /**
   * The login parameters
   */
  loginParams: IAuthCredentials | null = null;
  /**
   * Parse the JWT token to get the claims
   * @param token
   */
  static parseJwt(token: string): { claims: string[] } {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(
      window
        .atob(base64)
        .split('')
        .map(function (c) {
          return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        })
        .join('')
    );

    const returnObj = JSON.parse(jsonPayload);
    returnObj.claims =
      returnObj['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

    return returnObj;
  }

  constructor(
    private authService: AuthService,
    private store: Store,
    private globalDialogService: GlobalDialogService
  ) {}

  /**
   * Login to the application
   * @param ctx
   * @param action
   */
  @Action(Login)
  login(ctx: StateContext<IAuthState>, action: Login) {
    if (
      this.loginParams?.userName === action.payload.userName &&
      this.loginParams?.password === action.payload.password
    ) {
      return;
    }
    this.loginParams = action.payload;
    ctx.patchState({ isBusy: true, errors: null }); // Reset errors and set busy
    return this.authService.login(action.payload).pipe(
      tap((result: AuthStateModel | IError | string) => {
        if (this.isAuthStateModel(result as unknown as IAuthState | IError)) {
          const state = ctx.getState();
          const res = result as AuthStateModel;
          ctx.setState({
            ...state,
            ...res,
            claims: AuthState.parseJwt(<string>res.access_token).claims,
            errors: null,
            forgotPasswordErrors: null,
            isBusy: false,
            isPasswordReset: res.user?.resetRequired ?? false,
            isAuthenticated: true,
          });
          this.loginParams = null;
          if (res.user?.resetRequired) {
            this.handleResetPassword(ctx);
            return;
          } else {
            this.handleAuthSuccess(res, action);
          }
        } else {
          this.loginParams = null;
          this.handleAuthError(result, ctx);
        }
      })
    );
  }

  /**
   * Refresh the token
   * @param ctx
   * @param payload
   */
  @Action(RefreshToken)
  refreshToken(
    ctx: StateContext<IAuthState>,
    payload?: { refreshToken: string }
  ) {
    let refreshToken: IRefreshToken = {
      refreshToken: ctx.getState()?.refresh_token ?? '',
    };
    if (payload?.refreshToken) {
      refreshToken = payload;
    }
    return this.authService.refreshToken(refreshToken).pipe(
      tap((result: AuthStateModel | IError) => {
        // eslint-disable-next-line no-prototype-builtins
        if (!result.hasOwnProperty('status')) {
          const state = ctx.getState();
          const res = result as AuthStateModel;
          ctx.setState({
            ...state,
            ...result,
            claims: AuthState.parseJwt(<string>res.access_token).claims,
            errors: null,
            forgotPasswordErrors: null,
          });
          // if (res.expires_in_minutes) {
          //   this.setRefreshTimer(res.expires_in_minutes);
          // }
        }
      })
    );
  }

  /**
   * Reset the password
   * @param ctx
   * @param action
   */
  @Action(ResetPassword)
  resetPassword(ctx: StateContext<IAuthState>, action: ResetPassword) {
    ctx.patchState({ isBusy: true, errors: null }); // Reset errors and set busy
    return this.authService.resetPassword(action.payload).pipe(
      tap((result: boolean | IError) => {
        if (this.isLoginError(result)) {
          this.handlePasswordResetError(result, ctx);
        } else {
          ctx.patchState({
            isBusy: false,
            passwordResetComplete: true,
            errors: null,
            forgotPasswordErrors: null,
          });
        }
      })
    );
  }

  @Action(ForgotUsername)
  forgotUsername(ctx: StateContext<IAuthState>, payload: ForgotUsername) {
    return this.authService.forgotUsername(payload.model).pipe(
      tap(() => {
        this.globalDialogService.showSuccessError(
          'Request Sent',
          'You will receive an email if there is a username associated with the provided email',
          true
        );
      }),
      catchError((err: any) => {
        this.globalDialogService.showSuccessError('error', 'Error', false);
        return err;
      })
    );
  }

  @Action(ForgotPassword)
  forgotPassword(ctx: StateContext<IAuthState>, payload: ForgotPassword) {
    return this.authService.forgotPassword(payload.model).pipe(
      tap(() => {
        this.globalDialogService.showSuccessError(
          'Request Sent',
          'An email has been sent with a link to reset your password',
          true
        );
        ctx.patchState({
          forgotPasswordErrors: null,
        });
      }),
      catchError((err: any) => {
        this.globalDialogService.closeOpenDialog();
        ctx.patchState({
          forgotPasswordErrors: err,
        });
        return err;
      })
    );
  }

  @Action(ResetForgotPassword)
  resetForgotPassword(
    ctx: StateContext<IAuthState>,
    payload: ResetForgotPassword
  ) {
    return this.authService.resetForgotPassword(payload.model).pipe(
      tap(() => {
        this.globalDialogService.showSuccessError(
          'Password Reset',
          'Your password has been reset',
          true
        );
      }),
      catchError((err: any) => {
        this.globalDialogService.showSuccessError(
          'Error',
          'An error has occured resetting your password',
          false
        );
        return err;
      })
    );
  }

  /**
   * Logout of the application
   * @param ctx
   */
  @Action(Logout)
  logout(ctx: StateContext<IAuthState>) {
    if (this.refreshTimer) {
      clearTimeout(this.refreshTimer);
    }
    sessionStorage.clear();
    ctx.setState({
      access_token: '',
      refresh_token: '',
      token_type: '',
      userName: '',
      expiration: '',
      expires_in_minutes: 0,
      user: {} as IAppUserReadOnlyModel,
      claims: [],
      errors: null,
      forgotPasswordErrors: null,
      isBusy: false,
      isPasswordReset: false,
      passwordResetComplete: false,
      isAuthenticated: false,
    });
  }

  /**
   * Clear the errors
   * @param ctx
   */
  @Action(ClearAuthErrors)
  clearErrors(ctx: StateContext<IAuthState>) {
    ctx.patchState({
      errors: null,
      forgotPasswordErrors: null,
    });
  }

  /**
   * Set the refresh timer
   * @param expiresInMinutes
   * @private
   */
  private setRefreshTimer(expiresInMinutes = 0) {
    if (this.refreshTimer) {
      clearTimeout(this.refreshTimer);
    }
    let expires = expiresInMinutes;
    if (expires < 0) {
      expires = 0;
    }
    this.refreshTimer = setTimeout(() => {
      this.store.dispatch(
        new RefreshToken(
          this.store.selectSnapshot((state) => state.auth.refresh_token)
        )
      );
    }, expires * 60000);
  }

  /**
   * Handle the successful login
   * @param res
   * @param action
   * @private
   */
  private handleAuthSuccess(res: AuthStateModel, action: Login) {
    if (res.expires_in_minutes) {
      this.setRefreshTimer(res.expires_in_minutes);
    }
  }

  /**
   * Handle the reset password
   * @private
   */
  private handleResetPassword(ctx: StateContext<IAuthState>) {
    ctx.patchState({ isPasswordReset: true });
  }

  /**
   * Handle the error from the login
   * @param error
   * @param ctx
   * @private
   */
  private handleAuthError(
    error: AuthStateModel | IError | string,
    ctx: StateContext<IAuthState>
  ) {
    if (this.isLoginError(error)) {
      ctx.setState({
        access_token: '',
        refresh_token: '',
        token_type: '',
        userName: '',
        expiration: '',
        expires_in_minutes: 0,
        user: {} as IAppUserReadOnlyModel,
        claims: [],
        errors: <IError>error,
        forgotPasswordErrors: null,
        isBusy: false,
        isPasswordReset: false,
        passwordResetComplete: false,
        isAuthenticated: false,
      });
    } else {
      if (error === 'Login failed') {
        ctx.setState({
          access_token: '',
          refresh_token: '',
          token_type: '',
          userName: '',
          expiration: '',
          expires_in_minutes: 0,
          user: {} as IAppUserReadOnlyModel,
          claims: [],
          errors: {
            type: 'Login failed',
            title: 'Login failed',
            status: 400,
            traceId: '',
            errors: null,
          },
          forgotPasswordErrors: null,
          isBusy: false,
          isPasswordReset: false,
          passwordResetComplete: false,
          isAuthenticated: false,
        });
      }
    }
  }

  /**
   * If there is an error while resetting the password
   * Handle it differently than a login error
   * @param error
   * @param ctx
   * @private
   */
  private handlePasswordResetError(
    error: IError,
    ctx: StateContext<IAuthState>
  ) {
    ctx.patchState({
      errors: error,
      isBusy: false,
    });
  }

  /**
   * Check the type of the object to see if it is an IError
   * @param test
   * @private
   */
  private isLoginError(test: any): test is IError {
    return (<IError>test).type !== undefined;
  }

  /**
   * Check the type of the object to see if it is an IAuthState
   * @param test
   * @private
   */
  private isAuthStateModel(test: IAuthState | IError): test is IAuthState {
    return (<IAuthState>test).access_token !== undefined;
  }
}
