export class EntityModel{
    id: string = "";
    createdDate: string = "";
    updatedDate?: string | null = null;
    deletedDate?: string | null = null;
    isActive: boolean = true;
    isDeleted: boolean = false;
}