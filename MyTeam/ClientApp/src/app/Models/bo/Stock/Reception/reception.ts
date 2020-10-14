import { Commande } from ".././Commande/commande";

export class Reception {
  constructor() {
    this.id = '';
  }
  id: string = '';
  numReception: number;
  dateReception: string;
  quantiteLivree: number;
  COMMANDE: Commande[];
}
