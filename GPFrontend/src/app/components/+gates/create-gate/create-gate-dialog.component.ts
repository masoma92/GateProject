import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { ListPagination } from 'src/app/core/pagination/list-pagination';
import { AccountService } from 'src/app/services/account/account.service';
import { EntityListResult, EntityResult, Sorting } from 'src/app/services/common/entity.service';
import { GateType, GateTypeService } from 'src/app/services/gate-type/gate-type.service';
import { CreateGateCommand } from 'src/app/services/gate/gate';
import { Account } from 'src/app/services/account/account';
import { GateService } from 'src/app/services/gate/gate.service';
import { MatDialogRef } from '@angular/material';
import { map, startWith } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-create-gate-dialog',
  templateUrl: './create-gate-dialog.component.html',
  styleUrls: ['./create-gate-dialog.component.scss']
})
export class CreateGateDialogComponent implements OnInit {

  nameFormControl = new FormControl('', [
    Validators.required
  ]);

  createGateCommand = new CreateGateCommand();
  result = new EntityResult<boolean>();
  gateTypeResult = new EntityListResult<GateType>();
  accountResult = new EntityListResult<Account>();

  gateTypes: GateType[] = [];
  selectedGateType: string = "";

  options: string[] = [];
  filteredOptions: Observable<string[]>;
  myControl = new FormControl();
  selectedAccount: string;

  constructor(private gateTypeService: GateTypeService,
    private accountService: AccountService,
    private gateService: GateService,
    public dialogRef: MatDialogRef<CreateGateDialogComponent>) { }

  ngOnInit() {
    this.getGateTypes();
    this.getAccounts();
  }

  getGateTypes(pagination: ListPagination = null, sorting: Sorting = null, filter: string = null){
    this.gateTypeResult.onFinished = () => {
      if (this.gateTypeResult.hasValue)
        this.gateTypes = this.gateTypeResult.value;
        this.selectedGateType = this.gateTypes[0].name;
    }
    
    this.gateTypeService.getList(this.gateTypeResult, pagination || new ListPagination(), sorting || new Sorting(), filter || "");
  }

  getAccounts(pagination: ListPagination = null, sorting: Sorting = null, filter: string = null){
    this.accountResult.onFinished = () => {
      if (this.accountResult.hasValue){
        this.accountResult.value.forEach(x => {
          this.options.push(x.name);
        });
        this.setFilteredOptions();
      }
    }
    
    this.accountService.getList(this.accountResult, pagination || new ListPagination(), sorting || new Sorting(), filter || "");
  }

  setFilteredOptions() {
    console.log(this.options);
    this.filteredOptions = this.myControl.valueChanges
      .pipe(
        startWith(''),
        map(value => typeof value === 'string' ? value : value),
        map(name => name ? this._filter(name) : this.options.slice())
      );
  }

  displayFn(name: string): string {
    return name ? name : '';
  }

  private _filter(email: string): string[] {
    const filterValue = email.toLowerCase();

    return this.options.filter(option => option.toLowerCase().indexOf(filterValue) === 0);
  }

  select(value) {
    this.selectedAccount = value;
  }

  create() {
    this.createGateCommand.accountName = this.selectedAccount;
    this.createGateCommand.gateTypeName = this.selectedGateType;
    this.result.onFinished = () => {
      if (this.result.hasValue){
        this.dialogRef.close("success");
      }
      else if (this.result.hasError) {
        this.dialogRef.close(this.result.errorMessage);
      }
    }
    this.gateService.create(this.createGateCommand, this.result);
  }

}
