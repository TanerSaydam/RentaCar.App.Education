import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: "",
        loadComponent: () => import("./components/layouts/layouts.component"),
        children: [
            {
                path: "",
                loadComponent:() => import("./components/home/home.component")
            },
            {
                path: "vehicles",
                loadComponent:()=> import("./components/vehicles/vehicles.component")
            }
        ]
    }
];
