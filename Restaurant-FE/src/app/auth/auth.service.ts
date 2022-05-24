import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, tap, throwError } from 'rxjs';
import { User } from './user.model';
import { environment } from './../../environments/environment';

export interface AuthResponseData {
  token: string,
  type: string,
  expiresAt: string,
}

export interface CurrentUserResponseData {
  id: string,
  name: string,
  email: string,
  role: string,
  image: File,
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  URL = environment.apiURL;
  user = new BehaviorSubject<User>(null);

  private _tokenExpirationTimer: any;
  isLoggedIn = false;

  constructor(private _http: HttpClient, private _router: Router) {

  }

  login(email: string, password: string) {
    return this._http.post<AuthResponseData>(this.URL + "/Authentication/login", {
      "email": email,
      "password": password
    }).pipe(tap(resData => {
      this.handleAuthentication(email, resData.token, resData.expiresAt);
    }));
  }

  logout() {
    localStorage.removeItem('bearer');
    this.user.next(null);
    this._router.navigate(['/login']);
    localStorage.removeItem('userData');
    if (this._tokenExpirationTimer) {
      clearTimeout(this._tokenExpirationTimer);
    }
    this._tokenExpirationTimer = null;
    this.isLoggedIn = false;
  }

  autoLogout(expirationDuration: number) {
    this._tokenExpirationTimer = setTimeout(() => {
      this.logout();
    }, expirationDuration);
  }

  private handleAuthentication(email: string, token: string, expiresAt: string) {
    localStorage.setItem('bearer', token);
    console.log(email);
    console.log(token);
    console.log(expiresAt);
    this.getCurrentUser().subscribe(response => {
      console.log(response);
      let time = (new Date(expiresAt).getTime() - new Date().getTime());
      const expirationDate = new Date(expiresAt);
      const user = new User(
        email,
        response == null ? '' : response['userName'],
        response == null ? '' : response['role'],
        token,
        expirationDate,
      );
      this.user.next(user);
      this.autoLogout(time);
      localStorage.setItem('userData', JSON.stringify(user));
      this.isLoggedIn = true;
      console.log(user);
      this._router.navigate(['/users']);
    });
  }

  getCurrentUser() {
    return this._http.get<CurrentUserResponseData>(this.URL + "/User/me", {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    });
  }
}
