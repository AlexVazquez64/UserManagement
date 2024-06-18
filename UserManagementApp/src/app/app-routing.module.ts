// app-routing.module.ts
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { UserListComponent } from './user-list/user-list.component'; // Asegúrate de importar el componente UserListComponent
import { authGuard } from '../guards/auth.guard'; // Importaremos el AuthGuard más adelante
import { UserFormComponent } from './user-form/user-form.component';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' }, // Redireccionar a login por defecto
  { path: 'login', component: LoginComponent },
  { path: 'users', component: UserListComponent, canActivate: [authGuard] },
  { path: 'user/create', component: UserFormComponent }, // Ruta para crear usuario
  { path: 'user/edit/:id', component: UserFormComponent, canActivate: [authGuard] } // Ruta para editar usuario
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
