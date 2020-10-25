using DataAcess.Models;
using System;

namespace MyTeam.Common.Requests.bo.Stock.Commande
{
    public class CommandFindRequest
    {
        public virtual DateTime? date { get; set; }
        public virtual long Id { get; set; }
        public virtual FOURNISSEUR Fournisseur { get; set; }
    }
}
