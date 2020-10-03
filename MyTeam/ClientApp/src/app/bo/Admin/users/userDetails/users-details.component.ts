import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../../../../Shared/user.service';
import { User } from '../../../../Models/bo/user.model';
import Swal from 'sweetalert2';
import { MyToolsService } from '../../../../Shared/Tools/my-tools.service';
import * as $ from 'jquery';


@Component({
  selector: 'app-users-details',
  templateUrl: './users-details.component.html',
  styleUrls: ['./users-details.component.css'],
  host: { 'class': 'content-wrapper' },
})
export class UsersDetailsComponent implements OnInit {
  public Roles = [
    { id: 3, Label: "Admin" },
    { id: 1, Label: "Gestionnaire" },
    { id: 2, Label: "Superviseur" }] //TODO : add a new referetiel table

  user: User = new User();

  constructor(private route: ActivatedRoute, private userService: UserService,
    private tools: MyToolsService) { }

  ngOnInit() {
    $("#dropdownlist").kendoDropDownList({
      dataSource: this.Roles,
      dataTextField: "Label",
      dataValueField: "id"

    });
    this.user.id = this.route.snapshot.paramMap.get('id');
    this.loadUser();
  }
  loadUser() {
    this.userService.GetUsers(this.user)
      .subscribe((data: any) => {
        this.user = data.result[0];
        $("#dropdownlist").data("kendoDropDownList").value(this.user.role);
      });
  }

  Update() {
    if (!this.validateEmail(this.user.email)) {
      Swal.fire('Oops...', "Email Adress is not in correct format", 'error');
    } else {
      this.user.role = parseInt($("#dropdownlist").data("kendoDropDownList").value());
      switch (this.user.role) {
        case 3:
          this.user.roleLabel = "Admin";
          break
        case 1:
          this.user.roleLabel = "Gestionnaire";
          break;
        case 2:
          this.user.roleLabel = "Superviseur";
          break;
      }
      this.userService.register(this.user).subscribe(
        (res: any) => {
          if (res.succeeded) {
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
  validateEmail(email) {
    const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
  }
}
