import { Component, OnInit } from '@angular/core';
import { Fournisseur } from '../../../Models/bo/Stock/Fournisseur/Fournisseur';
import { FournisseurService } from '../../../Shared/Stock/Fournisseur/Fournisseur.service';

@Component({
  templateUrl: './Fournisseur.component.html',
  host: { 'class': 'content-wrapper' },
  styleUrls: ['./Fournisseur.component.css']
})
export class FournisseurComponent implements OnInit {

  constructor(private FournisseurService: FournisseurService) { }

  products: Fournisseur[];

  ngOnInit() {
    this.FournisseurService.GetFournisseur(null)
      .subscribe((data: any) => {
        console.log(data.result);
        this.products = data.result;
      });
  }

}
