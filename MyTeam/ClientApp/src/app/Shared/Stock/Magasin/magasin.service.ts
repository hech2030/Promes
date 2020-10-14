import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { catchError, map } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { Magasin } from '../../../Models/bo/Stock/Magasin/magasin';

@Injectable({
  providedIn: 'root'
})
export class MagasinService {

  readonly BaseURI = 'https://localhost:44384/api';//TODO: add this value in config file 
  constructor(private http: HttpClient, private router: Router) { }
  GetMagasin(form) {
    var host = this.BaseURI + '/Magasins';
    return this.http.get<any[]>(host, {}).pipe(
      map((data: Magasin[]) => {
        return data;
      }), catchError(error => {
        return throwError('Something went wrong!');
      })
    )
  }

}
