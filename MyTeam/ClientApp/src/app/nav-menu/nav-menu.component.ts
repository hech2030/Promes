import { Component, OnInit } from '@angular/core';
import { UserService } from '../Shared/user.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css'],
  host: { 'class': 'navbar default-layout col-lg-12 col-12 p-0 fixed-top d-flex flex-row' }
})
export class NavMenuComponent implements OnInit {

  constructor(private service: UserService) {

  }
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  ngOnInit(): void {
  }
  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  onLogout() {
    this.service.logout();
  }

}
