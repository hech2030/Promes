import { Article } from "./article";

export class Sortie {
  constructor() {
    this.id = '';
  }
  id: string = '';
  numSortie: number;
  quantite: number;
  dateSortie: string;
  prixDSortie: number;
  ARTICLEId: number;
  ARTICLE: Article;
}
