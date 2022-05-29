import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoriesService } from 'src/app/categories/categories.service';

export class Category {
  id: string;
  name: string;
  parentId: string;
  subcategories: Category[];
}

@Component({
  selector: 'app-recursive',
  templateUrl: './recursive.component.html',
  styleUrls: ['./recursive.component.css']
})
export class RecursiveComponent implements OnInit {
  @Input() categories: Category[];

  constructor(private _categoriesService: CategoriesService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    console.log(this.categories);
  }

  onEditCategory(id: string){
    this.router.navigate(['edit', id], {relativeTo: this.route});
  }

  onDeleteCategory(id: string){
    this._categoriesService.deleteCategory(id).subscribe(response => {
      this.router.navigate(['/categories'])
      .then(() => {
      window.location.reload();
      });
    });
  }
}
