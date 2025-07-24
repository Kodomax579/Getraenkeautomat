import { Component, effect, inject } from '@angular/core';
import { productList } from '../../../Models/product.model';
import { productAmount } from '../../partials/cart/cart';
import { Data } from '../../../services/data';
import { Navbar } from "../../partials/navbar/navbar";
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  imports: [Navbar],
  templateUrl: './home.html',
  styleUrl: './home.scss'
})
export class Home {
private route = inject(Router)

  isLogedOut()
  {
    this.route.navigateByUrl("Login")
  }
}
