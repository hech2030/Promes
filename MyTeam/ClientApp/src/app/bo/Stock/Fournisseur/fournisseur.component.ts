import { Component, OnInit } from '@angular/core';
import { Fournisseur } from '../../../Models/bo/Stock/Fournisseur/fournisseur';
import { FournisseurService } from '../../../Shared/Stock/Fournisseur/Fournisseur.service';
import { MyToolsService } from '../../../Shared/Tools/my-tools.service';
import { Router } from '@angular/router';
import * as $ from 'jquery';
import Swal from 'sweetalert2';

@Component({
  templateUrl: './Fournisseur.component.html',
  host: { 'class': 'content-wrapper' },
  styleUrls: ['./Fournisseur.component.css']
})
export class FournisseurComponent implements OnInit {

  constructor(private FournisseurService: FournisseurService, private tools: MyToolsService,
    private router: Router) { }

  fournisseurs: Fournisseur[];
  searchCriteria = {
    id: 0,
    nomCategorie: ''
  }
  fournisseur: Fournisseur = new Fournisseur();


  ngOnInit() {
    this.FournisseurService.GetFournisseur(this.searchCriteria)
      .subscribe((data: any) => {
        console.log(data.result);
        this.fournisseurs = data.result;
      });
  }


  AddFournisseur() {
    if (!this.validateEmail(this.fournisseur.email)) {
      Swal.fire('Oops...', "Le format de l\'adresse email n\'est pas valide", 'error');
    } else {
      this.FournisseurService.SaveFournisseur(this.fournisseur).subscribe(
        (res: any) => {
          if (res.id > 0) {
            this.ngOnInit();
            $("[data-dismiss=modal]").trigger({ type: "click" });
            this.tools.ShowSuccessNotification("Fournisseur", "Fournisseur ajouté avec succeés", '10000');
          }
          else {
            Swal.fire('Oops...', res.errors[0].description, 'error');
          }
        },
        err => {
          if (err.status == 400) {
            this.tools.ShowErrorNotification("Fournisseur", err.error.message, '10000');
          }
          else {
            return console.log(err);
          }
        });
    }
  }
  DeleteFournisseur(id) {
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
        this.FournisseurService.DeleteFournisseur(id)
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
  UpdateFournisseur(id) {
    this.router.navigate(['Fournisseur/fournisseurDetails/' + id])
  }

  validateEmail(email) {
    const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
  }
}
