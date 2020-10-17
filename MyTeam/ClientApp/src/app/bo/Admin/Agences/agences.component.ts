import { Component, OnInit } from '@angular/core';
import { AgenceService } from '../../../Shared/Agence/agence.service';
import { Agence } from '../../../Models/bo/Agence/agence.model';
import Swal from 'sweetalert2';
import * as $ from 'jquery';
import { MyToolsService } from '../../../Shared/Tools/my-tools.service';
import { Router } from '@angular/router';


@Component({
  templateUrl: './agences.component.html',
  styleUrls: ['./agences.component.css'],
  host: { 'class': 'content-wrapper' },
})
export class AgencesComponent implements OnInit {
  SearchCriteria = {
    id: 0,
    ville: ''
  }
  agences: Agence[];
  Isloading: boolean;
  agence: Agence = new Agence();
  constructor(private agenceService: AgenceService, private tools: MyToolsService, private router: Router) { }

  ngOnInit() {
    this.Isloading = true;
    this.agenceService.GetAgence(this.SearchCriteria)
      .subscribe((data: any) => {
        console.log(data);
        this.agences = data.result;
        this.Isloading = false;
      });
  }

  AddAgence() {
    this.agence.telephone = this.agence.telephone.toString();
    this.agenceService.SaveAgence(this.agence).subscribe(
      (res: any) => {
        if (res.id > 0) {
          this.reset();
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

  DeleteAgence(id) {
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
        this.agenceService.DeleteAgence(id)
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
  updateAgence(id) {
    this.router.navigate(['Agence/agenceDetails/'+id]);
  }
  reset() {
    this.SearchCriteria.ville = '';
    this.ngOnInit();
  }
}
