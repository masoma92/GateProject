import { Component } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent  {

  isAdmin = false;
  isAccountAdmin = false;
  isUser = false;

  constructor(private authenticationService: AuthenticationService) {
    if (this.authenticationService.role == 'Admin'){
      this.isAdmin = true;
    }
    else if (this.authenticationService.isAccountAdmin) {
      this.isAccountAdmin = true;
    }
    else {
      this.isUser = true;
    }
  }
}
