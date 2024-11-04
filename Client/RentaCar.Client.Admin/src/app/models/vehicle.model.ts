import { EntityModel } from "./entity.model";

export class VehicleModel extends EntityModel{
    brand: string = "";
    model: string = "";
    year: number = 2000;
    plate: string = "";
    dailyPrice: number = 0;
    coverImageUrl: string = "";
    file:any;
}