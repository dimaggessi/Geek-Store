import { Route } from "@angular/router";
import { RegisterComponent } from "./components/register/register.component";

export const registerRoute: Route[] = [
    {
        path: '',
        component: RegisterComponent
    }
];