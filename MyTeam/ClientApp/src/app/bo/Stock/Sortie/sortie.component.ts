import { Component, OnInit } from '@angular/core';
import { Sortie } from '../../../Models/bo/Stock/Sortie/sortie';
import { SortieService } from '../../../Shared/Stock/Sortie/sortie.service';

@Component({
  templateUrl: './sortie.component.html',
  host: { 'class': 'content-wrapper' },
  styleUrls: ['./sortie.component.css']
})
export class SortieComponent implements OnInit {

  constructor(private sortieService: SortieService) { }

  sorties: Sortie[];

  SearchCriteria = {
    numSortie: -1,
    ARTICLEId: -1
  }
  ngOnInit() {
    this.sortieService.GetSortie(this.SearchCriteria)
      .subscribe((data: any) => {
        console.log(data.result);
        this.sorties = data.result;
      });
  }

  InitSearchCriteria() {
    this.SearchCriteria.numSortie = -1,
      this.SearchCriteria.ARTICLEId = -1
  }
}
