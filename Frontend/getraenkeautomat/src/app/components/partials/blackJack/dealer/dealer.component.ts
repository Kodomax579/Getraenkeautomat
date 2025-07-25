import { Component, effect, input, OnChanges, SimpleChanges } from '@angular/core';
import { CardComponent } from "../card/card.component";
import { CardModel } from '../../../../Models/Card.Model';

@Component({
  selector: 'app-dealer',
  imports: [CardComponent],
  templateUrl: './dealer.component.html',
  styleUrl: './dealer.component.scss'
})
export class DealerComponent {
  card = input<CardModel | null>();
  cards: CardModel[] = [];

  constructor() {
    effect(() => {
      const newCard = this.card();
      if (newCard) {
        this.cards.push({... newCard});
      }
    });
  }
}