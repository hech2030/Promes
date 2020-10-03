import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../../../Shared/user.service';
import { User } from '../../../Models/bo/user.model';
import { NgForm } from '@angular/forms';
import Swal from 'sweetalert2';
import * as $ from 'jquery';
import '@progress/kendo-ui';
import { MyToolsService } from '../../../Shared/Tools/my-tools.service';

@Component({
  templateUrl: './users.component.html',
  host: { 'class': 'content-wrapper' },
  styleUrls: ['./users.component.css']
})
export class AdminUsersComponent implements OnInit {
  root = "Users/UsersDetails/";
  userModel = {
    email: '',
    username: '',
    Password: '',
    RepeatedPassword: '',
    fullName: '',
    roleLabel: '',
    role: -1
  }
  SearchCriteria = {
    username: null,
    UserRole: -1
  }
  SelectedRole: any;
  Isloading: boolean;
  public Roles = [
    { id: 3, Label: "Admin" },
    { id: 1, Label: "Gestionnaire" },
    { id: 2, Label: "Superviseur" }] // TODO : à les mettres dans une table referentiel
  users: User[];

  constructor(private userService: UserService, private tools: MyToolsService) { }

  ngOnInit() {
    this.Isloading = true;
    if (this.SelectedRole != undefined && this.SelectedRole != null) {
      this.SearchCriteria.UserRole = this.SelectedRole.id;
    }
    this.userService.GetUsers(this.SearchCriteria)
      .subscribe((data: any) => {
        console.log(data);
        this.users = data.result;
        this.loadGrid(data.result);
        this.Isloading = false;
      });
  }
  InitSearchCriteria() {
    this.SelectedRole = null;
    this.SearchCriteria = {
      username: null,
      UserRole: -1
    }
    this.ngOnInit();
  }
  loadGrid(users) {
    var gridDataSource = new kendo.data.DataSource({
      pageSize: 5,
      serverPaging: false,
      serverSorting: false,
      data: { "values": [], "total": 0 },
      transport: {
        read: function (options) {
          options.success(users);
        }
      },
      schema: {
        data: function (response) {
          return response;
        },
        total: function (response) {
          return response.length;
        }
      }
    });
    $("#grid").kendoGrid({
      dataSource: gridDataSource,
      columns: [
        { field: "email", title: "Email Adress" },
        { field: "userName", title: "User name" },
        { field: "roleLabel", title: "User Role" },
        { field: "phoneNumber", title: "Phone Number" },
        {
          template: "<a class=\"btn-circle btn-xl\" href='\\" + this.root + "#:id#' > <i class=\"fa fa-eye\"></i>"
          + "</a>"
        }

      ],
      pageable: {
        pageSize: 5
      }
    });
  }
  AddUser(Userform: NgForm) {
    //if (Userform.valid) {
    if (this.userModel.Password != this.userModel.RepeatedPassword) {
      Swal.fire('Oops...', "Passwords don't match", 'error');
    }
    else {
      if (!this.validateEmail(this.userModel.email)) {
        Swal.fire('Oops...', "Email Adress is not in correct format", 'error');
      } else {
        switch (this.userModel.roleLabel) {
          case "Admin":
            this.userModel.role = 3;
            break
          case "Gestionnaire":
            this.userModel.role = 1;
            break;
          case "Superviseur":
            this.userModel.role = 2;
            break;
        }
        this.userService.register(this.userModel).subscribe(
          (res: any) => {
            if (res.succeeded) {
              //res.succeeded;
              this.ngOnInit();
              this.resetModel();
              $("[data-dismiss=modal]").trigger({ type: "click" });
              this.tools.ShowSuccessNotification("User", "User Added Successfully", '10000');
            }
            else {
              Swal.fire('Oops...', res.errors[0].description, 'error');
            }
          },
          err => {
            if (err.status == 400) {
              this.tools.ShowErrorNotification("User", "Something went wrong", '10000');
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
    this.userModel.email = '';
    this.userModel.username = '';
    this.userModel.Password = '';
    this.userModel.RepeatedPassword = '';
    this.userModel.fullName = '';
    this.userModel.roleLabel = '';
    this.userModel.role = -1;
  }
  validateEmail(email) {
    const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
  }
}
