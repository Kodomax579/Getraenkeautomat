import { Component, effect, EventEmitter, inject, OnInit, Output } from '@angular/core';
import { Auth } from '../../../services/auth';
import { userModel } from '../../../Models/user.model';
import { Bank } from '../../../services/bank';

@Component({
  selector: 'app-profile',
  imports: [],
  templateUrl: './profile.html',
  styleUrl: './profile.scss',
})
export class Profile implements OnInit{

  private authService = inject(Auth);
  private bankService = inject(Bank);
  user: userModel| undefined = undefined
  money: number |undefined = undefined;
 
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


  ngOnInit(): void {
    this.user = this.authService.getUser();

    if(this.user === undefined)
      return
    this.bankService.getCurrentAccountBalance(this.user.id).subscribe({
      next:(val) => {
        this.bankService.setMoney(val.money);
        this.money = val.money
      }
    })
  }

  receiveMessage($event:boolean){
    this.islogout.emit($event);
  }
}
