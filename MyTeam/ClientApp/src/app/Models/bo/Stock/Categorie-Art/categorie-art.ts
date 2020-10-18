import { Article } from ".././Article/article";

export class CategorieArt {
  constructor() {
    this.id = 0;
  }
  id: number;
  nomCate: string;
  description: string;
  ARTICLE: Article[];
}
