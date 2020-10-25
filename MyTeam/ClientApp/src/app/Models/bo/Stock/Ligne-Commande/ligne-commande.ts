import { Article } from ".././Article/article";
import { Commande } from ".././Commande/commande";

export class LigneCommande {
  constructor() {
    this.id = 0;
    this.quantite = 0;
    this.ARTICLE = null;
  }
  id: number;
  index: number;
  quantite: number;
  prix: number;
  montant: number;
  ARTICLEId: number;
  COMMANDEId: number;
  ARTICLE: Article;
  COMMANDE: Commande;
}
