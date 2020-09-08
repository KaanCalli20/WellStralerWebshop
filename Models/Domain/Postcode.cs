using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WellStralerWebshop.Models.Domain
{
    [NotMapped]

    public class Postcode
    {
        public int tPosId { get; set; }
        public string TPOSPostcode { get; set; }
        public string TPOSGemeente { get; set; }
    }
}
