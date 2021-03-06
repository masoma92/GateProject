import { ChangeDetectorRef } from '@angular/core';
import { ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { MatDialog, MatPaginator, MatSnackBar, MatSort, MatTableDataSource } from '@angular/material';
import { ListPagination } from 'src/app/core/pagination/list-pagination';
import { Account } from 'src/app/services/account/account';
import { AccountService } from 'src/app/services/account/account.service';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';
import { EntityListResult, Sorting } from 'src/app/services/common/entity.service';
import { CreateAccountDialogComponent } from './create-account/create-account-dialog.component';

@Component({
  selector: 'accounts',
  templateUrl: './accounts.component.html',
  styleUrls: ['./accounts.component.scss']
})
export class AccountsComponent implements OnInit {

  displayedColumns: string[] = ['id', 'name', 'address', 'accountType', 'contactEmail'];

  filterInput: string = "";
  
  result = new EntityListResult<Account>();
  dataSource: MatTableDataSource<Account>;

  selectedRow;
  accountId: number;

  @ViewChild(MatPaginator, {static: false}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: false}) sort: MatSort;

  constructor(private accountService: AccountService,
    private changeDetectorRefs: ChangeDetectorRef,
    public dialog: MatDialog,
    public authenticationService: AuthenticationService,
    private snackBar: MatSnackBar) {
    this.getList();
  }

  ngOnInit() {}

  getList(pagination: ListPagination = null, sorting: Sorting = null, filter: string = null){
    this.result.onFinished = () => {
      if (this.result.hasValue)
        this.dataSource = new MatTableDataSource(this.result.value);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.refresh();
    }
    
    this.accountService.getList(this.result, pagination || new ListPagination(), sorting || new Sorting(), filter || "");
  }

  applyFilter() {
    this.getList(null, null, this.filterInput);
  }

  refresh() {
    this.changeDetectorRefs.detectChanges();
  }

  selectRow(row) {
    if (this.selectedRow === row) {
      this.selectedRow = null;
      this.accountId = null;
    }
    else {
      this.selectedRow = row;
      this.accountId = this.selectedRow.id;
    }
  }

  sorter() {
    console.log(this.sort.active);
    console.log(this.sort.start);
  }

  openCreateDialog(): void {
    const dialogRef = this.dialog.open(CreateAccountDialogComponent, {
      width: '350px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result){
        if (result == "success"){
          this.snackBar.open("Account successfully added!!", "Close", { duration: 2000, panelClass: 'toast.success' } );
          this.getList();
        }
        else{
          this.snackBar.open(result, "Close", { duration: 2000, panelClass: 'toast.success' } );
        }
      }
    });
  }

  concatAddress(row) {
    return `${row.country}, ${row.zip} ${row.city}, ${row.street} ${row.streetNo}`;
  }

}
