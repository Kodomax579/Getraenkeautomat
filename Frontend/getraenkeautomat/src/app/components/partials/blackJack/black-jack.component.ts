import { Component, EventEmitter, OnInit, Output, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DealerComponent } from './dealer/dealer.component';
import { PlayerComponent } from './player/player.component';
import { BlackJackServiceService } from '../../../services/black-jack-service.service';
import { CardModel } from '../../../Models/Card.Model';
import { WinModel } from '../../../Models/Win.Model';
import { Game } from '../../../services/game';
import { InputNumber } from 'primeng/inputnumber';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { Bank } from '../../../services/bank';

@Component({
  selector: 'app-blackjack',
  standalone: true,
  imports: [CommonModule, DealerComponent, PlayerComponent, InputNumber, FormsModule, ButtonModule],
  templateUrl: './black-jack.component.html',
  styleUrl: './black-jack.component.scss'
})
export class BlackjackComponent {
  private blackjackService = inject(BlackJackServiceService);
  private gameService = inject(Game);
  private bankService = inject(Bank)

  isNewGame: boolean = false;
  dealerCard?: CardModel;
  playerCard?: CardModel;
  playerScore: number = 0;
  dealerScore: number = 0;
  playerButtonDisable: boolean = false;
  Winner: WinModel | undefined;
  moneyValue: number = 0;

  @Output() leave = new EventEmitter<boolean>();

  async startGame(){ 
    if(this.moneyValue > 0)
    {
      this.bankService.putSpendMoney(this.moneyValue)?.subscribe({
        next:(val)=>{

          this.bankService.setMoney(val.money)
          this.playerButtonDisable = false

          this.blackjackService.NewGame(this.moneyValue).subscribe({
          next: async (val) => {
            this.isNewGame = val;
            await this.drawCard(true);
            await this.drawCard(false);
            }
          });
        }
      })

    }
  }

  onPlayer(): void {
    this.drawCard(false).then((card) => {
      this.playerScore = card.totalScore;
    });
  }

  onStay(): void {
    this.playerButtonDisable = true;
    this.dealerTurn();
  }

  private dealerTurn(): void {
    if (this.playerScore > 21) {
      return this.finishGame();
    }

    this.drawCard(true).then((card) => {
      this.dealerScore = card.totalScore;

      if (this.dealerScore < this.playerScore && this.dealerScore <= 21) {
        setTimeout(() => this.dealerTurn(), 700);
      } else {
        setTimeout(() => this.finishGame(), 1000);
      }
    });
  }

  private finishGame(): void {
    this.blackjackService.WhoWins().subscribe({
      next: (result) => {
        this.Winner = result;
        this.isNewGame = false;
        this.dealerCard = undefined;
        this.playerCard = undefined
        this.gameService.getCashOnly(this.Winner.money);
      }
    });
  }

  private async drawCard(isDealer: boolean): Promise<CardModel> {
    return new Promise<CardModel>((resolve) => {
      this.blackjackService.GetSingleCard(isDealer).subscribe({
        next: (card) => {
          if (isDealer) {
            this.dealerCard = card;
            this.dealerScore = card.totalScore;
          } else {
            this.playerCard = card;
            this.playerScore = card.totalScore
          }
          resolve(card);
        }
      });
    });
  }
  onLeave() {
    this.leave.emit(true);
  }
}
