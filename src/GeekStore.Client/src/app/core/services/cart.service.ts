import {HttpClient} from '@angular/common/http';
import {computed, inject, Injectable, signal} from '@angular/core';
import {environment} from '@environments/environment';
import {Cart, CartItemInterface} from '@shared/models/cart.interface';
import {ProductInterface} from '@shared/models/product.interface';
import {firstValueFrom, map, tap} from 'rxjs';
import {ToastService} from './toast.service';

@Injectable({providedIn: 'root'})
export class CartService {
  private http = inject(HttpClient);
  toastService = inject(ToastService);
  cart = signal<Cart | null>(null);
  shippingCost = 0;
  deliveryTime = 0;

  itemCount = computed(() => {
    return this.cart()?.items.reduce((sum, item) => sum + item.quantity, 0) ?? 0;
  });

  totals = computed(() => {
    const cart = this.cart();
    if (!cart) return null;
    const subtotal = cart.items.reduce((sum, item) => sum + item.price * item.quantity, 0);
    const shipping = this.shippingCost;
    const discount = 0;
    return {
      subtotal,
      shipping,
      discount,
      total: subtotal + shipping - discount,
    };
  });

  getCart(id: string) {
    const url = environment.apiUrl;

    return this.http.get<Cart>(url + '/cart?id=' + id).pipe(
      map((cart) => {
        this.cart.set(cart);
        return cart;
      })
    );
  }

  setCart(cart: Cart) {
    const url = environment.apiUrl;

    return this.http.post<Cart>(url + '/cart', cart).pipe(tap((cart) => this.cart.set(cart)));
  }

  async addItem(
    item: CartItemInterface | ProductInterface,
    quantity = 1,
    productQuantity?: number
  ) {
    const cart = this.cart() ?? this.createCart();
    if (productQuantity && quantity > productQuantity) {
      this.toastService.show({
        message: 'Não há estoque disponível do produto.',
        type: 'error',
        classname: 'bg-danger text-white text-center',
      });
      return;
    }

    if (this.isProduct(item)) {
      item = this.mapProductToCartItem(item);
    }
    cart.items = this.addOrUpdateItem(cart.items, item, quantity);
    await firstValueFrom(this.setCart(cart));
    this.toastService.show({
      message: 'Produto adicionado ao carrinho.',
      type: 'success',
      classname: 'bg-success text-white text-center',
    });
  }

  async removeItem(productId: number, quantity = 1) {
    const cart = this.cart();
    if (!cart) return;

    const index = cart.items.findIndex((x) => x.productId === productId);
    if (index !== -1) {
      if (cart.items[index].quantity > quantity) {
        cart.items[index].quantity -= quantity;
      } else {
        cart.items.splice(index, 1);
      }
      if (cart.items.length === 0) {
        this.deleteCart();
      } else {
        await firstValueFrom(this.setCart(cart));
      }
    }
    this.toastService.show({
      message: 'Produto removido do carrinho.',
      type: 'success',
      classname: 'bg-danger text-white text-center',
    });
  }

  async updatePostalCode(postalCode: string) {
    const currentCart = this.cart();
    if (currentCart) {
      const updatedCart: Cart = {
        ...currentCart,
        postalCode: postalCode,
      };
      this.cart.set(updatedCart);
      await firstValueFrom(this.setCart(updatedCart));
    } else {
      console.warn('Carrinho não está disponível para atualização.');
    }
  }

  deleteCart(): void {
    const url = environment.apiUrl;
    this.http.delete(url + '/cart?id=' + this.cart()?.id).subscribe({
      next: () => {
        localStorage.removeItem('cart_id');
        this.cart.set(null);
      },
    });
  }

  private createCart(): Cart {
    const cart = new Cart();
    localStorage.setItem('cart_id', cart.id);

    return cart;
  }

  private isProduct(item: CartItemInterface | ProductInterface): item is ProductInterface {
    return (item as ProductInterface).id !== undefined;
  }

  private mapProductToCartItem(item: ProductInterface): CartItemInterface {
    return {
      productId: item.id,
      productName: item.name,
      price: item.price,
      quantity: 0,
      maxQuantity: item.quantity,
      picture: item.picture,
      brand: item.brand,
      type: item.type,
    };
  }

  private addOrUpdateItem(
    items: CartItemInterface[],
    item: CartItemInterface,
    quantity: number
  ): CartItemInterface[] {
    const index = items.findIndex((x) => x.productId === item.productId);
    if (index === -1) {
      item.quantity = quantity;
      items.push(item);
    } else {
      items[index].quantity += quantity;
    }
    return items;
  }
}
