import { HttpClient, HttpParams } from "@angular/common/http";
import { inject, Injectable, signal } from "@angular/core";
import { bankModel } from "../Models/bank.model";
import { Auth } from "./auth";

@Injectable({
  providedIn: "root",
})
export class Bank {
  constructor(private http: HttpClient) {}
  private userService = inject(Auth);

  public money = signal<number | undefined>(undefined);

  public getCurrentAccountBalance(userId: number) {
    const params = new HttpParams()
      .set('userId', userId)
    return this.http.get<bankModel>("/api/Bank/GetMoney", { params });
  }

  public setMoney(money: number) {
    this.money.set(money);
  }

  public getMoney() {
    return this.money;
  }

  public putEarnMoney(money: number, userId: number) {
    this.http
      .put<bankModel>(`/api/Bank/EarnMoney`, {
        money: money,
        userId: userId,
      })
      .subscribe({
        next: (val) => {
          this.money.set(val.money);
        },
      });
  }
  public putSpendMoney(money: number) {
    const user = this.userService.getUser();
    if (!user) return;

    return this.http.put<bankModel>(`/api/Bank/SpendMoney`, {
      money: money,
      userId: user.id,
    });
  }
}
