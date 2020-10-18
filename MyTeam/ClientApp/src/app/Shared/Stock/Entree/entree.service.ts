import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { catchError, map } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { Entree } from '../../../Models/bo/Stock/Entree/Entree';

@Injectable({
  providedIn: 'root'
})
export class EntreeService {

  readonly BaseURI = 'https://localhost:44384/api';//TODO: add this value in config file 
  constructor(private http: HttpClient, private router: Router) { }
  GetEntree(form) {
    var host = this.BaseURI + '/ENTREEs/FindENTREE';
    return this.http.post(host, form).pipe(
      map((data: Entree[]) => {
        return data;
      }), catchError(error => {
        return throwError('Something went wrong!');
      })
    )
  }

}
