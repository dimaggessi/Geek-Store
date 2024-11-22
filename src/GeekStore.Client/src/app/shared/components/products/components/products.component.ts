import {GetProductsRequestInterface} from './../types/getProductsRequest.interface';
import {CommonModule} from '@angular/common';
import {Component, inject, OnInit} from '@angular/core';
import {ProductService} from '../services/product.service';
import {Store} from '@ngrx/store';
import {ProductStateInterface} from '../types/productState.interface';
import {productActions} from '../store/products.actions';
import {combineLatest, Observable} from 'rxjs';
import {
  selectApiErrors,
  selectIsLoading,
  selectIsSubmitting,
  selectProductsPaginated,
} from '../store/products.selectors';
import {Pagination} from '@shared/models/pagination.interface';
import {Product} from '@shared/models/product.interface';
import {ErrorResponseInterface} from '@shared/models/errors.interface';

@Component({
  standalone: true,
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrl: './products.component.scss',
  imports: [CommonModule],
})
export class ProductComponent implements OnInit {
  store = inject(Store<{product: ProductStateInterface}>);
  productService = inject(ProductService);
  data$!: Observable<ProductStateInterface>;
  request: GetProductsRequestInterface = {
    pageSize: 20,
  };
  result!: Pagination<Product[]>;

  ngOnInit(): void {
    this.store.dispatch(productActions.getProductsList({request: this.request}));

    this.data$ = combineLatest({
      isSubmitting: this.store.select(selectIsSubmitting),
      isLoading: this.store.select(selectIsLoading),
      data: this.store.select(selectProductsPaginated),
      errors: this.store.select(selectApiErrors),
    });
  }
}
