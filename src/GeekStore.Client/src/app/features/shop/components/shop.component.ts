import {CommonModule} from '@angular/common';
import {Component, EventEmitter, inject, OnInit, Output} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {NgbDropdownModule} from '@ng-bootstrap/ng-bootstrap';
import {Store} from '@ngrx/store';
import {ProductComponent} from '@shared/components/products/components/products.component';
import {selectBrands, selectTypes} from '@shared/components/products/store/products.selectors';
import {ProductStateInterface} from '@shared/components/products/types/productState.interface';
import {BrandsInterface} from '@shared/models/brands.interface';
import {TypesInterface} from '@shared/models/types.interface';
import {combineLatest, Observable} from 'rxjs';

@Component({
  standalone: true,
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss',
  imports: [CommonModule, ProductComponent, NgbDropdownModule, FormsModule],
})
export class ShopComponent implements OnInit {
  store = inject(Store<{product: ProductStateInterface}>);
  data$!: Observable<{brands: BrandsInterface | null; types: TypesInterface | null}>;
  selectedBrands: string[] = [];
  selectedTypes: string[] = [];
  appliedBrands: string[] = [];
  appliedTypes: string[] = [];
  sortBy: string = '';

  ngOnInit(): void {
    this.data$ = combineLatest({
      brands: this.store.select(selectBrands),
      types: this.store.select(selectTypes),
    });
  }

  onBrandSelection(event: Event, brand: string): void {
    const checkbox = event.target as HTMLInputElement;
    if (checkbox.checked) {
      this.selectedBrands.push(brand);
    } else {
      this.selectedBrands = this.selectedBrands.filter((value) => value !== brand);
    }
  }

  onTypeSelection(event: Event, type: string): void {
    const checkbox = event.target as HTMLInputElement;
    if (checkbox.checked) {
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
}
