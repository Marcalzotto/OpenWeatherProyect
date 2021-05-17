import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { UserDTO } from '../shared/DTOs/UserDTO';
import { AuthService } from '../shared/services/auth.service';

@Component({
  selector: 'register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  
  public nameErrorMessaje:string;
  public surnameErrorMessaje:string;
  public mailErrorMessaje:string;
  public passwordErrorMessaje:string;
  public confirmPasswordErrorMessaje:string;
  public equivalentPasswordsErrorMessaje:string;
  
  public registerMessaje:string;
  public icon:string;
  public color:string;

  public maxDate = new Date();

  registerForm = new FormGroup({
    name: new FormControl('',[Validators.required, Validators.minLength(3),Validators.maxLength(15),Validators.pattern('[^a-zA-Z]')]),
    surname: new FormControl('',[Validators.required, Validators.minLength(5),Validators.maxLength(30),Validators.pattern('[^a-zA-Z ]')]),
    email: new FormControl('',[Validators.required,Validators.pattern(/^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$]/)]),
    birthDate:new FormControl(new Date(), [this.DateValidator]),
    password: new FormControl('', [Validators.required,Validators.minLength(15),Validators.maxLength(40),Validators.pattern(/^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{15,40}$/)]),
    confirmPassword: new FormControl('', [Validators.required,Validators.minLength(15),Validators.maxLength(40),Validators.pattern(/^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{15,40}$/)])
  })

  constructor(private _authService:AuthService) { }

  ngOnInit(): void {
    
  }

  //funcion de validacion para nombre, muestra un mensaje de error segun la validacion activa
  nameErrorValidation()
  {
      if(this.registerForm.value.name === ''){
        this.nameErrorMessaje = "Name is required";
        return false;
      }

      if(this.registerForm.value.name.length < 3 || this.registerForm.value.name.length > 15){
        this.nameErrorMessaje = "Name must be between 3 and 15 characters length";
        return false;
      }

      if(this.registerForm.value.name.match('[^a-zA-Z]') !== null)
      {
        this.nameErrorMessaje = "Name must contain only letters";
        return false;
      }

      this.nameErrorMessaje = "";
      return true;
  }

  //funcion de validacion para apellido, muestra un mensaje de error segun la validacion activa
  surnameErrorValidation()
  {
      if(this.registerForm.value.surname === ''){
        this.surnameErrorMessaje = "Surname is required";
        return false;
      }

      if(this.registerForm.value.surname.length < 5 || this.registerForm.value.name.surname > 30){
        this.surnameErrorMessaje = "Surname must be between 5 and 30 characters length";
        return false;
      }

      if(this.registerForm.value.surname.match('[^a-zA-Z ]') !== null)
      {
        this.surnameErrorMessaje = "Surname must contain only letters or whitespaces";
        return false;
      }

      this.surnameErrorMessaje = "";
      return true;
  }

  mailErrorValidation(){
    
    if(this.registerForm.value.email === ''){ 
      this.mailErrorMessaje = "Email is Required";
      return false;
    }

    if(this.registerForm.value.email.match(/^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/) === null){
      this.mailErrorMessaje = "Email is invalid";
      return false;
    }

    this.mailErrorMessaje = "";
    return true;
  }

  DateValidator(control: AbstractControl): { [key: string]: boolean } | null {

    var date = new Date(control.value);
    var dateNow = new Date();
    var limitDate = new Date(1900,1,1);  

    if ((date.getTime() < limitDate.getTime())  || (date.getTime() > dateNow.getTime())) {
        return { 'DatesValidation': true };
    }
    return null;
  }

  dateErrorValidation()
  {
      var dateNow = new Date();
      var limitDate = new Date(1900,1,1); 
      var date = new Date(this.registerForm.value.birthDate);

      if((date.getTime() < limitDate.getTime())  || (date.getTime() > dateNow.getTime())) 
      {
        return false;
      }

      return true;
  }

  passwordErrorValidation()
  {
      if(this.registerForm.value.password === '')
      {
        this.passwordErrorMessaje = "password is required";
        return false;
      }

      if(this.registerForm.value.password.length < 15 || this.registerForm.value.password.length > 40)
      {
        this.passwordErrorMessaje = "password must be between 15 and 40 characters length";
        return false;
      }

      if(this.registerForm.value.password.match(/^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{15,40}$/) === null)
      {
        this.passwordErrorMessaje = "Password must contain letters, at least one uppercase and one number";
        return false;
      }

      this.passwordErrorMessaje = "";
      return true;
  }

  confirmPasswordErrorValidation()
  {
      if(this.registerForm.value.confirmPassword === '')
      {
        this.confirmPasswordErrorMessaje = "password is required";
        return false;
      }

      if(this.registerForm.value.confirmPassword.length < 15 || this.registerForm.value.confirmPassword.length > 40)
      {
        this.confirmPasswordErrorMessaje = "password must be between 15 and 40 characters length";
        return false;
      }

      if(this.registerForm.value.confirmPassword.match(/^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{15,40}$/) === null)
      {
        this.confirmPasswordErrorMessaje = "Password must contain letters, at least one uppercase and one number";
        return false;
      }

      this.confirmPasswordErrorMessaje = "";
      return true;
  }

  equivalentPasswords()
  {
      let password = this.registerForm.value.password;
      let confirmPassword =  this.registerForm.value.confirmPassword;

      if(password !== '' && confirmPassword !== ''){
        if(password !== confirmPassword){
          this.equivalentPasswordsErrorMessaje = "Passwords must be equivalent";
          return false;
        }
      }

      this.equivalentPasswordsErrorMessaje = ""; 
      return true;
  }

  clearForm(){
    this.registerForm.reset({
      name:"",
      surname:"",
      email:"",
      birthDate: new Date(),
      password:"",
      confirmPassword:""
    });

    this.setMessajeSettings("","","");

    return false;
  }

  disableButton(){
    
    if(!this.nameErrorValidation()) return false;
    
    if(!this.surnameErrorValidation()) return false;
    
    if(!this.mailErrorValidation()) return false;
    
    if(!this.dateErrorValidation()) return false;

    if(!this.passwordErrorValidation()) return false;

    if(!this.confirmPasswordErrorValidation()) return false;

    return true;
  }

  registerSubmit()
  {
    var name = this.registerForm.value.name;
    var surname = this.registerForm.value.surname;
    var email = this.registerForm.value.email;
    var brithDate = this.registerForm.value.birthDate;
    var password = this.registerForm.value.password;

    const User = new UserDTO(name,surname,email,brithDate,password);

    this._authService.register(User)
                  .subscribe(
                      res=>{
                        this.setMessajeSettings("Register Succeded", 
                                        "check_circle_outline",
                                        "primary");
                      },
                      error=>{
                        
                        var registerError = <UserDTO>error;
                        if(registerError !== null)
                        {
                            this.setMessajeSettings("Error: This email is already in use", 
                                        "highlight_off",
                                        "warn");
                        }else
                        {  
                            this.setMessajeSettings("Error: Invalid register, verify your data", 
                                        "highlight_off",
                                        "warn");
                        }
                        
                      }
                  )
  }  

  private setMessajeSettings(messaje:string, icon:string, color:string)
  {
      this.registerMessaje = messaje;
      this.icon = icon;
      this.color = color;
  }

}
