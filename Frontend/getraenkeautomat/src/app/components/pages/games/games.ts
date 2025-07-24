import { Component } from '@angular/core';
import { Navbar } from "../../partials/navbar/navbar";
import { Menu } from "../../partials/menu/menu";

@Component({
  selector: 'app-games',
  imports: [Navbar, Menu],
  templateUrl: './games.html',
  styleUrl: './games.scss'
})
export class Games {

}
