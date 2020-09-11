using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WellStralerWebshop.Models.Domain
{
   
    public class OnlineBestelling
    {
        public long Id { get; set; }
        public DateTime Datum { get; set; }

        public long KlantId { get; set; }
        public Klant Klant { get; set; }

        public long LeverKlantId { get; set; }
        public Klant LeverKlant { get; set; }

        public string Referentie { get; set; }
        public string Opmerking { get; set; }

        public short TransportId { get; set; }
        public Transport Transport { get; set; }

        public DateTime DatumInBreng { get; set; }

        public long KlantLoginId { get; set; }
        public KlantLogin KlantLogin { get; set; }

        public ICollection<OnlineBestelLijn> OnlineBesltelLijnen { get; set; }

        public OnlineBestelling(Klant klant, Klant leverklant, string referentie, string opmerking, Transport transport,
            KlantLogin klantLogin, List<OnlineBestelLijn> onlineBestellijnen)
        {
            this.Datum = DateTime.Today;
            this.Klant = klant;
            this.LeverKlant = leverklant;
            this.Referentie = referentie;
            this.Opmerking = opmerking;
            this.Transport = transport;
            this.DatumInBreng = DateTime.Now;
            this.KlantLogin = klantLogin;
            this.OnlineBesltelLijnen = onlineBestellijnen;
        }
        OnlineBestelling()
        {

        }
    }
}
