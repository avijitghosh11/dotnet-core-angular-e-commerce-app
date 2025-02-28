import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Pagination } from '../../shared/models/pagination';
import { Product } from '../../shared/models/product';
import { Observable } from 'rxjs';
import { ShopParam } from '../../shared/models/shopParam';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  private httpClient = inject(HttpClient);
  baseUrl: string = "http://localhost:5001/api/";
  brands: string[] = [];
  types: string[] = [];

  getProducts(shopParam: ShopParam): Observable<Pagination<Product>> {
    let params = new HttpParams();
    if (shopParam.brands && shopParam.brands.length > 0) {
      params = params.append("brands", shopParam.brands.join(','));
    }
    if (shopParam.types && shopParam.types.length > 0) {
      params = params.append("types", shopParam.types.join(','));
    }
    if (shopParam.sort) {
      params = params.append("sort", shopParam.sort);
    }
    if (shopParam.search) {
      params = params.append("search", shopParam.search);
    }
    params = params.append("pageSize", shopParam.pageSize);
    params = params.append("pageIndex", shopParam.pageIndex);
    return this.httpClient.get<Pagination<Product>>(this.baseUrl + "products", { params });
  }

  getProduct(id: number) {
    return this.httpClient.get<Product>(this.baseUrl + "products/" + id);
  }

  getBrands() {
    if (this.brands.length > 0) return;
    return this.httpClient.get<string[]>(this.baseUrl + "products/brands").subscribe({
      next: response => this.brands = response
    });
  }

  getTypes() {
    if (this.types.length > 0) return;
    return this.httpClient.get<string[]>(this.baseUrl + "products/types").subscribe({
      next: response => this.types = response
    });
  }
}
