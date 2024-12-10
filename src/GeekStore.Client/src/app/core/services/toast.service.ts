import {Injectable, TemplateRef} from '@angular/core';

export interface Toast {
  message?: string;
  template?: TemplateRef<any>;
  classname?: string;
  delay?: number;
  type?: 'success' | 'error' | 'info';
}

@Injectable({providedIn: 'root'})
export class ToastService {
  toasts: Toast[] = [];

  show(toast: Toast) {
    this.toasts.push(toast);
  }

  remove(toast: Toast) {
    this.toasts = this.toasts.filter((t) => t !== toast);
  }

  clear() {
    this.toasts.splice(0, this.toasts.length);
  }

  // show(toast: Toast) {
  //   const isDuplicate = this.toasts.some(
  //     (t) => t.message === toast.message && t.classname === toast.classname
  //   );

  //   if (!isDuplicate) {
  //     this.toasts.push(toast);
  //   }
  // }
}
