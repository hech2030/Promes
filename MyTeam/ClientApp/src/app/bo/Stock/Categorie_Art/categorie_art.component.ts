import { Component, OnInit } from '@angular/core';
import { CategorieArtService } from '../../../Shared/Stock/categorie_art/categorie_art.service';
import { CategorieArt } from '../../../Models/bo/Stock/Categorie-Art/categorie-art';

@Component({
  templateUrl: './categorie_art.component.html',
  host: { 'class': 'content-wrapper' },
  styleUrls: ['./categorie_art.component.css']
})
export class CategorieArtComponent implements OnInit {

  constructor(private CategorieArtService: CategorieArtService) { }

  products: CategorieArt[];

  ngOnInit() {
    this.CategorieArtService.GetCategorieArt(null)
      .subscribe((data: any) => {
        console.log(data.result);
        this.products = data.result;
      });
  }

}
