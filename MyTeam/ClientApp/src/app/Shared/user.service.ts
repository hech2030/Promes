import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { User } from '../Models/bo/user.model';
import { catchError, map } from 'rxjs/operators';
import { throwError, BehaviorSubject } from 'rxjs';
import Swal from 'sweetalert2';
import { Local } from 'protractor/built/driverProviders';




@Injectable({
  providedIn: 'root'
})
export class UserService {

  readonly BaseURI = 'https://localhost:44384/api';//TODO: add this value in config file 
  constructor(private http: HttpClient, private router: Router) { }

  private loggedIn = new BehaviorSubject<boolean>(false);
  private userName = new BehaviorSubject<string>('');
  private userRole = new BehaviorSubject<string>('');
  private appImage = new BehaviorSubject<string>('');
  private RoleId = new BehaviorSubject<number>(-1);

  get isLoggedIn() {
    if (localStorage.getItem('token') != null) {
      this.loggedIn.next(true);
    }
    return this.loggedIn.asObservable();
  }

  get getUserName() {
    if (localStorage.getItem('UserName') != null) {
      this.userName.next(localStorage.getItem('UserName'));
    }
    return this.userName.asObservable();
  }
  get getuserRole() {
    if (localStorage.getItem('Role') != null) {
      this.userRole.next(localStorage.getItem('Role'));
    }
    return this.userRole.asObservable();
  }
  get getuserRoleId() {
    if (sessionStorage.getItem('RoleId')) {
      this.RoleId.next(parseInt(sessionStorage.getItem('RoleId')));
    }
    return this.RoleId.asObservable();
  }
  get GetAppImage() {
    if (sessionStorage.getItem('resImage') != 'null') {
      this.appImage.next(sessionStorage.getItem('resImage'));
    }
    else {
      this.appImage.next('../../assets/images/faces/face8.jpg');
    }
    return this.appImage.asObservable();
  }

  updateUserName(userName) {
    localStorage.setItem('UserName', userName);
    this.userName.next(userName);
  }
  updateAppImage(ImageSource) {
    sessionStorage.setItem('resImage', ImageSource);
    this.appImage.next(ImageSource);
  }

  GetProfileUser(UserName) {
    var host = this.BaseURI + '/fw/Users/FindUser';
    return this.http.post(host, { 'FullUserName': UserName }).pipe(
      map((data: User[]) => {
        return data;
      }), catchError(error => {
        if (error.status == 401) {
          this.logout();
        }
        else {
          return throwError('Something went wrong!');
        }
      })
    )
  }
  login(formData) {
    var host = this.BaseURI + '/fw/Users/login';
    return this.http.post(host, formData)
      .subscribe(
        (res: any) => {
          localStorage.setItem('token', res.token);
          localStorage.setItem('UserName', res.userName);
          localStorage.setItem('Role', res.roleLabel);
          sessionStorage.setItem('RoleId', res.role);
          sessionStorage.setItem('resImage', res.image);
          this.userName.next(res.normalizedUserName);
          this.userRole.next(res.roleLabel);
          this.RoleId.next(res.role);
          this.appImage.next(res.image);
          var element = document.getElementById("MainClass");
          element.classList.add("container-fluid");
          element.classList.add("page-body-wrapper");
          var mainPanel = document.getElementById("mainPanel");
          mainPanel.classList.add("main-panel");
          this.loggedIn.next(true);
          this.router.navigate(['/Home'])
            .then(() => {
              //window.location.reload();
            });
        },
        err => {
          if (err.status == 400) {
            Swal.fire('Oops...', "user name or password are incorrect", 'error')
          }
          else {
            Swal.fire('Oops...', 'Something wrong with the serveur, please contact your administrator', 'error')
            console.log(err);
          }
        }
      );
  }

  register(formData) {
    var host = this.BaseURI + '/fw/Users/Register';
    return this.http.post(host, formData);
  }

  logout() {
    localStorage.clear();
    this.loggedIn.next(false);
    this.userName.next('');
    this.userRole.next('');
    var element = document.getElementById("MainClass");
    element.classList.remove("container-fluid");
    element.classList.remove("page-body-wrapper");

    var mainPanel = document.getElementById("mainPanel");
    mainPanel.classList.remove("main-panel");
    this.router.navigate(['/']);
  }

  GetUsers(form) {
    var host = this.BaseURI + '/fw/Users/FindUser';
    return this.http.post(host, form).pipe(
      map((data: User[]) => {
        return data;
      }), catchError(error => {
        if (error.status == 401) {
          this.logout();
        }
        else {
          return throwError('Something went wrong!');
        }
      })
    )
  }
  DeleteUser(id) {
    var host = this.BaseURI + '/fw/Users/DeleteUser';
    return this.http.post(host, { id: id }).pipe(
      map((data: boolean) => {
        return data;
      }), catchError(error => {
        if (error.status == 401) {
          this.logout();
        }
        else {
          return throwError('Something went wrong!');
        }
      })
    )
  }

  getToken() {
    return localStorage.getItem('token');
  }
}

