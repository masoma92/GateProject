import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { EntityResult } from 'src/app/services/common/entity.service';
import { ResetPasswordCommand, UserManagementService } from 'src/app/services/user-management/user-management.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent implements OnInit {

  result = new EntityResult<boolean>();
  command = new ResetPasswordCommand();

  passwordFormControl = new FormControl('', [
    Validators.required
  ]);

  passwordFormControl2 = new FormControl('', [
    Validators.required
  ]);

  constructor(private userManagementService: UserManagementService,
    private router: Router,
    private route: ActivatedRoute,
    private snackBar: MatSnackBar) { }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.command.email = params['email'];
      this.command.token = params['token'];
    });
  }

  get anythingIsInvalid() {
    return (
    this.passwordFormControl.invalid ||
    this.passwordFormControl2.invalid ||
    this.passwordFieldsNotMatch);
  }


  get passwordFieldsNotMatch() {
    return this.command.password != this.command.password2;
  }

  reset() {
    if (this.passwordFormControl.invalid || this.passwordFormControl2.invalid || this.passwordFieldsNotMatch) return;

    this.result.onFinished = () => {
      if(this.result.hasValue) {
        this.snackBar.open("Successfully reset-password!", "Close", { duration: 2000, panelClass: 'toast.success' })
        this.router.navigate(['/forget-password-success'], { queryParams: { email: this.command.email } });
      }
      else if (this.result.hasError) {
        this.snackBar.open(this.result.errorMessage, "Close", { duration: 2000, panelClass: 'toast.error' } );
      }
    }

    this.userManagementService.resetpassowrd(this.command, this.result);
  }

}
