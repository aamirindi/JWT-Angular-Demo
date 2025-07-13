import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})
export class NavbarComponent implements OnInit {
  user: any;

  constructor(private userService: UserService, private router: Router) {}

  ngOnInit() {
    this.userService.getProfile().subscribe({
      next: (res: any) => (this.user = res.result),
      error: () => this.router.navigate(['/login']),
    });
  }

  goToProfile() {
    this.router.navigate(['/dashboard/profile']);
  }

  goHome() {
    this.router.navigate(['/dashboard/home']);
  }

  logout() {
    this.userService.logout().subscribe({
      next: () => {
        this.userService.clearToken();
        this.router.navigate(['/login']);
      },
      error: () => alert('Logout failed!'),
    });
  }
}
