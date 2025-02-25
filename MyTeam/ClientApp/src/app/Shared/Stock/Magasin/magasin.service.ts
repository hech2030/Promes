import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { catchError, map } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { Magasin } from '../../../Models/bo/Stock/Magasin/magasin';
import { UserService } from '../../user.service';
import { MyToolsService } from '../../Tools/my-tools.service';

@Injectable({
  providedIn: 'root'
})
export class MagasinService {
  readonly BaseURI = 'https://localhost:44384/api';//TODO: add this value in config file 
  constructor(private http: HttpClient, private router: Router,
    private MainService: UserService,
    private tools: MyToolsService) { }
  GetMagasin(form) {
    var host = this.BaseURI + '/Magasins/FindMagasin';
    return this.http.post(host, form).pipe(
      map((data: Magasin[]) => {
        return data;
      }), catchError(error => {
        return throwError('Something went wrong!');
      })
    )
  }

  SaveMagasin(Magasin) {
    var host = this.BaseURI + '/Magasins/SaveMagasin';
    return this.http.post(host, { 'value': Magasin });
  }

  DeleteMagasin(id) {
    var host = this.BaseURI + '/Magasins/DeleteMagasin';
    return this.http.post(host, { id: id }).pipe(
      map((data: boolean) => {
        return data;
      }), catchError(error => {
        if (error.status == 401) {
          this.MainService.logout();
        }
        else {
          if (error.error.helpLink != null && error.error.helpLink != undefined) {
            this.tools.ShowErrorNotification('Magasin', error.error.helpLink, 10000);
          }
          return throwError('Something went wrong!');
        }
      })
    )
  }

}
