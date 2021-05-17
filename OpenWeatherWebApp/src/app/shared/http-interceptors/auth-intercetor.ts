import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor{

    constructor(private _auth:AuthService) {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        
        const token = this._auth.getAuthorizationToken(); 

        if(token){
            const authReq = req.clone({
                setHeaders: {
                    Authorization: `Bearer ${ token }`
                }
            });
            return next.handle(authReq);
        }else{
            const authReq = req.clone({
                setHeaders: {
                    'Content-Type':  'application/json'
                }
            });
            return next.handle(authReq);
        }
        
    }
}
