import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: 'register',
        loadChildren: () => 
            import('./features/auth/auth.routes').then((r) => r.registerRoute)
    },
];
