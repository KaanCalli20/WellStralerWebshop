using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Models.ViewModels
{
    public class MaakOrderViewModel
    {
        public List<Klant> lijstLeveradressen { get; set; }
        public List<Transport> transportTypes { get; set; }

        public Transport gekozenTransport { get; set; }
        public Klant leverKlant { get; set; }

        public string referentie { get; set; }
        public string opmerking { get; set; }

        public MaakOrderViewModel(List<Klant>lijstLeverAdressen,Klant leverKlant, string referentie, string opmerking,List<Transport> transportLijst , Transport transport)
        {
            this.gekozenTransport = transport;
            this.referentie = referentie;
            this.opmerking = opmerking;
            this.leverKlant = leverKlant;
            this.lijstLeveradressen = lijstLeverAdressen;
            this.transportTypes = transportLijst;
        }

    }
}
