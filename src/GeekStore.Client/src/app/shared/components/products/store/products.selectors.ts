import {createSelector} from '@ngrx/store';
import {ProductStateInterface} from '../types/productState.interface';

export const selectProductFeature = (state: {product: ProductStateInterface}) => state.product;

export const selectIsSubmitting = createSelector(
  selectProductFeature,
  (state) => state.isSubmitting
);

export const selectIsLoading = createSelector(selectProductFeature, (state) => state.isLoading);

export const selectApiErrors = createSelector(selectProductFeature, (state) => state.errors);

export const selectProductsPaginated = createSelector(
  selectProductFeature,
  (state) => state.products
);

export const selectProductById = createSelector(selectProductFeature, (state) => state.product);

export const selectBrands = createSelector(selectProductFeature, (state) => state.brands);

export const selectTypes = createSelector(selectProductFeature, (state) => state.types);

export const selectProductsAreLoaded = createSelector(
  selectProductFeature,
  (state) => !!state.products?.data?.length
);

export const selectBrandsAreLoaded = createSelector(
  selectProductFeature,
  (state) => !!state.brands?.brands.length
);

export const selectTypesAreLodaded = createSelector(
  selectProductFeature,
  (state) => !!state.types?.types.length
);
