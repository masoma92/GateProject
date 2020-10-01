import { Component, OnInit } from '@angular/core';
import { FormControl, NgForm, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthenticationResponse, AuthenticationService } from '../../../services/authentication/authentication.service';
import { EntityResult } from '../../../services/common/entity.service';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.email
  ]);

  passwordFormControl = new FormControl('', [
    Validators.required
  ]);

  authenticationResult = new EntityResult<AuthenticationResponse>();

  email: string = "";
  password: string = "";

  get anythingIsInvalid() {
    return (this.emailFormControl.invalid ||
    this.passwordFormControl.invalid);
  }

  constructor(
    private authenticationService: AuthenticationService,
    private snackBar: MatSnackBar) { }

  ngOnInit() {
  }

  public login() {

    if (this.emailFormControl.invalid) {
      if(this.emailFormControl.hasError('required')){
        return;
      }
      else if(this.emailFormControl.hasError('email')){
        return;
      }
    }
    else if (this.passwordFormControl.invalid) {
      return;
    }
    
    this.authenticationResult.onFinished = () => {
      if (this.authenticationResult.hasValue)
        this.snackBar.open("Successfully login!", "Close", { duration: 2000, panelClass: 'toast.success' } );

      else if (this.authenticationResult.hasError){
        let errMessage = this.authenticationResult.errorMessage;
        if (errMessage.includes("email doesn't exist") || errMessage.includes("Password is not correct")) {
            this.snackBar.open("Email or password is not correct!", "Close", { duration: 2000, panelClass: 'toast.error' } );
        }

      }
    }
    this.authenticationService.authenticate(this.email, this.password, this.authenticationResult);
  }

}
