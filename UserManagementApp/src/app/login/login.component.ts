import { Component } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

import { AuthService } from '../../services/auth.service';
import { LoginRequest } from '../../models/auth.models';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  standalone: true,
  imports: [RouterOutlet, FormsModule]
})
export class LoginComponent {
  username = '';
  password = '';
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {} // Inyectamos Router

  login() {
    const loginRequest: LoginRequest = {
      username: this.username,
      password: this.password
    };

    this.authService.login(loginRequest).subscribe(
      (response) => {
        // Verificar si la respuesta tiene la propiedad token
        if (response && response.token) {
            localStorage.setItem('token', response.token); 
            this.router.navigate(['/users']); // Redirigir a /users
        } else {
            console.error('Error al iniciar sesión: El token no fue proporcionado en la respuesta.');
            // Manejar el error adecuadamente, por ejemplo, mostrar un mensaje al usuario
        }
      },
      (error: HttpErrorResponse) => {
        this.errorMessage = error.error.message || 'Error desconocido al iniciar sesión';
      }
    );
  }
}
