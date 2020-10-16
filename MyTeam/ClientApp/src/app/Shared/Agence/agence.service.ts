import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Agence } from '../../Models/bo/Agence/agence.model';
import { throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { UserService } from '../user.service';

@Injectable({
  providedIn: 'root'
})

export class AgenceService {
  readonly BaseURI = 'https://localhost:44384/api';//TODO: add this value in config file 

  constructor(private http: HttpClient, private MainService: UserService) { }

  GetAgence(agence) {
    var host = this.BaseURI + '/fw/Agences/FindAgence';
    return this.http.post(host, agence).pipe(
      map((data: Agence[]) => {
        return data;
      }), catchError(error => {
        if (error.status == 401) {
          this.MainService.logout();
        }
        else {
          return throwError('Something went wrong!');
        }
      })
    )
  }
  SaveAgence(agence) {
    var host = this.BaseURI + '/fw/Agences/SaveAgence';
    return this.http.post(host, { 'value': agence});
  }
  DeleteAgence(id) {
    var host = this.BaseURI + '/fw/Agences/DeleteAgence';
    return this.http.post(host, { id: id }).pipe(
      map((data: boolean) => {
        return data;
      }), catchError(error => {
        if (error.status == 401) {
          this.MainService.logout();
        }
        else {
          return throwError('Something went wrong!');
        }
      })
    )
  }
}
