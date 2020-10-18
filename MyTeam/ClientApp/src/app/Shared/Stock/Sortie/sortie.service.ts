import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { catchError, map } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { Sortie } from '../../../Models/bo/Stock/Sortie/sortie';

@Injectable({
  providedIn: 'root'
})
export class SortieService {

  readonly BaseURI = 'https://localhost:44384/api';//TODO: add this value in config file 
  constructor(private http: HttpClient, private router: Router) { }
  GetSortie(form) {
    var host = this.BaseURI + '/Sorties/FindSORTIE';
    return this.http.post(host, form).pipe(
      map((data: Sortie[]) => {
        return data;
      }), catchError(error => {
        return throwError('Something went wrong!');
      })
    )
  }

}
