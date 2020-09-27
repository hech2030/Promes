using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTeam.Common.Models
{
    public class Stock
    {
        private IList<Product> products { get; set; }

        public Stock()
        {
            this.products = new List<Product>();
        }
    }
}
