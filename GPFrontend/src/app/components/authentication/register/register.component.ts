import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
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

  dateFormControl = new FormControl('', [
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
    this.passwordFormControl.invalid ||
    this.passwordFormControl2.invalid ||
    this.passwordFieldsNotMatch);
  }

  get passwordFieldsNotMatch() {
    return this.command.password != this.command.password2;
  }

  result = new EntityResult<boolean>();
  command = new RegisterUserCommand();

  constructor(private userManagementService: UserManagementService) { }
  ngOnInit() {
  }

  register() {
    if (this.anythingIsInvalid || this.passwordFieldsNotMatch) return;

    this.result.onFinished = () => {
      if(this.result.hasValue) {
        console.log("success");
      }
      else if (this.result.hasError) {
        console.log("something wrong happened");
      }
    }

    this.userManagementService.register(this.command, this.result);
  }

}
