import {
  Component,
  ElementRef,
  EventEmitter,
  inject,
  Output,
  signal,
  ViewChild,
} from "@angular/core";
import { Circle } from "./circle/circle";
import { Game } from "../../../services/game";
import { CommonModule } from "@angular/common";

@Component({
  selector: "app-aimlabs",
  imports: [Circle, CommonModule],
  templateUrl: "./aimlabs.html",
  styleUrl: "./aimlabs.scss",
})
export class Aimlabs {
  private gameService = inject(Game);

  public counter: number = 0;
  public top = signal<number>(0);
  public left = signal<number>(0);

  @Output() leave = new EventEmitter<boolean>();
  @ViewChild("background") backgroundRef!: ElementRef<HTMLDivElement>;

  currentBg: string = "/getraenkeautomat/assets/BadBackground.png";
  fallbackBg: string = "assets/BadBackground.png";

  ngOnInit() {
    const img: HTMLImageElement = new Image();
    img.src = this.currentBg;
    img.onerror = () => {
      this.currentBg = this.fallbackBg;
    };
  }

  ngAfterViewInit() {
    const backgroundEl: HTMLDivElement = this.backgroundRef.nativeElement;
    const width: number = backgroundEl.clientWidth - 96;
    const height: number = backgroundEl.clientHeight - 96;

    this.top.set(this.getRandomInt(height));
    this.left.set(this.getRandomInt(width));
  }

  onClick() {
    this.counter++;
    this.ngAfterViewInit();
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
