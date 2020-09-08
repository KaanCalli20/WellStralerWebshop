using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WellStralerWebshop.Models.Domain
{
    [NotMapped]
    public class Transport
    {
        public int tTRAId { get; set; }
        public string tTRANaam { get; set; }
        public string tTRAEigen { get; set; }

    }
}
