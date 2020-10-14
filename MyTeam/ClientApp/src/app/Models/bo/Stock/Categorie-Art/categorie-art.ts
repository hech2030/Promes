import { Article } from ".././Article/article";

export class CategorieArt {
  constructor() {
    this.id = '';
  }
  id: string = '';
  nomCate: string;
  description: string;
  ARTICLE: Article[];
}
