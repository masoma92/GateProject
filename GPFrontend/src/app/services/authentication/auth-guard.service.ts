import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {

    return this.isLoginInProgress().then(loginInProgress => {
      if (!loginInProgress){
        if (this.authenticationService.isAuthenticated()) {
          if (route.data.roles && route.data.roles.indexOf(this.authenticationService.currentIdentity.value._role) === -1){
            if (route.data.isAccountAdmin && this.authenticationService.currentIdentity.value._isAccountAdmin)
            {
              return true;
            }
            this.authenticationService.truncateLocalStorage();
              
            return false;
          }
            // this.router.navigate(['403']);

          return true;
        }
      }
      this.router.navigate(['login']);
      return false;
    });

  }

  

  isLoginInProgress() {
    return new Promise<boolean>((resolve, reject) => {
      this.authenticationService.isLoginInProgress.subscribe(x => {
        if (!x) {
          resolve(x); //csak akkor hívódik meg a canActivate amikor ez false-t dobott
        }
      });
    });
  }
}