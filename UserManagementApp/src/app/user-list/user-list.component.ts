import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../../models/user.models';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css'],
})
export class UserListComponent implements OnInit {
  users: User[] = [];
  loading = true;
  error: string | null = null;

  constructor(private userService: UserService, private router: Router) {}

  ngOnInit() {
    this.loadUsers();
  }

  loadUsers() {
    this.userService.getUsers().subscribe({
      next: (users) => {
        this.users = users;
        this.loading = false;
      },
      error: (error) => {
        this.error = error.message || 'Error al cargar usuarios';
        this.loading = false;
      },
    });
  }

  createUser() {
    this.router.navigate(['/user/create']); // Navegar al formulario de creación
  }

  editUser(user: User) {
    this.router.navigate(['/user/edit', user.id]).catch((error) => {
      console.error('Error al navegar a la edición de usuario:', error);
      this.error = 'Error al navegar a la edición de usuario';
    });
  }

  deleteUser(user: User) {
    if (confirm(`¿Estás seguro de que quieres eliminar a ${user.username}?`)) {
      this.userService.deleteUser(user.id).subscribe({
        next: () => this.loadUsers(),
        error: (error) => {
          console.error('Error al eliminar usuario:', error);
          this.error = error.message || 'Error al eliminar usuario';
        },
      });
    }
  }
}
