import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Supplier } from '../interfaces/supplier.model';
import { PaginatedList } from 'src/app/shared/interfaces/paginated-list.model';
import { ApiResponse } from 'src/app/shared/interfaces/api-response.model';
import { Filter } from '../interfaces/filter.model';

@Injectable({
  providedIn: 'root'
})
export class SupplierService {
  private backendUrl: string;

  constructor(private http: HttpClient) {
    this.backendUrl = `http://localhost:5000/api/supplier`;
  }

  getSuppliers(pageIndex: number, pageSize: number, filter: Filter): Observable<PaginatedList<Supplier>> {
    const params = new HttpParams()
      .set("pageIndex", pageIndex)
      .set("pageSize", pageSize)
      .set("legalName", filter.legalName)
      .set("tradeName", filter.tradeName)
      .set("taxIdentificationNumber", filter.taxIdentificationNumber)
      .set("countryId", filter.countryId);

    return this.http.get<PaginatedList<Supplier>>(this.backendUrl, { params });
  }

  getSupplier(id: string): Observable<ApiResponse<Supplier>> {
    return this.http.get<ApiResponse<Supplier>>(`${this.backendUrl}/${id}`);
  }

  createSupplier(supplier: Supplier): Observable<ApiResponse<Supplier>> {
    return this.http.post<ApiResponse<Supplier>>(this.backendUrl, supplier);
  }

  updateSupplier(id: string, supplier: Supplier): Observable<ApiResponse<Supplier>> {
    return this.http.put<ApiResponse<Supplier>>(`${this.backendUrl}/${id}`, supplier);
  }

  deleteSupplier(id: string): Observable<ApiResponse<string>> {
    return this.http.delete<ApiResponse<string>>(`${this.backendUrl}/${id}`);
  }
}
