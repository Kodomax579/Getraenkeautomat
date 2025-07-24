import { Component, inject } from '@angular/core';
import { SignInUp } from '../../partials/sign-in-up/sign-in-up';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  imports: [SignInUp, CommonModule],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login {
  private route = inject(Router)

  
  receiveMessage(isLoggedIn: boolean) {
    if(isLoggedIn === true)
    {
      this.route.navigateByUrl("Home")
    }
  }
}
