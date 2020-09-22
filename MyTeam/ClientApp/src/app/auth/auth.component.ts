import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { UserService } from '../Shared/user.service';
import { Router } from '@angular/router';
import { Local } from 'protractor/built/driverProviders';

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
    if (localStorage.getItem('token') != null)
      this.router.navigate(['/Home']);
  }

  OnSubmit(form: NgForm) {
    this.service.login(form).subscribe(
      (res: any) => {
        localStorage.setItem('token', res.token);
        this.router.navigateByUrl('/Home');
      },
      err => {
        if (err.status == 400) {
          console.log("user name or p assword are incorrect");
        }
        else {
          console.log(err);
        }
      }
    );
  } 

}
