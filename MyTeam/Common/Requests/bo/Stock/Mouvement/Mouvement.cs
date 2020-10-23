using DataAcess.Models;

namespace MyTeam.Common.Requests.bo.Stock.Mouvement
{
    public class Mouvement
    {
        public virtual ARTICLE Article { get; set; }
        public virtual long Quantite { get; set; }
        public virtual long Prix { get; set; }
        public virtual MouvementType Type { get; set; }
    }
}
