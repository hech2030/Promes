import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { MyToolsService } from '../../../../Shared/Tools/my-tools.service';
import * as $ from 'jquery';
import { ArticleService } from '../../../../Shared/Stock/Article/article.service';
import { Magasin } from '../../../../Models/bo/Stock/Magasin/magasin';
import { MagasinService } from '../../../../Shared/Stock/Magasin/magasin.service';
import { Article } from '../../../../Models/bo/Stock/Article/article';
import { CategorieArt } from '../../../../Models/bo/Stock/Categorie-Art/categorie-art';
import { CategorieArtService } from '../../../../Shared/Stock/categorie_art/categorie_art.service';


@Component({
  selector: 'app-article-details',
  templateUrl: './article-details.component.html',
  styleUrls: ['./article-details.component.css'],
  host: { 'class': 'content-wrapper' },
})
export class ArticleDetailsComponent implements OnInit {

  article: Article = new Article();
  Magasins: Magasin[];
  SelectedMagasin: any;
  SelectedCategorie: any;
  Categories: CategorieArt[];
  constructor(private CategorieService: CategorieArtService,
    private route: ActivatedRoute, private articleService: ArticleService, private magasinService: MagasinService,
    private tools: MyToolsService, private router: Router) { }

  ngOnInit() {
    this.article.id = this.route.snapshot.paramMap.get('id');
    this.loadArticle();
  }
  loadArticle() {
    this.magasinService.GetMagasin({})
      .subscribe((data: any) => {
        console.log(data.result);
        this.Magasins = data.result;
      });
    this.CategorieService.GetCategorieArt({})
      .subscribe((data: any) => {
        console.log(data.result);
        this.Categories = data.result;
      });
    this.articleService.GetArticle(this.article)
      .subscribe((data: any) => {
        this.article = data.result[0];
        this.SelectedMagasin = this.article.magasin;
        this.SelectedCategorie = this.article.categoriE_ART;
      });
  }
  Quit() {
    this.router.navigate(['/Article']);
  }
  Update() {
    this.article.MAGASINId = this.SelectedMagasin.id;
    this.article.CATEGORIE_ARTId = this.SelectedCategorie.id;
    this.articleService.SaveArticle(this.article).subscribe(
        (res: any) => {
        if (res.id>0) {
            this.tools.ShowSuccessNotification("Article", "La modification a été effectuée avec succès", '10000');
            this.router.navigate(['/Article']);
          }
          else {
            Swal.fire('Oops...', res.errors[0].description, 'error');
          }
        },
        err => {
          if (err.status == 400) {
            this.tools.ShowErrorNotification("Article", err.error.message, '10000');
          }
          else {
            return console.log(err);
          }
        }
      );
    
  }
}
