import { Component, OnInit } from '@angular/core';
import { FormControl, NgForm, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { AuthenticationResponse, AuthenticationService } from '../../../services/authentication/authentication.service';
import { EntityResult } from '../../../services/common/entity.service';
import { UserManagementService } from '../../../services/user-management/user-management.service';

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
  isNotConfirmed: boolean = false;

  get anythingIsInvalid() {
    return (this.emailFormControl.invalid ||
    this.passwordFormControl.invalid);
  }

  constructor(
    private authenticationService: AuthenticationService,
    private userManageMentService: UserManagementService,
    private snackBar: MatSnackBar,
    private router: Router) { }

  ngOnInit() {
  }

  login() {

    if (this.emailFormControl.invalid || this.passwordFormControl.invalid) return; 
    
    this.authenticationResult.onFinished = () => {
      if (this.authenticationResult.hasValue)
        this.snackBar.open("Successfully login!", "Close", { duration: 2000, panelClass: 'toast.success' } );

      else if (this.authenticationResult.hasError){
        let errMessage = this.authenticationResult.errorMessage;
        if (errMessage.includes("email doesn't exist") || errMessage.includes("Password is not correct")) {
            this.snackBar.open("Email or password is not correct!", "Close", { duration: 2000, panelClass: 'toast.error' } );
        }
        if (errMessage.includes("Email is not confirmed")) {
          this.isNotConfirmed = true;
        }

      }
    }
    this.authenticationService.authenticate(this.email, this.password, this.authenticationResult);
  }
  
  resend() {
    if (this.emailFormControl.invalid) return;

    let resendResult = new EntityResult<boolean>();

    resendResult.onFinished = () => {
      if(resendResult.hasValue) {
        this.snackBar.open("Confirmation link resended!", "Close", { duration: 2000, panelClass: 'toast.success' })
        this.router.navigate(['/register-success'], { queryParams: { email: this.email } });
      }
      else if (resendResult.hasError) {
        this.snackBar.open(resendResult.errorMessage, "Close", { duration: 2000, panelClass: 'toast.error' } );
      }
    }
    this.userManageMentService.resendconfirmemail(this.email, resendResult);
  }
}
