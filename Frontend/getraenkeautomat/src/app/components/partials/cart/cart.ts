import { Component, EventEmitter, inject, input, Output } from '@angular/core';
import { Data } from '../../../services/data';
import { Bank } from '../../../services/bank';
import { Game } from '../../../services/game';
import { productList } from '../../../Models/product.model';

export interface productAmount {
  id: number;
  amount: number;
}

@Component({
  selector: 'app-cart',
  imports: [],
  templateUrl: './cart.html',
  styleUrl: './cart.scss',
})
export class Cart {
  private _bankService = inject(Bank);
  private _dataService = inject(Data);
  private _gameService = inject(Game);
  public cartItems = input.required<productList[]>();
  public itemAmount = input.required<productAmount[]>();

  @Output() removeItem = new EventEmitter<number>();
  @Output() onPurchase = new EventEmitter<boolean>();

  totalPrice: number = 0;

  getAmountForItem(itemId: number): number | null {
    let match = this.itemAmount().find((a) => a.id === itemId);
    const totalItems: number | undefined = this.cartItems().find(
      (e) => e.id === itemId
    )?.amount;
    if (match && totalItems) {
      if (match.amount <= totalItems) {
        return match ? match.amount : null;
      }
      match.amount = totalItems;
      return match.amount;
    }
    return 0;
  }

  increaseAmount(itemId: number) {
    const item = this.itemAmount().find((a) => a.id === itemId);
    const totalItems: number | undefined = this.cartItems().find(
      (e) => e.id === itemId
    )?.amount;
    if (item && totalItems && item.amount < totalItems) {
      item.amount++;
      console.log(item.amount);
    }
  }

  decreaseAmount(itemId: number) {
    const item = this.itemAmount().find((a) => a.id === itemId);
    if (item && item.amount > 1) {
      item.amount--;
      console.log(item.amount);
    }
  }

  deleteItem(itemId: number) {
    this.removeItem.emit(itemId);
  }

  getTotalPrice(): number {
    this.totalPrice = 0;
    this.cartItems().forEach((e) => {
      let amount: number | undefined = this.itemAmount().find(
        (a) => a.id === e.id
      )?.amount;
      if (amount) {
        this.totalPrice += e.price * amount;
      }
    });
    return this.totalPrice;
  }

  buy() {
    this._gameService.getCashAndLevel(0, 0.1);
    this._bankService.putSpendMoney(this.getTotalPrice())?.subscribe({
      next: (val) => {
        if (val) {
          this._bankService.setMoney(val.money);

          for (let product of this.cartItems()) {
            let productAmount: number = 1;
            const singleCartAmount = this.itemAmount().find(
              (p) => p.id === product.id
            );
            if (singleCartAmount) {
              productAmount = product.amount - singleCartAmount.amount;
              console.log(productAmount);
              this._dataService
                .updateProducts(product.name, product.amount, productAmount)
                .subscribe();
              this.onPurchase.emit(true);
            }
          }
        }
      },
    });
  }
}
