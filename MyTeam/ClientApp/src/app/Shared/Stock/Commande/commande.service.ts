import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { UserService } from '../../user.service';
import { MyToolsService } from '../../Tools/my-tools.service';
import { map, catchError } from 'rxjs/operators';
import { Commande } from '../../../Models/bo/Stock/Commande/commande';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CommandeService {

  readonly BaseURI = 'https://localhost:44384/api';//TODO: add this value in config file 
  constructor(private http: HttpClient, private router: Router, private MainService: UserService,
    private tools: MyToolsService) { }
  GetCommande(form) {
    var host = this.BaseURI + '/COMMANDEs/FindCommande';
    return this.http.post(host, form).pipe(
      map((data: Commande[]) => {
        return data;
      }), catchError(error => {
        return throwError('Something went wrong!');
      })
    )
  }

  SaveCommande(commande) {
    var host = this.BaseURI + '/COMMANDEs/SaveCommande';
    return this.http.post(host, { 'Value': commande });
  }

}
