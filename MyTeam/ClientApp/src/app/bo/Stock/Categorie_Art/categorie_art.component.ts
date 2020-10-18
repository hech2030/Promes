import { Component, OnInit } from '@angular/core';
import { CategorieArtService } from '../../../Shared/Stock/categorie_art/categorie_art.service';
import { CategorieArt } from '../../../Models/bo/Stock/Categorie-Art/categorie-art';
import * as $ from 'jquery';
import { MyToolsService } from '../../../Shared/Tools/my-tools.service';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';

@Component({
  templateUrl: './categorie_art.component.html',
  host: { 'class': 'content-wrapper' },
  styleUrls: ['./categorie_art.component.css']
})
export class CategorieArtComponent implements OnInit {

  constructor(private CategorieArtService: CategorieArtService, private tools: MyToolsService,
    private router: Router) { }

  categories: CategorieArt[];
  searchCriteria = {
    id: 0,
    nomCategorie: ''
  }
  categorie: CategorieArt = new CategorieArt();


  ngOnInit() {
    this.CategorieArtService.GetCategorieArt(this.searchCriteria)
      .subscribe((data: any) => {
        console.log(data.result);
        this.categories = data.result;
      });
  }


  AddCategorie() {
    this.CategorieArtService.SaveCategorie(this.categorie).subscribe(
      (res: any) => {
        if (res.id > 0) {
          this.ngOnInit();
          $("[data-dismiss=modal]").trigger({ type: "click" });
          this.tools.ShowSuccessNotification("Agence", "Agence ajouté avec succeés", '10000');
        }
        else {
          Swal.fire('Oops...', res.errors[0].description, 'error');
        }
      },
      err => {
        if (err.status == 400) {
          this.tools.ShowErrorNotification("Agence", err.error.message, '10000');
        }
        else {
          return console.log(err);
        }
      });
  }
  DeleteCategorie(id) {
    Swal.fire({
      title: 'Êtes-vous sûr de vouloir supprimer?',
      text: "Vous ne pourrez pas revenir en arrière!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Oui, Supprimer!'
    }).then((result) => {
      if (result.isConfirmed) {
        this.CategorieArtService.DeleteCategorie(id)
          .subscribe((data: any) => {
            if (data) {
              this.ngOnInit();
              Swal.fire(
                'Supprimé!',
                'Succès',
                'success'
              )
            }
            else {
              Swal.fire(
                'Erreur!',
                'Un problème est survenu avec le serveur.',
                'error'
              )
            }
          });

      }
    })
  }
  UpdateCategorie(id) {
    this.router.navigate(['Categorie/categorieDetails/' + id])
  }

}
