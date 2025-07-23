import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { userModel } from '../Models/user.model';

@Injectable({
  providedIn: 'root',
})
export class Auth {
  constructor(private http: HttpClient) {}

  public user = signal<userModel | undefined>(undefined);

  api: string = 'http://localhost:9002/';

  register(username: string, email: string, password: string) {
    return this.http.post<userModel>(`${this.api}CreateUser`, {
      name: username,
      email: email,
      password: password,
    });
  }

  login(username: string, password: string) {
    const params = new HttpParams()
      .set('uName', username)
      .set('uPassword', password);

    return this.http.get<userModel>(`${this.api}Login`, { params });
  }

  setUser(user: userModel) {
    this.user.set(user);
  }

  updateLevel(level: number) {
    return this.http.put<userModel>(`${this.api}UpdateUser`, {
      id: this.user()?.id,
      name: this.user?.name,
      level: level,
    });
  }

  public getUser(): userModel | undefined {
    const user = this.user;

    if (user) return user();

    return undefined;
  }
}
