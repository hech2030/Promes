import { Component, OnInit } from '@angular/core';
import { Fournisseur } from '../../../../Models/bo/Stock/Fournisseur/fournisseur';
import { Router, ActivatedRoute } from '@angular/router';
import { FournisseurService } from '../../../../Shared/Stock/Fournisseur/Fournisseur.service';
import { MyToolsService } from '../../../../Shared/Tools/my-tools.service';
import Swal from 'sweetalert2';

@Component({
  host: { 'class': 'content-wrapper' },
  templateUrl: './Fournisseur-details.component.html',
  styleUrls: ['./Fournisseur-details.component.css']
})
export class FournisseurDetailsComponent implements OnInit {

  Isloading: boolean;
  fournisseur: Fournisseur = new Fournisseur();

  constructor(private router: Router, private fournisseurService: FournisseurService,
    private route: ActivatedRoute, private tools: MyToolsService) { }

  ngOnInit() {
    this.Isloading = true;
    this.fournisseur.id = parseInt(this.route.snapshot.paramMap.get('id'));

    this.fournisseurService.GetFournisseur(this.fournisseur)
      .subscribe((data: any) => {
        console.log(data);
        this.fournisseur = data.result[0];
        this.Isloading = false;
      });
  }
  Quit() {
    this.router.navigate(['/Fournissuer']);
  }
  Update() {
    this.fournisseurService.SaveFournisseur(this.fournisseur).subscribe(
      (res: any) => {
        if (res.id > 0) {
          this.tools.ShowSuccessNotification("Fournissuer", "La modification a été effectuée avec succès", '10000');
          this.router.navigate(['/Fournisseur']);
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
      }
    );
  }

}
