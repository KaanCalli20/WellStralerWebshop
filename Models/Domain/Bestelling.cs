using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WellStralerWebshop.Models.Domain
{
    [NotMapped]
    public class Bestelling
    {
        public long Id { get; set; }
        public DateTime Datum { get; set; }
        public long KlantId { get; set; }
        public long? Afleverdag { get; set; }
        public DateTime DatumInbreng { get; set; }
        public DateTime DatumWijziging { get; set; }
        public string KlantFirma { get; set; }
        public string KlantNaam { get; set; }
        public string KlantStraat { get; set; }
        public string KlantPostcode { get; set; }
        public string KlantLand { get; set; }
        public string KlantLeverFirma { get; set; }
        public string KlantLeverNaam { get; set; }
        public string KlantLevStraat { get; set; }
        public string KlantLevPostcode { get; set; }
        public string KlantLevLand { get; set; }
        public string KlantFacStraat { get; set; }
        public string KlantFacPostcode { get; set; }
        public string KlantBtwNr { get; set; }
        public short? KlantTaal { get; set; }
        public short? KlantLevTaal { get; set; }
        public byte KlantBtwPlicht { get; set; }
        public byte Geblokkeerd { get; set; }
        public byte Afgewerkt { get; set; }

    }
}
