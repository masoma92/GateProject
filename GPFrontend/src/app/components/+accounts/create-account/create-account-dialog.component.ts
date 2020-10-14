import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material';
import { ListPagination } from 'src/app/core/pagination/list-pagination';
import { AccountType, AccountTypeService } from 'src/app/services/account-type/account-type.service';
import { CreateAccountCommand } from 'src/app/services/account/account';
import { AccountService } from 'src/app/services/account/account.service';
import { EntityListResult, EntityResult, Sorting } from 'src/app/services/common/entity.service';


@Component({
  selector: 'app-create-account-dialog',
  templateUrl: './create-account-dialog.component.html',
  styleUrls: ['./create-account-dialog.component.scss']
})
export class CreateAccountDialogComponent implements OnInit {

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

  createAccountCommand = new CreateAccountCommand();
  result = new EntityResult<boolean>();
  accountTypeResult = new EntityListResult<AccountType>();

  accountTypes: AccountType[] = [];
  selectedAccountType: string = "";

  constructor(private accountService: AccountService,
    private accountTypeService: AccountTypeService,
    public dialogRef: MatDialogRef<CreateAccountDialogComponent>) { }

  ngOnInit() {
    this.getAccountTypes();
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

  create() {
    if (this.anythingIsInvalid()) return;
    this.createAccountCommand.accountType = this.selectedAccountType;
    this.result.onFinished = () => {
      if (this.result.hasValue){
        this.dialogRef.close("success");
      }
      else if (this.result.hasError) {
        this.dialogRef.close(this.result.errorMessage);
      }
    }
    this.accountService.create(this.createAccountCommand, this.result);
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
