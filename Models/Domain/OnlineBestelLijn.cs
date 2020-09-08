using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WellStralerWebshop.Models.Domain
{
    [NotMapped]
    public class OnlineBestelLijn
    {
        public long Id { get; set; }
        public long BestellingId { get; set; }
        public long ProductId { get; set; }
        public string ProductOmschrijving { get; set; }
        public int Aantal { get; set; }
        public int AantalKlaar { get; set; }
        public int AantalGeleverd { get; set; }
        public decimal Prijs { get; set; }
        public decimal BtwPerc { get; set; }
        public string Opmerking { get; set; }
        public string KlantReferentie { get; set; }
        public int AfleverWeek { get; set; }
        public int AfleverJaar { get; set; }
        public byte Afgewerkt { get; set; }
        public byte Geblokkeerd { get; set; }
        public DateTime DatumInbreng { get; set; }
        public long UserIdInbreng { get; set; }
        public DateTime DatumWijziging { get; set; }
        public long UserIdWijziging { get; set; }
        public long PrimaryProdOrderLineId { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
