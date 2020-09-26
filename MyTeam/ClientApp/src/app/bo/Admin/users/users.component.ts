import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../Shared/user.service';
import { User } from '../../../Models/bo/user.model';

@Component({
  //selector: 'div',
  templateUrl: './users.component.html',
  host: { 'class': 'main-panel' },
  styleUrls: ['./users.component.css']
})
export class AdminUsersComponent implements OnInit {

  constructor(private userService: UserService) { }


  users: User[];

  ngOnInit() {
    this.userService.GetUsers(null)
      .subscribe((data: any) => {
        console.log(data);
        this.users = data.result;
      });
  }

}
