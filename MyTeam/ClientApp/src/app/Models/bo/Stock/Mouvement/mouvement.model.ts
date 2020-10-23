import { Article } from "../Article/article";

export class Mouvement {
  constructor() {
    this.id = 0;
    this.article = null;
    this.prix = 0;
    this.quantite = 0;
    this.type = 0;
  }
  id: number;
  article: Article;
  prix: number;
  quantite: number;
  type: number;
}
