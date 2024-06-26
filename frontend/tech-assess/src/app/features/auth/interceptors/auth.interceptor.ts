import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable, throwError, BehaviorSubject } from 'rxjs';
import { catchError, filter, take, switchMap } from 'rxjs/operators';
import { TokenService } from '../services/token.service';
import { AuthService, UNAUTHORIZED } from '../services/auth.service';
import { AuthResponse } from '../interfaces/auth-response.model';
import { Router } from '@angular/router';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  private isRefreshing = false;
  private refreshTokenSubject: BehaviorSubject<string | null> = new BehaviorSubject<string | null>(null);

  constructor(private tokenService: TokenService, private authService: AuthService, private router: Router) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.tokenService.getAccessToken();
    if (token) {
      req = this.addToken(req, token);
    }

    return next.handle(req).pipe(
      catchError(error => {
        if (error.status === 401 && !req.url.includes('/refresh-token')) {
          if (!this.isRefreshing) {
            this.isRefreshing = true;
            this.refreshTokenSubject.next(null);

            const accessToken = this.tokenService.getAccessToken();
            const refreshToken = this.tokenService.getRefreshToken();

            if (accessToken && refreshToken) {
              return this.authService.refreshToken(accessToken, refreshToken).pipe(
                switchMap((response: AuthResponse) => {
                  this.isRefreshing = false;
                  this.refreshTokenSubject.next(response.accessToken);
                  return next.handle(this.addToken(req, response.accessToken));
                }),
                catchError( _ => {
                  this.isRefreshing = false;
                  this.authService.logout();
                  this.router.navigate(['/login']);
                  return throwError(() => new Error(UNAUTHORIZED));
                })
              );
            } else {
              this.isRefreshing = false;
              this.authService.logout();
              this.router.navigate(['/login']);
              return throwError(() => new Error(UNAUTHORIZED));
            }
          } else {
            console.log('error 3');
            return this.refreshTokenSubject.pipe(
              filter(token => token != null),
              take(1),
              switchMap(token => next.handle(this.addToken(req, token as string)))
            );
          }
        } else {
          return throwError(() => new Error(UNAUTHORIZED));
        }
      })
    );
  }

  private addToken(request: HttpRequest<any>, token: string) {
    return request.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
  }


}
