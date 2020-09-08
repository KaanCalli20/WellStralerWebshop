using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WellStralerWebshop.Models.Domain
{
    public class ProductKoppeling
    {
        public long Id { get; set; }

        public long HoofdId { get; set; }
        public long GekoppeldProdId { get; set; }
        public Product HoofdProduct { get; set; }
        public Product GekoppeldProduct { get; set; }
        public KopTypes KoppelType { get; set; }
        public short KoppelVolgorde { get; set; }

    }
}
