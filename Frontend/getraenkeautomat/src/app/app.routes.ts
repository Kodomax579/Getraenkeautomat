import { Routes } from '@angular/router';
import { Login } from './components/pages/Login/login';
import { Home } from './components/pages/home/home';

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
  }
];
