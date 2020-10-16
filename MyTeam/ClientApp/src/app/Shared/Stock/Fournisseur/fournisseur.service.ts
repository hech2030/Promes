import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { catchError, map } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { Fournisseur } from '../../../Models/bo/Stock/Fournisseur/fournisseur';

@Injectable({
  providedIn: 'root'
})
export class FournisseurService {

  readonly BaseURI = 'https://localhost:44384/api';//TODO: add this value in config file 
  constructor(private http: HttpClient, private router: Router) { }
  GetFournisseur(form) {
    var host = this.BaseURI + '/Fournisseurs/FindFournisseur';
    return this.http.post(host, form).pipe(
      map((data: Fournisseur[]) => {
        return data;
      }), catchError(error => {
        return throwError('Something went wrong!');
      })
    )
  }

}
