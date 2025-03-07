import {CommonModule} from '@angular/common';
import {AfterViewInit, Component, inject, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {ActivatedRoute, RouterLink} from '@angular/router';
import {NgbCarousel, NgbCarouselModule} from '@ng-bootstrap/ng-bootstrap';
import {Store} from '@ngrx/store';
import {ProductService} from '@shared/components/products/services/product.service';
import {productActions} from '@shared/components/products/store/products.actions';
import {selectProductsPaginated} from '@shared/components/products/store/products.selectors';
import {GetProductsRequestInterface} from '@shared/components/products/types/getProductsRequest.interface';
import {ProductStateInterface} from '@shared/components/products/types/productState.interface';
import {Pagination} from '@shared/models/pagination.interface';
import {ProductInterface} from '@shared/models/product.interface';
import {Observable, Subject, takeUntil, tap} from 'rxjs';

@Component({
  standalone: true,
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
  imports: [CommonModule, NgbCarousel, NgbCarouselModule, RouterLink],
})
export class HomeComponent implements OnInit, AfterViewInit, OnDestroy {
  private destroy$ = new Subject<void>();
  private typingTimeout: any;
  private isTypingActive: boolean = false;
  store = inject(Store<{product: ProductStateInterface}>);
  activatedRoute = inject(ActivatedRoute);
  productService = inject(ProductService);
  data$!: Observable<{products: Pagination<ProductInterface[]> | null}>;
  request: GetProductsRequestInterface = {pageIndex: 1, pageSize: 9, maxPrice: 50};
  showNavigationArrows = true;
  showNavigationIndicators = true;
  groupedProducts: any[][] = [];

  ngOnInit(): void {
    this.startTypingEffect('typing-effect', 'Acesse nossa loja! As melhores ofertas!');
    this.store.dispatch(productActions.getProductsList({request: this.request}));

    this.store
      .select(selectProductsPaginated)
      .pipe(
        tap((response) => {
          if (response?.data) {
            this.groupedProducts = this.sliceArray(response.data, 3);
          }
        }),
        takeUntil(this.destroy$)
      )
      .subscribe();

    // this.store.select(selectProductsPaginated).subscribe((data) => console.log(data));
  }

  ngAfterViewInit(): void {
    this.activatedRoute.fragment.pipe(takeUntil(this.destroy$)).subscribe((fragment) => {
      if (fragment) {
        document.getElementById(fragment)?.scrollIntoView({behavior: 'smooth', block: 'start'});
      }
    });
  }

  sliceArray(array: any[], pieceSize: number) {
    let result = [];
    for (let i = 0; i < array.length; i += pieceSize) {
      result.push(array.slice(i, i + pieceSize));
    }
    return result;
  }

  startTypingEffect(elementId: string, message: string): void {
    this.isTypingActive = true;
    this.typingEffect(elementId, message);
  }

  stopTypingEffect(): void {
    this.isTypingActive = false;
    if (this.typingTimeout) {
      clearTimeout(this.typingTimeout);
    }
  }

  typingEffect(value: string, message: string) {
    const element = document.getElementById(value);
    const delay = 120;

    if (element && this.isTypingActive) {
      element.textContent = '';
      this.typeText(message, element, delay, () => {
        this.typingTimeout = setTimeout(() => {
          this.typingEffect(value, message);
        }, 1000);
      });
    }
  }

  typeText(text: string, element: HTMLElement, delay: number, callback: () => void): void {
    let i = 0;
    const type = () => {
      if (i < text.length && this.isTypingActive) {
        element.textContent += text.charAt(i);
        i++;
        setTimeout(type, delay);
      } else {
        callback();
      }
    };
    type();
  }

  ngOnDestroy(): void {
    this.stopTypingEffect();
    this.destroy$.next();
    this.destroy$.complete();
  }
}
