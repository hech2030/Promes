import { Article } from "./article";

export class Magasin {
  constructor() {
    this.id = '';
  }
  id: string = '';
  nomMagasin: string;
  ARTICLE: Article[];
}
