import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from './../../environments/environment';

export interface GetProductsResponseData {
  productId: string,
  name: string,
  description: string,
  categoryId: string,
  categoryName: string,
  price: number,
}

export interface ProductResponseData {
  message: string,
}

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  productURL = environment.apiURL + "/Product";

  constructor(private _http: HttpClient) { }

  getAllProducts(){
    return this._http.get<GetProductsResponseData[]>(this.productURL, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    });
  }

  getFilteredProducts(productName: string, categoryId: string, pageNumber: number, sortBy: string, sortDirection: string){
    let params = new HttpParams();
    if(productName != null){
      params = params.append('product', productName);
    }
    if(categoryId != null){
      params = params.append('categoryId', categoryId);
    }
    params = params.append('pageNumber', pageNumber === null ? 0 : pageNumber);
    params = params.append('pageSize', 20);
    if(sortBy != null){
      params = params.append('sortBy', sortBy);
    }
    if(sortDirection != null){
      params = params.append('sortDirection', sortDirection);
    }
    return this._http.get<GetProductsResponseData[]>(this.productURL, {
      params: params
    });
  }

  getProduct(id: string){
    return this._http.get<GetProductsResponseData>(this.productURL + `/${id}`);
  }

  createNewProduct(name: string, categoryId: string, description: string, price: number){
    if(description === null){
      description = '';
    }

    return this._http.post<ProductResponseData>(this.productURL, {
      name,
      description,
      price,
      categoryId,
    })
  }

  updateProduct(id: string, name: string, categoryId: string, description: string, price: number){
    if(description === null){
      description = '';
    }

    return this._http.put<ProductResponseData>(this.productURL + `/${id}`, {
      name,
      description,
      price,
      categoryId,
    })
  }

  deleteProduct(id: string){
    return this._http.delete<ProductResponseData>(this.productURL + `/${id}`);
  }
}
