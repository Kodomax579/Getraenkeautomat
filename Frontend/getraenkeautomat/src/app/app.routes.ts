import { Routes } from '@angular/router';
import { Login } from './components/pages/home/login';
import { Home } from './components/pages/home/home';

export const routes: Routes = [
  {path:"", component:Login},
  {path:"Home", component:Home},
];
