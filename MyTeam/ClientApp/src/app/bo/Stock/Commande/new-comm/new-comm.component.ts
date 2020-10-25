import { Component, OnInit } from '@angular/core';
import { Article } from '../../../../Models/bo/Stock/Article/article';
import { Fournisseur } from '../../../../Models/bo/Stock/Fournisseur/fournisseur';
import { FournisseurService } from '../../../../Shared/Stock/Fournisseur/Fournisseur.service';
import { ArticleService } from '../../../../Shared/Stock/Article/article.service';
import { CommandeService } from '../../../../Shared/Stock/Commande/commande.service';
import { Router } from '@angular/router';
import { Commande } from '../../../../Models/bo/Stock/Commande/commande';
import { LigneCommande } from '../../../../Models/bo/Stock/Ligne-Commande/ligne-commande';
import Swal from 'sweetalert2';
import { MyToolsService } from '../../../../Shared/Tools/my-tools.service';

@Component({
  host: { 'class': 'content-wrapper' },
  templateUrl: './new-comm.component.html',
  styleUrls: ['./new-comm.component.css']
})
export class NewCommComponent implements OnInit {

  Articles: Article[];
  SelectedArticle: Article;
  Fournisseurs: Fournisseur[];
  SelectedFournisseur: Fournisseur;
  Transactions: LigneCommande[] = new Array();
  Transaction: LigneCommande;
  quantite: number;
  counter: number = 0;
  commande: Commande = new Commande();
  canModifyFournisseur: boolean = true;

  constructor(private fournisseurService: FournisseurService, private tools: MyToolsService,
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
  }
  AddLigneCommange() {
    if (this.quantite > 0 && this.SelectedArticle != undefined && this.SelectedArticle != null) {
      this.canModifyFournisseur = false;
      this.counter += 1;
      this.Transaction = new LigneCommande();
      this.Transaction.id = this.counter;
      this.Transaction.ARTICLE = this.SelectedArticle;
      this.Transaction.quantite = this.quantite
      if (this.Transactions != undefined) {
        this.Transactions = this.Transactions.concat(this.Transaction);
      }
      else {
        this.Transactions = [this.Transaction];
      }
      this.commande.LIGNE_COMMANDE = this.Transactions;
    }
  }

  DeleteLigneCommande(id) {
    var arr: LigneCommande[] = new Array();
    for (var i = 0; i < this.Transactions.length; i++) {
      if (this.Transactions[i].id != id) {
        arr = arr.concat(this.Transactions[i]);
      }
    }
    this.Transactions = arr;
    if (this.Transactions.length == 0) {
      this.canModifyFournisseur = true;
    }
  }

  save() {
    for (var i = 0; i < this.commande.LIGNE_COMMANDE.length; i++) {
      this.commande.LIGNE_COMMANDE[i].id = 0;
    }
    this.commande.FOURNISSEUR = this.SelectedFournisseur;
    this.commande.FOURNISSEURId = this.SelectedFournisseur.id;
    this.commandeService.SaveCommande(this.commande).subscribe(
      (res: any) => {
        if (res.id > 0) {
          this.ngOnInit();
          this.tools.ShowSuccessNotification("Commande", "Commande ajouté avec succeés", '10000');
        }
        else {
          Swal.fire('Oops...', res.errors[0].description, 'error');
        }
      },
      err => {
        if (err.status == 400) {
          this.tools.ShowErrorNotification("Commande", err.error.message, '10000');
        }
        else {
          return console.log(err);
        }
      });
  }


}
