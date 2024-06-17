import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
  FormGroup,
  FormControl,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { User } from '../../models/user.models';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.css'],
  standalone: true,
  imports: [ReactiveFormsModule], // Importar ReactiveFormsModule para formularios reactivos
})
export class UserFormComponent implements OnInit {
  userForm!: FormGroup;
  isEditing = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService
  ) {}

  ngOnInit() {
    this.userForm = new FormGroup({
      username: new FormControl('', Validators.required),
      email: new FormControl('', Validators.email),
      password: new FormControl(''),
    });

    const userId = this.route.snapshot.paramMap.get('id');
    if (userId) {
      this.isEditing = true;
      this.userService.getUser(+userId).subscribe((user) => {
        this.userForm.patchValue(user);
        this.userForm.get('password')?.clearValidators(); // No requerir contraseña al editar
        this.userForm.get('password')?.updateValueAndValidity();
      });
    }
  }

  saveUser() {
    if (this.userForm.valid) {
      const user: User = this.userForm.value;
      if (this.isEditing) {
        this.userService
          .updateUser(user)
          .subscribe(() => this.router.navigate(['/users']));
      } else {
        this.userService
          .createUser(user)
          .subscribe(() => this.router.navigate(['/users']));
      }
    }
  }
}
