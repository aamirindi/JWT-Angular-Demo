import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private tokenKey = 'jwt_token';
  private baseUrl = 'http://localhost:5264/api/Auth';
  public isLoggedIn = new BehaviorSubject<boolean>(false);

  constructor(private http: HttpClient) {}

  login(payload: { email: string; pass: string }) {
    return this.http.post<{ message: string; token: string }>(
      `${this.baseUrl}/jwt-login`,
      payload
    );
  }

  storeToken(token: string) {
    localStorage.setItem(this.tokenKey, token);
    this.isLoggedIn.next(true);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  isUserLoggedIn(): boolean {
    return !!this.getToken();
  }

  getProfile(): Observable<any> {
    return this.http.get(`${this.baseUrl}/getUserData`);
  }

  logout(): Observable<any> {
    return this.http.post(`${this.baseUrl}/logout`, {});
  }

  clearToken() {
    localStorage.removeItem(this.tokenKey);
    this.isLoggedIn.next(false);
  }
}
