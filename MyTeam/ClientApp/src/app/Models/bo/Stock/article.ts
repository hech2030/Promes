import { CategorieArt } from "./categorie-art";
import { Entree } from "./entree";
import { Sortie } from "./sortie";
import { LigneCommande } from "./ligne-commande";
import { Fournisseur } from "./fournisseur";
import { Magasin } from "./magasin";

export class Article {
  constructor() {
    this.id = '';
  }
  id: string = '';
  designation: string;
  unit: string;
  quantite: number;
  prix: number;
  newAttr: number;
  emplacement: string;
  CATEGORIE_ARTId: number;
  FOURNISSEURId: number;
  MAGASINId: number;
  CATEGORIE_ART: CategorieArt;
  ENTREE: Entree[];
  SORTIE: Sortie[];
  LIGNE_COMMANDE: LigneCommande[];
  FOURNISSEUR: Fournisseur;
  MAGASIN: Magasin;
}
