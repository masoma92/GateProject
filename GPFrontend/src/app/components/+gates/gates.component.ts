import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatPaginator, MatSnackBar, MatSort, MatTableDataSource } from '@angular/material';
import { ListPagination } from 'src/app/core/pagination/list-pagination';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';
import { EntityListResult, Sorting } from 'src/app/services/common/entity.service';
import { Gate } from 'src/app/services/gate/gate';
import { GateService } from 'src/app/services/gate/gate.service';
import { CreateGateDialogComponent } from './create-gate/create-gate-dialog.component';

@Component({
  selector: 'app-gates',
  templateUrl: './gates.component.html',
  styleUrls: ['./gates.component.scss']
})
export class GatesComponent implements OnInit {

  displayedColumns: string[] = ['id', 'name', 'gateTypeName', 'serviceId', 'characteristicId', 'accountName'];

  filterInput: string = "";
  
  result = new EntityListResult<Gate>();
  dataSource: MatTableDataSource<Gate>;

  selectedRow;
  gateId: number;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  constructor(private gateService: GateService,
    private changeDetectorRefs: ChangeDetectorRef,
    public dialog: MatDialog,
    private snackBar: MatSnackBar,
    public authenticationService: AuthenticationService) {
    this.getList();
  }

  ngOnInit() {
  }

  getList(pagination: ListPagination = null, sorting: Sorting = null, filter: string = null){
    this.result.onFinished = () => {
      if (this.result.hasValue)
        this.dataSource = new MatTableDataSource(this.result.value);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.refresh();
    }
    
    this.gateService.getList(this.result, pagination || new ListPagination(), sorting || new Sorting(), filter || "");
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
      this.gateId = null;
    }
    else {
      this.selectedRow = row;
      this.gateId = this.selectedRow.id;
    }
  }

  sorter() {
    console.log(this.sort.active);
    console.log(this.sort.start);
  }

  openCreateDialog(): void {
    const dialogRef = this.dialog.open(CreateGateDialogComponent, {
      width: '350px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result){
        if (result == "success"){
          this.snackBar.open("Gate successfully added!!", "Close", { duration: 2000, panelClass: 'toast.success' } );
          this.getList();
        }
        else{
          this.snackBar.open(result, "Close", { duration: 2000, panelClass: 'toast.success' } );
        }
      }
    });
  }

}
