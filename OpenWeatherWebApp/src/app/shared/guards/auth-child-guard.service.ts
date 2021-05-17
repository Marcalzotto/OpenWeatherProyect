import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateChild, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable()
//creamos nuestro guard para proteger nuestros componentes de accesos no autorizados
export class AuthChildGuardService implements CanActivateChild{

  constructor(private _authService:AuthService, private router:Router) { }
    
  canActivateChild(childRoute: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {

    //puede ingresar solo si existe el token y que este no haya expirado(5 minutos de vida)
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
