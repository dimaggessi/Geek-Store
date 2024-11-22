import {AuthStateInterface} from '@features/auth/types/authState.interface';
import {combineLatest, Observable, take} from 'rxjs';
import {CommonModule} from '@angular/common';
import {Component, inject, OnInit, ViewEncapsulation} from '@angular/core';
import {RouterLink} from '@angular/router';
import {NgbCollapseModule, NgbTooltipModule} from '@ng-bootstrap/ng-bootstrap';
import {Store} from '@ngrx/store';
import {selectUser} from '@features/auth/store/auth.selectors';
import {UserInterface} from '@shared/models/user.interface';
import {authActions} from '@features/auth/store/auth.actions';

@Component({
  standalone: true,
  selector: 'app-navbar',
  imports: [NgbCollapseModule, CommonModule, RouterLink, NgbTooltipModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
  encapsulation: ViewEncapsulation.None,
})
export class NavbarComponent implements OnInit {
  isCollapsed = false;
  store = inject(Store<{auth: AuthStateInterface}>);
  data$!: Observable<{user: UserInterface | null | undefined}>;

  ngOnInit(): void {
    this.store
      .select(selectUser)
      .pipe(take(1))
      .subscribe((user) => {
        if (!user) {
          this.store.dispatch(authActions.getUser());
        } else {
        }
      });

    this.data$ = combineLatest({
      user: this.store.select(selectUser),
    });
  }

  logout(): void {
    this.store.dispatch(authActions.logout());
  }
}
