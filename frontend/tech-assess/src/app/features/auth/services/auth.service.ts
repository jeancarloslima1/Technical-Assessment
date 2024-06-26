import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { TokenService } from './token.service';
import { AuthResponse } from '../interfaces/auth-response.model';
import { Router } from '@angular/router';

export const UNAUTHORIZED: string = "UNAUTHORIZED";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private backendUrl: string;

  constructor(private http: HttpClient, private tokenService: TokenService, private router: Router) {
    this.backendUrl = `http://localhost:5000/api/auth`;
  }

  login(username: string, password: string): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.backendUrl}/login`, { username, password });
  }

  logout() {
    this.tokenService.removeAccessToken();
    this.tokenService.removeRefreshToken();
  }

  isLoggedIn(): boolean {
    return !!this.tokenService.getAccessToken();
  }

  refreshToken(accessToken: string, refreshToken: string): Observable<AuthResponse> {
    if (!accessToken || !refreshToken) {
      return throwError(() => new Error('Tokens are missing'));
    }

    return this.http.post<AuthResponse>(`${this.backendUrl}/refresh-token`, {
      accessToken,
      refreshToken
    }).pipe(
      tap(response => {
        this.tokenService.setAccessToken(response.accessToken);
        this.tokenService.setRefreshToken(response.refreshToken);
      }),
      catchError(error => {
        if (error.status === 401) {
          this.logout();
          this.router.navigate(['/login']);
        }
        return throwError(() => new Error(error));
      })

    );
  }

}
