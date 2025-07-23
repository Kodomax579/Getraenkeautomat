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
  TicTacToe: boolean = false;
  AimLab: boolean = false;

  showTicTacToe(){
    this.TicTacToe = true;
  }

  showAimLab(){
    this.AimLab = true;
  }

  onLeave()
  {
    this.AimLab = false
  }
}
