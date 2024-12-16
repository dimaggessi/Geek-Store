import {InitializerService} from './core/services/initializer.service';
import {
  APP_INITIALIZER,
  ApplicationConfig,
  isDevMode,
  LOCALE_ID,
  provideZoneChangeDetection,
} from '@angular/core';
import {provideRouter} from '@angular/router';
import {routes} from './app.routes';
import {provideState, provideStore} from '@ngrx/store';
import {provideStoreDevtools} from '@ngrx/store-devtools';
import {provideHttpClient, withInterceptors} from '@angular/common/http';
import {authFeatureKey, authReducer} from '@features/auth/store/auth.reducer';
import {provideEffects} from '@ngrx/effects';
import * as authEffects from '@features/auth/store/auth.effects';
import * as productEffects from '@shared/components/products/store/products.effects';
import {authInterceptorFn} from '@core/interceptors/auth.interceptor';
import {provideRouterStore, routerReducer} from '@ngrx/router-store';
import {metaReducers, rehydrateState} from '@features/auth/store/auth.meta-reducer';
import {
  productFeatureKey,
  productReducer,
} from '@shared/components/products/store/products.reducer';
import {errorInterceptorFn} from '@core/interceptors/error.interceptor';
import {loadingInterceptor} from '@core/interceptors/loading.interceptor';

function initialize(initializerService: InitializerService) {
  return () => {
    initializerService.init().subscribe({
      next: (cart) => console.log('Carrinho carregado:', cart),
      error: (err) => console.warn('Erro ao carregar carrinho:', err),
    });
  };
}

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
    provideState(productFeatureKey, productReducer),
    provideRouterStore(),
    provideEffects(authEffects, productEffects),
    provideHttpClient(
      withInterceptors([errorInterceptorFn, authInterceptorFn, loadingInterceptor])
    ),
    {
      provide: APP_INITIALIZER,
      useFactory: initialize,
      multi: true,
      deps: [InitializerService],
    },
    {provide: LOCALE_ID, useValue: 'pt-BR'},
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
