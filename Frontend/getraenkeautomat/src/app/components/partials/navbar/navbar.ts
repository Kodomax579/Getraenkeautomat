import { Component, effect, EventEmitter, inject, OnInit, Output } from '@angular/core';
import { MenuItem, PrimeIcons } from 'primeng/api';
import { MenuModule } from 'primeng/menu';
import { BadgeModule } from 'primeng/badge';
import { RippleModule } from 'primeng/ripple';
import { AvatarModule } from 'primeng/avatar';
import { Auth } from '../../../services/auth';
import { userModel } from '../../../Models/user.model';
import { Bank } from '../../../services/bank';


@Component({
  selector: 'app-navbar',
  imports: [MenuModule, BadgeModule, RippleModule, AvatarModule],
  templateUrl: './navbar.html',
  styleUrl: './navbar.scss',
})

export class Navbar implements OnInit {
  private authService = inject(Auth)
  private bankService = inject(Bank)

  money : number |undefined;
  user : userModel |undefined;
  items : MenuItem[] | undefined;
  isToggle : boolean = false;

  @Output() islogout = new EventEmitter<boolean>();
  constructor()
  {
    effect(()=>{
      const val = this.bankService.money();

      if(val)
        this.money = val
    })
    
    effect(()=>{
      const val = this.authService.user();

      if(val)
        this.user = val
    })
  }

 ngOnInit() {
    this.items = [
        {
            label: 'Shop',
            icon: PrimeIcons.SHOPPING_CART,
        },
        {
            label: 'Game',
            icon: PrimeIcons.CROWN
        }
    ];
    this.user = this.authService.getUser()
  }

    receiveMessage($event:boolean){
    this.islogout.emit($event);
  }
}
