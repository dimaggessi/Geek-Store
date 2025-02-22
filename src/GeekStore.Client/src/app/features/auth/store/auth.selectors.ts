import {createSelector} from '@ngrx/store';
import {AuthStateInterface} from '../types/authState.interface';

// state is a global state
// this feature select a slice of the global state
export const selectAuthFeature = (state: {auth: AuthStateInterface}) => state.auth;

export const selectIsSubmitting = createSelector(selectAuthFeature, (state) => state.isSubmitting);

export const selectIsLoading = createSelector(selectAuthFeature, (state) => state.isLoading);

export const selectUser = createSelector(selectAuthFeature, (state) => state.user);

export const selectIsAdmin = createSelector(selectAuthFeature, (state) => state.isAdmin);

export const selectApiErrors = createSelector(selectAuthFeature, (state) => state.errors);
