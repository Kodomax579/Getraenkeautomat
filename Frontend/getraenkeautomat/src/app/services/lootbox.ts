import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Auth } from './auth';
import { Bank } from './bank';
import { lootboxModel } from '../Models/lootbox.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LootboxService {
  constructor(
    private http: HttpClient,
    private auth: Auth,
    private bank: Bank
  ) {}

  getLootboxes(): Observable<lootboxModel[]> {
    return this.http.get<lootboxModel[]>(`/api/Lootbox/GetLootboxes`);
  }

  getLootbox(id:number) {
     const params = new HttpParams()
      .set('id', id)

    return this.http.get<number>(`/api/Lootbox/GetResult`, { params });
  }
}
