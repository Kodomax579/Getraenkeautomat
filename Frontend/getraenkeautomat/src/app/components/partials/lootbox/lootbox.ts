import { Component, EventEmitter, inject, Output } from '@angular/core';
import { LootboxService } from '../../../services/lootbox';
import { lootboxModel } from '../../../Models/lootbox.model';
import { __values } from 'tslib';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { Bank } from '../../../services/bank';
import { Game } from '../../../services/game';

@Component({
  selector: 'app-lootbox',
  imports: [CommonModule],
  templateUrl: './lootbox.html',
  styleUrl: './lootbox.scss',
})
export class Lootbox {
  private toastr = inject(ToastrService);
  private lb = inject(LootboxService);
  private bank = inject(Bank);
  private gameService = inject(Game);

  lootboxes: lootboxModel[] = [];
  
  @Output() leave = new EventEmitter<boolean>();

  ngOnInit() {
    this.lb.getLootboxes().subscribe({
      next: (value) => {
        this.lootboxes = value;
      },
      error: (err) => {
        return err;
      },
    });
    console.log(this.lootboxes);
  }

  startGambling(id: number, isPrice: number) {
    this.bank.putSpendMoney(isPrice)?.subscribe({
      next: (val) => {
        if (val) {
          this.bank.setMoney(val.money);
          this.lb.getLootbox(id).subscribe({
            next: (value) => {
              switch (value) {
                case -1:
                  this.toastr.error('Etwas ist schief gelaufen', 'Fehler!');
                  break;
                case 0:
                  this.toastr.warning(
                    'Sie haben nichts erhalten.',
                    'Verloren!'
                  );
                  break;
                default:
                  this.toastr.success(
                    'Sie haben ' + value + '€ erhalten.',
                    'Gewonnen!'
                  );
                  this.gameService.getCashOnly(value);
              }
              console.log(value);
            },
            error: (err) => {
              this.toastr.error(err, 'Fehler');
              return err;
            },
          });
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
