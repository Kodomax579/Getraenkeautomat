import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { CardModel } from "../Models/Card.Model";
import { WinModel } from "../Models/Win.Model";

@Injectable({
  providedIn: "root",
})
export class BlackJackServiceService {
  private http = inject(HttpClient);
  private url = "http://localhost:9011/api/BlackJack/";

  public NewGame(money: number) {
    return this.http.post<boolean>(
      `http://localhost:9011/api/BlackJack/NewGame?money=${money}`,
      {}
    );
  }

  public GetSingleCard(isDealer: boolean) {
    return this.http.get<CardModel>(
      `http://localhost:9011/api/BlackJack/GetSingleCard?isDealer=${isDealer}`
    );
  }

  public WhoWins() {
    return this.http.get<WinModel>("http://localhost:9011/api/BlackJack/Win");
  }
}
