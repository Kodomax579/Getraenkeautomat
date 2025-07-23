import { Component, ElementRef, EventEmitter, Output, ViewChild } from '@angular/core';
import { Game } from '../../../services/game';

@Component({
  selector: 'app-games',
  imports: [],
  templateUrl: './games.html',
  styleUrl: './games.scss',
})
export class Games {
  constructor(private game: Game) {
    this.gameBoard = game.getTicTacToe();
  }

  gameBoard: number[] = [];
  result: number = 0;

  @ViewChild('gameResult') gameResult!: ElementRef<HTMLDivElement>;
  
  @Output() isTicTacToe = new EventEmitter<boolean>();

  ngOnInit() {}

  restart() {
    this.gameBoard = this.game.getTicTacToe();
    console.log(this.gameBoard);
  }

  playerClicked(id: number): void {
    this.game.updateTicTacToe(id).subscribe({
      next: (value) => {
        this.gameBoard = value.board.flat();
        this.result = value.result;

        switch (this.result) {
          case 1:
            this.gameResult.nativeElement.innerText = '';
            //this.gameResult.nativeElement.className.replace('', '');
            break;
          case 2:
            this.game.getCashAndLevel(0.1, 0.01);
            this.gameResult.nativeElement.innerText = 'Du hast Gewonnen!';
            //this.gameResult.nativeElement.className.replace('', '');
            this.gameBoard = this.game.getTicTacToe();
            //this.isMoneyUptdated.emit(true);
            break;
          case 3:
            this.gameResult.nativeElement.innerText = 'Bot hat Gewonnen';
            //this.gameResult.nativeElement.className.replace('', '');
            this.gameBoard = this.game.getTicTacToe();
            break;
          case 4:
            this.gameResult.nativeElement.innerText = 'Unentschieden';
            //this.gameResult.nativeElement.className.replace('', '');
            this.gameBoard = this.game.getTicTacToe();
            break;
          default:
            this.gameResult.nativeElement.innerText = 'Das geht nicht...';
            //this.gameResult.nativeElement.className.replace('', '');
            break;
        }
      },
      error: (err) => {
        console.error('Error updating game:', err);
      },
    });
  }
  backToMenu(){
    this.isTicTacToe.emit(false);
  }
}
