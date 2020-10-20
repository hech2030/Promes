import { Component, OnInit } from '@angular/core';
import { SousTraitantService } from '../../../Shared/SousTraitant/soustraitant.service';
import { SousTraitant } from '../../../Models/bo/SousTraitant/soustraitant.model';
import Swal from 'sweetalert2';
import * as $ from 'jquery';
import { MyToolsService } from '../../../Shared/Tools/my-tools.service';
import { Router } from '@angular/router';


@Component({
  templateUrl: './soustraitants.component.html',
  styleUrls: ['./soustraitants.component.css'],
  host: { 'class': 'content-wrapper' },
})
export class SousTraitantsComponent implements OnInit {
  SearchCriteria = {
    id: 0,
    ville: ''
  }
  soustraitants: SousTraitant[];
  Isloading: boolean;
  soustraitant: SousTraitant = new SousTraitant();
  constructor(private sousTraitantService: SousTraitantService, private tools: MyToolsService, private router: Router) { }

  ngOnInit() {
    this.Isloading = true;
    this.sousTraitantService.GetSousTraitant(this.SearchCriteria)
      .subscribe((data: any) => {
        console.log(data);
        this.soustraitants = data.result;
        this.Isloading = false;
      });
  }

  AddSousTraitant() {
    this.soustraitant.telephone = this.soustraitant.telephone.toString();
    this.sousTraitantService.SaveSousTraitant(this.soustraitant).subscribe(
      (res: any) => {
        if (res.id > 0) {
          this.reset();
          $("[data-dismiss=modal]").trigger({ type: "click" });
          this.tools.ShowSuccessNotification("SousTraitant", "Sous-Traitant ajouté avec succeés", '10000');
        }
        else {
          Swal.fire('Oops...', res.errors[0].description, 'error');
        }
      },
      err => {
        if (err.status == 400) {
          this.tools.ShowErrorNotification("SousTraitant", err.error.message, '10000');
        }
        else {
          return console.log(err);
        }
      });
  }

  DeleteSousTraitant(id) {
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
        this.sousTraitantService.DeleteSousTraitant(id)
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
  updateSousTraitant(id) {
    this.router.navigate(['SousTraitant/sousTraitantDetails/'+id]);
  }
  reset() {
    this.SearchCriteria.ville = '';
    this.ngOnInit();
  }
}
