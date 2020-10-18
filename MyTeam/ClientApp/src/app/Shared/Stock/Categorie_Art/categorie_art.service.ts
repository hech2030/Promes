import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { catchError, map } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { CategorieArt } from '../../../Models/bo/Stock/Categorie-Art/categorie-art';
import { UserService } from '../../user.service';
import { MyToolsService } from '../../Tools/my-tools.service';

@Injectable({
  providedIn: 'root'
})
export class CategorieArtService {

  readonly BaseURI = 'https://localhost:44384/api';//TODO: add this value in config file 
  constructor(private http: HttpClient, private router: Router, private MainService: UserService,
    private tools: MyToolsService) { }
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

  SaveCategorie(categorie) {
    var host = this.BaseURI + '/CATEGORIE_ARTs/SaveCategorie';
    return this.http.post(host, { 'value': categorie });
  }

  DeleteCategorie(id) {
    var host = this.BaseURI + '/CATEGORIE_ARTs/DeleteCategorie';
    return this.http.post(host, { id: id }).pipe(
      map((data: boolean) => {
        return data;
      }), catchError(error => {
        if (error.status == 401) {
          this.MainService.logout();
        }
        else {
          if (error.error.helpLink != null && error.error.helpLink != undefined) {
            this.tools.ShowErrorNotification('Categorie', error.error.helpLink, 10000);
          }
          return throwError('Something went wrong!');
        }
      })
    )
  }

}
