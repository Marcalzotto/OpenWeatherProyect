import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginDTO } from '../shared/DTOs/LoginDTO';
import { AuthService } from '../shared/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {


  public loginForm = new FormGroup({
    email: new FormControl('',[Validators.required,Validators.pattern(/^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$]/)]),
    password: new FormControl('',[Validators.required]),
  })
  constructor(private _authService: AuthService,
              private _router: Router) { }
  
  public invalidLogin:boolean;
  public emailErrorMsj:string; 
  public passwordErrorMsj:string;
  
  public loginMessaje:string;
  public icon:string;
  public color:string;

  ngOnInit(): void {
    this.invalidLogin = false;
    this.isLoggedIn();
  }

  login()
  {
      const login = new LoginDTO();
      login.email = this.loginForm.value.email;
      login.password = this.loginForm.value.password;
      
      this._authService.login(login)
                .subscribe(data =>
                          {
                            const jwtToken = (<any>data).token;
                            this._authService.setAuthorizationToken(jwtToken);

                            this.invalidLogin = false;
                            this._router.navigate(["/weather-report"]);
                          },
                          error=>{
                            console.log(error);
                            this.invalidLogin = true; 
                           
                            this.setMessajeSettings("Invalid login: User or password is wrong","highlight_off","warn");
                          });
  }

  isLoggedIn()
  {
      if(!this._authService.isTokenExpired())
      {
        this._router.navigate(["/weather-report"]);
      }
  }

  buttonDisabled(){
    
    if(!this.getEmailErrorMsj()) return false;

    if(!this.getPasswordErrorMsj()) return false;

    return true;
  }

  getEmailErrorMsj(){

    if(this.loginForm.value.email === ''){ 
      this.emailErrorMsj = "Email is Required";
      return false;
    }

    if(this.loginForm.value.email.match(/^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/) === null){
      this.emailErrorMsj = "Email is invalid";
      return false;
    }
    
    this.emailErrorMsj = "";
    return true;
  }

  getPasswordErrorMsj()
  {
    if(this.loginForm.value.password === '')
    {
      this.passwordErrorMsj = "Password is required";
      return false;
    }
    
    this.passwordErrorMsj ="";
    return true;
  }

  resetLogin(){
      this.loginForm.reset({
          email:"",
          password:""
      });
      this.setMessajeSettings("","","");  
      
      return false;
  }

  private setMessajeSettings(messaje:string, icon:string, color:string)
  {
      this.loginMessaje = messaje;
      this.icon = icon;
      this.color = color;
  }
}
