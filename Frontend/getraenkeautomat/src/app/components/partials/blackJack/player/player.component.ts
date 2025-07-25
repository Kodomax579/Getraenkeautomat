import { Component, effect, input } from '@angular/core';
import { CardComponent } from "../card/card.component";
import { CardModel } from '../../../../Models/Card.Model';

@Component({
  selector: 'app-player',
  imports: [CardComponent],
  templateUrl: './player.component.html',
  styleUrl: './player.component.scss'
})
export class PlayerComponent {
  card = input<CardModel |null>();
  cards: CardModel[] = [];

  constructor() {
    effect(() => {
      const newCard = this.card();
      if (newCard) {
        this.cards.push({... newCard});
      }
      console.log(this.cards)
    });
  }
}
