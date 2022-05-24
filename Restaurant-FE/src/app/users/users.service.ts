import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { environment } from './../../environments/environment';

export interface GetUsersResponseData {
  id: string,
  name: string,
  email: string,
  role: string,
  image?: File,
}

export interface UserResponseData {
  message: string;
}

@Injectable({
  providedIn: 'root'
})
export class UsersService implements OnInit {
  userURL = environment.apiURL + "/User";

  constructor(private _http: HttpClient) {
  }

  ngOnInit(): void {
  }

  getAllUsers() {
    return this._http.get<GetUsersResponseData[]>(this.userURL, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    });
  }

  getFilteredUsers(pageNumber: number) {
    let params = new HttpParams();
    params = params.append('pageNumber', pageNumber === null ? 0 : pageNumber);
    params = params.append('pageSize', 20);
    return this._http.get<GetUsersResponseData[]>(this.userURL, {
      params: params
    });
  }

  getTargetUser(userId: string){
    return this._http.get(this.userURL + `/${userId}`,
    {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    });
  }

  CreateNewUser(firstName: string, lastName: string, email: string, role: string, password: string) {
    return this._http.post<UserResponseData>(this.userURL, {
      firstName,
      lastName,
      email,
      role,
      password
    });
  }

  updateTargetUser(userId: string, firstName: string, lastName: string, email: string, role: string, password: string){
    return this._http.put<UserResponseData>(this.userURL + `/${userId}`, {
      firstName,
      lastName,
      email,
      role,
      password,
    },
    {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    });
  }

  deleteTargetUser(userId: string){
    return this._http.delete<UserResponseData>(this.userURL + `/${userId}`,
    {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    });
  }
}
