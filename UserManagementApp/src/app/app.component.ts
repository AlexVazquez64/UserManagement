import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { LoginComponent } from './login/login.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, LoginComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'UserManagementApp';

  constructor(private authService: AuthService) {
    const router = inject(Router);

    if (!this.authService.isLoggedIn()) {
      router.navigate(['/login']); // Redirigir al login al iniciar
    } else {
      // Redirigir a la página de inicio (por ejemplo, '/users')
      router.navigate(['/users']);
    }
  }

}
