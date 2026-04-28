import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CurrentUser, LoginResponse } from '../../models/auth.model';

@Component({
  selector: 'app-login',
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {
  username = '';
  password = '';
  rememberMe = false;
  showPassword = false;
  loading = false;
  alertType: 'success' | 'error' | '' = '';
  alertMessage = '';

  constructor(
    private readonly authService: AuthService,
    private readonly router: Router
  ) {}

  ngOnInit(): void {
    const savedUsername = localStorage.getItem('rememberedUser');
    if (savedUsername) {
      this.username = savedUsername;
      this.rememberMe = true;
    }
  }

  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }

  onSubmit(): void {
    if (!this.username.trim() || !this.password.trim() || this.loading) {
      return;
    }

    this.loading = true;
    this.alertType = '';
    this.alertMessage = '';

    const payload = {
      correoElectronico: this.username.trim(),
      contrasena: this.password
    };

    this.authService.login(payload).subscribe({
      next: (data: LoginResponse) => {
        const roleData = resolveRoleFromRoles(data.roles);

        if (this.rememberMe) {
          localStorage.setItem('rememberedUser', this.username.trim());
        } else {
          localStorage.removeItem('rememberedUser');
        }

        const currentUser: CurrentUser = {
          userId: data.usuarioId,
          username: data.correoElectronico,
          role: roleData.role,
          roles: Array.isArray(data.roles) ? data.roles : [],
          name: data.nombreCompleto || data.correoElectronico,
          loginTime: new Date().toISOString()
        };

        sessionStorage.setItem('currentUser', JSON.stringify(currentUser));
        this.alertType = 'success';
        this.alertMessage = 'Acceso autorizado. Redirigiendo al sistema...';

        setTimeout(() => {
          void this.router.navigateByUrl(roleData.redirectTo);
        }, 900);
      },
      error: () => {
        this.alertType = 'error';
        this.alertMessage = 'Credenciales invalidas o API no disponible.';
        this.password = '';
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      }
    });
  }

  handleForgotPassword(event: MouseEvent): void {
    event.preventDefault();
    this.alertType = 'error';
    this.alertMessage = 'Para recuperar tu contrasena, contacta al area de Sistemas Institucionales.';
  }
}

function resolveRoleFromRoles(roles: unknown): { role: string; redirectTo: string } {
  const normalized = Array.isArray(roles)
    ? roles.map(value => String(value).trim().toLowerCase())
    : [];

  if (normalized.includes('product_manager') || normalized.includes('admin')) {
    return { role: 'product_manager', redirectTo: '/home/pm' };
  }

  if (normalized.includes('developer')) {
    return { role: 'developer', redirectTo: '/home/developer' };
  }

  return { role: 'user', redirectTo: '/home/cliente' };
}
