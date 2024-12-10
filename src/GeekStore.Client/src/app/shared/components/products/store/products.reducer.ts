import {createFeature, createReducer, on} from '@ngrx/store';
import {ProductStateInterface} from '../types/productState.interface';
import {brandActions, productActions, typesActions} from './products.actions';
import {getProductById} from './products.effects';

const initialState: ProductStateInterface = {
  isSubmitting: false,
  isLoading: true,
  product: null,
  products: null,
  errors: null,
  brands: null,
  types: null,
};

const productFeature = createFeature({
  name: 'product',
  reducer: createReducer(
    initialState,
    on(productActions.getProductsList, (state) => ({...state, isSubmitting: true, errors: null})),
    on(productActions.getProductsListSuccess, (state, action) => ({
      ...state,
      isLoading: false,
      isSubmitting: false,
      errors: null,
      products: action.result,
    })),
    on(productActions.getProductsListFailure, (state, action) => ({
      ...state,
      isLoading: false,
      isSubmitting: false,
      products: null,
      errors: action.errors,
    })),
    on(productActions.getProductById, (state) => ({...state, isSubmitting: true, errors: null})),
    on(productActions.getProductByIdSuccess, (state, action) => ({
      ...state,
      isLoading: false,
      isSubmitting: false,
      errors: null,
      product: action.product,
    })),
    on(productActions.getProductByIdFailure, (state, action) => ({
      ...state,
      isLoading: false,
      isSubmitting: false,
      product: null,
      erros: action.errors,
    })),
    on(brandActions.getBrandsList, (state) => ({...state, isSubmitting: true, errors: null})),
    on(brandActions.getBrandsSuccess, (state, action) => ({
      ...state,
      isSubmitting: false,
      errors: null,
      brands: action.result,
    })),
    on(brandActions.getBrandsFailure, (state, action) => ({
      ...state,
      isSubmitting: false,
      brands: null,
      errors: action.errors,
    })),
    on(typesActions.getTypesList, (state) => ({...state, isSubmitting: true, errors: null})),
    on(typesActions.getTypesSuccess, (state, action) => ({
      ...state,
      isSubmitting: false,
      errors: null,
      types: action.result,
    })),
    on(typesActions.getTypesFailure, (state, action) => ({
      ...state,
      isSubmitting: false,
      types: null,
      errors: action.errors,
    }))
  ),
});

export const {name: productFeatureKey, reducer: productReducer} = productFeature;
