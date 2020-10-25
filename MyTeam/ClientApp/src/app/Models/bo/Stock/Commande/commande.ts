import { LigneCommande } from ".././Ligne-Commande/ligne-commande";
import { Fournisseur } from "../Fournisseur/fournisseur";

export class Commande {
  constructor() {
    this.id = 0;
  }
  id: number;
  numCommande: number;
  dateCOMMANDE: string;
  etat: string;
  RECEPTIONId: number;
  FOURNISSEURId: number;
  LIGNE_COMMANDE: LigneCommande[];
  //RECEPTION: Commande;
  FOURNISSEUR: Fournisseur;
}
