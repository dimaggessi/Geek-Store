import {CommonModule} from '@angular/common';
import {HttpErrorResponse} from '@angular/common/http';
import {Component, inject, OnDestroy, OnInit, signal, TemplateRef, ViewChild} from '@angular/core';
import {RouterLink} from '@angular/router';
import {AdminService} from '@core/services/admin.service';
import {ToastService} from '@core/services/toast.service';
import {
  NgbDropdownModule,
  NgbModal,
  NgbNavModule,
  NgbPaginationModule,
  NgbTooltipModule,
} from '@ng-bootstrap/ng-bootstrap';
import {OrderInterface, OrderParams, OrderStatus} from '@shared/models/order.interface';
import {ProductInterface, ProductParams} from '@shared/models/product.interface';
import {Subject, takeUntil} from 'rxjs';
import {StatusPipe} from '@shared/pipes/status.pipe';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [
    CommonModule,
    NgbNavModule,
    NgbDropdownModule,
    NgbPaginationModule,
    RouterLink,
    NgbTooltipModule,
    StatusPipe,
  ],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.scss',
})
export class AdminComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();
  toastService = inject(ToastService);
  modalService = inject(NgbModal);
  adminService = inject(AdminService);
  active = 'first';
  orders: OrderInterface[] = [];
  products: ProductInterface[] = [];
  loading: boolean = true;
  orderParams = new OrderParams();
  productParams = new ProductParams();
  orderStatus = OrderStatus; // enum OrderStatus
  ordersError: boolean = false;
  productsError: boolean = false;
  errorMessage: string = '';
  status = signal<OrderStatus | undefined>(undefined);
  @ViewChild('orderModal') orderModalContent!: TemplateRef<any>;
  @ViewChild('productModal') productModalContent!: TemplateRef<any>;
  @ViewChild('orderErrorModal') orderErrorModalContent!: TemplateRef<any>;
  @ViewChild('productErrorModal') productErrorModalContent!: TemplateRef<any>;
  selectedOrder: OrderInterface | null = null;
  selectedProduct: ProductInterface | null = null;
  search: string = '';

  ngOnInit(): void {
    this.loadOrders(this.orderParams);
    this.loadProducts(this.productParams);
  }

  loadOrders(orderParams: OrderParams) {
    this.adminService
      .getAllOrders(orderParams)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (response) => {
          if (response.data) {
            this.orders = response.data;
            this.orderParams.totalItems = response.totalCount;
            this.loading = false;
          }
        },
        error: (err: HttpErrorResponse) => {
          // console.error('Erro ao carregar os pedidos:', err);
          this.ordersError = true;
          this.errorMessage = err.message;
          this.openErrorModal(this.orderErrorModalContent);
          this.loading = false;
        },
      });
  }

  loadProducts(productParams: ProductParams) {
    if (this.search !== '') {
      this.productParams.search = this.search;
    }

    this.adminService
      .getAllProducts(productParams)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (response) => {
          if (response.response.data) {
            this.products = response.response.data;
            this.productParams.totalItems = response.response.totalCount;
            this.loading = false;
          }
        },
        error: (err: HttpErrorResponse) => {
          // console.error('Erro ao carregar os produtos:', err);
          this.productsError = true;
          this.errorMessage = err.message;
          this.openErrorModal(this.productErrorModalContent);
          this.loading = false;
        },
      });
  }

  deleteProduct(product: ProductInterface) {
    this.selectedProduct = product;
    if (!this.modalService.hasOpenModals() && this.productModalContent) {
      const modalRef = this.modalService.open(this.productModalContent, {
        centered: true,
      });
    } else {
      console.error("TemplateRef 'productModalContent' não foi inicializado");
    }
  }

  refund(order: OrderInterface) {
    this.selectedOrder = order;
    if (!this.modalService.hasOpenModals() && this.orderModalContent) {
      const modalRef = this.modalService.open(this.orderModalContent, {
        centered: true,
      });
    } else {
      console.error("TemplateRef 'orderModalContent' não foi inicializado");
    }
  }

  openErrorModal(template: TemplateRef<any>) {
    if (!this.modalService.hasOpenModals() && template) {
      const modalRef = this.modalService.open(template, {
        centered: true,
      });
    } else {
      console.error(`TemplateRef ${template} não foi inicializado`);
    }
  }

  onOrdersPageChange(page: number) {
    this.orderParams.pageIndex = page;
    this.loadOrders(this.orderParams);
  }

  onProductsPageChange(page: number) {
    this.productParams.pageIndex = page;
    this.loadProducts(this.productParams);
  }

  onSearch(): void {
    const inputElement = document.querySelector('.input-search') as HTMLInputElement;

    if (inputElement) {
      this.search = inputElement.value;
    } else {
      this.search = '';
    }
    this.loadProducts(this.productParams);
  }

  reload() {
    this.search = '';
    this.productParams.search = '';
    this.loadProducts(this.productParams);
  }

  setStatus(newStatus: OrderStatus) {
    this.orderParams.status = newStatus;
    this.loadOrders(this.orderParams);
  }

  confirmOrderRefund(order: OrderInterface | null) {
    // console.log('Tipo de order.status:', typeof order?.status);
    if (order?.status.toString() === 'Pending') {
      this.toastService.show({
        message: 'O pagamento não foi efetuado. Não é possível reembolsar.',
        type: 'error',
        classname: 'bg-danger text-center text-white',
      });
      this.modalService.dismissAll();
      return;
    }
    if (order?.status.toString() === 'Refunded') {
      this.toastService.show({
        message: 'O pagamento já foi reembolsado.',
        type: 'error',
        classname: 'bg-danger text-center text-white',
      });
      this.modalService.dismissAll();
      return;
    }
    if (order) {
      this.adminService
        .refundOrder(order.id)
        .pipe(takeUntil(this.destroy$))
        .subscribe({
          next: () => {
            this.loadOrders(this.orderParams);
          },
          error: (err) => console.log(err),
        });
    }

    this.modalService.dismissAll();
  }

  confirmDeleteProduct(id?: number) {
    if (id) {
      this.adminService
        .deleteProduct(id)
        .pipe(takeUntil(this.destroy$))
        .subscribe({
          next: () => {
            this.loadProducts(this.productParams);
            this.toastService.show({
              message: 'Produto excluído da base de dados.',
              type: 'success',
              classname: 'bg-success text-center text-white',
            });
            this.modalService.dismissAll();
          },
        });
    } else {
      this.toastService.show({
        message: `Não foi possível excluir o produto. Id recebido é inválido: ${id}.`,
        type: 'error',
        classname: 'bg-danger text-center text-white',
      });
      this.modalService.dismissAll();
    }
  }

  dismiss() {
    this.modalService.dismissAll();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
