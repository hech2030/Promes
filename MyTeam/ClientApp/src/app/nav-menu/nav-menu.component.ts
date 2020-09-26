import { Component, OnInit } from '@angular/core';
import { UserService } from '../Shared/user.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit{

  constructor(private service: UserService) {

  }
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  //loginStatus$: Observable<boolean>;
  loginStatus: boolean;

  ngOnInit(): void {
    this.loginStatus = false;
    if (localStorage.getItem('token') != null) {
      this.loginStatus = true;
    }
    //this.loginStatus$ = this.service.isLoggedIn;
  }
  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  onLogout() {
    this.loginStatus = false;
    this.service.logout();
  }

}
