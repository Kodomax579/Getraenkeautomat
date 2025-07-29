import { HttpClient } from '@angular/common/http';
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
  api: string = 'http://localhost:9010/api/Lootbox/';

  getLootboxes(): Observable<lootboxModel[]> {
    return this.http.get<lootboxModel[]>(`${this.api}GetLootboxes`);
  }

  getLootbox(id:number) {
    return this.http.get<number>(`${this.api}GetResult/${id}`);
  }
}
