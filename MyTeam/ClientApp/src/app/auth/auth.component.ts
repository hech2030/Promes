import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { UserService } from '../Shared/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {
  formModel = {
    UserName: '',
    Password: ''
  }
  constructor(private service: UserService, private router: Router) { }

  ngOnInit() {
    var mainPanel = document.getElementById("mainPanel");
    if (mainPanel.classList.contains("main-panel")) {
      mainPanel.classList.remove("main-panel");
    }
    if (localStorage.getItem('token') != null) {
      this.router.navigate(['/Home']);
    }
    else {
      var element = document.getElementById("MainClass");
      element.classList.remove("container-fluid");
      element.classList.remove("page-body-wrapper");
    }
  }




  OnSubmit(form) {
    this.service.login(form);
  } 

}
