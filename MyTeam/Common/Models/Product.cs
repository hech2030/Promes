using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTeam.Common.Models
{
    public class Product
    {
        public string designation { get; set; }
        public string marque { get; set; }
        public string type { get; set; }
        public string quantite { get; set; }
        public string code_fournisseur { get; set; }
        public string fournisseur { get; set; }
        public string prix_achat { get; set; }
    }
}
