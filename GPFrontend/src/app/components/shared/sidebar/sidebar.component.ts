import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';

export interface RouteInfo {
  path: string;
  title: string;
  icon: string;
  role: string;
  isAccountAdminAccess: boolean;
}

export const ROUTES: RouteInfo[] = [
  { path: '/main', title: 'Dashboard', icon: 'fas fa-tachometer-alt', role: 'Admin, User', isAccountAdminAccess: false },
  { path: '/main/accounts', title: 'Accounts', icon: 'fas fa-building', role: 'Admin', isAccountAdminAccess: true },
  { path: '/main/gates', title: 'Gates', icon: 'fas fa-dungeon', role: 'Admin, User',isAccountAdminAccess: false }
];

@Component({
  selector: 'sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {
  public menuItems: any[] = [];


  constructor(private authenticationService: AuthenticationService) {

  }

  ngOnInit() {
    ROUTES.forEach(menuItem => {
      if (menuItem.role.includes(this.authenticationService.currentIdentity.value._role) || 
      (menuItem.isAccountAdminAccess && this.authenticationService.currentIdentity.value._isAccountAdmin)) {
        this.menuItems.push(menuItem);
      }
    });
  }
}
