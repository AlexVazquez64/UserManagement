import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { LoginRequest, LoginResponse } from '../models/auth.models'; // Asegúrate de crear este archivo de modelos

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'http://localhost:5147/api/auth/login'; // URL de tu API de autenticación

  constructor(private http: HttpClient) {}

  login(loginRequest: LoginRequest): Observable<LoginResponse> {
    console.log(loginRequest)
    return this.http.post<LoginResponse>(this.apiUrl, loginRequest).pipe(
      tap((response) => {
        console.log(response)
        // Almacenar el token JWT en el almacenamiento local o sessionStorage
        localStorage.setItem('token', response.token);
      }),
      catchError((error) => {
        // Manejar el error de autenticación
        console.error('Error al iniciar sesión:', error);
        return throwError(error); // Puedes personalizar el manejo de errores aquí
      })
    );
  }

  isLoggedIn(): boolean {
    const token = localStorage.getItem('token');
    return !!token; // Doble negación para convertir a booleano (true si hay token, false si no)
  }
}
