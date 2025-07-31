import { Component, ElementRef, EventEmitter, Output, ViewChild } from '@angular/core';
import { Game } from '../../../services/game';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-games',
  imports: [NgClass],
  templateUrl: './games.html',
  styleUrl: './games.scss',
})
export class Games {
  constructor(private game: Game) {
    this.gameBoard = game.getTicTacToe();
  }

  gameFinish: boolean = false
  gameBoard: number[] = [];
  result: number = 0;

  @ViewChild('gameResult') gameResult!: ElementRef<HTMLDivElement>;
  
  @Output() isTicTacToe = new EventEmitter<boolean>();

  ngOnInit() {}

  restart() {
    this.gameBoard = this.game.getTicTacToe();
  }

  playerClicked(id: number): void {
    this.game.updateTicTacToe(id).subscribe({
      next: (value) => {
        this.gameBoard = value.board.flat();
        this.result = value.result;

        switch (this.result) {
          case 1:
            this.gameResult.nativeElement.innerText = '';
            break;
          case 2:
            this.game.getCashAndLevel(0.1, 0.01);
            this.gameResult.nativeElement.innerText = 'Du hast Gewonnen!';
            this.gameFinish = true;
            break;
          case 3:
            this.gameResult.nativeElement.innerText = 'Bot hat Gewonnen';
            this.gameFinish = true;
            break;
          case 4:
            this.gameResult.nativeElement.innerText = 'Unentschieden';
            this.gameFinish = true;
            break;
          default:
            this.gameResult.nativeElement.innerText = 'Das geht nicht...';
            break;
        }
      },
      error: (err) => {
        console.error('Error updating game:', err.error.message);
      },
    });
  }
  backToMenu(){
    this.isTicTacToe.emit(false);
  }

  onNewGame()
  {
    this.gameBoard = this.game.getTicTacToe();
     this.gameResult.nativeElement.innerText = '';
    this.gameFinish = false;
  }

}
