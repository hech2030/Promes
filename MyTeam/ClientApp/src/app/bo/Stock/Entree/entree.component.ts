import { Component, OnInit } from '@angular/core';
import { Entree } from '../../../Models/bo/Stock/Entree/entree';
import { EntreeService } from '../../../Shared/Stock/Entree/entree.service';

@Component({
  templateUrl: './entree.component.html',
  host: { 'class': 'content-wrapper' },
  styleUrls: ['./entree.component.css']
})
export class EntreeComponent implements OnInit {

  constructor(private entreeService: EntreeService) { }

  entrees: Entree[];

  SearchCriteria = {
    numEntree: -1,
    ARTICLEId: -1
  }
  ngOnInit() {
    this.entreeService.GetEntree(this.SearchCriteria)
      .subscribe((data: any) => {
        console.log(data.result);
        this.entrees = data.result;
      });
  }
  InitSearchCriteria() {
    this.SearchCriteria.numEntree = -1,
      this.SearchCriteria.ARTICLEId = -1
  }
}
