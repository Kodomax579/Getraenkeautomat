import { Component, effect } from '@angular/core';
import { productList } from '../../../Models/product.model';
import { productAmount, Cart } from '../../partials/cart/cart';
import { Data } from '../../../services/data';
import { ProductCard } from "../../partials/product-card/product-card";
import { Menu } from "../../partials/menu/menu";
import { Navbar } from "../../partials/navbar/navbar";

@Component({
  selector: 'app-home',
  imports: [ProductCard, Cart, Menu, Navbar],
  templateUrl: './home.html',
  styleUrl: './home.scss'
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
        },100);
      }
    })
  }

  isloggedin: boolean = true; //false
  products: productList[] = [];
  cart: productList[] = [];
  cartAmount: productAmount[] = [];

  ngOnInit() {
    this.data.getProducts().subscribe((data: productList[]) => {
      console.log('Produkte geladen:', data);
      this.products = data;
    });
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
