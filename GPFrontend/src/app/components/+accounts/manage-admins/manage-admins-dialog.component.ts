import { Inject } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { ListPagination } from 'src/app/core/pagination/list-pagination';
import { EntityListResult, Sorting } from 'src/app/services/common/entity.service';
import { User, UserService } from 'src/app/services/user/user.service';
import { map, startWith } from 'rxjs/operators';

@Component({
  selector: 'app-manage-admins-dialog',
  templateUrl: './manage-admins-dialog.component.html',
  styleUrls: ['./manage-admins-dialog.component.scss']
})
export class ManageAdminsDialogComponent implements OnInit {

  warningMessage: string = "";

  selectedOption: string;

  options: User[];
  filteredOptions: Observable<User[]>;
  myControl = new FormControl();

  getUsersResult = new EntityListResult<User>();

  constructor(public dialogRef: MatDialogRef<ManageAdminsDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public currentAdminEmails: string[],
    private userService: UserService) {
      console.log(currentAdminEmails);
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
    if (this.currentAdminEmails.indexOf(this.selectedOption) != -1){
      this.warningMessage = "Admin already in the list!";
      return;
    }
    this.currentAdminEmails.push(this.selectedOption);
    this.warningMessage = "";
  }

  select(value) {
    this.selectedOption = value.email;
  }

  remove(email) {
    const index = this.currentAdminEmails.indexOf(email);
    if (index > -1) {
      this.currentAdminEmails.splice(index, 1);
    }
  }

  save() {
    this.dialogRef.close(this.currentAdminEmails);
  }

}
