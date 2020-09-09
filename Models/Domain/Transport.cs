using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WellStralerWebshop.Models.Domain
{
    public class Transport
    {
        public short Id { get; set; }
        public string Naam { get; set; }
        public byte Eigen { get; set; }

    }
}
