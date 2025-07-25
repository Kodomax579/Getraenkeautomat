import { Component, effect, inject, runInInjectionContext } from '@angular/core';
import { Navbar } from "../../partials/navbar/navbar";
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  imports: [Navbar],
  templateUrl: './home.html',
  styleUrl: './home.scss'
})
export class Home {
  private router = inject(Router)
startGame() {
  this.router.navigate(['/Games']);
}

}
