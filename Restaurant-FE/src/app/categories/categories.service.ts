import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Category } from './category.model';
import { environment } from './../../environments/environment';

export interface GetCategoriesResponseData {
  id: string,
  parentId: string,
  name: string,
  subcategories: Category[];
}

export interface CategoryResponseData {
  message: string,
}

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {
  categoryURL = environment.apiURL + "/Category";

  constructor(private _http: HttpClient) { }

  getAllCategories(){
    return this._http.get<GetCategoriesResponseData[]>(this.categoryURL, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    });
  }

  getTargetCategory(id: string){
    return this._http.get<GetCategoriesResponseData>(this.categoryURL + `/${id}`, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    });
  }

  createNewCategory(name: string, parentId: string){
    return this._http.post<CategoryResponseData>(this.categoryURL, {
      name,
      parentId,
    }, 
    {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    })
  }

  updateCategory(id: string, name: string, parentId: string){
    return this._http.put<CategoryResponseData>(this.categoryURL + `/${id}`, {
      name,
      parentId,
    }, 
    {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    })
  }

  deleteCategory(id: string){
    return this._http.delete<CategoryResponseData>(this.categoryURL + `/${id}`, 
    {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    })
  }
}
