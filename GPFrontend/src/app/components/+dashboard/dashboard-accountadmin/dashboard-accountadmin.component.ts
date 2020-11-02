import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { ListPagination } from 'src/app/core/pagination/list-pagination';
import { AccountService } from 'src/app/services/account/account.service';
import { EntityListResult, EntityResult, Sorting } from 'src/app/services/common/entity.service';
import { Account } from 'src/app/services/account/account';
import { DashboardService } from 'src/app/services/dashboard/dashboard.service';
import { FormControl } from '@angular/forms';
import { GetEnters, GetEntersResponse } from 'src/app/services/dashboard/dashboard';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';

@Component({
  selector: 'dashboard-accountadmin',
  templateUrl: './dashboard-accountadmin.component.html',
  styleUrls: ['./dashboard-accountadmin.component.scss']
})
export class DashboardAccountadminComponent implements OnInit {
  
  currentDate = new Date();

  // sum
  sumAdminsResult = new EntityResult<number>();
  sumGatesResult = new EntityResult<number>();
  sumUsersResult = new EntityResult<number>();
  sumErrorsResult = new EntityResult<number>();

  // gate using
  filterInput: string = "";
  public chart;
  isDatePickerHidden = false;
  fromDate = new FormControl(new Date(new Date().setHours(0,0,0,1)));
  toDate = new FormControl(new Date(new Date().setDate(new Date().getDate() + 7)));

  accountListResult = new EntityListResult<Account>();
  displayedColumns: string[] = ['name', 'email', 'firstUse', 'lastUse', 'gateName'];

  private _selectedAccount: Account;

  get selectedAccount() {
    return this._selectedAccount;
  }

  set selectedAccount(value :Account) {
    if (value){
      this._selectedAccount = value;
      this.getSums();
      this.getEnters();
    }
  }

  result = new EntityListResult<GetEntersResponse>();
  dataSource: MatTableDataSource<GetEntersResponse>;

  @ViewChild(MatPaginator, {static: false}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: false}) sort: MatSort;

  constructor(private accountService: AccountService,
    private dashboardService: DashboardService,
    private changeDetectorRefs: ChangeDetectorRef) { }

  ngOnInit() {
    this.getAccountList();
    window.onresize = () => this.isDatePickerHidden = window.innerWidth <= 1112;
  }

  getAccountList(pagination: ListPagination = null, sorting: Sorting = null, filter: string = null) {
    this.accountListResult.onFinished = () => {
      this.selectedAccount = this.accountListResult.value[0];
      this.getSums();
      this.getEnters();
    }
    
    this.accountService.getList(this.accountListResult, pagination || new ListPagination(), sorting || new Sorting(), filter || "");
  }

  getSums() {
    this.dashboardService.getAccountAdminSums("sumGates", this.selectedAccount.id, this.sumGatesResult);
    this.dashboardService.getAccountAdminSums("sumAdmins", this.selectedAccount.id, this.sumAdminsResult);
    this.dashboardService.getAccountAdminSums("sumUsers", this.selectedAccount.id, this.sumUsersResult);
    this.dashboardService.getAccountAdminSums("sumErrors", this.selectedAccount.id, this.sumErrorsResult);
  }

  getEnters(pagination: ListPagination = null, sorting: Sorting = null, filter: string = null) {
    this.result.onFinished = () => {
      if (this.result.hasValue)
        console.log(this.result.value);
        this.dataSource = new MatTableDataSource(this.result.value);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.refresh();
    }
    
    this.dashboardService.getEnters(new GetEnters(new Date(this.fromDate.value.setHours(0,0,0,1)), new Date(this.toDate.value.setHours(23,59,59,59)), this.selectedAccount.id) ,this.result, pagination || new ListPagination(), sorting || new Sorting(), filter || "");
  }

  applyFilter() {
    this.getEnters(null, null, this.filterInput);
  }

  refresh() {
    this.changeDetectorRefs.detectChanges();
  }
}
