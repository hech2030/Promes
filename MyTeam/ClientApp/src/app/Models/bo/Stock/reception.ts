import { Commande } from "./commande";

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
