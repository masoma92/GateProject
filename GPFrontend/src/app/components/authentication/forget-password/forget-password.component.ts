import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
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

  constructor(private userManagementService: UserManagementService) { }

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
        console.log("success");
      }
      else if (this.result.hasError) {
        console.log("something wrong happened");
      }
    }

    this.userManagementService.forgetpassword(this.email, this.result);
  }

}
