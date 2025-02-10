import {CommonModule} from '@angular/common';
import {Component, inject, OnInit, ViewChild} from '@angular/core';
import {RouterLink} from '@angular/router';
import {NgbCarousel, NgbCarouselModule} from '@ng-bootstrap/ng-bootstrap';
import {Store} from '@ngrx/store';
import {ProductService} from '@shared/components/products/services/product.service';
import {productActions} from '@shared/components/products/store/products.actions';
import {selectProductsPaginated} from '@shared/components/products/store/products.selectors';
import {GetProductsRequestInterface} from '@shared/components/products/types/getProductsRequest.interface';
import {ProductStateInterface} from '@shared/components/products/types/productState.interface';
import {Pagination} from '@shared/models/pagination.interface';
import {ProductInterface} from '@shared/models/product.interface';
import {Observable, tap} from 'rxjs';

@Component({
  standalone: true,
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
  imports: [CommonModule, NgbCarousel, NgbCarouselModule, RouterLink],
})
export class HomeComponent implements OnInit {
  store = inject(Store<{product: ProductStateInterface}>);
  productService = inject(ProductService);
  data$!: Observable<{products: Pagination<ProductInterface[]> | null}>;
  request: GetProductsRequestInterface = {pageIndex: 1, pageSize: 9, maxPrice: 50};
  showNavigationArrows = true;
  showNavigationIndicators = true;
  groupedProducts: any[][] = [];

  ngOnInit(): void {
    this.typingEffect('typing-effect', 'Acesse nossa loja! As melhores ofertas!');
    this.store.dispatch(productActions.getProductsList({request: this.request}));

    this.store
      .select(selectProductsPaginated)
      .pipe(
        tap((response) => {
          console.log('selectProductsPaginated', response);
          if (response?.data) {
            this.groupedProducts = this.sliceArray(response.data, 3);
            console.log('groupedProducts', this.groupedProducts);
          }
        })
      )
      .subscribe();

    // this.store.select(selectProductsPaginated).subscribe((data) => console.log(data));
  }

  sliceArray(array: any[], pieceSize: number) {
    let result = [];
    for (let i = 0; i < array.length; i += pieceSize) {
      result.push(array.slice(i, i + pieceSize));
    }
    return result;
  }

  typingEffect(value: string, message: string) {
    const text = message;
    const element = document.getElementById(value);
    const delay = 120;

    if (element) {
      element.textContent = '';
      this.typeText(text, element, delay, () => {
        setTimeout(() => {
          this.typingEffect(value, message);
        }, 1000);
      });
    }
  }

  typeText(text: string, element: HTMLElement, delay: number, callback: () => void) {
    element.textContent = '';

    for (let i = 0; i < text.length; i++) {
      setTimeout(() => {
        element.textContent += text.charAt(i);
        if (i === text.length - 1) {
          callback();
        }
      }, delay * i);
    }
  }
}
