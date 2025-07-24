import { Routes } from '@angular/router';
import { Login } from './components/pages/Login/login';
import { Home } from './components/pages/home/home';
import { Shop } from './components/pages/shop/shop';
import { Games } from './components/pages/games/games';

export const routes: Routes = [
  {
    path:"",
    redirectTo:"Login",
    pathMatch:'full'
  },
  {
    path:"Home",
    component:Home
  },
  {
    path:"Login",
    component:Login
  },
  {
    path:"Shop",
    component:Shop
  },
  {
    path:"Games",
    component:Games
  }
];
