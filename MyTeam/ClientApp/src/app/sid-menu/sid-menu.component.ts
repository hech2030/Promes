import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { UserService } from '../Shared/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sid-menu',
  templateUrl: './sid-menu.component.html',
  styleUrls: ['./sid-menu.component.css']
})
export class sideMenuComponent implements OnInit {

  constructor(private service: UserService, private router: Router) { }
  
  //loginStatus$: Observable<boolean>;
  userName: string;
  userRole: string;
  loginStatus: boolean ;

  ngOnInit(): void {
    this.loginStatus = false;
    if (localStorage.getItem('token') != null) {
      this.loginStatus = true;
      this.userName = localStorage.getItem('UserName');
      this.userRole = localStorage.getItem('Role');
    }
    //this.loginStatus$ = this.service.isLoggedIn;
    //this.userName = this.service.UserName;
    //this.userRole = this.service.UserRole;
  }
  GetUsers() {
    this.router.navigate(['/Users']);
  }  GetStock() {
    this.router.navigate(['/Stock']);
  }
  GetHome() {
    this.router.navigate(['/']);
  }
}
