import { Component, OnInit } from '@angular/core';
import { FournisseurService } from '../../../Shared/Stock/Fournisseur/Fournisseur.service';
import { ArticleService } from '../../../Shared/Stock/Article/article.service';
import { Article } from '../../../Models/bo/Stock/Article/article';
import { Fournisseur } from '../../../Models/bo/Stock/Fournisseur/fournisseur';
import { Commande } from '../../../Models/bo/Stock/Commande/commande';
import { CommandeService } from '../../../Shared/Stock/Commande/commande.service';
import { Router } from '@angular/router';

@Component({
  host: { 'class': 'content-wrapper' },
  templateUrl: './commande.component.html',
  styleUrls: ['./commande.component.css']
})
export class CommandeComponent implements OnInit {

  Isloading: boolean = true;
  SelectedFournisseur: Fournisseur;
  SelectedArticle: Article;
  SelectedStatus: any;
  Articles: Article[];
  Fournisseurs: Fournisseur[];
  commandesData: Commande[];
  public Status = [
    { id: 1, Label: "Envoyé" },
    { id: 2, Label: "En cours" },
    { id: 3, Label: "Réalisé" }] // TODO : à les mettres dans une table referentiel
  constructor(private fournisseurService: FournisseurService,
    private articleService: ArticleService, private commandeService: CommandeService, private router: Router) { }

  ngOnInit() {
    this.fournisseurService.GetFournisseur({})
      .subscribe((data: any) => {
        console.log(data.result);
        this.Fournisseurs = data.result;
      });
    this.articleService.GetArticle({})
      .subscribe((data: any) => {
        console.log(data.result);
        this.Articles = data.result;
      });
    this.commandeService.GetCommande({})
      .subscribe((data: any) => {
        console.log(data.result);
        this.commandesData = data.result;
        this.Isloading = false;
      });
  }
  NewCommand() {
    this.router.navigate(['NewRequest']);
  }
  InitSearchCriteria(){
    
  }

}
