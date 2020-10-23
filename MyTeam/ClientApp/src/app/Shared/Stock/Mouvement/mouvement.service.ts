import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MouvementService {

  constructor(private http: HttpClient) { }
  readonly BaseURI = 'https://localhost:44384/api/fw';//TODO: add this value in config file 

  SaveMouvement(mouvements) {
    var host = this.BaseURI + '/Mouvements/SaveMouvements';
    return this.http.post(host, { 'values': mouvements });
  }

}
