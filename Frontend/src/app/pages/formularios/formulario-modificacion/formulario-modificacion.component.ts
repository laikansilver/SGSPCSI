import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-formulario-modificacion',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="formulario-container">
      <div class="formulario-header">
        <button class="back-btn" (click)="goBack()">
          <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
            <path d="M19 12H5M12 19l-7-7 7-7"/>
          </svg>
          Volver al Home
        </button>
        <h1>Formulario de Modificación</h1>
      </div>
      <iframe 
        src="/assets/formularios/Formulario_modificacion.html" 
        class="formulario-iframe"
        frameborder="0"
      ></iframe>
    </div>
  `,
  styles: [`
    .formulario-container {
      min-height: 100vh;
      background: #C8D8EB;
    }
    .formulario-header {
      background: white;
      padding: 16px 24px;
      box-shadow: 0 2px 10px rgba(0,0,0,0.1);
      display: flex;
      align-items: center;
      gap: 20px;
    }
    .back-btn {
      display: flex;
      align-items: center;
      gap: 8px;
      background: #004B87;
      color: white;
      border: none;
      padding: 10px 16px;
      border-radius: 8px;
      cursor: pointer;
      font-weight: 600;
      transition: background 0.3s;
    }
    .back-btn:hover {
      background: #003B6D;
    }
    .formulario-header h1 {
      margin: 0;
      color: #004B87;
      font-size: 20px;
    }
    .formulario-iframe {
      width: 100%;
      height: calc(100vh - 80px);
      border: none;
    }
  `]
})
export class FormularioModificacionComponent implements OnInit {
  constructor(private readonly router: Router) {}

  ngOnInit(): void {}

  goBack(): void {
    this.router.navigateByUrl('/home/cliente');
  }
}