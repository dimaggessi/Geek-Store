import {GetProductsRequestInterface} from '@shared/components/products/types/getProductsRequest.interface';
import {CommonModule} from '@angular/common';
import {Component, EventEmitter, inject, Input, OnChanges, OnInit, Output} from '@angular/core';
import {ProductService} from '../services/product.service';
import {Store} from '@ngrx/store';
import {ProductStateInterface} from '../types/productState.interface';
import {brandActions, productActions, typesActions} from '../store/products.actions';
import {combineLatest, Observable} from 'rxjs';
import {
  selectApiErrors,
  selectBrands,
  selectBrandsAreLoaded,
  selectIsLoading,
  selectIsSubmitting,
  selectProductById,
  selectProductsPaginated,
  selectTypes,
  selectTypesAreLodaded,
} from '../store/products.selectors';
import {Pagination} from '@shared/models/pagination.interface';
import {Product} from '@shared/models/product.interface';
import {RouterModule} from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrl: './products.component.scss',
  imports: [CommonModule, RouterModule],
})
export class ProductComponent implements OnInit, OnChanges {
  store = inject(Store<{product: ProductStateInterface}>);
  productService = inject(ProductService);
  data$!: Observable<ProductStateInterface>;
  @Input() brandsFiltered: string[] = [];
  @Input() typesFiltered: string[] = [];
  @Input() sortBy: string = '';
  @Input() search: string = '';
  @Input() pageIndex?: number = 1;
  @Input() pageSize?: number = 9;
  @Output() totalCountChange = new EventEmitter<number>();
  request: GetProductsRequestInterface = {};
  result!: Pagination<Product[]>;

  ngOnChanges(): void {
    this.request = {
      ...this.request,
      search: this.search ? this.search : '',
      sort: this.sortBy,
      pageIndex: this.pageIndex,
      pageSize: this.pageSize,
      brands: [...this.brandsFiltered],
      types: [...this.typesFiltered],
    };

    this.store.dispatch(productActions.getProductsList({request: this.request}));

    this.store.select(selectBrandsAreLoaded).subscribe((areBrandsLodaded) => {
      if (!areBrandsLodaded) {
        this.store.dispatch(brandActions.getBrandsList());
      }
    });
    this.store.select(selectTypesAreLodaded).subscribe((areTypesLoaded) => {
      if (!areTypesLoaded) {
        this.store.dispatch(typesActions.getTypesList());
      }
    });
  }

  ngOnInit(): void {
    this.data$ = combineLatest({
      isSubmitting: this.store.select(selectIsSubmitting),
      isLoading: this.store.select(selectIsLoading),
      product: this.store.select(selectProductById),
      products: this.store.select(selectProductsPaginated),
      errors: this.store.select(selectApiErrors),
      brands: this.store.select(selectBrands),
      types: this.store.select(selectTypes),
    });

    this.data$.subscribe((response) => {
      if (response.products) {
        this.totalCountChange.emit(response.products.totalCount);
      }
    });
  }

  reload(): void {
    this.search = '';
    this.ngOnChanges();
  }
}
