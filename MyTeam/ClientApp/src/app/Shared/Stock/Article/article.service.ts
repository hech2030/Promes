import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { catchError, map } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { Article } from '../../../Models/bo/Stock/Article/article';

@Injectable({
  providedIn: 'root'
})
export class ArticleService {

  readonly BaseURI = 'https://localhost:44384/api';//TODO: add this value in config file 
    article: any;
  constructor(private http: HttpClient, private router: Router) { }
  GetArticle(form) {
    var host = this.BaseURI + '/ARTICLEs/findArticle';
    return this.http.post(host, form).pipe(
      map((data: Article[]) => {
        return data;
      }), catchError(error => {
        return throwError('Something went wrong!');
      })
    )
  }
  addArticle(form) {
    var host = this.BaseURI + '/ARTICLEs';
    return this.http.post(host, form);    
  }

  DeleteArticle(id) {
    var host = this.BaseURI + '/ARTICLEs/' + id;
    return this.http.delete(host).pipe(
      map((data: boolean) => {
        return data;
      }), catchError(error => {
        return throwError('Something went wrong!');
      })
    )
  }
  Update(id,article) {
    var host = this.BaseURI + '/ARTICLEs/' + id;
    return this.http.put(host, article);
  }
}
