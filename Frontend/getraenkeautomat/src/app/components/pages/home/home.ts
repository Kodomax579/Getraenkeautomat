import { Component, effect } from '@angular/core';
import { ProductCard } from '../../partials/product-card/product-card';
import { SignInUp } from '../../partials/sign-in-up/sign-in-up';
import { Cart, productAmount } from '../../partials/cart/cart';
import { CommonModule } from '@angular/common';
import { Profile } from '../../partials/profile/profile';
import { Data } from '../../../services/data';
import { Menu } from "../../partials/menu/menu";
import { productList } from '../../../Models/product.model';

@Component({
  selector: 'app-home',
  imports: [ProductCard, SignInUp, Cart, CommonModule, Profile, Menu],
  templateUrl: './home.html',
  styleUrl: './home.scss',
})
export class Home {
  constructor(private data: Data) 
  {
    effect(()=>{
      const val = this.data.isUpdated()
      console.log(val)
      if(val)
      {
        setTimeout(() => {
          this.ngOnInit()
          this.data.isUpdated.set(false)
        }, this.cart.length * 20);
      }
    })
  }

  isloggedin: boolean = false; //false
  products: productList[] = [];
  cart: productList[] = [];
  cartAmount: productAmount[] = [];

  ngOnInit() {
    this.data.getProducts().subscribe((data: productList[]) => {
      console.log('Produkte geladen:', data);
      this.products = data;
    });
  }

  receiveMessage(isLoggedIn: boolean) {
    this.isloggedin = isLoggedIn;
  }

  addProduct(item: productList) {
    const existing = this.cartAmount.find((e) => e.id === item.id);
    if (!this.cart.includes(item)) {
      this.cart.push(item);
      this.cartAmount.push({ id: item.id, amount: 1 });
      console.log(this.cart);
      console.log(this.cartAmount);
    } else if (existing && existing.amount < item.amount) {
      if (existing) {
        existing.amount += 1;
        console.log(this.cartAmount);
      }
    }
  }

  onRemoveItem(itemId: number) {
    this.cart = this.cart.filter((item) => item.id !== itemId);
    this.cartAmount = this.cartAmount.filter((e) => e.id !== itemId);
  }

  public onPurchase(isPurchased : boolean)
  {
    if(isPurchased)
    {
      this.cart = []
      this.cartAmount = []
      this.data.isUpdated.set(true);
      
    }
  }
}
