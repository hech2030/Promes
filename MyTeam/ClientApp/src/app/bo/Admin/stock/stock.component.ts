import { Component, OnInit } from '@angular/core';
import { Stock } from '../../../Models/bo/Stock/stock.model';
import { StockService } from '../../../Shared/Stock/stock.service';

@Component({
  templateUrl: './stock.component.html',
  host: { 'class': 'content-wrapper' },
  styleUrls: ['./stock.component.css']
})
export class StockComponent implements OnInit {

  constructor(private stockService : StockService) { }

  products: Stock[];

  ngOnInit() {
    this.stockService.GetStock(null)
      .subscribe((data: any) => {
        console.log(data);
        this.products = data.result;
      });
  }

}
