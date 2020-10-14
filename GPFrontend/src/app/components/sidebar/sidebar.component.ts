import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';

export interface RouteInfo {
  path: string;
  title: string;
  icon: string;
  role: string;
}

export const ROUTES: RouteInfo[] = [
  { path: '/main', title: 'Dashboard', icon: 'fas fa-tachometer-alt', role: 'Admin, User' },
  { path: '/main/accounts', title: 'Accounts', icon: 'fas fa-building', role: 'Admin' },
  { path: '/main/gates', title: 'Gate', icon: 'fas fa-dungeon', role: 'Admin' }
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
      if (menuItem.role.includes(this.authenticationService.currentIdentity.value._role)) {
        this.menuItems.push(menuItem);
      }
    });
  }
}
