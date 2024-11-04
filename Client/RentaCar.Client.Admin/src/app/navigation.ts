export class NavigationModel{
    name: string = "";
    routerLink: string = "";
    icon: string = "";
}

export const navigations: NavigationModel[] = [
    {
        name: "Ana Sayfa",
        routerLink: "/",
        icon: "fa-solid fa-home"
    },
    {
        name: "Araçlar",
        routerLink: "/vehicles",
        icon: "fa-solid fa-car"
    }
]