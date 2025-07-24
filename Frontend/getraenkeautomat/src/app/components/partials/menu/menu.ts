import { Component } from '@angular/core';
import { Games } from '../games/games';
import { Aimlabs } from "../aimlabs/aimlabs";

@Component({
  selector: 'app-menu',
  imports: [Games, Aimlabs],
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

  onLeave()
  {
    this.selectedGame = 0
  }
}
