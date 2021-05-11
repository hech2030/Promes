import { Component, OnInit } from '@angular/core';
import { Magasin } from '../../../Models/bo/Stock/Magasin/magasin';
import { MagasinService } from '../../../Shared/Stock/Magasin/magasin.service';
import { MyToolsService } from '../../../Shared/Tools/my-tools.service';
import { Router } from '@angular/router';
import * as $ from 'jquery';
import Swal from 'sweetalert2';

@Component({
  templateUrl: './Magasin.component.html',
  host: { 'class': 'content-wrapper' },
  styleUrls: ['./Magasin.component.css']
})
export class MagasinComponent implements OnInit {

  constructor(private MagasinService: MagasinService, private tools: MyToolsService,
    private router: Router) { }

  Isloading : boolean;
  magasins: Magasin[];
  searchCriteria = {
    id: 0,
    nomMagasin: ''
  }
  magasin: Magasin = new Magasin();


  ngOnInit() {
    this.Isloading = true;
    this.MagasinService.GetMagasin(this.searchCriteria)
      .subscribe((data: any) => {
        console.log(data.result);
        this.magasins = data.result;
        this.Isloading = false;
      });
  }


  AddMagasin() {
    this.MagasinService.SaveMagasin(this.magasin).subscribe(
      (res: any) => {
        if (res.id > 0) {
          this.ngOnInit();
          $("[data-dismiss=modal]").trigger({ type: "click" });
          this.tools.ShowSuccessNotification("Magasin", "Magasin ajouté avec succeés", '10000');
        }
        else {
          Swal.fire('Oops...', res.errors[0].description, 'error');
        }
      },
      err => {
        if (err.status == 400) {
          this.tools.ShowErrorNotification("Magasin", err.error.message, '10000');
        }
        else {
          return console.log(err);
        }
      });
  }
  DeleteMagasin(id) {
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
        this.MagasinService.DeleteMagasin(id)
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
  UpdateMagasin(id) {
    this.router.navigate(['Magasin/magasinDetails/' + id])
  }


}
