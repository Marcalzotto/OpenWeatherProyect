import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { LoginDTO } from '../DTOs/LoginDTO';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private url = `${environment.environment}/api/auth/login`;

  constructor(private _http:HttpClient,
              private _authService:AuthService) { }

  //envia las credenciales al servidor y si son validas devuelve un jwt
  login(credentials:LoginDTO):Observable<string>
  {   
    const httpOptions = {
      headers: new HttpHeaders({
          'Content-Type':  'application/json',
      })
    } 

    return this._http.post<string>(this.url,credentials,httpOptions);
  }

  isLoggedIn():boolean
  {
    const token = this._authService.getAuthorizationToken();
    if(token)
      return true;
    else
      return false;
  }

}
