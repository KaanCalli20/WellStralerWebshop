using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WellStralerWebshop.Models.Domain
{
    public class ProductKoppeling
    {
        public int Id { get; set; }
        public Product HoofdProduct { get; set; }
        public Product GekoppeldProduct { get; set; }
        //public KopTypes KoppelType { get; set; }
        public int KoppelVolgorde { get; set; }

    }
}
