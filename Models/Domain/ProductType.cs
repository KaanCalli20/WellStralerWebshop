using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WellStralerWebshop.Models.Domain
{
    public class ProductType
    {
        public long Id { get; set; }
        public string NaamNL { get; set; }
        public string NaamFR { get; set; }
        public string NaamDU { get; set; }
        public string NaamEN { get; set; }

    }
}
