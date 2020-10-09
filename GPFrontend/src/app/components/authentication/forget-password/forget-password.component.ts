import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material';
import { Router } from '@angular/router';
import { EntityResult } from '../../../services/common/entity.service';
import { UserManagementService } from '../../../services/user-management/user-management.service';

@Component({
  selector: 'app-forget-password',
  templateUrl: './forget-password.component.html',
  styleUrls: ['./forget-password.component.scss']
})
export class ForgetPasswordComponent implements OnInit {

  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.email
  ]);

  email: string;

  result = new EntityResult<boolean>();

  constructor(
    private userManagementService: UserManagementService,
    private router: Router,
    private snackBar: MatSnackBar) { }

  ngOnInit() {
  }

  public forgetpassword() {

    if (this.emailFormControl.invalid) {
      if(this.emailFormControl.hasError('required')){
        return;
      }
      else if(this.emailFormControl.hasError('email')){
        return;
      }
    }

    this.result.onFinished = () => {
      if(this.result.hasValue) {
        this.snackBar.open("Successfully sent reset password email!", "Close", { duration: 2000, panelClass: 'toast.success' });
        this.router.navigate(['/forget-password-requested'], { queryParams: { email: this.email } });
      }
      else if (this.result.hasError) {
        this.snackBar.open("Some error has happened!", "Close", { duration: 2000, panelClass: 'toast.error' });
      }
    }

    this.userManagementService.forgetpassword(this.email, this.result);
  }

}
