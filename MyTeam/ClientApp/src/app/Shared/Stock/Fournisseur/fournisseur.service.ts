import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { catchError, map } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { Fournisseur } from '../../../Models/bo/Stock/Fournisseur/fournisseur';
import { UserService } from '../../user.service';
import { MyToolsService } from '../../Tools/my-tools.service';

@Injectable({
  providedIn: 'root'
})
export class FournisseurService {

  readonly BaseURI = 'https://localhost:44384/api';//TODO: add this value in config file 
  constructor(private http: HttpClient, private router: Router,
    private MainService: UserService,
    private tools: MyToolsService) { }
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

  SaveFournisseur(Fournisseur) {
    var host = this.BaseURI + '/Fournisseurs/SaveFournissuer';
    return this.http.post(host, { 'value': Fournisseur });
  }

  DeleteFournisseur(id) {
    var host = this.BaseURI + '/Fournisseurs/DeleteFournisseur';
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
