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
  // Rutas de formularios
  {
    path: 'formulario/urgente',
    loadComponent: () => import('./pages/formularios/formulario-urgente/formulario-urgente.component').then(m => m.FormularioUrgenteComponent)
  },
  {
    path: 'formulario/modificacion',
    loadComponent: () => import('./pages/formularios/formulario-modificacion/formulario-modificacion.component').then(m => m.FormularioModificacionComponent)
  },
  {
    path: 'formulario/viabilidad',
    loadComponent: () => import('./pages/formularios/formulario-viabilidad/formulario-viabilidad.component').then(m => m.FormularioViabilidadComponent)
  },
  {
    path: 'formulario/requerimientos',
    loadComponent: () => import('./pages/formularios/formulario-requerimientos/formulario-requerimientos.component').then(m => m.FormularioRequerimientosComponent)
  },
  {
    path: '**',
    redirectTo: 'login'
  }
];
