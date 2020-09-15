using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Models.ViewModels
{
    public class FactuurViewModel
    {
        public List<Factuur> Facturen { get; set; }

        public FactuurViewModel(List<Factuur> facturen)
        {
            this.Facturen = facturen;
        }
        public string? ProductNaam { get; set; }
        public long? FactuurNr { get; set; }
        public DateTime? VanDatum { get; set; }
        public DateTime? TotDatum { get; set; }
        public long? ZendNotaNr { get; set; }
        public string? Serienummer { get; set; }

    }
}
