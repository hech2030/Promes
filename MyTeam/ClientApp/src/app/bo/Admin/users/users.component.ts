import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../Shared/user.service';
import { User } from '../../../Models/bo/user.model';

@Component({
  templateUrl: './users.component.html',
  host: { 'class': 'content-wrapper' },
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
