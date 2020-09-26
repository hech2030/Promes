import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { User } from '../Models/bo/user.model';
import { catchError, map } from 'rxjs/operators';
import { throwError } from 'rxjs';



@Injectable({
  providedIn: 'root'
})
export class UserService {
  readonly BaseURI = 'https://localhost:44384/api';//TODO: add this value in config file 
  constructor(private http: HttpClient, private router: Router) { }

  login(formData) {
    var host = this.BaseURI + '/fw/Users/login';
    return this.http.post(host, formData)
      .subscribe(
        (res: any) => {
          localStorage.setItem('token', res.token);
          localStorage.setItem('UserName', res.normalizedUserName);
          localStorage.setItem('Role', res.Role);
          //this.userFullName.next(res.normalizedUserName);
          //this.userRole.next(res.role);
          var element = document.getElementById("MainClass");
          element.classList.add("container-fluid");
          element.classList.add("page-body-wrapper");
          //this.router.navigate(['/Home']);
          //this.window.location.reload();
          this.router.navigate(['/Home'])
            .then(() => {
              window.location.reload();
            });
          //this.loggedIn.next(true);
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

  logout() {
    localStorage.clear();
    //this.loggedIn.next(false);
    var element = document.getElementById("MainClass");
    element.classList.remove("container-fluid");
    element.classList.remove("page-body-wrapper");
    this.router.navigate(['/']);
  }

  GetUsers(form) {
    var host = this.BaseURI + '/fw/Users/FindUser';
    return this.http.post<any[]>(host, {}).pipe(
      map((data: User[]) => {
        return data;
      }), catchError(error => {
        return throwError('Something went wrong!');
      })
    )
  }

  getToken() {
    return localStorage.getItem('token');
  }
}

