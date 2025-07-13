import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
})
export class LoginComponent {
  email = '';
  pass = '';
  errorMsg = '';

  constructor(private userService: UserService, private router: Router) {}

  onSubmit() {
    this.userService.login({ email: this.email, pass: this.pass }).subscribe({
      next: (res) => {
        this.userService.storeToken(res.token);
        this.router.navigate(['/dashboard']);
      },
      error: (err) => {
        this.errorMsg = err.error.message || 'Login failed';
      },
    });
  }
}
