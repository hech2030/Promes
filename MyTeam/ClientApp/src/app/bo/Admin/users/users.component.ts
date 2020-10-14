import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../../../Shared/user.service';
import { User } from '../../../Models/bo/user.model';
import { NgForm } from '@angular/forms';
import Swal from 'sweetalert2';
import * as $ from 'jquery';
import '@progress/kendo-ui';
import { MyToolsService } from '../../../Shared/Tools/my-tools.service';
import { Router } from '@angular/router';

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

  public gridData: User[];

  constructor(private userService: UserService, private tools: MyToolsService, private router: Router) { }

  ngOnInit() {
    this.Isloading = true;
    if (this.SelectedRole != undefined && this.SelectedRole != null) {
      this.SearchCriteria.UserRole = this.SelectedRole.id;
    }
    this.userService.GetUsers(this.SearchCriteria)
      .subscribe((data: any) => {
        console.log(data);
        this.users = data.result;
        this.gridData = data.result;
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
  DeleteUser(id) {
    Swal.fire({
      title: 'Êtes-vous sûr de vouloir supprimer?',
      text: "Vous ne pourrez pas revenir en arrière!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Oui, Supprimer!'
    }).then((result) => {
      if (result.isConfirmed) {
        this.userService.DeleteUser(id)
          .subscribe((data: any) => {
            if (data.success) {
              this.ngOnInit();
              Swal.fire(
                'Supprimé!',
                'Succès',
                'success'
              )
            }
            else {
              Swal.fire(
                'Erreur!',
                'Un problème est survenu avec le serveur.',
                'error'
              )
            }
          });

      }
    })
  }

  AddUser(Userform: NgForm) {
    if (this.userModel.Password != this.userModel.RepeatedPassword) {
      Swal.fire('Oops...', "Les mot de passes ne sont pas identiques", 'error');
    }
    else {
      if (!this.validateEmail(this.userModel.email)) {
        Swal.fire('Oops...', "Le format de l\'adresse email n\'est pas valide", 'error');
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
              this.ngOnInit();
              this.resetModel();
              $("[data-dismiss=modal]").trigger({ type: "click" });
              this.tools.ShowSuccessNotification("User", "Utilisateur ajouté avec succeés", '10000');
            }
            else {
              Swal.fire('Oops...', res.errors[0].description, 'error');
            }
          },
          err => {
            if (err.status == 400) {
              this.tools.ShowErrorNotification("User", "Quelque chose ne va pas... veillez contacter l\'administrateur", '10000');
            }
            else {
              return console.log(err);
            }
          }
        );
      }
    }
  }
  UpdateUser(id) {
    this.router.navigate(['Users/UsersDetails/' + id]);
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
