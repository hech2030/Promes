import { Component, OnInit } from '@angular/core';
import { Magasin } from '../../../Models/bo/Stock/Magasin/magasin';
import { MagasinService } from '../../../Shared/Stock/Magasin/magasin.service';

@Component({
  templateUrl: './Magasin.component.html',
  host: { 'class': 'content-wrapper' },
  styleUrls: ['./Magasin.component.css']
})
export class MagasinComponent implements OnInit {

  constructor(private magasinService: MagasinService) { }

  products: Magasin[];

  ngOnInit() {
    this.magasinService.GetMagasin(null)
      .subscribe((data: any) => {
        console.log(data.result);
        this.products = data.result;
      });
  }

}
