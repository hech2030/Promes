import { Article } from "./article";
import { Commande } from "./commande";

export class LigneCommande {
  constructor() {
    this.id = '';
  }
  id: string = '';
  index: number;
  quantite: number;
  prix: number;
  montant: number;
  ARTICLEId: number;
  COMMANDEId: number;
  ARTICLE: Article;
  COMMANDE: Commande;
}
