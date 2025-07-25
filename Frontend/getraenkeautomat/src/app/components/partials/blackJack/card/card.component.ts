import { Component, computed, input } from '@angular/core';
import { CardModel } from '../../../../Models/Card.Model';

@Component({
  selector: 'app-card',
  imports: [],
  templateUrl: './card.component.html',
  styleUrl: './card.component.scss'
})
export class CardComponent {
  card = input<CardModel>();

  suitSymbol = computed(() => {
    const suit = this.card()?.suit;
    switch (suit) {
      case 'Hearts': return '♥';
      case 'Diamonds': return '♦';
      case 'Clubs': return '♣';
      case 'Spades': return '♠';
      default: return '';
    }
  });

  suitColor = computed(() => {
    const suit = this.card()?.suit;
    return (suit === 'Hearts' || suit === 'Diamonds') ? 'red' : 'black';
  });
}
