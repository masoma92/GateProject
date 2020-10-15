import { Component, Inject, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Observable } from 'rxjs';
import { ListPagination } from 'src/app/core/pagination/list-pagination';
import { EntityListResult, Sorting } from 'src/app/services/common/entity.service';
import { UserGate } from 'src/app/services/gate/gate';
import { User, UserService } from 'src/app/services/user/user.service';
import { map, startWith } from 'rxjs/operators';

@Component({
  selector: 'app-manage-gateusers-dialog',
  templateUrl: './manage-gateusers-dialog.component.html',
  styleUrls: ['./manage-gateusers-dialog.component.scss']
})
export class ManageGateusersDialogComponent implements OnInit {
  warningMessage: string = "";

  selectedOption = new UserGate();

  options: User[];
  filteredOptions: Observable<User[]>;
  myControl = new FormControl();

  getUsersResult = new EntityListResult<User>();

  constructor(public dialogRef: MatDialogRef<ManageGateusersDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public currentUsers: UserGate[],
    private userService: UserService) {
      console.log(currentUsers);
      this.getUsers();
    }

  ngOnInit() {

  }

  getUsers(pagination: ListPagination = null, sorting: Sorting = null, filter: string = null){
    this.getUsersResult.onFinished = () => {
      if (this.getUsersResult.hasValue)
        this.options = this.getUsersResult.value;
        this.setFilteredOptions();
    }
    this.userService.getList(this.getUsersResult, pagination || new ListPagination(), sorting || new Sorting(), filter || "");
  }

  setFilteredOptions() {
    this.filteredOptions = this.myControl.valueChanges
      .pipe(
        startWith(''),
        map(value => typeof value === 'string' ? value : value.email),
        map(email => email ? this._filter(email) : this.options.slice())
      );
  }

  displayFn(user: User): string {
    return user && user.email ? user.email : '';
  }

  private _filter(email: string): User[] {
    const filterValue = email.toLowerCase();

    return this.options.filter(option => option.email.toLowerCase().indexOf(filterValue) === 0);
  }
  
  add() {
    if (!this.selectedOption){
      this.warningMessage = "You must select first!";
      return;
    }
    if (this.currentUsers.findIndex(x => x.email == this.selectedOption.email) != -1){
      this.warningMessage = "Admin already in the list!";
      return;
    }
    if (this.selectedOption.accessRight == undefined)
      this.selectedOption.accessRight = false;
    if (this.selectedOption.adminRight == undefined)
      this.selectedOption.adminRight = false;
    this.currentUsers.push(this.selectedOption);
    this.warningMessage = "";
  }

  select(value) {
    this.selectedOption = value;
  }

  remove(email) {
    const index = this.currentUsers.findIndex(x => x.email == email);
    if (index > -1) {
      this.currentUsers.splice(index, 1);
    }
  }

  save() {
    this.dialogRef.close(this.currentUsers);
  }

}
