import { Component, OnInit } from '@angular/core';
import { ListPagination } from 'src/app/core/pagination/list-pagination';
import { AccountService } from 'src/app/services/account/account.service';
import { EntityListResult, EntityResult, Sorting } from 'src/app/services/common/entity.service';
import { Account } from 'src/app/services/account/account';
import { DashboardService } from 'src/app/services/dashboard/dashboard.service';

@Component({
  selector: 'dashboard-accountadmin',
  templateUrl: './dashboard-accountadmin.component.html',
  styleUrls: ['./dashboard-accountadmin.component.scss']
})
export class DashboardAccountadminComponent implements OnInit {

  //sum
  sumAdminsResult = new EntityResult<number>();
  sumGatesResult = new EntityResult<number>();
  sumUsersResult = new EntityResult<number>();
  sumErrorsResult = new EntityResult<number>();

  accountListResult = new EntityListResult<Account>();

  private _selectedAccount: Account;

  get selectedAccount() {
    return this._selectedAccount;
  }

  set selectedAccount(value :Account) {
    if (value){
      this._selectedAccount = value;
      this.getSums();
    }
  }

  

  constructor(private accountService: AccountService,
    private dashboardService: DashboardService) { }

  ngOnInit() {
    this.getAccountList();
  }

  getAccountList(pagination: ListPagination = null, sorting: Sorting = null, filter: string = null) {
    this.accountListResult.onFinished = () => {
      this.selectedAccount = this.accountListResult.value[0];
      this.getSums();
    }
    
  this.accountService.getList(this.accountListResult, pagination || new ListPagination(), sorting || new Sorting(), filter || "");
  }

  getSums() {
    this.dashboardService.getAccountAdminSums("sumGates", this.selectedAccount.id, this.sumGatesResult);
    this.dashboardService.getAccountAdminSums("sumAdmins", this.selectedAccount.id, this.sumAdminsResult);
    this.dashboardService.getAccountAdminSums("sumUsers", this.selectedAccount.id, this.sumUsersResult);
    this.dashboardService.getAccountAdminSums("sumErrors", this.selectedAccount.id, this.sumErrorsResult);
  }

  test() {
    console.log(this.selectedAccount);
  }
}
