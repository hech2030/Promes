import { LigneCommande } from "./ligne-commande";

export class Commande {
  constructor() {
    this.id = '';
  }
  id: string = '';
  numCommande: number;
  dateCOMMANDE: string;
  etat: string;
  RECEPTIONId: number;
  FOURNISSEURId: number;
  LIGNE_COMMANDE: LigneCommande[];
  RECEPTION: Commande;
  FOURNISSEUR: Commande;
}
