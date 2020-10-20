import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ListPagination } from 'src/app/core/pagination/list-pagination';
import { AccountService } from 'src/app/services/account/account.service';
import { EntityListResult, EntityResult, Sorting } from 'src/app/services/common/entity.service';
import { GateType, GateTypeService } from 'src/app/services/gate-type/gate-type.service';
import { Gate } from 'src/app/services/gate/gate';
import { GateService } from 'src/app/services/gate/gate.service';
import { Account } from 'src/app/services/account/account';
import { FormControl, Validators } from '@angular/forms';
import { map, startWith } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { ManageGateusersDialogComponent } from '../manage-gateusers/manage-gateusers-dialog.component';
import { MatSnackBar } from '@angular/material';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';

@Component({
  selector: 'gate-details',
  templateUrl: './gate-details.component.html',
  styleUrls: ['./gate-details.component.scss']
})
export class GateDetailsComponent implements OnInit {

  nameFormControl = new FormControl('', [
    Validators.required
  ]);

  private _gateId: number;

  @Input('gateId') set gateId(value: number){
    if (value) {
      this._gateId = value;
      this.get();
    }
  }

  get gateId() {
    return this._gateId;
  }

  @Output() updateGateEmitter = new EventEmitter();

  getResult = new EntityResult<Gate>();

  gateTypeResult = new EntityListResult<GateType>();
  accountResult = new EntityListResult<Account>();
  updateResult = new EntityResult<boolean>();
  deleteResult = new EntityResult<boolean>();

  gateTypes: GateType[] = [];
  selectedGateType: string = "";

  options: string[] = [];
  filteredOptions: Observable<string[]>;
  myControl = new FormControl();
  selectedAccount: string;

  isAccountProcessFinished = false;

  constructor(private gateService: GateService,
    private gateTypeService: GateTypeService,
    private accountService: AccountService,
    public dialog: MatDialog,
    private snackBar: MatSnackBar,
    public authenticationService: AuthenticationService) { }

  ngOnInit() {
  }

  get() {
    this.getResult.onFinished = () => {
      this.getGateTypes();
      if (this.authenticationService.role == "Admin")
        this.getAccounts();
    }
    this.gateService.get(this.gateId, this.getResult);
  }

  getGateTypes(pagination: ListPagination = null, sorting: Sorting = null, filter: string = null){
    this.gateTypeResult.onFinished = () => {
      if (this.gateTypeResult.hasValue)
        this.gateTypes = this.gateTypeResult.value;
        this.selectedGateType = this.getResult.value.gateTypeName;
    }
    
    this.gateTypeService.getList(this.gateTypeResult, pagination || new ListPagination(), sorting || new Sorting(), filter || "");
  }

  getAccounts(pagination: ListPagination = null, sorting: Sorting = null, filter: string = null){
    this.accountResult.onFinished = () => {
      if (this.accountResult.hasValue){
        this.options = [];
        this.accountResult.value.forEach(x => {
          this.options.push(x.name);
        });
        this.setFilteredOptions();
        this.myControl.setValue(this.getResult.value.accountName);
      }
    }
    
    this.accountService.getList(this.accountResult, pagination || new ListPagination(), sorting || new Sorting(), filter || "");
  }

  setFilteredOptions() {
    this.filteredOptions = this.myControl.valueChanges
      .pipe(
        startWith(''),
        map(value => typeof value === 'string' ? value : value),
        map(name => name ? this._filter(name) : this.options.slice()),
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

  manageUsers() {
    const dialogRef = this.dialog.open(ManageGateusersDialogComponent, {
      data: this.getResult.value.users,
      width: '400px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getResult.value.users = result;
        this.save();
      }
    });
  }

  save() {
    this.getResult.value.gateTypeName = this.selectedGateType;
    this.getResult.value.accountName = this.selectedAccount;
    this.updateResult.onFinished = () => {
      if (this.updateResult.hasValue){
        this.updateGateEmitter.emit('success');
        this.snackBar.open("Gate saved!!", "Close", { duration: 2000, panelClass: 'toast.success' } );
      }
      else if (this.updateResult.hasError) {
        this.snackBar.open(this.updateResult.errorMessage, "Close", { duration: 2000, panelClass: 'toast.error' } );
      }
    }
    this.gateService.update(this.getResult.value, this.updateResult);
  }

  delete() {
    this.deleteResult.onFinished = () => {
      if (this.deleteResult.hasValue){
        this.getResult = new EntityResult<Gate>();
        this.updateGateEmitter.emit('success');
        this.snackBar.open("Successfully deleted!!", "Close", { duration: 2000, panelClass: 'toast.success' } );
      }
      else if (this.deleteResult.hasError) {
        this.snackBar.open(this.deleteResult.errorMessage, "Close", { duration: 2000, panelClass: 'toast.error' } );
      }
    }
    this.gateService.delete(this.gateId, this.deleteResult);
  }

}
