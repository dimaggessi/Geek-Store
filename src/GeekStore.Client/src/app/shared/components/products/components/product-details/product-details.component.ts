import {ProductStateInterface} from '@shared/components/products/types/productState.interface';
import {Component, OnInit, inject} from '@angular/core';
import {Store} from '@ngrx/store';
import {combineLatest, Observable} from 'rxjs';
import {selectProductById} from '../../store/products.selectors';
import {ActivatedRoute, RouterModule} from '@angular/router';
import {productActions} from '../../store/products.actions';
import {Product} from '@shared/models/product.interface';
import {CommonModule} from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss',
  imports: [CommonModule, RouterModule],
})
export class ProductDetailsComponent implements OnInit {
  store = inject(Store<{product: ProductStateInterface}>);
  activatedRoute = inject(ActivatedRoute);
  data$!: Observable<{product: Product | null}>;
  id: string | null = this.activatedRoute.snapshot.paramMap.get('id');

  ngOnInit(): void {
    if (this.id) {
      this.store.dispatch(productActions.getProductById({id: +this.id}));
    }

    this.data$ = combineLatest({
      product: this.store.select(selectProductById),
    });
  }
}
