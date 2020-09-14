using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WellStralerWebshop.Models.Domain
{
    public class FactuurLijn
    {
        public long Id { get; set; }
        public long FactuurNummer { get; set; }
        public Factuur Factuur { get; set; }
        public long ProductId { get; set; }
        public string ProductOmschrijving { get; set; }
        public int Aantal { get; set; }
        public decimal Prijs { get; set; }
        public decimal BtwPercentage { get; set; }
        public string KlantReferentie { get; set; }
        public string Opmerking { get; set; }
        public string SerieNummer { get; set; }
        public int AfleverWeek { get; set; }
        public int AfleverJaar { get; set; }
        public long ZendNummer { get; set; }
        //public long ZendLijnNummer { get; set; }
        public DateTime DatumInbreng { get; set; }


        
    }
}
