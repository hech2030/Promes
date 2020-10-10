import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { Article } from '../../Models/bo/Stock/article';
import { catchError, map } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ArticleService {

  readonly BaseURI = 'https://localhost:44384/api';//TODO: add this value in config file 
  constructor(private http: HttpClient, private router: Router) { }
  GetArticle(form) {
    var host = this.BaseURI + '/ARTICLEs';
    return this.http.get<any[]>(host, {}).pipe(
      map((data: Article[]) => {
        return data;
      }), catchError(error => {
        return throwError('Something went wrong!');
      })
    )
  }

}
