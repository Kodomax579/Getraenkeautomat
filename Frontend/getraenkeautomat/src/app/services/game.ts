import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Auth } from './auth';
import { Bank } from './bank';
import { TicTacToe } from '../Models/TicTacToe.model';

@Injectable({
  providedIn: 'root',
})
export class Game {
  constructor(
    private http: HttpClient,
    private auth: Auth,
    private bank: Bank
  ) {}
  api: string = 'http://localhost:9008/';

  getTicTacToe() {
    let result: any[] = [];
    this.http.get<TicTacToe>(`${this.api}TicTacToe/create`).subscribe({
      next(value) {
        value.board.forEach((e) => {
          e.forEach((f) => {
            result.push(f);
          });
        });
      },
      error(err) {
        return err;
      },
    });
    return result;
  }

  updateTicTacToe(id: number) {
    let x: any;
    let y: any;

    switch (id) {
      case 0:
      case 1:
      case 2:
        x = 0;
        y = id;
        break;
      case 3:
      case 4:
      case 5:
        x = 1;
        y = id - 3;
        break;
      case 6:
      case 7:
      case 8:
        x = 2;
        y = id - 6;
        break;
    }
    return this.http.put<TicTacToe>(
      `${this.api}TicTacToe/update?x=${x}&y=${y}`,
      null
    );
  }

  getCashAndLevel(earnedMoney:number, earnedLevel:number) {
    let user: any = this.auth.getUser();
    let money: number = this.roundToTwoDecimalPlaces(earnedMoney * user.level);
    user.level += earnedLevel;
    this.auth.updateLevel(user.level).subscribe((updatedUser) => {
      this.auth.setUser(updatedUser);
    });
    this.bank.putEarnMoney(money, user.id);
  }

    getCashOnly(earnedMoney:number) {
    let user: any = this.auth.getUser();
    let money: number = earnedMoney;
    this.bank.putEarnMoney(money, user.id);
  }

  roundToTwoDecimalPlaces(num: number): number {
    return Number(num.toFixed(2));
  }
}
