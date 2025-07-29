import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { Observable } from 'rxjs';
import { productList } from '../Models/product.model';

@Injectable({
  providedIn: 'root',
})
export class Data {
  constructor(private http: HttpClient) {}
  api: string = 'http://localhost:9000/api/Vorrat/';

  getProducts(): Observable<productList[]> {
    return this.http.get<productList[]>(`${this.api}GetProducts`);
  }

  updateProducts(pName:string, pPreis:number, pAnzahl:number) {

    let param = new HttpParams()
      .set("name",pName)

    return this.http.put<productList>(`${this.api}UpdateProduct`, {
      price: pPreis,
      amount: pAnzahl
    }, {params: param});
  }
}
