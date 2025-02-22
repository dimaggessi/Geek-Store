import {CommonModule} from '@angular/common';
import {Component, inject, OnDestroy, OnInit, signal, TemplateRef, ViewChild} from '@angular/core';
import {PaymentSummaryPipe} from '@shared/pipes/payment-summary.pipe';
import {AddressPipe} from '@shared/pipes/address.pipe';
import {AdminService} from '@core/services/admin.service';
import {ActivatedRoute, RouterLink} from '@angular/router';
import {ToastService} from '@core/services/toast.service';
import {OrderInterface, OrderStatus} from '@shared/models/order.interface';
import {Subject, takeUntil} from 'rxjs';
import {NgbActiveModal, NgbModal, NgbModalModule} from '@ng-bootstrap/ng-bootstrap';
import {StatusPipe} from '@shared/pipes/status.pipe';

@Component({
  selector: 'app-admin-order-details',
  standalone: true,
  imports: [CommonModule, PaymentSummaryPipe, AddressPipe, RouterLink, NgbModalModule, StatusPipe],
  templateUrl: './admin-order-details.component.html',
  styleUrl: './admin-order-details.component.scss',
  providers: [NgbActiveModal],
})
export class AdminOrderDetailsComponent implements OnInit, OnDestroy {
  modalService = inject(NgbModal);
  private destroy$ = new Subject<void>();
  private adminService = inject(AdminService);
  private activatedRoute = inject(ActivatedRoute);
  @ViewChild('modal') modalContent!: TemplateRef<any>;
  toastService = inject(ToastService);
  order?: OrderInterface;
  status = signal<OrderStatus | undefined>(undefined);

  ngOnInit(): void {
    this.loadOrder();
  }

  loadOrder() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (!id) return;

    this.adminService
      .getOrderById(+id)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (order) => {
          (this.order = order), this.status.set(order.status);
        },
        error: (error) => {
          this.toastService.show({
            message: 'Não foi possível carregar os detalhes do pedido',
            type: 'error',
            classname: 'bg-danger text-white text-center',
          });
          console.log('Não foi possível carregar o pedido: ', error);
        },
      });
  }

  cancelOrder() {
    if (!this.modalService.hasOpenModals() && this.modalContent) {
      const modalRef = this.modalService.open(this.modalContent, {
        centered: true,
      });
    } else {
      console.error("TemplateRef 'content' não foi inicializado");
    }
  }

  confirm() {
    if (this.order) {
      this.adminService
        .refundOrder(this.order.id)
        .pipe(takeUntil(this.destroy$))
        .subscribe({
          next: (response) => {
            if (response.status) {
              this.status.set(response.status);
            }
            console.log('O pedido foi reembolsado:', response);
          },
          error: (err) => console.log(err),
        });
    }

    this.modalService.dismissAll();
  }

  dismiss() {
    this.modalService.dismissAll();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
