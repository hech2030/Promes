import { Article } from ".././Article/article";
import { Commande } from ".././Commande/commande";

export class Fournisseur {
  constructor() {
    this.id = 0;
  }
  id: number;
  numF: string;
  NomF: string;
  adresse: string;
  codP: number;
  ville: string;
  payes: string;
  tele: number;
  fax: number;
  email: string;
  COMMANDE: Commande[];
  ARTICLE: Article[];
}
