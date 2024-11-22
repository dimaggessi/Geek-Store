import {HttpClient, HttpParams} from '@angular/common/http';
import {inject, Injectable} from '@angular/core';
import {environment} from '@environments/environment';
import {GetProductsRequestInterface} from '@shared/components/products/types/getProductsRequest.interface';
import {GetProductsResponseInterface} from '@shared/components/products/types/getProductsResponse.interface';
import {Pagination} from '@shared/models/pagination.interface';
import {Product} from '@shared/models/product.interface';
import {map, Observable} from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private http = inject(HttpClient);

  getProducts(request: GetProductsRequestInterface): Observable<Pagination<Product[]>> {
    const url = environment.apiUrl + '/products';

    let params = new HttpParams();

    if (request.search) params = params.set('search', request.search);
    if (request.sort) params = params.set('sort', request.sort);
    if (request.maxPrice) params = params.set('maxPrice', request.maxPrice.toString());
    if (request.minPrice) params = params.set('minPrice', request.minPrice.toString());
    if (request.pageIndex) params = params.set('pageIndex', request.pageIndex.toString());
    if (request.pageSize) params = params.set('pageSize', request.pageSize.toString());

    return this.http
      .get<GetProductsResponseInterface>(url, {params})
      .pipe(map((response) => response.response));
  }
}
