import {GetProductsRequestInterface} from '@shared/components/products/types/getProductsRequest.interface';
import {CommonModule, ViewportScroller} from '@angular/common';
import {
  Component,
  EventEmitter,
  inject,
  Input,
  OnChanges,
  OnDestroy,
  OnInit,
  Output,
  signal,
} from '@angular/core';
import {ProductService} from '../services/product.service';
import {Store} from '@ngrx/store';
import {ProductStateInterface} from '../types/productState.interface';
import {brandActions, productActions, typesActions} from '../store/products.actions';
import {combineLatest, Observable, Subject, takeUntil} from 'rxjs';
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
import {RouterModule} from '@angular/router';
import {CartService} from '@core/services/cart.service';

@Component({
  standalone: true,
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrl: './products.component.scss',
  imports: [CommonModule, RouterModule],
})
export class ProductComponent implements OnInit, OnChanges, OnDestroy {
  private viewportScroller = inject(ViewportScroller);
  private destroy$ = new Subject<void>();
  store = inject(Store<{product: ProductStateInterface}>);
  productService = inject(ProductService);
  cartService = inject(CartService);
  data$!: Observable<ProductStateInterface>;
  @Input() brandsFiltered: string[] = [];
  @Input() typesFiltered: string[] = [];
  @Input() sortBy: string = '';
  @Input() search: string = '';
  @Input() pageIndex?: number = 1;
  @Input() pageSize?: number = 9;
  @Output() totalCountChange = new EventEmitter<number>();
  request: GetProductsRequestInterface = {};
  totalCount = signal<number | undefined>(1);

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

    this.viewportScroller.scrollToPosition([0, 0]);
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

    this.data$.pipe(takeUntil(this.destroy$)).subscribe({
      next: (response) => {
        if (response.products && this.totalCount() != response.products.totalCount) {
          // console.log('Dados recebidos:', response);
          this.totalCount.set(response.products?.totalCount);
          // console.log('Total count atualizado:', this.totalCount());
          this.totalCountChange.emit(this.totalCount());
        }
      },
    });
  }

  // Avoid memory leak on data$ subscription
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  reload(): void {
    this.search = '';
    this.ngOnChanges();
  }
}
