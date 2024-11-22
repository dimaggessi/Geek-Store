import {CommonModule} from '@angular/common';
import {Component} from '@angular/core';
import {ProductComponent} from '@shared/components/products/components/products.component';

@Component({
  standalone: true,
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss',
  imports: [CommonModule, ProductComponent],
})
export class ShopComponent {}
