import {
  Component,
  ElementRef,
  EventEmitter,
  inject,
  Output,
  ViewChild,
} from '@angular/core';
import { Bank } from '../../../services/bank';
import { ToastrService } from 'ngx-toastr';
import { Game } from '../../../services/game';

@Component({
  selector: 'app-coin-toss',
  imports: [],
  templateUrl: './coin-toss.html',
  styleUrl: './coin-toss.scss',
})
export class CoinToss {
  private toastr = inject(ToastrService);
  private bank = inject(Bank);
  private _gameService = inject(Game);

  @ViewChild('price') priceInput!: ElementRef<HTMLInputElement>;
  @Output() leave = new EventEmitter<boolean>();

  play() {
    let isPrice: number = +this.priceInput.nativeElement.value;
    if (isPrice == null || isPrice == undefined || isPrice <= 0) return;
    let rnd = Math.floor(Math.random() * 100);
    this.bank.putSpendMoney(isPrice)?.subscribe({
      next: (val) => {
        if (val) {
          this.bank.setMoney(val.money);
          if (rnd < 35) {
            this.toastr.success(
              'Sie haben ' + isPrice * 2 + '€ erhalten.',
              'Gewonnen!'
            );
            this._gameService.getCashOnly(isPrice * 2);
          } else {
            this.toastr.warning('Sie haben nichts erhalten.', 'Verloren!');
          }
        }
      },
      error: (err) => {
        this.toastr.error(err, 'Fehler');
        return err;
      },
    });
  }
  
  onLeave() {
    this.leave.emit(true);
  }
}
