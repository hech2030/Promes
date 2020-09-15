import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-hichem-data',
  templateUrl: './hichem-data.component.html'
})
export class HichemDataComponent {
  public forecasts: Hichem[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Hichem[]>(baseUrl + 'hichemstest').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }
}

interface Hichem {
  birthDate: string;
  age: number;
  name: string;
}
