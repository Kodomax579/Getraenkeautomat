import { Component } from '@angular/core';
import { Games } from '../games/games';
import { Aimlabs } from "../aimlabs/aimlabs";
import { Lootbox } from "../lootbox/lootbox";
import { CoinToss } from "../coin-toss/coin-toss";
import { BlackjackComponent } from "../blackJack/black-jack.component";

@Component({
  selector: 'app-menu',
  imports: [Games, Aimlabs, Lootbox, CoinToss, BlackjackComponent],
  templateUrl: './menu.html',
  styleUrl: './menu.scss',
})
export class Menu {
  selectedGame: number = 0;

  showTicTacToe(){
    this.selectedGame = 1;
  }

  showAimLab(){
    this.selectedGame = 2;
  }

  showBlackjack(){
    this.selectedGame = 3;
  }

  showLootbox(){
    this.selectedGame = 4;
  }

  showCoinToss(){
    this.selectedGame = 5;
  }

  onLeave()
  {
    this.selectedGame = 0
  }
}
