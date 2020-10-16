import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { catchError, map } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { CategorieArt } from '../../../Models/bo/Stock/Categorie-Art/categorie-art';

@Injectable({
  providedIn: 'root'
})
export class CategorieArtService {

  readonly BaseURI = 'https://localhost:44384/api';//TODO: add this value in config file 
  constructor(private http: HttpClient, private router: Router) { }
  GetCategorieArt(form) {
    var host = this.BaseURI + '/CATEGORIE_ARTs/FindCATEGORIE_ART';
    return this.http.post(host, form).pipe(
      map((data: CategorieArt[]) => {
        return data;
      }), catchError(error => {
        return throwError('Something went wrong!');
      })
    )
  }

}
