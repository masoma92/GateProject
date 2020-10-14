import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from 'src/app/services/account/account.service';
import { EntityResult } from 'src/app/services/common/entity.service';
import { Account, UpdateAccountCommand } from 'src/app/services/account/account';
import { FormControl, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material';

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
  
  streetFormControl = new FormControl('', [
    Validators.required
  ]);

  streetNoFormControl = new FormControl('', [
    Validators.required
  ]);
  
  accountTypeFormControl = new FormControl('', [
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
    }
  }

  get accountId() {
    return this._accountId;
  }

  @Output() updatedAccountEmitter = new EventEmitter();

  getResult = new EntityResult<Account>();
  updateResult = new EntityResult<boolean>();
  deleteResult = new EntityResult<boolean>();

  constructor(private accountService: AccountService,
    private snackBar: MatSnackBar) { }

  ngOnInit() {
  }

  get() {
    this.getResult.onFinished = () => {
      console.log(this.getResult.value);
    }
    this.accountService.get(this.accountId, this.getResult);
  }

  save() {
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

}
