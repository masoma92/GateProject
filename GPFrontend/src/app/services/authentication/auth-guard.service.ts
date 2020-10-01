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

    const currentUser = this.authenticationService.currentIdentity.value;

    return this.isLoginInProgress().then(x => {
      if(!x){
        if (currentUser._authenticationTokenString == null || currentUser._authenticationTokenString == ""){
          return false;
        }
        this.router.navigate(['403']);
        this.authenticationService.truncateLocalStorage();
        return false;
      }
      this.router.navigate(['/login']);
      return false;
    });
  }

  isLoginInProgress() {
    return new Promise<boolean>((resolve, reject) => {

      this.authenticationService.isLoginInProgress.subscribe(x => {
          console.log(this.authenticationService.currentIdentity.value._authenticationTokenString);
          if (!x) {
              console.log("inside async method", x)
              resolve(x);
          }
      });
  });
  }
}
