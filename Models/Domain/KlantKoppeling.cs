using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WellStralerWebshop.Models.Domain
{
    public class KlantKoppeling
    {
        public long HoofdKlantId { get; set; }
        public long GekoppeldKlantId { get; set; }
        public Klant HoofdKlant { get; set; }
        public Klant GekoppeldKlant { get; set; }

    }
}
