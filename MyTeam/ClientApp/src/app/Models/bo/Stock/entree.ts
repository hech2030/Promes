import { Article } from "./article";

export class Entree {
  constructor() {
    this.id = '';
  }
  id: string = '';
  numEntree: number;
  quantite: number;
  dateEntree: string;
  prixDentree: number;
  ARTICLEId: number;
  ARTICLE: Article;
}
