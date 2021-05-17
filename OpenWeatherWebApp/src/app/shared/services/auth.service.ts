import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import jwt_decode from 'jwt-decode';
import { Observable } from 'rxjs';
import { LoginDTO } from '../DTOs/LoginDTO';
import { environment } from '../../../environments/environment';
import { UserDTO } from '../DTOs/UserDTO';

export const TOKEN_NAME:string ="jwt";

@Injectable({
  providedIn: 'root'
})

export class AuthService {

  private url:string;

  constructor(private _http:HttpClient) {
    this.url = environment.environment;
  }

  getTokenExpirationDate(token: string): Date {
    
    const decodedToken = <any>jwt_decode(token);
    
    if (decodedToken.exp === undefined) return null;

    const date = new Date(0); 
    date.setUTCSeconds(decodedToken.exp);
    return date;
  }

  isTokenExpired(token?: string): boolean
  {
    if(!token) 
      token = this.getAuthorizationToken();

    if(!token) 
      return true;

    const expireDate = this.getTokenExpirationDate(token);

    if(expireDate === undefined) 
      return false;
    
    return !(expireDate.valueOf() > new Date().valueOf());
  }

  public getAuthorizationToken():string{
    return localStorage.getItem(TOKEN_NAME);
  }

  public setAuthorizationToken(token:string):void{
      localStorage.setItem(TOKEN_NAME,token);
  }

  public destroyToken(){
      localStorage.removeItem(TOKEN_NAME);
  } 

  login(credentials:LoginDTO):Observable<string>
  { 
    const url = `${this.url}/api/auth/login`;  
    const httpOptions = {
      headers: new HttpHeaders({
          'Content-Type':  'application/json',
      })
    } 

    return this._http.post<string>(url,credentials,httpOptions);
  }

  register(User: UserDTO)
  {
    const url = `${this.url}/api/auth/user`;
    const httpOptions = {
      headers: new HttpHeaders({
          'Content-Type':  'application/json',
      })
    }
    
    return this._http.post<string>(url,User,httpOptions);
  }

}
