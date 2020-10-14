import { Component, OnInit } from '@angular/core';
import { Article } from '../../../Models/bo/Stock/Article/article';
import { ArticleService } from '../../../Shared/Stock/Article/article.service';

@Component({
  templateUrl: './article.component.html',
  host: { 'class': 'content-wrapper' },
  styleUrls: ['./article.component.css']
})
export class ArticleComponent implements OnInit {

  constructor(private articleService: ArticleService) { }

  products: Article[];

  ngOnInit() {
    this.articleService.GetArticle(null)
      .subscribe((data: any) => {
        console.log(data);
        this.products = data;
      });
  }

}
