import {AuthStateInterface} from '@features/auth/types/authState.interface';
import {combineLatest, Observable, Subject, take, takeUntil} from 'rxjs';
import {CommonModule} from '@angular/common';
import {Component, inject, OnDestroy, OnInit, ViewEncapsulation} from '@angular/core';
import {Router, RouterLink, RouterModule} from '@angular/router';
import {NgbCollapseModule, NgbTooltipModule} from '@ng-bootstrap/ng-bootstrap';
import {Store} from '@ngrx/store';
import {selectIsAdmin, selectUser} from '@features/auth/store/auth.selectors';
import {UserInterface} from '@shared/models/user.interface';
import {authActions} from '@features/auth/store/auth.actions';
import {FormsModule} from '@angular/forms';
import {CartService} from '@core/services/cart.service';
import {SignalrService} from '@core/services/signalr.service';
import {ThemeService} from '@core/services/theme.service';

@Component({
  standalone: true,
  selector: 'app-navbar',
  imports: [
    NgbCollapseModule,
    CommonModule,
    RouterLink,
    NgbTooltipModule,
    FormsModule,
    RouterModule,
  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
  encapsulation: ViewEncapsulation.None,
})
export class NavbarComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();
  themeService = inject(ThemeService);
  store = inject(Store<{auth: AuthStateInterface}>);
  router = inject(Router);
  cartService = inject(CartService);
  signalRService = inject(SignalrService);
  data$!: Observable<{user: UserInterface | null | undefined; isAdmin: boolean}>;
  searchTerm: string = '';
  isCollapsed: boolean = false;

  ngOnInit(): void {
    this.store
      .select(selectUser)
      .pipe(takeUntil(this.destroy$), take(1))
      .subscribe((user) => {
        this.store.dispatch(authActions.getUser());
      });

    this.data$ = combineLatest({
      user: this.store.select(selectUser),
      isAdmin: this.store.select(selectIsAdmin),
    });
  }

  scrollToTop() {
    window.scrollTo({top: 0, behavior: 'smooth'});
  }

  toggleNavbar() {
    this.isCollapsed = !this.isCollapsed;
    this.scrollToTop();
  }

  logout(): void {
    this.store.dispatch(authActions.logout());
  }

  ngOnDestroy(): void {
    this.destroy$.next(), this.destroy$.complete();
  }
}
