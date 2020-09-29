import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { UserService } from '../Shared/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sid-menu',
  templateUrl: './sid-menu.component.html',
  styleUrls: ['./sid-menu.component.css'],
  host: { 'class': 'sidebar sidebar-offcanvas','id':'sidebar' }
})
export class sideMenuComponent implements OnInit {

  constructor(private service: UserService, private router: Router) { }

  //loggedIn$: Observable<boolean>;
  userName$: Observable<string>;
  userRole$: Observable<string>;
  RoleId$: Observable<number>;

  ngOnInit(): void {
    //this.loggedIn$ = this.service.isLoggedIn;
    this.userName$ = this.service.getUserName;
    this.userRole$ = this.service.getuserRole;
    this.RoleId$ = this.service.getuserRoleId;
  }
  GetUsers() {
    this.router.navigate(['/Users']);
  }  GetStock() {
    this.router.navigate(['/Stock']);
  }

  GetHome() {
    this.router.navigate(['/Home']);
  }
}
