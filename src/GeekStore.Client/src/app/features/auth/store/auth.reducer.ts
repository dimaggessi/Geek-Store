import {createFeature, createReducer, on} from '@ngrx/store';
import {AuthStateInterface} from '../types/authState.interface';
import {authActions} from './auth.actions';

const initialState: AuthStateInterface = {
  isSubmitting: false,
  isLoading: false,
  user: undefined,
  errors: null,
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
      isSubmitting: false,
      user: action.user,
    })),
    on(authActions.registerFailure, (state, action) => ({
      ...state,
      isSubmitting: false,
      errors: action.errors,
    }))
  ),
});

// this feature is available on all application because it's provided in app.config.ts
export const {name: authFeatureKey, reducer: authReducer} = authFeature;
