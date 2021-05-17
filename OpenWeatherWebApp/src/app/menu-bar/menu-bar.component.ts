import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../shared/services/auth.service';
import { LoginService } from '../shared/services/login.service';

@Component({
  selector: 'app-menu-bar',
  templateUrl: './menu-bar.component.html',
  styleUrls: ['./menu-bar.component.css']
})
export class MenuBarComponent implements OnInit {

  public name:string;

  constructor(private _loginService:LoginService,
              private _router:Router,
              private _authService:AuthService) { }

  ngOnInit(): void {
      this.name = "OpenWeatherWebApp";
  }

  showMenu():boolean
  {
      return !this._authService.isTokenExpired();
  }

  logOut(){

      if(this._authService.getAuthorizationToken())
        this._authService.destroyToken();

      this._router.navigate(['/']);
  }
  
}
