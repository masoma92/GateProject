import { Component, ElementRef, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';

@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  
  public isCollapsed = true;
  private sidebarVisible: boolean = false;
  private toggleButton;

  constructor(
    private element : ElementRef,
    private authenticationService: AuthenticationService) { }

  ngOnInit() {
    var navbar : HTMLElement = this.element.nativeElement;
    this.toggleButton = navbar.getElementsByClassName('navbar-toggle')[0];
  }

  sidebarToggle() {
    if (this.sidebarVisible === false) {
        this.sidebarOpen();
    } else {
        this.sidebarClose();
    }
  }
  sidebarOpen() {
      const toggleButton = this.toggleButton;
      const html = document.getElementsByTagName('html')[0];
      const mainPanel =  <HTMLElement>document.getElementsByClassName('main-panel')[0];
      setTimeout(function(){
          toggleButton.classList.add('toggled');
      }, 500);

      html.classList.add('nav-open');
      if (window.innerWidth < 991) {
        mainPanel.style.position = 'fixed';
      }
      this.sidebarVisible = true;
  };
  sidebarClose() {
      const html = document.getElementsByTagName('html')[0];
      const mainPanel =  <HTMLElement>document.getElementsByClassName('main-panel')[0];
      if (window.innerWidth < 991) {
        setTimeout(function(){
          mainPanel.style.position = '';
        }, 500);
      }
      this.toggleButton.classList.remove('toggled');
      this.sidebarVisible = false;
      html.classList.remove('nav-open');
  };

  logout() {
    this.authenticationService.logout();
  }

}
