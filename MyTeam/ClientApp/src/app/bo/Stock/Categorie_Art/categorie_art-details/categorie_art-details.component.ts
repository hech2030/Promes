import { Component, OnInit } from '@angular/core';
import { CategorieArt } from '../../../../Models/bo/Stock/Categorie-Art/categorie-art';
import { Router, ActivatedRoute } from '@angular/router';
import { CategorieArtService } from '../../../../Shared/Stock/categorie_art/categorie_art.service';
import { MyToolsService } from '../../../../Shared/Tools/my-tools.service';
import Swal from 'sweetalert2';

@Component({
  host: { 'class': 'content-wrapper' },
  templateUrl: './categorie_art-details.component.html',
  styleUrls: ['./categorie_art-details.component.css']
})
export class categorie_artDetailsComponent implements OnInit {

  Isloading: boolean;
  categorie: CategorieArt = new CategorieArt();

  constructor(private router: Router, private categorieService: CategorieArtService,
    private route: ActivatedRoute, private tools: MyToolsService) { }

  ngOnInit() {
    this.Isloading = true;
    this.categorie.id = parseInt(this.route.snapshot.paramMap.get('id'));

    this.categorieService.GetCategorieArt(this.categorie)
      .subscribe((data: any) => {
        console.log(data);
        this.categorie = data.result[0];
        this.Isloading = false;
      });
  }
  Quit() {
    this.router.navigate(['/Categorie']);
  }
  Update() {
    this.categorieService.SaveCategorie(this.categorie).subscribe(
      (res: any) => {
        if (res.id > 0) {
          this.tools.ShowSuccessNotification("Categorie", "La modification a été effectuée avec succès", '10000');
          this.router.navigate(['/Categorie']);
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
      }
    );
  }
}
