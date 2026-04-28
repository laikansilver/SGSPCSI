export interface LoginRequest {
  correoElectronico: string;
  contrasena: string;
}

export interface LoginResponse {
  usuarioId: number;
  correoElectronico: string;
  nombreCompleto?: string;
  roles: string[];
}

export interface CurrentUser {
  userId: number;
  username: string;
  role: string;
  roles: string[];
  name: string;
  loginTime: string;
}
