import { CategorieArt } from ".././Categorie-Art/categorie-art";
import { Entree } from ".././Entree/entree";
import { Sortie } from ".././Sortie/sortie";
import { LigneCommande } from ".././Ligne-Commande/ligne-commande";
import { Magasin } from ".././Magasin/magasin";

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
  MAGASINId: number;
  categoriE_ART: CategorieArt;
  ENTREE: Entree[];
  SORTIE: Sortie[];
  LIGNE_COMMANDE: LigneCommande[];
  magasin: Magasin;
}
