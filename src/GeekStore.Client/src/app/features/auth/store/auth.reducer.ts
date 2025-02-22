import {createFeature, createReducer, on} from '@ngrx/store';
import {AuthStateInterface} from '../types/authState.interface';
import {authActions} from './auth.actions';
import {routerNavigationAction} from '@ngrx/router-store';

const initialState: AuthStateInterface = {
  isLoggedIn: false,
  isSubmitting: false,
  isLoading: false,
  user: null,
  errors: null,
  isAdmin: false,
};

const authFeature = createFeature({
  name: 'auth',
  reducer: createReducer(
    initialState,
    on(authActions.register, (state) => ({
      ...state,
      isSubmitting: true,
      errors: null,
    })),
    on(authActions.registerSuccess, (state, action) => ({
      ...state,
      errors: null,
      isSubmitting: false,
      isLoggedIn: true,
      user: action.user,
    })),
    on(authActions.registerFailure, (state, action) => ({
      ...state,
      isSubmitting: false,
      errors: action.errors,
    })),
    on(authActions.login, (state) => ({
      ...state,
      isSubmitting: true,
      erros: null,
    })),
    on(authActions.loginSuccess, (state, action) => ({
      ...state,
      isLoggedIn: true,
      isSubmitting: false,
      user: action.user,
      isAdmin: action.user?.roles
        ? Array.isArray(action.user.roles)
          ? action.user.roles.includes('Admin')
          : action.user.roles === 'Admin'
        : false,
    })),
    on(authActions.loginFailure, (state, action) => ({
      ...state,
      isSubmitting: false,
      errors: action.errors,
    })),
    on(authActions.getUser, (state) => ({
      ...state,
      isLoading: true,
      errors: null,
    })),
    on(authActions.getUserSuccess, (state, action) => ({
      ...state,
      isLoading: false,
      user: action.user,
    })),
    on(authActions.getUserFailure, (state) => ({
      ...state,
      isLoading: false,
      user: null,
    })),
    on(authActions.logout, (state) => ({
      ...state,
      ...initialState,
      isLoggedIn: false,
      user: null,
    })),
    // ngrx router-store package (clean errors on navigation)
    on(routerNavigationAction, (state) => ({...state, errors: null}))
  ),
});

// this feature is available on all application because it's provided in app.config.ts
export const {name: authFeatureKey, reducer: authReducer} = authFeature;
