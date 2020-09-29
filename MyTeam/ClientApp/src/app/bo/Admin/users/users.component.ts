import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../Shared/user.service';
import { User } from '../../../Models/bo/user.model';
import { NgForm } from '@angular/forms';
import Swal from 'sweetalert2';
import * as jquery from 'jquery';

@Component({
  templateUrl: './users.component.html',
  host: { 'class': 'content-wrapper' },
  styleUrls: ['./users.component.css']
})
export class AdminUsersComponent implements OnInit {
  userModel = {
    EmailAdress: '',
    UserName: '',
    Password: '',
    RepeatedPassword: '',
    fullName: '',
    role: '',
    roleId: -1
  }
  users: User[];

  constructor(private userService: UserService) { }



  ngOnInit() {
    jquery(document).ready(function () {
      jquery('#dtBasicExample').DataTable();
      jquery('.dataTables_length').addClass('bs-select');
    });
    this.userService.GetUsers(null)
      .subscribe((data: any) => {
        console.log(data);
        this.users = data.result;
      });
  }
  AddUser(Userform: NgForm) {
    //if (Userform.valid) {
    if (this.userModel.Password != this.userModel.RepeatedPassword) {
      Swal.fire('Oops...', "Passwords don't match", 'error');
    }
    else {
      if (!this.validateEmail(this.userModel.EmailAdress)) {
        Swal.fire('Oops...', "Email Adress is not in correct format", 'error');
      } else {
        switch (this.userModel.role) {
          case "Admin":
            this.userModel.roleId = 0;
            break
          case "Gestionnaire":
            this.userModel.roleId = 1;
            break;
          case "Superviseur":
            this.userModel.roleId = 2;
            break;
        }
        this.userService.register(this.userModel).subscribe(
          (res: any) => {
            if (res.succeeded) {
              //res.succeeded;
              this.ngOnInit();
              this.resetModel();
              jquery("[data-dismiss=modal]").trigger({ type: "click" });
            }
            else {
              Swal.fire('Oops...', res.errors[0].description, 'error');
            }
          },
          err => {
            if (err.status == 400) {
            }
            else {
              return console.log(err);
            }
          }
        );
      }
    }
    //}
  }
  resetModel() {
    this.userModel.EmailAdress = '';
    this.userModel.UserName = '';
    this.userModel.Password = '';
    this.userModel.RepeatedPassword = '';
    this.userModel.fullName = '';
    this.userModel.role = '';
    this.userModel.roleId = -1;
  }
  validateEmail(email) {
    const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
  }


}
