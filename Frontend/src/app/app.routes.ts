import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'login'
  },
  {
    path: 'login',
    loadComponent: () => import('./pages/login/login.component').then(m => m.LoginComponent)
  },
  {
    path: 'home',
    loadComponent: () => import('./pages/home/home.component').then(m => m.HomeComponent)
  },
  {
    path: 'home/cliente',
    loadComponent: () => import('./pages/cliente-home/cliente-home.component').then(m => m.ClienteHomeComponent)
  },
  {
    path: 'home/developer',
    loadComponent: () => import('./pages/developer-home/developer-home.component').then(m => m.DeveloperHomeComponent)
  },
  {
    path: 'home/pm',
    loadComponent: () => import('./pages/pm-home/pm-home.component').then(m => m.PmHomeComponent)
  },
  {
    path: '**',
    redirectTo: 'login'
  }
];
