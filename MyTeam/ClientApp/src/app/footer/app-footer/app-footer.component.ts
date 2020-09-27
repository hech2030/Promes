import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'footer',
  templateUrl: './app-footer.component.html',
  styleUrls: ['./app-footer.component.css'],
  host: { 'class': 'footer' }
})
export class AppFooterComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
