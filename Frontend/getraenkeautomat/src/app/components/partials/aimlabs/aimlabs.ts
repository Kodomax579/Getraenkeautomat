import { Component, EventEmitter, inject, Output, signal } from '@angular/core';
import { Circle } from './circle/circle';
import { Game } from '../../../services/game';

@Component({
  selector: 'app-aimlabs',
  imports: [Circle],
  templateUrl: './aimlabs.html',
  styleUrl: './aimlabs.scss',
})
export class Aimlabs {
  private gameService = inject(Game);

  public counter: number = 0;
  public top = signal<number>(0);
  public left = signal<number>(0);

  @Output() leave = new EventEmitter<boolean>();

  ngOnInit(): void {
    this.top.set(this.getRandomInt(300));
    if (window.innerWidth < 1280) {
      this.left.set(this.getRandomInt(300));
    } else {
      this.left.set(this.getRandomInt(500));
    }
  }

  onClick() {
    this.counter++;
    this.ngOnInit();
    if (this.counter % 5 === 0) {
      this.gameService.getCashAndLevel(0.1, 0.1);
    }
  }

  getRandomInt(max: number) {
    return Math.floor(Math.random() * max);
  }

  onLeave() {
    this.leave.emit(true);
  }
}
