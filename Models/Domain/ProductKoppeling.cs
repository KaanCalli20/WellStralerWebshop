using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WellStralerWebshop.Models.Domain
{
    public class ProductKoppeling
    {
        public int TPRKId { get; set; }
        public Product TPRKHoofdProduct { get; set; }
        public Product TPRKGekoppeldProId { get; set; }
        public ProductType TPRKKoppelType { get; set; }
        public int TPRKKoppelVolgorde { get; set; }

    }
}
