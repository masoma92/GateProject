import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from 'src/app/services/account/account.service';
import { EntityListResult, EntityResult, Sorting } from 'src/app/services/common/entity.service';
import { Account, UpdateAccountCommand } from 'src/app/services/account/account';
import { FormControl, Validators } from '@angular/forms';
import { MatDialog, MatSnackBar } from '@angular/material';
import { ListPagination } from 'src/app/core/pagination/list-pagination';
import { AccountType, AccountTypeService } from 'src/app/services/account-type/account-type.service';
import { ManageAdminsDialogComponent } from '../manage-admins/manage-admins-dialog.component';

@Component({
  selector: 'account-details',
  templateUrl: './account-details.component.html',
  styleUrls: ['./account-details.component.scss']
})
export class AccountDetailsComponent implements OnInit {

  nameFormControl = new FormControl('', [
    Validators.required
  ]);

  zipFormControl = new FormControl('', [
    Validators.required
  ]);

  countryFormControl = new FormControl('', [
    Validators.required
  ]);

  cityFormControl = new FormControl('', [
    Validators.required
  ]);
  
  streetFormControl = new FormControl('', [
    Validators.required
  ]);

  streetNoFormControl = new FormControl('', [
    Validators.required
  ]);
  
  contactEmailFormControl = new FormControl('', [
    Validators.required,
    Validators.email
  ]);
  
  private _accountId: number;

  @Input('accountId') set accountId(value: number){
    if (value) {
      console.log(value);
      this._accountId = value;
      this.get();
      this.getAccountTypes();
    }
  }

  get accountId() {
    return this._accountId;
  }

  @Output() updatedAccountEmitter = new EventEmitter();

  getResult = new EntityResult<Account>();
  updateResult = new EntityResult<boolean>();
  deleteResult = new EntityResult<boolean>();
  accountTypeResult = new EntityListResult<AccountType>();

  accountTypes: AccountType[] = [];
  selectedAccountType: string = "";

  currentAdminEmails: string[];

  constructor(private accountService: AccountService,
    private snackBar: MatSnackBar,
    private accountTypeService: AccountTypeService,
    public dialog: MatDialog) { }

  ngOnInit() {
  }

  get() {
    this.getResult.onFinished = () => {
      console.log(this.getResult.value);
    }
    this.accountService.get(this.accountId, this.getResult);
  }

  save() {
    this.getResult.value.accountType = this.selectedAccountType;
    console.log(this.getResult.value.accountType);
    this.updateResult.onFinished = () => {
      if (this.updateResult.hasValue){
        this.updatedAccountEmitter.emit('success');
        this.snackBar.open("Account saved!!", "Close", { duration: 2000, panelClass: 'toast.success' } );
      }
      else if (this.updateResult.hasError) {
        this.snackBar.open(this.updateResult.errorMessage, "Close", { duration: 2000, panelClass: 'toast.error' } );
      }
    }
    this.accountService.update(this.getResult.value, this.updateResult);
  }

  delete() {
    this.deleteResult.onFinished = () => {
      if (this.deleteResult.hasValue){
        this.getResult = new EntityResult<Account>();
        this.updatedAccountEmitter.emit('success');
        this.snackBar.open("Successfully deleted!!", "Close", { duration: 2000, panelClass: 'toast.success' } );
      }
      else if (this.deleteResult.hasError) {
        this.snackBar.open(this.deleteResult.errorMessage, "Close", { duration: 2000, panelClass: 'toast.error' } );
      }
    }
    this.accountService.delete(this.accountId, this.deleteResult);
  }

  getAccountTypes(pagination: ListPagination = null, sorting: Sorting = null, filter: string = null){
    this.accountTypeResult.onFinished = () => {
      if (this.accountTypeResult.hasValue)
        console.log("called");
        this.accountTypes = this.accountTypeResult.value;
        this.selectedAccountType = this.accountTypes[0].name;
    }
    
    this.accountTypeService.getList(this.accountTypeResult, pagination || new ListPagination(), sorting || new Sorting(), filter || "");
  }

  manageAdmins() {
    const dialogRef = this.dialog.open(ManageAdminsDialogComponent, {
      data: this.getResult.value.adminEmails,
      width: '400px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getResult.value.adminEmails = result;
        this.save();
      }
    });
  }

  anythingIsInvalid() {
    return (this.nameFormControl.invalid || 
    this.zipFormControl.invalid ||
    this.countryFormControl.invalid ||
    this.cityFormControl.invalid ||
    this.streetFormControl.invalid ||
    this.streetNoFormControl.invalid ||
    this.contactEmailFormControl.invalid )
  }

}
