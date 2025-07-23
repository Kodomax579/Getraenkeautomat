import { CommonModule } from '@angular/common';
import { Component, ElementRef, EventEmitter, Output, ViewChild } from '@angular/core';
import { Auth } from '../../../services/auth';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-sign-in-up',
  imports: [CommonModule],
  templateUrl: './sign-in-up.html',
  styleUrl: './sign-in-up.scss',
})
export class SignInUp {
  constructor(private auth: Auth, private toastr:ToastrService) {}

  isloginHeaderPressed: boolean = true;
  isBtnPressed: boolean = false;

  @ViewChild('userNameOrEmail') userNameOrEmail!: ElementRef<HTMLInputElement>;
  @ViewChild('userName') userName!: ElementRef<HTMLInputElement>;
  @ViewChild('userEmail') userEmail!: ElementRef<HTMLInputElement>;
  @ViewChild('userPassword') userPassword!: ElementRef<HTMLInputElement>;

  @Output() isLoggedIn = new EventEmitter<boolean>();

  register(): void {
    const uName = this.userName?.nativeElement.value;
    const uEmail = this.userEmail?.nativeElement.value;
    const uPassword = this.userPassword?.nativeElement.value;

    if (!uName || !uEmail || !uPassword) {
      this.toastr.error('Benutzername / Email oder Passwort fehlt!', 'Fehler');
      return;
    }
    if (uName.length < 3) {
      this.toastr.error('Benutzername muss mindestens 3 Buchstaben lang sein!', 'Fehler');
      return;
    }
    if (uEmail.length < 3 || !uEmail.includes('@')) {
      this.toastr.error('Bitte geben Sie eine vollständige Email ein!', 'Fehler');
      return;
    }
    if (uPassword.length < 8) {
      this.toastr.error('Passwort muss mindestens 8 Zeichen lang sein!', 'Fehler');
      return;
    }

    this.auth.register(uName, uEmail, uPassword).subscribe(
      {
        next: (val) => {
          this.auth.setUser(val);
          this.isLoggedIn.emit(true);
        }
      }
    );
  }

  login(): void {
    const uNameEmail = this.userNameOrEmail?.nativeElement.value;
    const uPassword = this.userPassword?.nativeElement.value;

    if (!uNameEmail || !uPassword) {
      this.toastr.error('Benutzername oder Passwort fehlt', 'Error');
      return;
    }
    if (uNameEmail.length < 3) {
      this.toastr.error('Benutzername muss mindestens 3 Buchstaben lang sein!', 'Fehler');
      return;
    }
    if (uPassword.length < 8) {
      this.toastr.error('Passwort muss mindestens 8 Zeichen lang sein!', 'Fehler');
      return;
    }
     this.auth.login(uNameEmail,uPassword).subscribe(
      {
        next: (val) => {
          this.auth.setUser(val);
          this.isLoggedIn.emit(true);
        }
      }
    );
  }
}
