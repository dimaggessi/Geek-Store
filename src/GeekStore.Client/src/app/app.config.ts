import {
  ApplicationConfig,
  isDevMode,
  provideZoneChangeDetection,
} from '@angular/core';
import {provideRouter} from '@angular/router';
import {routes} from './app.routes';
import {provideState, provideStore} from '@ngrx/store';
import {provideStoreDevtools} from '@ngrx/store-devtools';
import {HTTP_INTERCEPTORS, provideHttpClient} from '@angular/common/http';
import {authFeatureKey, authReducer} from '@features/auth/store/auth.reducer';
import {provideEffects} from '@ngrx/effects';
import * as authEffects from '@features/auth/store/auth.effects';
import {AuthInterceptor} from '@core/interceptors/auth.interceptor';
import {provideRouterStore, routerReducer} from '@ngrx/router-store';
import {
  metaReducers,
  rehydrateState,
} from '@features/auth/store/auth.meta-reducer';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({eventCoalescing: true}),
    provideRouter(routes),
    provideStore(
      {router: routerReducer},
      {
        metaReducers,
        initialState: rehydrateState(),
      }
    ),
    provideState(authFeatureKey, authReducer),
    provideRouterStore(),
    provideEffects(authEffects),
    provideHttpClient(),
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
    provideStoreDevtools({
      maxAge: 25,
      logOnly: !isDevMode(),
      autoPause: true,
      trace: false,
      traceLimit: 75,
      connectInZone: true,
    }),
  ],
};
