import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { MyToolsService } from '../../../../Shared/Tools/my-tools.service';
import * as $ from 'jquery';
import { ArticleService } from '../../../../Shared/Stock/Article/article.service';
import { Magasin } from '../../../../Models/bo/Stock/Magasin/magasin';
import { MagasinService } from '../../../../Shared/Stock/Magasin/magasin.service';
import { Article } from '../../../../Models/bo/Stock/Article/article';


@Component({
  selector: 'app-article-details',
  templateUrl: './article-details.component.html',
  styleUrls: ['./article-details.component.css'],
  host: { 'class': 'content-wrapper' },
})
export class ArticleDetailsComponent implements OnInit {
  public Roles = [
    { id: 3, Label: "Admin" },
    { id: 1, Label: "Gestionnaire" },
    { id: 2, Label: "Superviseur" }] //TODO : add a new referetiel table

  article: Article = new Article();
  Magasins: Magasin[];

  constructor(private route: ActivatedRoute, private articleService: ArticleService, private magasinService: MagasinService,
    private tools: MyToolsService, private router: Router) { }

  ngOnInit() {
    $("#magasinDropdownList").kendoDropDownList({
      dataSource: this.Magasins,
      dataTextField: "Label",
      dataValueField: "id"

    });
    this.article.id = this.route.snapshot.paramMap.get('id');
    this.loadArticle();
  }
  loadArticle() {
    this.magasinService.GetMagasin(null)
      .subscribe((data: any) => {
        console.log(data);
        this.Magasins = data;
      });
    this.articleService.GetArticle(this.article)
      .subscribe((data: any) => {
        this.article = data.result[0];
        $("#magasinDropdownList").data("kendoDropDownList").value(this.Magasins[0].nomMagasin);
      });
  }
  Quit() {
    this.router.navigate(['/Article']);
  }
  //Update() {
  //    this.article.role = parseInt($("#dropdownlist").data("kendoDropDownList").value());
  //    switch (this.article.role) {
  //      case 3:
  //        this.article.roleLabel = "Admin";
  //        break
  //      case 1:
  //        this.article.roleLabel = "Gestionnaire";
  //        break;
  //      case 2:
  //        this.article.roleLabel = "Superviseur";
  //        break;
  //    }
  //    this.articleService.register(this.article).subscribe(
  //      (res: any) => {
  //        if (res.succeeded) {
  //          this.tools.ShowSuccessNotification("Utilsateur", "La modification a été effectuée avec succès", '10000');
  //          this.router.navigate(['/Users']);
  //        }
  //        else {
  //          Swal.fire('Oops...', res.errors[0].description, 'error');
  //        }
  //      },
  //      err => {
  //        if (err.status == 400) {
  //          this.tools.ShowErrorNotification("User", "Something went wrong", '10000');
  //        }
  //        else {
  //          return console.log(err);
  //        }
  //      }
  //    );
    
  //}
}
