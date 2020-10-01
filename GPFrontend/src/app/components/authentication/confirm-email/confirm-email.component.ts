import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { EntityResult } from '../../../services/common/entity.service';
import { UserManagementService } from '../../../services/user-management/user-management.service';

@Component({
  selector: 'confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.scss']
})
export class ConfirmEmailComponent implements OnInit {

  token: string = "";
  email: string = "";
  result = new EntityResult<boolean>();
  isInProgress = true;

  constructor(
    private route: ActivatedRoute,
    private userManagementService: UserManagementService) { }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.token = params['token'];
      this.email = params['email'];
    });
    if (!this.email || !this.token) return;

    this.result.onFinished = () => {
      this.isInProgress = false;
    };

    this.userManagementService.confirmemail(this.email, this.token, this.result);
  }

}
