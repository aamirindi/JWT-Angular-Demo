import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Product } from '../interfaces/product.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private baseUrl = 'http://localhost:5264/api/Product';

  constructor(private http: HttpClient) {}

  addProduct(product: Product): Observable<any> {
    return this.http.post(`${this.baseUrl}/add`, product);
  }

  getAllProducts(): Observable<{ success: boolean; data: Product[] }> {
    return this.http.get<{ success: boolean; data: Product[] }>(
      `${this.baseUrl}/all`
    );
  }

  getProductById(id: number): Observable<{ success: boolean; data: Product }> {
    return this.http.get<{ success: boolean; data: Product }>(
      `${this.baseUrl}/${id}`
    );
  }

  updateProduct(product: Product): Observable<any> {
    return this.http.put(`${this.baseUrl}/update`, product);
  }

  deleteProduct(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/delete/${id}`);
  }
}
