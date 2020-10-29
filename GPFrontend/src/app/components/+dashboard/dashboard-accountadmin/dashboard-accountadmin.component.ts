import { Component, OnInit } from '@angular/core';
import { ListPagination } from 'src/app/core/pagination/list-pagination';
import { AccountService } from 'src/app/services/account/account.service';
import { EntityListResult, Sorting } from 'src/app/services/common/entity.service';
import { Account } from 'src/app/services/account/account';

@Component({
  selector: 'dashboard-accountadmin',
  templateUrl: './dashboard-accountadmin.component.html',
  styleUrls: ['./dashboard-accountadmin.component.scss']
})
export class DashboardAccountadminComponent implements OnInit {

  accountListResult = new EntityListResult<Account>();

  accounts: Account[] = [];
  selectedAccount: string = "";

  constructor(private accountService: AccountService) { }

  ngOnInit() {
    this.getAccountList();
  }

  getAccountList(pagination: ListPagination = null, sorting: Sorting = null, filter: string = null) {
    this.accountListResult.onFinished = () => {
  }
    
    this.accountService.getList(this.accountListResult, pagination || new ListPagination(), sorting || new Sorting(), filter || "");
  }
}
