// app.routes.ts
import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { UserListComponent } from './user-list/user-list.component'; // Asegúrate de importar el componente UserListComponent
import { authGuard } from '../guards/auth.guard'; // Importaremos el AuthGuard

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' }, // Redireccionar a login por defecto
  { path: 'login', component: LoginComponent },
  { path: 'users', component: UserListComponent, canActivate: [authGuard] } // Proteger la ruta de usuarios
];
