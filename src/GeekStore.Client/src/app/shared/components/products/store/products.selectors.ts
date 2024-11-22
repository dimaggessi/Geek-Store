import {createSelector} from '@ngrx/store';
import {ProductStateInterface} from '../types/productState.interface';

export const selectProductFeature = (state: {product: ProductStateInterface}) => state.product;

export const selectIsSubmitting = createSelector(
  selectProductFeature,
  (state) => state.isSubmitting
);

export const selectIsLoading = createSelector(selectProductFeature, (state) => state.isLoading);

export const selectApiErrors = createSelector(selectProductFeature, (state) => state.errors);

export const selectProductsPaginated = createSelector(selectProductFeature, (state) => state.data);
