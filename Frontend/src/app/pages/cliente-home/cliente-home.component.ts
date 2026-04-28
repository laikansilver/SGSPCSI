import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cliente-home',
  imports: [CommonModule],
  templateUrl: './cliente-home.component.html',
  styleUrl: './cliente-home.component.scss'
})
export class ClienteHomeComponent implements OnInit {
  sidebarOpen = false;
  userMenuOpen = false;
  notificationsOpen = false;
  
  readonly currentUser = this.readCurrentUser();

  constructor(private readonly router: Router) {}

  ngOnInit(): void {
    // Verificar si hay sesión activa
    if (!sessionStorage.getItem('currentUser')) {
      this.router.navigateByUrl('/login');
    }
  }

  toggleSidebar(): void {
    this.sidebarOpen = !this.sidebarOpen;
  }

  toggleUserMenu(): void {
    this.userMenuOpen = !this.userMenuOpen;
    if (this.userMenuOpen) {
      this.notificationsOpen = false;
    }
  }

  toggleNotifications(): void {
    this.notificationsOpen = !this.notificationsOpen;
    if (this.notificationsOpen) {
      this.userMenuOpen = false;
    }
  }

  logout(): void {
    sessionStorage.removeItem('currentUser');
    localStorage.removeItem('rememberedUser');
    this.router.navigateByUrl('/login');
  }

  private readCurrentUser(): { name: string; role: string; username: string } {
    const raw = sessionStorage.getItem('currentUser');
    if (!raw) {
      return { name: 'Invitado', role: 'user', username: 'sin-sesion' };
    }
    try {
      const parsed = JSON.parse(raw) as { name?: string; role?: string; username?: string };
      return {
        name: parsed.name || 'Usuario',
        role: parsed.role || 'user',
        username: parsed.username || 'sin-sesion'
      };
    } catch {
      return { name: 'Invitado', role: 'user', username: 'sin-sesion' };
    }
  }
}
