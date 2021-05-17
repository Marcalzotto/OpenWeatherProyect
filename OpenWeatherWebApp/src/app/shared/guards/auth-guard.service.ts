import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable()
//creamos nuestro guard para proteger nuestros componentes de accesos no autorizados
export class AuthGuardService implements CanActivate{

  constructor(private _authService:AuthService, private router:Router) { }
  
  canActivate()
  {
      //puede ingresar solo si el token y no ha expirado(5 minutos de vida)
      if(!this._authService.isTokenExpired())
      {
          return true;
      }
      else
      {
          this.router.navigate(["/"])
          return false;
      }
  }
}
