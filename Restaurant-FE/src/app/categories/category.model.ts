export class Category {
    public id: string;
    public parentId: string;
    public name: string;
    public subcategories: Category[];

    constructor(id: string, parentId: string, name: string, subcategories: Category[]){
        this.id = id;
        this.parentId = parentId;
        this.name = name;
        this.subcategories = subcategories;
    }
}