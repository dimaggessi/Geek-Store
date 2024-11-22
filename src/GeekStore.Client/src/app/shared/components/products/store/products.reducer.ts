import {createFeature, createReducer, on} from '@ngrx/store';
import {ProductStateInterface} from '../types/productState.interface';
import {productActions} from './products.actions';

const initialState: ProductStateInterface = {
  isSubmitting: false,
  isLoading: false,
  data: null,
  errors: null,
};

const productFeature = createFeature({
  name: 'product',
  reducer: createReducer(
    initialState,
    on(productActions.getProductsList, (state) => ({...state, isSubmitting: true, errors: null})),
    on(productActions.getProductsListSuccess, (state, action) => ({
      ...state,
      isSubmitting: false,
      errors: null,
      data: action.result,
    })),
    on(productActions.getProductsListFailure, (state, action) => ({
      ...state,
      isSubmitting: false,
      data: null,
      errors: action.errors,
    }))
  ),
});

export const {name: productFeatureKey, reducer: productReducer} = productFeature;
