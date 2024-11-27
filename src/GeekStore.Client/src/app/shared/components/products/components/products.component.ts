import {GetProductsRequestInterface} from '@shared/components/products/types/getProductsRequest.interface';
import {CommonModule} from '@angular/common';
import {Component, inject, Input, OnChanges, OnInit} from '@angular/core';
import {ProductService} from '../services/product.service';
import {Store} from '@ngrx/store';
import {ProductStateInterface} from '../types/productState.interface';
import {brandActions, productActions, typesActions} from '../store/products.actions';
import {combineLatest, Observable} from 'rxjs';
import {
  selectApiErrors,
  selectBrands,
  selectIsLoading,
  selectIsSubmitting,
  selectProductsPaginated,
  selectTypes,
} from '../store/products.selectors';
import {Pagination} from '@shared/models/pagination.interface';
import {Product} from '@shared/models/product.interface';

@Component({
  standalone: true,
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrl: './products.component.scss',
  imports: [CommonModule],
})
export class ProductComponent implements OnInit, OnChanges {
  store = inject(Store<{product: ProductStateInterface}>);
  productService = inject(ProductService);
  data$!: Observable<ProductStateInterface>;
  @Input() brandsFiltered: string[] = [];
  @Input() typesFiltered: string[] = [];
  @Input() sortBy: string = '';
  request: GetProductsRequestInterface = {
    pageSize: 20,
  };
  result!: Pagination<Product[]>;

  ngOnChanges(): void {
    this.request = {
      ...this.request,
      sort: this.sortBy,
      brands: [...this.brandsFiltered],
      types: [...this.typesFiltered],
    };

    this.store.dispatch(productActions.getProductsList({request: this.request}));
  }

  ngOnInit(): void {
    this.store.dispatch(brandActions.getBrandsList());
    this.store.dispatch(typesActions.getTypesList());

    this.data$ = combineLatest({
      isSubmitting: this.store.select(selectIsSubmitting),
      isLoading: this.store.select(selectIsLoading),
      products: this.store.select(selectProductsPaginated),
      errors: this.store.select(selectApiErrors),
      brands: this.store.select(selectBrands),
      types: this.store.select(selectTypes),
    });
  }
}
