import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material';
import { Router } from '@angular/router';
import { EntityResult } from '../../../services/common/entity.service';
import { RegisterUserCommand, UserManagementService } from '../../../services/user-management/user-management.service';

@Component({
  selector: 'register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  firstNameFormControl = new FormControl('', [
    Validators.required
  ]);

  lastNameFormControl = new FormControl('', [
    Validators.required
  ]);

  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.email
  ]);

  birthFormControl = new FormControl('', [
    Validators.required
  ]);

  passwordFormControl = new FormControl('', [
    Validators.required
  ]);

  passwordFormControl2 = new FormControl('', [
    Validators.required
  ]);

  get anythingIsInvalid() {
    return (this.firstNameFormControl.invalid || 
    this.lastNameFormControl.invalid ||
    this.emailFormControl.invalid ||
    this.birthFormControl.invalid ||
    this.passwordFormControl.invalid ||
    this.passwordFormControl2.invalid ||
    this.passwordFieldsNotMatch);
  }

  get passwordFieldsNotMatch() {
    return this.command.password != this.command.password2;
  }

  result = new EntityResult<boolean>();
  command = new RegisterUserCommand();

  constructor(private userManagementService: UserManagementService,
    private router: Router,
    private snackBar: MatSnackBar) { }
  ngOnInit() {
  }

  register() {
    if (this.anythingIsInvalid || this.passwordFieldsNotMatch) return;

    this.result.onFinished = () => {
      if(this.result.hasValue) {
        this.snackBar.open("Successfully registered!", "Close", { duration: 2000, panelClass: 'toast.success' })
        this.router.navigate(['/register-success'], { queryParams: { email: this.command.email } });
      }
      else if (this.result.hasError) {
        this.snackBar.open(this.result.errorMessage, "Close", { duration: 2000, panelClass: 'toast.error' } );
      }
    }

    this.userManagementService.register(this.command, this.result);
  }

}
