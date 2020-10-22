import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { catchError, map } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { Article } from '../../../Models/bo/Stock/Article/article';
import { MyToolsService } from '../../Tools/my-tools.service';
import { UserService } from '../../user.service';

@Injectable({
  providedIn: 'root'
})
export class ArticleService {

  readonly BaseURI = 'https://localhost:44384/api';//TODO: add this value in config file 
  constructor(private http: HttpClient, private router: Router, private MainService: UserService,
    private tools: MyToolsService) { }
  GetArticle(form) {
    var host = this.BaseURI + '/ARTICLEs/FindArticle';
    return this.http.post(host, form).pipe(
      map((data: Article[]) => {
        return data;
      }), catchError(error => {
        return throwError('Something went wrong!');
      })
    )
  }

  SaveArticle(article) {
    var host = this.BaseURI + '/ARTICLEs/SaveArticle';
    return this.http.post(host, { 'value': article });
  }

  DeleteArticle(id) {
    var host = this.BaseURI + '/ARTICLEs/DeleteArticle';
    return this.http.post(host, { id: id }).pipe(
      map((data: boolean) => {
        return data;
      }), catchError(error => {
        if (error.status == 401) {
          this.MainService.logout();
        }
        else {
          if (error.error.helpLink != null && error.error.helpLink != undefined) {
            this.tools.ShowErrorNotification('Article', error.error.helpLink, 10000);
          }
          return throwError('Something went wrong!');
        }
      })
    )
  }
}
