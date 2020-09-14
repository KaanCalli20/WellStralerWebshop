using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WellStralerWebshop.Models.Domain
{
    [NotMapped]
    public class Factuur
    {
        public long Id { get; set; }
        public long FactuurNummer { get; set; }
        public string Type { get; set; }
        public DateTime Datum { get; set; }
        public DateTime BestelDatum { get; set; }
        public byte FacAutoAangemaakt { get; set; }
        public DateTime DatumInbreng { get; set; }
        public long KllId { get; set; }
        public string Commentaar { get; set; }
        public byte Doorgestuurd { get; set; }

        public Klant Klant { get; set; }
        public Klant LeverKlant { get; set; }
        public long KlantId { get; set; }
        public string KlantFirma { get; set; }
        public string KlantNaam { get; set; }
        public string KlantStraat { get; set; }
        public string KlantPostcode { get; set; }
        public string KlantLand { get; set; }
        public string KlantFactuurStraat { get; set; }
        public string KlantFactuurPostcode { get; set; }
        public byte KlantBtwPlichtig { get; set; }
        public string KlantBtwNr { get; set; }
        public short? KlantTaal { get; set; }
        public long LeverKlantId { get; set; }

        public List<FactuurLijn> FactuurLijnen { get; set; }


    }
}
