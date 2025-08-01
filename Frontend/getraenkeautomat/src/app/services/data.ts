import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { Observable } from 'rxjs';
import { productList } from '../Models/product.model';

@Injectable({
  providedIn: 'root',
})
export class Data {
  constructor(private http: HttpClient) {}

  getProducts(): Observable<productList[]> {
    return this.http.get<productList[]>(`/api/Vorrat/GetProducts`);
  }

  updateProducts(pName:string, pAnzahl:number) {

    let param = new HttpParams()
    .set("amount",pAnzahl)
    .set("name",pName)

    return this.http.put<productList>(`/api/Vorrat/UpdateProductAmount`,null,{params : param});
  }
}
