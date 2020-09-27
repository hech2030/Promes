import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { Stock } from '../../Models/bo/Stock/stock.model';
import { catchError, map } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StockService {

  readonly BaseURI = 'https://localhost:44384/api';//TODO: add this value in config file 
  constructor(private http: HttpClient, private router: Router) { }
  GetStock(form) {
    var host = this.BaseURI + '/fw/Stock/FindStock';
    return this.http.post<any[]>(host, {}).pipe(
      map((data: Stock[]) => {
        return data;
      }), catchError(error => {
        return throwError('Something went wrong!');
      })
    )
  }

}
