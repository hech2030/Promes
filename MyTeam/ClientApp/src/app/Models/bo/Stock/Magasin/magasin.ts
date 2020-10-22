import { Article } from ".././Article/article";

export class Magasin {
  constructor() {
    this.id = 0;
  }
  id: number;
  nomMagasin: string;
  ARTICLE: Article[];
}
