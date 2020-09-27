import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserService } from '../Shared/user.service';
import { map, take  } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router, private service: UserService) {

  }
  //canActivate(
  //  next: ActivatedRouteSnapshot,
  //  state: RouterStateSnapshot): boolean {
  //  if (localStorage.getItem('token') != null)
  //    return true;
  //  else {
  //    this.router.navigate(['/']);
  //    return false;
  //  }
  //}
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> {
    return this.service.isLoggedIn         // {1}
      .pipe(
        take(1),                              // {2} 
        map((isLoggedIn: boolean) => {         // {3}
          if (!isLoggedIn) {
            this.router.navigate(['/']);  // {4}
            return false;
          }
          return true;
        })
      )  
  }
}
