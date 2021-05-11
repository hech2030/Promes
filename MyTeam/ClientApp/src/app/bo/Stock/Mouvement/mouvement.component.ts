import { Component, OnInit } from '@angular/core';
import { ArticleService } from '../../../Shared/Stock/Article/article.service';
import { Article } from '../../../Models/bo/Stock/Article/article';
import { Mouvement } from '../../../Models/bo/Stock/Mouvement/mouvement.model';
import Swal from 'sweetalert2';
import { MouvementService } from '../../../Shared/Stock/Mouvement/mouvement.service';
import { MyToolsService } from '../../../Shared/Tools/my-tools.service';

@Component({
  host: { 'class': 'content-wrapper' },
  templateUrl: './mouvement.component.html',
  styleUrls: ['./mouvement.component.css']
})
export class MouvementComponent implements OnInit {
  Isloading : boolean;
  counter: number = 0;
  mouvements: Mouvement[] = new Array();
  mouvement: Mouvement = new Mouvement();
  SelectedArticle: Article;
  SelectedArticleModel: Article;
  SelectedType: number;
  public Types = [
    { id: 1, Label: "Entrée" },
    { id: 2, Label: "Sortie" }] // TODO : à les mettres dans une table referentiel

  constructor(private articleService: ArticleService, private mouvementService: MouvementService,
    private tools: MyToolsService) { }
  Articles: Article[];

  ngOnInit() {
    this.Isloading = true;
    this.articleService.GetArticle({})
      .subscribe((data: any) => {
        console.log(data.result);
        this.Articles = data.result;
        this.Isloading = false;
      });

  }
  AddMouvement() {
    if (this.SelectedArticle != null && this.SelectedType != null && this.mouvement.quantite > 0 && this.mouvement.prix > 0) {
      this.counter += 1;
      this.mouvement.id = this.counter;
      this.mouvement.article = this.Articles.find(x => x.id == this.SelectedArticle.id);
      this.mouvement.type = this.SelectedType;
      if (this.mouvements != undefined)
        this.mouvements = this.mouvements.concat(this.mouvement);
      else
        this.mouvements = [this.mouvement];
      this.reset();
    }
    else {
      Swal.fire(
        'Erreur!',
        'Tous les champs doit être remplis.',
        'error'
      )
    }
  }
  reset() {
    this.mouvement = new Mouvement();
    this.SelectedArticle = null;
    this.SelectedType = null;
  }
  DeleteMouvement(id) {
    var arr: Mouvement[] = new Array();
    for (var i = 0; i < this.mouvements.length; i++) {
      if (this.mouvements[i].id != id) {
        arr = arr.concat(this.mouvements[i]);
      }
    }
    this.mouvements = arr;
  }
  Init() {
    this.mouvements = [];
  }
  save() {
    this.mouvementService.SaveMouvement(this.mouvements).subscribe(
      (res: any) => {
        if (res.success) {
          this.ngOnInit();
          this.tools.ShowSuccessNotification("Mouvement", "Mouvement ajouté avec succeés", '10000');
        }
        else {
          Swal.fire('Oops...', res.errors[0].description, 'error');
        }
      },
      err => {
        if (err.status == 400) {
          this.tools.ShowErrorNotification("Mouvement", err.error.message, '10000');
        }
        else {
          return console.log(err);
        }
      });
  }

}
