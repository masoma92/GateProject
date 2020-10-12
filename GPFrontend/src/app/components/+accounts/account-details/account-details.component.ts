import { Component, Input, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account/account.service';
import { EntityResult } from 'src/app/services/common/entity.service';
import { Account } from 'src/app/services/account/account';

@Component({
  selector: 'account-details',
  templateUrl: './account-details.component.html',
  styleUrls: ['./account-details.component.scss']
})
export class AccountDetailsComponent implements OnInit {

  private _accountId: number;

  @Input('accountId') set accountId(value: number){
    if (value) {
      console.log(value);
      this._accountId = value;
      this.get();
    }
  }

  getResult = new EntityResult<Account>();

  constructor(private accountService: AccountService) { }

  ngOnInit() {
  }

  get() {
    this.accountService.get(this._accountId, this.getResult);
  }

}
