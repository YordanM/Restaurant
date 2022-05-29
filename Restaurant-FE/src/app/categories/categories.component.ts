import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoriesService } from './categories.service';
import { Category } from './category.model';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {
  categories: Category[];
  isLoading = false;

  // error messages
  productError = 'There is product in this category.'

  constructor(private _categoriesService: CategoriesService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.isLoading = true;
    this._categoriesService.getAllCategories().subscribe(response => {
      this.categories = response.filter(c => c.parentId === null);
      console.log(this.categories);
      this.isLoading = false;
    });
  }

  onEditCategory(id: string){
    this.router.navigate(['edit', id], {relativeTo: this.route});
  }

  onDeleteCategory(id: string){
    this._categoriesService.deleteCategory(id).subscribe(response => {
      this.isLoading = true;
      this._categoriesService.getAllCategories().subscribe(response => {
        this.categories = response.filter(c => c.parentId === null);
        console.log(this.categories);
        this.isLoading = false;
      });
    });
  }

}
