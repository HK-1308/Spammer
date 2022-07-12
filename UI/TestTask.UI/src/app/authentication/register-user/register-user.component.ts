import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserForRegistrationDto } from 'src/app/models/UserForRegistrationDto.model';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css']
})

export class RegisterUserComponent implements OnInit {

  registerForm: FormGroup = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
    confirm: new FormControl('')
  }); 

  constructor(private authService: AuthenticationService, private router: Router) { }
  ngOnInit(): void {

    this.registerForm.get('confirm')?.setValidators([Validators.required,
      this.validateConfirmPassword(this.registerForm.get('password'))]);
  }
  public validateControl = (controlName: string) => {

    return  this.registerForm.get(controlName)?.invalid && this.registerForm.get(controlName)?.touched

  }

  public hasError = (controlName: string, errorName: string) => {
    return this.registerForm.get(controlName)?.hasError(errorName)
  }
  public registerUser = (registerFormValue: any)  => {
    const formValues = { ...registerFormValue };
    const user: UserForRegistrationDto = {
      email: formValues.email,
      password: formValues.password,
      confirmPassword: formValues.confirm
    };
    this.authService.registerUser("Authentication/registration", user)
    .subscribe({
      next: (_)  => this.router.navigate(["login"]),
      error: (err: HttpErrorResponse) => console.log(err.error.errors)
    })
  }

  public validateConfirmPassword = (passwordControl: AbstractControl | null): ValidatorFn => {
    return (confirmationControl: AbstractControl) : { [key: string]: boolean } | null => {
      const confirmValue = confirmationControl.value;
      const passwordValue = passwordControl?.value;
      if (confirmValue === '') {
          return { mustMatch: true };
      }
      if (confirmValue !== passwordValue) {
          return  { mustMatch: true }
      } 
      return null;
    };
  }
}
