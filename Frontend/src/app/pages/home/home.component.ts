import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-home',
  imports: [CommonModule, RouterLink],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  readonly currentUser = readCurrentUser();
  readonly roleCards = [
    {
      title: 'Cliente',
      description: 'Consultar solicitudes, crear nuevas peticiones y revisar el seguimiento.',
      path: '/home/cliente'
    },
    {
      title: 'Developer',
      description: 'Atender solicitudes asignadas, registrar avances y documentar cambios.',
      path: '/home/developer'
    },
    {
      title: 'Product Manager',
      description: 'Aprobar solicitudes, revisar prioridades y coordinar la operación.',
      path: '/home/pm'
    }
  ];
}

function readCurrentUser(): { name: string; role: string; username: string } {
  const raw = sessionStorage.getItem('currentUser');

  if (!raw) {
    return {
      name: 'Invitado',
      role: 'user',
      username: 'sin-sesion'
    };
  }

  try {
    const parsed = JSON.parse(raw) as { name?: string; role?: string; username?: string };
    return {
      name: parsed.name || 'Usuario',
      role: parsed.role || 'user',
      username: parsed.username || 'sin-sesion'
    };
  } catch {
    return {
      name: 'Invitado',
      role: 'user',
      username: 'sin-sesion'
    };
  }
}
