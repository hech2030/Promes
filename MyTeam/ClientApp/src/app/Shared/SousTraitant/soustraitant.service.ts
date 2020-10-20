import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SousTraitant } from '../../Models/bo/SousTraitant/soustraitant.model';
import { throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { UserService } from '../user.service';

@Injectable({
  providedIn: 'root'
})

export class SousTraitantService {
  readonly BaseURI = 'https://localhost:44384/api';//TODO: add this value in config file 

  constructor(private http: HttpClient, private MainService: UserService) { }

  GetSousTraitant(soustraitant) {
    var host = this.BaseURI + '/fw/SousTraitants/FindSousTraitant';
    return this.http.post(host, soustraitant).pipe(
      map((data: SousTraitant[]) => {
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
  SaveSousTraitant(soustraitant) {
    var host = this.BaseURI + '/fw/SousTraitants/SaveSousTraitant';
    return this.http.post(host, { 'value': soustraitant});
  }
  DeleteSousTraitant(id) {
    var host = this.BaseURI + '/fw/SousTraitants/DeleteSousTraitant';
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
