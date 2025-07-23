import { Component, input } from '@angular/core';
import { productList } from '../../../Models/product.model';

@Component({
  selector: 'app-product-card',
  imports: [],
  templateUrl: './product-card.html',
  styleUrl: './product-card.scss',
})
export class ProductCard {
  public item = input.required<productList>();

  
}