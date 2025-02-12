import {ProductStateInterface} from '@shared/components/products/types/productState.interface';
import {Component, OnInit, inject} from '@angular/core';
import {Store} from '@ngrx/store';
import {combineLatest, Observable} from 'rxjs';
import {selectProductById} from '../../store/products.selectors';
import {ActivatedRoute, RouterModule} from '@angular/router';
import {productActions} from '../../store/products.actions';
import {ProductInterface} from '@shared/models/product.interface';
import {CommonModule, Location} from '@angular/common';
import {CartService} from '@core/services/cart.service';
import {FormsModule} from '@angular/forms';
import {ToastService} from '@core/services/toast.service';

@Component({
  standalone: true,
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss',
  imports: [CommonModule, RouterModule, FormsModule],
})
export class ProductDetailsComponent implements OnInit {
  store = inject(Store<{product: ProductStateInterface}>);
  cartService = inject(CartService);
  activatedRoute = inject(ActivatedRoute);
  toastService = inject(ToastService);
  location = inject(Location);
  data$!: Observable<{product: ProductInterface | null}>;
  id: string | null = this.activatedRoute.snapshot.paramMap.get('id');
  quantity: number = 1;

  ngOnInit(): void {
    if (this.id) {
      this.store.dispatch(productActions.getProductById({id: +this.id}));
    }

    this.data$ = combineLatest({
      product: this.store.select(selectProductById),
    });
  }

  addItem(product: ProductInterface, quantity: number, productQuantity: number): void {
    this.cartService.addItem(product, quantity, productQuantity);
  }

  goBack(): void {
    this.location.back();
  }
}
