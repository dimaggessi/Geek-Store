import {CommonModule} from '@angular/common';
import {Component, EventEmitter, inject, OnInit, Output} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {Router} from '@angular/router';
import {NgbDropdownModule, NgbPagination} from '@ng-bootstrap/ng-bootstrap';
import {Store} from '@ngrx/store';
import {ProductComponent} from '@shared/components/products/components/products.component';
import {selectBrands, selectTypes} from '@shared/components/products/store/products.selectors';
import {ProductStateInterface} from '@shared/components/products/types/productState.interface';
import {BrandsInterface} from '@shared/models/brands.interface';
import {TypesInterface} from '@shared/models/types.interface';
import {combineLatest, debounceTime, distinctUntilChanged, Observable, Subject} from 'rxjs';

@Component({
  standalone: true,
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss',
  imports: [CommonModule, ProductComponent, NgbDropdownModule, FormsModule, NgbPagination],
})
export class ShopComponent implements OnInit {
  router = inject(Router);
  store = inject(Store<{product: ProductStateInterface}>);
  data$!: Observable<{brands: BrandsInterface | null; types: TypesInterface | null}>;
  searchSubject: Subject<string> = new Subject<string>();
  selectedBrands: string[] = [];
  selectedTypes: string[] = [];
  appliedBrands: string[] = [];
  appliedTypes: string[] = [];
  sortBy: string = '';
  search: string = '';
  currentPage: number = 1;
  totalCount: number = 0;

  ngOnInit(): void {
    this.data$ = combineLatest({
      brands: this.store.select(selectBrands),
      types: this.store.select(selectTypes),
    });
  }

  onBrandSelection(event: Event, brand: string): void {
    const checkbox = event.target as HTMLInputElement;
    if (checkbox && checkbox.checked) {
      this.selectedBrands.push(brand);
    } else {
      this.selectedBrands = this.selectedBrands.filter((value) => value !== brand);
    }
  }

  onTypeSelection(event: Event, type: string): void {
    const checkbox = event.target as HTMLInputElement;
    if (checkbox && checkbox.checked) {
      this.selectedTypes.push(type);
    } else {
      this.selectedTypes = this.selectedTypes.filter((value) => value !== type);
    }
  }

  applyFilters(): void {
    this.appliedBrands = [...this.selectedBrands];
    this.appliedTypes = [...this.selectedTypes];
  }

  applySort(): string {
    return this.sortBy;
  }

  onSearch(event: Event): void {
    event.preventDefault();
    const inputElement = document.querySelector('.input-search') as HTMLInputElement;

    if (inputElement) {
      this.search = inputElement.value;
    } else {
      this.search = '';
    }
  }

  onTotalCountChange(value: number) {
    this.totalCount = value;
    console.log('Total Count Atualizado', this.totalCount);
  }
}
